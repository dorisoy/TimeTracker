using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PersonalTimeTracker
{
    /// <summary>
    /// The main form used for PTT.
    /// </summary>
    public partial class PTTMainForm : Form
    {
        private const string CUSTOMER_LOG_COLUMN_NAME = "m_customerColumn";
        private const string TASK_LOG_COLUMN_NAME = "m_taskColumn";
        private const string START_LOG_COLUMN_NAME = "m_startColumn";
        private const string STOP_LOG_COLUMN_NAME = "m_stopColumn";
        private const string INTERRUPTIONS_LOG_COLUMN_NAME = "m_interruptionsColumn";
        private const string DURATION_LOG_COLUMN_NAME = "m_durationColumn";
        private const string BILLABLE_LOG_COLUMN_NAME = "m_billableColumn";
        private const string DETAILS_LOG_COLUMN_NAME = "m_detailsColumn";

        private const char MRU_TASK_DELIMETER = '\r';
        private const char MRU_TASK_PIECES_DELIMETER = '\t';
        private const int MRU_TASK_INDEX = 0;
        private const int MRU_CUSTOMER_INDEX = 1;

        public PTTMainForm()
        {
            m_blockUndoSaves = true;
            m_undoStack = new LimitedStack<UndoItem>();
            m_taskMRU = new List<string>();

            InitializeComponent();

            m_newTaskMenuPosInGridMenu = m_contextMenuStrip.Items.IndexOf(m_currentTaskToolStripMenuItem);

            // Read in MRU List
            ReadMRUFromSettings();

            // Hide minimize button that is used only to allow Escape to hide window
            m_minimizeButton.Location = new Point(100, 100); // hide behind grid
            m_minimizeButton.SendToBack();

            // Not in designer properties for some reason
            m_logDataGridView.AutoGenerateColumns = false;
            m_logDataGridView.Columns[DETAILS_LOG_COLUMN_NAME].CellTemplate = new DataGridViewMemoCell();

            // Update save interval value
            m_saveIntervalNumericUpDown.Value = Properties.Settings.Default.SaveFrequency;
            // Update date label
            m_dateLabel.Text = m_dateTimePicker.Value.ToLongDateString();
            m_needsToUpdateSysTrayMenu = true;
            // Set clear on standby checkbox
            _clearOnStandyCheckBox.Checked = Properties.Settings.Default.ClearTaskOnStandby;

            // Load the initial data file (this may require looking for files from previous months)
            // Clear old data
            Customer.AllCustomers.Clear();
            Task.AllTasks.Clear();
            TimeLogEntry.AllTimeLogEntries.Clear();
            LogFileManager.LoadInitialDataFile();

            BindLogGrid();
            UpdateSummaryArea();

            // Set up event handlers
            m_logDataGridView.CellContentClick += new DataGridViewCellEventHandler(m_logDataGridView_CellContentClick);

            // Start timer
            m_timer = new Timer();
            m_timer.Interval = 10000; // update every 10 secs
            m_timer.Tick += new EventHandler(m_timer_Tick);
            m_timer.Enabled = true; // Turn on the timer
        }

        /// <summary>
        /// Gets if the logs for today are being displayed.
        /// </summary>
        public bool IsViewingToday
        {
            get
            {
                DateTime now = DateTime.Now;
                return (now.Year == m_dateTimePicker.Value.Year
                        && now.Month == m_dateTimePicker.Value.Month
                        && now.Day == m_dateTimePicker.Value.Day);
            }
        }

        /// <summary>
        /// Gets if the logs for today are being displayed.
        /// </summary>
        public bool IsViewingCurrentMonth
        {
            get
            {
                DateTime now = DateTime.Now;
                return (now.Year == m_dateTimePicker.Value.Year
                        && now.Month == m_dateTimePicker.Value.Month);
            }
        }

        #region Methods

        /// <summary>
        /// Adjust existing log entries based on new time entry as needed.  Also sort when done.
        /// </summary>
        /// <param name="newLog">The log entry just edited.</param>
        private void AdjustTimeEntriesForNewItem(TimeLogEntry newLog)
        {
            // Fix up any overlapping times
            TimeLogEntry.AdjustTimeEntriesForNewItem(newLog);

            bool adjustedTimes = false;
            // We need to sort the items, and IF they are re-ordered we need to rebind.
            // So, we need to remember the original order
            List<TimeLogEntry> originalOrder = new List<TimeLogEntry>();
            originalOrder.AddRange(m_logsForToday);
            int oldSelectedRowIdx = m_logDataGridView.CurrentRow.Index;
            TimeLogEntry selectedLog = m_logDataGridView.CurrentRow.DataBoundItem as TimeLogEntry;

            // Now sort them by starting times
            TimeLogEntry.SortLogs(m_logsForToday);

            // See if they are in the same order as before being sorted
            for (int idx = 0; !adjustedTimes && idx < m_logsForToday.Count; ++idx)
            {
                adjustedTimes = !object.ReferenceEquals(originalOrder[idx], m_logsForToday[idx]);
            }

            if (adjustedTimes)
            {
                // Find position of log in sorted list
                int newRowIdx = 0;
                for (; newRowIdx < m_logDataGridView.Rows.Count; ++newRowIdx)
                {
                    if (object.ReferenceEquals(selectedLog, m_logDataGridView.Rows[newRowIdx].DataBoundItem))
                    {
                        break;
                    }
                }
                // fake keystrokes to select proper row (otherwise any selection changes are overridden)
                string keysToSend = "";
                if (oldSelectedRowIdx > newRowIdx)
                {
                    for (; oldSelectedRowIdx > newRowIdx; ++newRowIdx)
                    {
                        keysToSend += "{UP}";
                    }
                }
                else if (oldSelectedRowIdx < newRowIdx)
                {
                    for (; oldSelectedRowIdx < newRowIdx; --newRowIdx)
                    {
                        keysToSend += "{DOWN}";
                    }
                }
                SendKeys.Send(keysToSend);
                m_logDataGridView.Refresh();
            }
                
            // If there was currently an active log entry then make the last one in the list active now
            if (null != m_currentActivity)
            {
                if (m_currentActivity != m_logsForToday[m_logsForToday.Count - 1])
                {
                    m_currentActivity = m_logsForToday[m_logsForToday.Count - 1];
                    m_logDataGridView.Refresh();
                }
            }
        }

        /// <summary>
        /// Bind the <see cref="DataGridView"/> to the logs for the selected day and initialize list columns.
        /// </summary>
        private void BindLogGrid()
        {
            m_logDataGridView.DataSource = null;

            DataGridViewComboBoxColumn combo = m_logDataGridView.Columns[CUSTOMER_LOG_COLUMN_NAME] as DataGridViewComboBoxColumn;
            combo.DisplayMember = "Name";
            combo.ValueMember = "Self";
            combo.Items.Clear();
            foreach (Customer cust in Customer.AllCustomers) combo.Items.Add(cust);

            combo = m_logDataGridView.Columns[TASK_LOG_COLUMN_NAME] as DataGridViewComboBoxColumn;
            combo.DisplayMember = "Name";
            combo.ValueMember = "Self";
            combo.Items.Clear();
            foreach (Task task in Task.AllTasks) combo.Items.Add(task);

            // Which day are we displaying for?
            DateTime dayToDisplay = m_dateTimePicker.Value;
            m_logsForToday = TimeLogEntry.LogsForDay(dayToDisplay);
            if (null == m_logsForToday)
            {
                m_logsForToday = new SortableBindingList<TimeLogEntry>();
            }
            m_logDataGridView.DataSource = m_logsForToday;
            UpdateButtons();
        }

        /// <summary>
        /// Read in list of MRU items
        /// </summary>
        private void ReadMRUFromSettings()
        {
            string taskMRU = Properties.Settings.Default.TaskMRU;
            if (null != taskMRU && 0 != taskMRU.Length)
            {
                string[] entries = taskMRU.Split('\r');
                foreach (string entry in entries)
                {
                    m_taskMRU.Add(entry);
                }
            }
        }

        /// <summary>
        /// Generate daily summary.
        /// </summary>
        private void UpdateSummaryArea()
        {
            // Remove old entries
            m_sumaryListView.Items.Clear();

            ListViewItem item = new ListViewItem("All Customers");
            TimeSpan totalBillableTime = new TimeSpan();
            TimeSpan totalNonBillableTime = new TimeSpan();

            // compute totals for all customers used today
            Dictionary<Customer, TimeSpan> billablePerCustomer = new Dictionary<Customer, TimeSpan>();
            Dictionary<Customer, TimeSpan> nonBillablePerCustomer = new Dictionary<Customer, TimeSpan>();
            List<Customer> allCustomers = new List<Customer>();
            foreach (TimeLogEntry log in m_logsForToday)
            {
                TimeSpan diff = log.Duration;
                if (log.IsBillable) totalBillableTime += diff;
                else totalNonBillableTime += diff;

                // Remember all customers seen
                if (!allCustomers.Contains(log.Customer)) allCustomers.Add(log.Customer);

                Dictionary<Customer, TimeSpan> dictToUse = billablePerCustomer;
                if (!log.IsBillable)
                {
                    dictToUse = nonBillablePerCustomer;
                }
                // add time to selected dictionary
                if (!dictToUse.ContainsKey(log.Customer))
                {
                    dictToUse[log.Customer] = diff;
                }
                else
                {
                    dictToUse[log.Customer] += diff;
                }
            }
            // Billable time
            double timePercent = totalBillableTime.Hours + (totalBillableTime.Minutes / 60.0);
            item.SubItems.Add(string.Format("{0:0.00}", timePercent));
            item.SubItems.Add(string.Format("{0:00}:{1:00}", totalBillableTime.Hours, totalBillableTime.Minutes));
            // NonBillable time
            timePercent = totalNonBillableTime.Hours + (totalNonBillableTime.Minutes / 60.0);
            item.SubItems.Add(string.Format("{0:0.00}", timePercent));
            item.SubItems.Add(string.Format("{0:00}:{1:00}", totalNonBillableTime.Hours, totalNonBillableTime.Minutes));
            // Total time
            TimeSpan totalTime = totalBillableTime + totalNonBillableTime;
            timePercent = totalTime.Hours + (totalTime.Minutes / 60.0);
            item.SubItems.Add(string.Format("{0:0.00}", timePercent));
            item.SubItems.Add(string.Format("{0:00}:{1:00}", totalTime.Hours, totalTime.Minutes));

            m_sumaryListView.Items.Add(item);

            // Add a line for each customer
            foreach (Customer customer in allCustomers)
            {
                item = new ListViewItem(customer.Name);
                TimeSpan timeSpan = new TimeSpan();
                TimeSpan totalTimeSpan = new TimeSpan();
                // add Billiable columns
                if (billablePerCustomer.ContainsKey(customer))
                {
                    timeSpan = billablePerCustomer[customer];
                    totalTimeSpan += timeSpan;
                }
                timePercent = timeSpan.Hours + (timeSpan.Minutes / 60.0);
                item.SubItems.Add(string.Format("{0:0.00}", timePercent));
                item.SubItems.Add(string.Format("{0:00}:{1:00}", timeSpan.Hours, timeSpan.Minutes));
                // add NonBilliable columns
                timeSpan = new TimeSpan();
                if (nonBillablePerCustomer.ContainsKey(customer))
                {
                    timeSpan = nonBillablePerCustomer[customer];
                    totalTimeSpan += timeSpan;
                }
                timePercent = timeSpan.Hours + (timeSpan.Minutes / 60.0);
                item.SubItems.Add(string.Format("{0:0.00}", timePercent));
                item.SubItems.Add(string.Format("{0:00}:{1:00}", timeSpan.Hours, timeSpan.Minutes));
                // Add total time
                timePercent = totalTimeSpan.Hours + (totalTimeSpan.Minutes / 60.0);
                item.SubItems.Add(string.Format("{0:0.00}", timePercent));
                item.SubItems.Add(string.Format("{0:00}:{1:00}", totalTimeSpan.Hours, totalTimeSpan.Minutes));
                // Add to list
                m_sumaryListView.Items.Add(item);
            }
        }

        /// <summary>
        /// Show the main form.  It may be hidden and minimized or just in the background.
        /// </summary>
        private void ShowMainForm()
        {
            UpdateSummaryArea(); // update summary since it could be stale as it is not updated while minimized
            Show();
            TopMost = true;
            WindowState = FormWindowState.Normal;
            BringToFront();
            Focus();
            ShowInTaskbar = true;
            TopMost = false;
        }

        /// <summary>
        /// Delete a log.
        /// </summary>
        /// <param name="rowIndex"></param>
        private void DeleteLogRow(int rowIndex)
        {
            PushStateToUndoStack();
            DataGridViewRow row = m_logDataGridView.Rows[rowIndex];
            TimeLogEntry log = row.DataBoundItem as TimeLogEntry; //get the data bound item
            if (object.ReferenceEquals(log, m_currentActivity))
            {
                m_currentActivity = null;
            }
            m_logDataGridView.Rows.Remove(row);
            MarkAsDirty();
            m_saveButton.Enabled = m_isDirty;
            ChangedCurrentActivity();
        }

        /// <summary>
        /// Update the current activity status label.
        /// </summary>
        private void UpdateCurrentActivityLabel()
        {
            if (null == m_currentActivity)
            {
                m_currentTaskToolStripStatusLabel.Text = "No active task";
            }
            else
            {
                m_currentTaskToolStripStatusLabel.Text = string.Format("{0} for {1}",
                    m_currentActivity.Task.Name, m_currentActivity.Customer.Name);
            }
        }

        /// <summary>
        /// Stop working on the current activity.
        /// </summary>
        private void DoneWithCurrentTask()
        {
            if (null != m_currentActivity)
            {
                m_currentActivity.StopTime = DateTime.Now;
                m_currentActivity = null;
                ChangedCurrentActivity();
            }
        }

        /// <summary>
        /// Perform all actions requored when the active task changes.
        /// </summary>
        private void ChangedCurrentActivity()
        {
            m_pauseCheckBox.Enabled = (null != m_currentActivity);
            if (null == m_currentActivity)
            {
                Icon = Properties.Resources.PTT_Main_Icon;
                Text = Properties.Resources.StrAppTitle;
            }
            else
            {
                Icon = Properties.Resources.PTT_Active_Icon;
                string title = string.Format(Properties.Resources.StrAppTitleWithTaskInfo,
                    Properties.Resources.StrAppTitle, m_currentActivity.Task.Name, m_currentActivity.Customer.Name);
                // A window's title can only be 64 characters long
                if (title.Length > 64)
                {
                    title = title.Substring(0, 59) + "...";
                }
                Text = title;
                UpdateMruListWithCurrentActivity();
            }
            // sys tray should match main app
            m_notifyIcon.Icon = Icon;
            m_notifyIcon.Text = Text;

            UpdateCurrentActivityLabel();
            UpdateButtons();
            m_logDataGridView.Refresh();
            UpdateSummaryArea();
        }

        /// <summary>
        /// Update the MRU list.
        /// </summary>
        private void UpdateMruListWithCurrentActivity()
        {
            // Update MRU list
            string mruEntry = string.Format("{0}{1}{2}",
                m_currentActivity.Task.Name,
                MRU_TASK_PIECES_DELIMETER,
                m_currentActivity.Customer.Name);
            int existingIndex = m_taskMRU.IndexOf(mruEntry);
            // If already at the top just skip this step
            if (0 != existingIndex)
            {
                // If previously in list remove from old position
                if (-1 != existingIndex) m_taskMRU.RemoveAt(existingIndex);
                m_taskMRU.Insert(0, mruEntry); // Insert at top of list

                // Now update setting for this
                StringBuilder builder = new StringBuilder();
                foreach (string entry in m_taskMRU)
                {
                    if (0 != builder.Length) builder.Append(MRU_TASK_DELIMETER);
                    builder.Append(entry);
                }
                Properties.Settings.Default.TaskMRU = builder.ToString();
                Properties.Settings.Default.Save();
            }
        }
        /// <summary>
        /// Enable and disable buttons as required.
        /// </summary>
        private void UpdateButtons()
        {
            m_pauseCheckBox.Enabled = (null != m_currentActivity);
            m_doneWithTaskButton.Enabled = (null != m_currentActivity);
        }

        /// <summary>
        /// Edit the list of customers.
        /// </summary>
        private void EditCustomerList()
        {
            CustomersForm customersForm = new CustomersForm();
            if (DialogResult.OK == customersForm.ShowDialog())
            {
                // Clear list to prevent removing customers that are bound to rows
                m_logDataGridView.DataSource = null;
                // Sync changes with real customer list
                Customer.SyncChanges(customersForm.Customers, customersForm.DeletedCustomers, customersForm.NewToOldMap);
                BindLogGrid();
                m_isDirty = !LogFileManager.SaveDataFile();
                m_saveButton.Enabled = m_isDirty;
                m_needsToUpdateSysTrayMenu = true;
                m_undoStack.Clear(); // this invalidates the undo stack
            }
        }

        /// <summary>
        /// Edit the list of tasks.
        /// </summary>
        private void EditTaskList()
        {
            TasksForm tasksForm = new TasksForm();
            if (DialogResult.OK == tasksForm.ShowDialog())
            {
                // Clear list to prevent removing tasks that are bound to rows
                m_logDataGridView.DataSource = null;
                // Sync changes with real task list
                Task.SyncChanges(tasksForm.Tasks, tasksForm.DeletedTasks, tasksForm.NewToOldMap);
                BindLogGrid();
                m_isDirty = !LogFileManager.SaveDataFile();
                m_saveButton.Enabled = m_isDirty;
                m_needsToUpdateSysTrayMenu = true;
                m_undoStack.Clear(); // this invalidates the undo stack
            }
        }

        /// <summary>
        /// Fill the shared customer/task menu which is used in both grid and sys tray context menus
        /// </summary>
        private void FillCustomersAndTaskMenusItems()
        {
            // Add customers to drop down menu
            if (m_needsToUpdateSysTrayMenu)
            {
                m_currentTaskToolStripMenuItem.DropDownItems.Clear();
                foreach (Customer customer in Customer.AllCustomers)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem(customer.Name);
                    item.Tag = customer;
                    item.DropDownOpening += new EventHandler(customer_DropDownOpening);
                    item.DropDownItems.Add("temp"); // just a place holder which will be filled in when sub-menu is selected
                    m_currentTaskToolStripMenuItem.DropDownItems.Add(item);
                }
                m_needsToUpdateSysTrayMenu = false;
                // Add a "None" entry if empty menu
                if (0 == m_currentTaskToolStripMenuItem.DropDownItems.Count)
                {
                    ToolStripMenuItem newMenuItem = new ToolStripMenuItem(Properties.Resources.StrNoneInBrackets);
                    newMenuItem.Enabled = false;
                    m_currentTaskToolStripMenuItem.DropDownItems.Add(newMenuItem);
                }
            }
        }

        /// <summary>
        /// Fill the MRU contect menu item.
        /// </summary>
        private void BuildMRUMenuItems()
        {
            // Fill in MRU section
            m_recentTasksToolStripMenuItem.DropDownItems.Clear();
            foreach (string entry in m_taskMRU)
            {
                string[] parts = entry.Split(MRU_TASK_PIECES_DELIMETER);
                if (2 == parts.Length)
                {
                    string str = string.Format(Properties.Resources.StrMruMenuText, parts[MRU_TASK_INDEX], parts[MRU_CUSTOMER_INDEX]);
                    ToolStripMenuItem newMenuItem = new ToolStripMenuItem(str);
                    newMenuItem.Tag = entry;
                    newMenuItem.Click += new EventHandler(recentTaskMenuItem_Click);
                    m_recentTasksToolStripMenuItem.DropDownItems.Add(newMenuItem);
                }
            }
            // Add a "None" entry if empty menu
            if (0 == m_recentTasksToolStripMenuItem.DropDownItems.Count)
            {
                ToolStripMenuItem newMenuItem = new ToolStripMenuItem(Properties.Resources.StrNoneInBrackets);
                newMenuItem.Enabled = false;
                m_recentTasksToolStripMenuItem.DropDownItems.Add(newMenuItem);
            }
        }

        /// <summary>
        /// Start the task indicated
        /// </summary>
        /// <param name="task">The <see cref="Task"/> to start.</param>
        /// <param name="customer">The <see cref="Customer"/> for which the task is to be performed.</param>
        private void StartGivenTask(Task task, Customer customer)
        {
            PushStateToUndoStack();

            DateTime now = DateTime.Now;
            if (null != m_currentActivity)
            {
                m_currentActivity.StopTime = now;
            }
            // Check if currently paused, we don't want to start a new task in the paused state
            m_pauseCheckBox.Checked = false;

            m_currentActivity = TimeLogEntry.CreateNewLogEntryForDay(now, customer, task);
            if (null == m_currentActivity) return;
            if (IsViewingToday)
            {
                BindLogGrid(); // in case no logs existed prior to this
            }
            else
            {
                m_dateTimePicker.Value = now; // Move back to today's list
            }
            // Update buttons
            m_saveButton.Enabled = true;

            ChangedCurrentActivity();
            // Select newly added row
            m_logDataGridView.CurrentCell = m_logDataGridView.Rows[m_logDataGridView.RowCount - 1].Cells[0];

            m_statusMessageToolStripStatusLabel.Text = "Starting new task";
        }

        #region Undo Support

        /// <summary>
        /// Used to hold information needed to perform an undo of a single action.
        /// </summary>
        private class UndoItem
        {
            public int RowIndex;
            public int ColumnIndex;
            public List<TimeLogEntry> Rows;
            public TimeLogEntry CurrentActivity;
        }

        /// <summary>
        /// Save the current state of things to the undo stack
        /// </summary>
        /// <remarks>Since editing a row can cause a chain reaction including new rows being created,
        /// or rows being re-ordered then we must save the entire list.</remarks>
        private void PushStateToUndoStack()
        {
            if (!m_blockUndoSaves)
            {
                UndoItem undo = new UndoItem();
                undo.RowIndex = -1;
                undo.ColumnIndex = -1;
                undo.Rows = new List<TimeLogEntry>();
                if (null != m_logDataGridView.CurrentRow && null != m_logDataGridView.CurrentCell)
                {
                    undo.RowIndex = m_logDataGridView.CurrentRow.Index;
                    undo.ColumnIndex = m_logDataGridView.CurrentCell.ColumnIndex;
                }
                // Now copy current state of displayed logs
                foreach (TimeLogEntry log in m_logsForToday)
                {
                    undo.Rows.Add(log.Clone() as TimeLogEntry);
                }
                // Must save current activity in log to the cloned task not the current m_currentActivity reference.
                // So do that by finding the index of the current activity (should be last normally)
                if (null != m_currentActivity)
                {
                    for (int idx = m_logsForToday.Count - 1; idx >= 0; --idx)
                    {
                        if (object.ReferenceEquals(m_currentActivity, m_logDataGridView.Rows[idx].DataBoundItem))
                        {
                            undo.CurrentActivity = undo.Rows[idx];
                        }
                    }
                }
                m_undoStack.Push(undo);
            }
        }

        /// <summary>
        /// Pull the last item off the undo stack and restore the state as of that time.
        /// </summary>
        /// <remarks>This may involve changing the date currently displayed.</remarks>
        private void PerformUndo()
        {
            if (0 != m_undoStack.Count)
            {
                m_blockUndoSaves = true;
                UndoItem undo = m_undoStack.Pop();
                // First see if we are displaying the correct date!
                if (0 != undo.Rows.Count)
                {
                    if (!TimeLogEntry.IsTheSameDay(undo.Rows[0].StartTime, m_dateTimePicker.Value))
                    {
                        // User changed days so move back to day on which the change took place
                        m_dateTimePicker.Value = undo.Rows[0].StartTime;
                    }
                }
                // unbind for preparation of replacing the entire underlying list
                m_logDataGridView.DataSource = null;
                m_logsForToday = TimeLogEntry.ReplaceLogEntriesForDay(m_dateTimePicker.Value, undo.Rows);
                m_logDataGridView.DataSource = m_logsForToday;
                m_currentActivity = undo.CurrentActivity;
                if (-1 != undo.RowIndex && -1 != undo.ColumnIndex)
                {
                    m_logDataGridView.CurrentCell = m_logDataGridView.Rows[undo.RowIndex].Cells[undo.ColumnIndex];
                }
                // Update everything
                m_logDataGridView.Refresh();
                ChangedCurrentActivity();
                UpdateSummaryArea();
                m_blockUndoSaves = false;
            }
        }

        #endregion Undo Support

        #endregion Methods

        #region Event Handlers

        #region Menu Event Handlers

        private void m_contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            // Set state of sys tray menu items
            m_pauseToolStripMenuItem.Checked = m_pauseCheckBox.Checked;
            m_pauseToolStripMenuItem.Enabled = (null != m_currentActivity);
            m_doneWithTaskMenuItem.Enabled = (null != m_currentActivity);
            // swap out customer/task menu item
            FillCustomersAndTaskMenusItems();
            BuildMRUMenuItems();
            if (-1 == m_contextMenuStrip.Items.IndexOf(m_currentTaskToolStripMenuItem))
            {
                // Not currently in this menu so put it back
                m_contextMenuStrip.Items.Insert(m_newTaskMenuPosInGridMenu, m_recentTasksToolStripMenuItem);
                m_contextMenuStrip.Items.Insert(m_newTaskMenuPosInGridMenu, m_currentTaskToolStripMenuItem);
            }
        }

        /// <summary>
        /// Customer menu is opening up so fill sub-menu with tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void customer_DropDownOpening(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (null != item)
            {
                // Remove "temp" menu item (if present) and fill in tasks
                if (1 == item.DropDownItems.Count && item.DropDownItems[0].Text == "temp")
                {
                    item.DropDownItems.Clear();
                    foreach (Task task in Task.AllTasks)
                    {
                        ToolStripMenuItem newItem = new ToolStripMenuItem(task.Name);
                        newItem.Tag = task;
                        newItem.Click += new EventHandler(taskMenuItem_Click);
                        item.DropDownItems.Add(newItem);
                    }
                }
            }
        }

        /// <summary>
        /// Start a new task.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void taskMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            Task task = item.Tag as Task;
            ToolStripMenuItem customerItem = item.OwnerItem as ToolStripMenuItem;
            Customer customer = customerItem.Tag as Customer;
            StartGivenTask(task, customer);
        }

        // The user selected one of the MRU items
        void recentTaskMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (null != item)
            {
                string taskEntry = item.Tag as string;
                string[] parts = taskEntry.Split(MRU_TASK_PIECES_DELIMETER);
                if (2 == parts.Length)
                {
                    Task task = Task.FindTask(parts[MRU_TASK_INDEX]);
                    Customer customer = Customer.FindCustomer(parts[MRU_CUSTOMER_INDEX]);
                    if (null != task && null != customer)
                    {
                        StartGivenTask(task, customer);
                    }
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMainForm();
        }

        private void newCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditCustomerList();
        }

        private void newTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditTaskList();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_pauseCheckBox.Checked = !m_pauseCheckBox.Checked;
        }

        private void m_pauseGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_pauseCheckBox.Checked = !m_pauseCheckBox.Checked;
        }

        private void m_doneWithTaskMenuItem_Click(object sender, EventArgs e)
        {
            DoneWithCurrentTask();
        }

        #endregion Menu Event Handlers

        #region Data Grid Menu Event Handlers

        /// <summary>
        /// Tell if the last log entry line for today is currently selected
        /// </summary>
        public bool IsLastLogEntryForTodaySelected
        {
            get
            {
                DataGridViewRow row = m_logDataGridView.CurrentRow;
                bool isLastRow = (row != null && ((m_logDataGridView.RowCount - 1) == row.Index));
                if (isLastRow)
                {
                    if (isLastRow && null != row)
                    {
                        TimeLogEntry log = row.DataBoundItem as TimeLogEntry;
                        isLastRow = (log.IsForToday);
                    }
                }
                return isLastRow;
            }
        }

        /// <summary>
        /// DataGridView context menu is opening up so initialize menu items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_logGridContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            // See if the currently active item is selected
            DataGridViewRow selectedRow = m_logDataGridView.CurrentRow;
            bool isCurrentTaskSelected = false;
            if (null != selectedRow)
            {
                isCurrentTaskSelected = object.ReferenceEquals(m_currentActivity, m_logDataGridView.CurrentRow.DataBoundItem);
            }

            m_resumeTaskAndUpdateTimeToolStripMenuItem.Enabled = !isCurrentTaskSelected && IsLastLogEntryForTodaySelected;
            m_resumeAndInsertInterruptToolStripMenuItem.Enabled = !isCurrentTaskSelected && IsViewingToday && (null != m_logDataGridView.CurrentRow);

            m_doneWithTaskToolStripMenuItem.Enabled = (null != m_currentActivity);
            m_saveToolStripMenuItem.Enabled = m_saveButton.Enabled; // should mirror the save button state
            m_pauseGridToolStripMenuItem.Checked = m_pauseCheckBox.Checked;
            m_pauseGridToolStripMenuItem.Enabled = (null != m_currentActivity);
            m_undoToolStripMenuItem.Enabled = (0 != m_undoStack.Count);
            // swap out customer/task menu item
            FillCustomersAndTaskMenusItems();
            BuildMRUMenuItems();
            if (-1 == m_logGridContextMenuStrip.Items.IndexOf(m_currentTaskToolStripMenuItem))
            {
                // Not currently in this menu so put it back
                m_logGridContextMenuStrip.Items.Add(m_currentTaskToolStripMenuItem);
                m_logGridContextMenuStrip.Items.Add(m_recentTasksToolStripMenuItem);
            }
        }

        private void m_logDataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            // If right click on a row select that row
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hitInfo = m_logDataGridView.HitTest(e.X, e.Y);
                if (hitInfo.Type == DataGridViewHitTestType.Cell || hitInfo.Type == DataGridViewHitTestType.RowHeader)
                {
                    DataGridViewRow row = m_logDataGridView.Rows[hitInfo.RowIndex];
                    if (null != row && !row.Selected)
                    {
                        m_logDataGridView.ClearSelection();
                        m_logDataGridView.CurrentCell = row.Cells[0];
                        row.Selected = true;
                    }
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = string.Format("{0} was written by Vance Kessler.\r\n\r\nFor help email VanceProductSupport@gmail.com.", Properties.Resources.StrAppTitle);
            MessageBox.Show(this, msg, "About " + Properties.Resources.StrAppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void previousDayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_dateTimePicker.Value = m_dateTimePicker.Value.AddDays(-1);
        }

        private void nextDayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_dateTimePicker.Value = m_dateTimePicker.Value.AddDays(1);
        }

        private void m_undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PerformUndo();
        }

        private void removeLogEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = m_logDataGridView.CurrentRow;
            if (null != row)
            {
                DeleteLogRow(row.Index);
            }
        }

        private void addNewLogEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_addLogButton_Click(sender, e);
        }

        /// <summary>
        /// The user wants to restart the last item they were working on and extend time to include time up to now.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resumeTaskAndUpdateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = m_logDataGridView.CurrentRow;
            if (IsLastLogEntryForTodaySelected && null != row)
            {
                m_currentActivity = row.DataBoundItem as TimeLogEntry;
                m_currentActivity.StopTime = DateTime.Now;
                ChangedCurrentActivity();
            }
        }

        /// <summary>
        /// Restart the selected task.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>This may not be the last task in the list so we may have to create a new log entry.</remarks>
        private void resumeAndInsertInterruptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGridViewRow selectedRow = m_logDataGridView.CurrentRow;
            if (IsLastLogEntryForTodaySelected && null != selectedRow)
            {
                m_currentActivity = null; // set to null to prevent updates from check box event
                m_currentActivity = selectedRow.DataBoundItem as TimeLogEntry;
                // Determine how long task was on hold
                DateTime now = DateTime.Now;
                TimeSpan diff = now - m_currentActivity.StopTime;
                m_currentActivity.InterruptionTime += diff;
                m_currentActivity.StopTime = now;
                ChangedCurrentActivity();
            }
            else
            {
                // OK, some other row was selected.  Let's create a new log entry and fire it up
                TimeLogEntry selectedEntry = selectedRow.DataBoundItem as TimeLogEntry;
                if (null != selectedEntry)
                {
                    TimeLogEntry entry = selectedEntry.Clone() as TimeLogEntry;
                    entry.StartTime = DateTime.Now;
                    entry.StopTime = entry.StartTime;
                    entry.InterruptionTime = new TimeSpan(); // This new task has no interruptions
                    m_logsForToday.Add(entry);
                    m_currentActivity = entry;
                    m_saveButton.Enabled = true;
                    // Select new last row
                    m_logDataGridView.ClearSelection();
                    m_logDataGridView.Rows[m_logDataGridView.Rows.Count - 1].Selected = true;
                    m_logDataGridView.Refresh();
                    ChangedCurrentActivity();
                }
            }
        }

        private void doneWithTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoneWithCurrentTask();
        }

        #endregion Data Grid Menu Event Handlers

        private void notifyIcon_Click(object sender, MouseEventArgs e)
        {
            // Show main window when user left clicks the systray icon
            if (e.Button == MouseButtons.Left)
            {
                    ShowMainForm();
            }
        }

        private void PTTMainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                // Reset to displaying today when minimized
                m_dateTimePicker.Value = DateTime.Now;
                ShowInTaskbar = false;
                Hide();
            }
        }

        private void m_editCustomersButton_Click(object sender, EventArgs e)
        {
            EditCustomerList();
        }

        private void m_editTasksButton_Click(object sender, EventArgs e)
        {
            EditTaskList();
        }

        private void m_SaveIntervalNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SaveFrequency = (int)m_saveIntervalNumericUpDown.Value;
        }

        private void ClearOnStandyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ClearTaskOnStandby = _clearOnStandyCheckBox.Checked;
        }

        void m_timer_Tick(object sender, EventArgs e)
        {
            // clear out status every once in a while so it does not get stales
            m_statusMessageToolStripStatusLabel.Text = "";

            if (!m_editingCell)
            {
                DateTime now = DateTime.Now;
                if (null != m_currentActivity)
                {
                    if (!m_pauseCheckBox.Enabled) m_pauseCheckBox.Enabled = true; // just in case
                    int oldMinute = m_currentActivity.StopTime.Minute;
                    m_currentActivity.StopTime = now;
                    MarkAsDirty();
                    m_saveButton.Enabled = m_isDirty;
                    if (WindowState != FormWindowState.Minimized)
                    {
                        // Only update summary if visible & the minute changed (otherwise it is just a waste of processing)
                        if (IsViewingToday && oldMinute != m_currentActivity.StopTime.Minute)
                        {
                            UpdateSummaryArea();
                        }
                    }
                }
                // calc next save time and see if we are there yet
                DateTime nextSaveTime = LogFileManager.LastSaveTime.AddMinutes(Properties.Settings.Default.SaveFrequency);
                if (m_isDirty && now >= nextSaveTime)
                {
                    m_isDirty = !LogFileManager.SaveDataFile();
                    m_saveButton.Enabled = m_isDirty;
                }
            }
        }

        private void m_saveButton_Click(object sender, EventArgs e)
        {
            m_isDirty = !LogFileManager.SaveDataFile();
            m_saveButton.Enabled = m_isDirty;
        }

        private void m_dateScroll_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.Type == ScrollEventType.SmallIncrement)
            {
                m_dateTimePicker.Value = m_dateTimePicker.Value.AddDays(1);
            }
            else if (e.Type == ScrollEventType.SmallDecrement)
            {
                m_dateTimePicker.Value = m_dateTimePicker.Value.AddDays(-1);
            }
        }

        /// <summary>
        /// Called when the day to display is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            // Load appropriate month's log entries
            DateTime selectedMonth = m_dateTimePicker.Value;
            m_dateLabel.Text = selectedMonth.ToLongDateString();
            if (LogFileManager.LoadDataFileIfNotAlreadyLoaded(selectedMonth))
            {
                m_needsToUpdateSysTrayMenu = true;
            }
            BindLogGrid();
            UpdateSummaryArea();
        }

        /// <summary>
        /// Called to format the contents of DataGridView cells
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_logDataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            string columnName = m_logDataGridView.Columns[e.ColumnIndex].Name;
            if (START_LOG_COLUMN_NAME == columnName || STOP_LOG_COLUMN_NAME == columnName)
            {
                if (null != e.Value)
                {
                    DateTime value = (DateTime)e.Value;
                    e.Value = value.ToShortTimeString();
                    e.FormattingApplied = true;
                }
            }
            else if (DURATION_LOG_COLUMN_NAME == columnName)
            {
                TimeSpan value = (TimeSpan)e.Value;
                double timePercent = value.Hours + (value.Minutes / 60.0);
                e.Value = string.Format("{0:00}:{1:00}({2:0.00})", value.Hours, value.Minutes, timePercent);
                e.FormattingApplied = true;
            }
            else if (INTERRUPTIONS_LOG_COLUMN_NAME == columnName)
            {
                TimeSpan value = (TimeSpan)e.Value;
                e.Value = string.Format("{0:00}:{1:00}", value.Hours, value.Minutes);
                e.FormattingApplied = true;
            }
            if (null != m_currentActivity
                && object.ReferenceEquals(m_currentActivity, m_logDataGridView.Rows[e.RowIndex].DataBoundItem))
            {
                if (m_pauseCheckBox.Checked)
                {
                    e.CellStyle.BackColor = Color.IndianRed;
                }
                else
                {
                    e.CellStyle.BackColor = Color.LightGreen;
                }
            }
        }

        private void m_logDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Invalid entry");
        }

        private void m_logDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //check if the click is in the delete column and viewing the current month
            if (m_logDataGridView.Columns[e.ColumnIndex].CellType == typeof(DataGridViewButtonCell) )
            {
                DeleteLogRow(e.RowIndex);
            }
        }

        /// <summary>
        /// Validate contents of cells.  Also push undo state since this occurs prior to row being changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_logDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridViewCell cell = m_logDataGridView.CurrentRow.Cells[e.ColumnIndex];
            string newStr = e.FormattedValue as string;
            string oldStr = cell.FormattedValue as string;
            // First see if the values actually changed
            if (null != newStr && null != oldStr)
            {
                if (!newStr.Equals(oldStr))
                {
                    string columnName = m_logDataGridView.Columns[e.ColumnIndex].Name;
                    switch (columnName)
                    {
                        case STOP_LOG_COLUMN_NAME:
                            TimeLogEntry log = m_logDataGridView.Rows[e.RowIndex].DataBoundItem as TimeLogEntry;
                            DateTime newStopTime;
                            if (!DateTime.TryParse(e.FormattedValue as string, out newStopTime))
                            {
                                m_statusMessageToolStripStatusLabel.Text = "Invlaid time format.";
                                e.Cancel = true;
                            }
                            else if (log.StartTime > newStopTime)
                            {
                                m_statusMessageToolStripStatusLabel.Text = "Stop time must be greater than start time.";
                                e.Cancel = true;
                            }
                            break;
                        case INTERRUPTIONS_LOG_COLUMN_NAME:
                            if (-1 == newStr.IndexOf(':'))
                            {
                                cell.Value = string.Format("0:{0}", newStr);
                            }
                            break;
                    }
                    if (!e.Cancel)
                    {
                        // Save undo info
                        PushStateToUndoStack();
                    }
                }
            }
        }

        private void m_logDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            MarkAsDirty();
        }

        private void MarkAsDirty()
        {
            m_isDirty = true;
            LogFileManager.MarkMonthAsDirty(m_dateTimePicker.Value);
        }
        /// <summary>
        /// Remember that start time changed and start a timer.
        /// </summary>
        /// <param name="entry">The <see cref="TimeLogEntry"/> that changed.</param>
        private void StartTimeChanged(TimeLogEntry entry)
        {
            m_startTimeChangedFor = entry;
            if (null == m_startTimeChangedTimer)
            {
                m_startTimeChangedTimer = new Timer();
                m_startTimeChangedTimer.Tick += startTimeChangedTimer_Tick;
            }
            else
            {
                // Stop timer so it will fire after the desired interval
                m_startTimeChangedTimer.Enabled = false;
            }
            m_startTimeChangedTimer.Interval = 5000;
            m_startTimeChangedTimer.Enabled = true;
        }

        /// <summary>
        /// Kill the start time changed timer if running.
        /// </summary>
        private void ClearStartTimeChanged(bool adjustTimesIfRunning)
        {
            if (null != m_startTimeChangedTimer)
            {
                m_startTimeChangedTimer.Dispose(); // kill timer
                m_startTimeChangedTimer = null;
                if (adjustTimesIfRunning)
                {
                    // Adjust times based on new start time if needed
                    AdjustTimeEntriesForNewItem(m_startTimeChangedFor);
                }
            }
        }

        /// <summary>
        /// Start time changed timer tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>The start time was changed (about 5 seconds ago) and now we
        /// need to adjust other row entries to remove any overlap.</remarks>
        void startTimeChangedTimer_Tick(object sender, EventArgs e)
        {
            // If in the middle of editing a cell then skip this tick and wait for the next one
            if (!m_editingCell)
            {
                ClearStartTimeChanged(true);
            }
        }

        private void m_logDataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            m_editingCell = true;
            m_saveButton.Enabled = true;
        }

        /// <summary>
        /// Called when a cell has been changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_logDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string columnName = m_logDataGridView.Columns[e.ColumnIndex].Name;
            TimeLogEntry entry = m_logDataGridView.Rows[e.RowIndex].DataBoundItem as TimeLogEntry;
            switch (columnName)
            {
                case START_LOG_COLUMN_NAME:
                    // We must manually adjust the date portion of the time to the day being viewed,
                    // this is becuase only the time portion is being input and the date parsing sets
                    // the date to today instead of the day being displayed
                    entry.StartTime = new DateTime(m_dateTimePicker.Value.Year, m_dateTimePicker.Value.Month, m_dateTimePicker.Value.Day,
                        entry.StartTime.Hour, entry.StartTime.Minute, entry.StartTime.Second);
                    UpdateSummaryArea();
                    StartTimeChanged(entry);
                    break;
                case STOP_LOG_COLUMN_NAME:
                    // Stop timer if running for start time change since we about to do the same thing it would do
                    ClearStartTimeChanged(false);
                    // We must manually adjust the date portion of the time to the day being viewed,
                    // this is becuase only the time portion is being input and the date parsing sets
                    // the date to today instead of the day being displayed
                    entry.StopTime = new DateTime(m_dateTimePicker.Value.Year, m_dateTimePicker.Value.Month, m_dateTimePicker.Value.Day,
                        entry.StopTime.Hour, entry.StopTime.Minute, entry.StopTime.Second);
                    UpdateSummaryArea();
                    // Now is when we validate that this edited time does not overlap an existing task's time
                    AdjustTimeEntriesForNewItem(entry);
                    break;
                case BILLABLE_LOG_COLUMN_NAME:
                case INTERRUPTIONS_LOG_COLUMN_NAME:
                    UpdateSummaryArea();
                    break;
                case CUSTOMER_LOG_COLUMN_NAME:
                    UpdateSummaryArea();
                    UpdateCurrentActivityLabel();
                    break;
                case TASK_LOG_COLUMN_NAME:
                    UpdateCurrentActivityLabel();
                    break;
            }
            MarkAsDirty();
            m_saveButton.Enabled = true;
            m_editingCell = false;
        }

        private void m_startNewTaskButton_Click(object sender, EventArgs e)
        {
            PushStateToUndoStack();

            DateTime now = DateTime.Now;
            if (null != m_currentActivity)
            {
                m_currentActivity.StopTime = now;
            }

            // Check if currently paused, we don't want to start a new task in the paused state
            m_pauseCheckBox.Checked = false;

            m_currentActivity = TimeLogEntry.CreateNewLogEntryForDay(now);
            if (null == m_currentActivity) return;

            if (IsViewingToday)
            {
                BindLogGrid(); // in case no logs existed prior to this
            }
            else
            {
                m_dateTimePicker.Value = now; // Move back to today's list
            }
            MarkAsDirty();
            m_saveButton.Enabled = m_isDirty;

            ChangedCurrentActivity();
            // Select newly added row
            m_logDataGridView.CurrentCell = m_logDataGridView.Rows[m_logDataGridView.RowCount - 1].Cells[0];

            m_statusMessageToolStripStatusLabel.Text = "Starting new task";
        }

        private void m_addLogButton_Click(object sender, EventArgs e)
        {
            PushStateToUndoStack();
            TimeLogEntry entry = TimeLogEntry.CreateNewLogEntryForDay(m_dateTimePicker.Value);
            if (null == entry) return;
            BindLogGrid(); // in case no logs existed prior to this
            UpdateSummaryArea();
            MarkAsDirty();
            m_saveButton.Enabled = m_isDirty;
            // Select newly added row
            m_logDataGridView.Focus();
            m_logDataGridView.CurrentCell = m_logDataGridView.Rows[m_logDataGridView.RowCount - 1].Cells[0];

            m_statusMessageToolStripStatusLabel.Text = "Adding new log line";
        }

        private void PTTMainForm_Load(object sender, EventArgs e)
        {
            // Determine total display area
            Rectangle totalResolution = new Rectangle();
            foreach (Screen screen in Screen.AllScreens)
            {
                totalResolution = Rectangle.Union(totalResolution, screen.Bounds);
            }
            bool wasMinimized = (WindowState == FormWindowState.Minimized);
            if (!Properties.Settings.Default.WindowPos.IsEmpty
                && totalResolution.Contains(Properties.Settings.Default.WindowPos))
            {
                if (wasMinimized) WindowState = FormWindowState.Normal;
                DesktopBounds = Properties.Settings.Default.WindowPos;
                if (!wasMinimized && Properties.Settings.Default.IsMaximized) WindowState = FormWindowState.Maximized;
            }
            if (0 != Properties.Settings.Default.SplitterPos)
            {
                m_splitContainer.SplitterDistance = Properties.Settings.Default.SplitterPos;
            }
            if (wasMinimized) WindowState = FormWindowState.Minimized;
            m_blockUndoSaves = false;

            // Sign up for System stand-by/Hibernate event
            Microsoft.Win32.SystemEvents.PowerModeChanged += OnPowerModeChanged;
        }

        private void OnPowerModeChanged(object sender, Microsoft.Win32.PowerModeChangedEventArgs e)
        {
            if (_clearOnStandyCheckBox.Checked
                && e.Mode == Microsoft.Win32.PowerModes.Suspend
                && null != m_currentActivity)
            {
                // Update Time
                m_currentActivity.StopTime = DateTime.Now;
                // Going into standby mode so clear current event
                m_currentActivity = null;
                // Save latest changes
                m_isDirty = !LogFileManager.SaveDataFile();
            }
        }

        private void PTTMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // turn off timers
            m_timer.Enabled = false;
            ClearStartTimeChanged(true);

            // Save latest changes
            if (m_isDirty && !LogFileManager.SaveDataFile())
            {
                m_isDirty = false;
            }
            // Save user settings
            Properties.Settings.Default.IsMaximized = (WindowState == FormWindowState.Maximized);
            if (FormWindowState.Normal != WindowState)
            {
                // Must restore to get actual position
                ShowMainForm();
            }
            Properties.Settings.Default.WindowPos = DesktopBounds;
            Properties.Settings.Default.SplitterPos = m_splitContainer.SplitterDistance;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Called when the pause check box is checked or unchecked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>This is used to pause tracking time for the current activity.</remarks>
        private void m_pauseCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (null != m_currentActivity)
            {
                if (m_pauseCheckBox.Checked)
                {
                    m_timer.Enabled = false; // No need to waste timer ticks
                    m_interruptionBegan = DateTime.Now;
                    m_currentActivity.StopTime = m_interruptionBegan;
                    MarkAsDirty();

                    Icon = Properties.Resources.PTT_Paused_Icon;
                    m_statusMessageToolStripStatusLabel.Text = "Paused";
                }
                else
                {
                    DateTime now = DateTime.Now;
                    TimeSpan diff = now - m_interruptionBegan;
                    // Update activities interruption time
                    m_currentActivity.InterruptionTime += diff;
                    // must also update the stop time so the actual time worked calc will be correct
                    m_currentActivity.StopTime = now;
                    MarkAsDirty();
                    m_timer.Enabled = true; // No need to waste timer ticks

                    Icon = Properties.Resources.PTT_Active_Icon;
                    m_statusMessageToolStripStatusLabel.Text = "Resumed task";
                }
                m_saveButton.Enabled = m_isDirty;
                m_notifyIcon.Icon = Icon;
                m_logDataGridView.Refresh();
                UpdateSummaryArea();
            }
        }

        private void m_doneWithTaskButton_Click(object sender, EventArgs e)
        {
            DoneWithCurrentTask();
        }

        private void m_minimizeButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
            WindowState = FormWindowState.Minimized;
        }

        private void m_gotoTodayButton_Click(object sender, EventArgs e)
        {
            m_dateTimePicker.Value = DateTime.Now;
        }

        private void m_reportsButton_Click(object sender, EventArgs e)
        {
            ReportsForm reportForm = new ReportsForm();
            reportForm.ShowDialog();
        }

        #endregion Event Handlers

        #region Fields

        private Timer m_timer;
        private TimeLogEntry m_currentActivity;
        private bool m_isDirty;
        private SortableBindingList<TimeLogEntry> m_logsForToday;
        private DateTime m_interruptionBegan;
        private bool m_needsToUpdateSysTrayMenu;
        private int m_newTaskMenuPosInGridMenu;
        private bool m_editingCell;
        private LimitedStack<UndoItem> m_undoStack;
        private bool m_blockUndoSaves;
        protected List<string> m_taskMRU;
        private Timer m_startTimeChangedTimer;
        private TimeLogEntry m_startTimeChangedFor;

        #endregion Fields
    }
}
