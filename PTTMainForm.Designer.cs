namespace PersonalTimeTracker
{
    partial class PTTMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label m_summaryLabel;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.ColumnHeader m_customerColumnHeader;
            System.Windows.Forms.ColumnHeader m_billableTimeColumnHeader;
            System.Windows.Forms.TableLayoutPanel m_gridTableLayoutPanel;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.ToolStripSeparator m_toolStripSeparator6;
            System.Windows.Forms.ToolStripSeparator m_toolStripSeparator3;
            System.Windows.Forms.ToolStripSeparator m_toolStripSeparator5;
            System.Windows.Forms.ToolStripMenuItem previousDayToolStripMenuItem;
            System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
            System.Windows.Forms.ToolStripSeparator m_toolStripSeparator4;
            System.Windows.Forms.ToolStripSeparator m_toolStripSeparator1;
            System.Windows.Forms.ToolStripSeparator m_toolStripSeparator2;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PTTMainForm));
            this.m_logDataGridView = new System.Windows.Forms.DataGridView();
            this.m_customerColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.m_taskColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.m_startColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_stopColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_interruptionsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_durationColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_billableColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.m_detailsColumn = new PersonalTimeTracker.DataGridViewMemoColumn();
            this.m_deleteColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.m_logGridContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_removeLogEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_addNewLogEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_pauseGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_resumeTaskAndUpdateTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_resumeAndInsertInterruptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_doneWithTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_nextDayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_sumaryListView = new System.Windows.Forms.ListView();
            this.m_billiableTimePerColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.m_nonBillableTimePerColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.m_nonBillableTimeColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.m_totalTimePerColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.m_totalTimeColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.m_notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.m_contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.m_openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_doneWithTaskMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_newCustomerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_newTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_currentTaskToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_recentTasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.m_errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.m_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.m_dateScroll = new System.Windows.Forms.HScrollBar();
            this.m_pauseCheckBox = new System.Windows.Forms.CheckBox();
            this.m_editCustomersButton = new System.Windows.Forms.Button();
            this.m_editTasksButton = new System.Windows.Forms.Button();
            this.m_saveIntervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.m_saveButton = new System.Windows.Forms.Button();
            this.m_addLogButton = new System.Windows.Forms.Button();
            this.m_startNewTaskButton = new System.Windows.Forms.Button();
            this.m_doneWithTaskButton = new System.Windows.Forms.Button();
            this.m_minimizeButton = new System.Windows.Forms.Button();
            this.m_statusStrip = new System.Windows.Forms.StatusStrip();
            this.m_currentTaskToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_statusMessageToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.m_gotoTodayButton = new System.Windows.Forms.Button();
            this.m_splitContainer = new System.Windows.Forms.SplitContainer();
            this.m_dateLabel = new System.Windows.Forms.Label();
            this.m_reportsButton = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewMemoColumn1 = new PersonalTimeTracker.DataGridViewMemoColumn();
            this.m_dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.m_dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._clearOnStandyCheckBox = new System.Windows.Forms.CheckBox();
            label1 = new System.Windows.Forms.Label();
            m_summaryLabel = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            m_customerColumnHeader = new System.Windows.Forms.ColumnHeader();
            m_billableTimeColumnHeader = new System.Windows.Forms.ColumnHeader();
            m_gridTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            m_toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            m_toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            m_toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            previousDayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            m_toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            m_toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            m_toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            m_gridTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_logDataGridView)).BeginInit();
            this.m_logGridContextMenuStrip.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            this.m_contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_saveIntervalNumericUpDown)).BeginInit();
            this.m_statusStrip.SuspendLayout();
            this.m_splitContainer.Panel1.SuspendLayout();
            this.m_splitContainer.Panel2.SuspendLayout();
            this.m_splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(10, 14);
            label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(26, 13);
            label1.TabIndex = 0;
            label1.Text = "Day";
            // 
            // m_summaryLabel
            // 
            m_summaryLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            m_summaryLabel.Location = new System.Drawing.Point(2, 0);
            m_summaryLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            m_summaryLabel.Name = "m_summaryLabel";
            m_summaryLabel.Size = new System.Drawing.Size(678, 20);
            m_summaryLabel.TabIndex = 0;
            m_summaryLabel.Text = "Summary for Day";
            m_summaryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(133, 60);
            label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(61, 13);
            label7.TabIndex = 9;
            label7.Text = "Save every";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(248, 60);
            label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(43, 13);
            label8.TabIndex = 11;
            label8.Text = "minutes";
            // 
            // m_customerColumnHeader
            // 
            m_customerColumnHeader.Text = "Customer";
            m_customerColumnHeader.Width = 180;
            // 
            // m_billableTimeColumnHeader
            // 
            m_billableTimeColumnHeader.Text = "Time";
            m_billableTimeColumnHeader.Width = 100;
            // 
            // m_gridTableLayoutPanel
            // 
            m_gridTableLayoutPanel.ColumnCount = 1;
            m_gridTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            m_gridTableLayoutPanel.Controls.Add(this.m_logDataGridView, 0, 0);
            m_gridTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            m_gridTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            m_gridTableLayoutPanel.Name = "m_gridTableLayoutPanel";
            m_gridTableLayoutPanel.RowCount = 1;
            m_gridTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            m_gridTableLayoutPanel.Size = new System.Drawing.Size(682, 161);
            m_gridTableLayoutPanel.TabIndex = 21;
            // 
            // m_logDataGridView
            // 
            this.m_logDataGridView.AllowUserToResizeRows = false;
            this.m_logDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.m_logDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.m_logDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_logDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.m_customerColumn,
            this.m_taskColumn,
            this.m_startColumn,
            this.m_stopColumn,
            this.m_interruptionsColumn,
            this.m_durationColumn,
            this.m_billableColumn,
            this.m_detailsColumn,
            this.m_deleteColumn});
            this.m_logDataGridView.ContextMenuStrip = this.m_logGridContextMenuStrip;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.m_logDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.m_logDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_logDataGridView.Location = new System.Drawing.Point(2, 2);
            this.m_logDataGridView.Margin = new System.Windows.Forms.Padding(2);
            this.m_logDataGridView.Name = "m_logDataGridView";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.m_logDataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.m_logDataGridView.RowTemplate.Height = 24;
            this.m_logDataGridView.Size = new System.Drawing.Size(678, 157);
            this.m_logDataGridView.TabIndex = 0;
            this.m_logDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_logDataGridView_CellValueChanged);
            this.m_logDataGridView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_logDataGridView_MouseDown);
            this.m_logDataGridView.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.m_logDataGridView_CellBeginEdit);
            this.m_logDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.m_logDataGridView_CellFormatting);
            this.m_logDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.m_logDataGridView_CellValidating);
            this.m_logDataGridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_logDataGridView_CellEndEdit);
            this.m_logDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.m_logDataGridView_DataError);
            // 
            // m_customerColumn
            // 
            this.m_customerColumn.DataPropertyName = "Customer";
            this.m_customerColumn.HeaderText = "Customer";
            this.m_customerColumn.Name = "m_customerColumn";
            // 
            // m_taskColumn
            // 
            this.m_taskColumn.DataPropertyName = "Task";
            this.m_taskColumn.HeaderText = "Task";
            this.m_taskColumn.Name = "m_taskColumn";
            // 
            // m_startColumn
            // 
            this.m_startColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.m_startColumn.DataPropertyName = "StartTime";
            this.m_startColumn.HeaderText = "Start";
            this.m_startColumn.Name = "m_startColumn";
            this.m_startColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_startColumn.Width = 35;
            // 
            // m_stopColumn
            // 
            this.m_stopColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.m_stopColumn.DataPropertyName = "StopTime";
            this.m_stopColumn.HeaderText = "Stop";
            this.m_stopColumn.Name = "m_stopColumn";
            this.m_stopColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_stopColumn.Width = 35;
            // 
            // m_interruptionsColumn
            // 
            this.m_interruptionsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.m_interruptionsColumn.DataPropertyName = "InterruptionTime";
            this.m_interruptionsColumn.HeaderText = "Interruptions";
            this.m_interruptionsColumn.Name = "m_interruptionsColumn";
            this.m_interruptionsColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_interruptionsColumn.Width = 71;
            // 
            // m_durationColumn
            // 
            this.m_durationColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.m_durationColumn.DataPropertyName = "Duration";
            this.m_durationColumn.HeaderText = "Duration";
            this.m_durationColumn.MinimumWidth = 65;
            this.m_durationColumn.Name = "m_durationColumn";
            this.m_durationColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.m_durationColumn.Width = 65;
            // 
            // m_billableColumn
            // 
            this.m_billableColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.m_billableColumn.DataPropertyName = "IsBillable";
            this.m_billableColumn.HeaderText = "Bill";
            this.m_billableColumn.Name = "m_billableColumn";
            this.m_billableColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.m_billableColumn.Width = 26;
            // 
            // m_detailsColumn
            // 
            this.m_detailsColumn.DataPropertyName = "Details";
            this.m_detailsColumn.FillWeight = 50F;
            this.m_detailsColumn.HeaderText = "Details";
            this.m_detailsColumn.Name = "m_detailsColumn";
            this.m_detailsColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.m_detailsColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // m_deleteColumn
            // 
            this.m_deleteColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.m_deleteColumn.HeaderText = "";
            this.m_deleteColumn.Name = "m_deleteColumn";
            this.m_deleteColumn.Text = "Del";
            this.m_deleteColumn.ToolTipText = "Delete the selected log";
            this.m_deleteColumn.UseColumnTextForButtonValue = true;
            this.m_deleteColumn.Width = 50;
            // 
            // m_logGridContextMenuStrip
            // 
            this.m_logGridContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_saveToolStripMenuItem,
            m_toolStripSeparator6,
            this.m_removeLogEntryToolStripMenuItem,
            this.m_addNewLogEntryToolStripMenuItem,
            this.m_undoToolStripMenuItem,
            m_toolStripSeparator3,
            this.m_pauseGridToolStripMenuItem,
            this.m_resumeTaskAndUpdateTimeToolStripMenuItem,
            this.m_resumeAndInsertInterruptToolStripMenuItem,
            this.m_doneWithTaskToolStripMenuItem,
            m_toolStripSeparator5,
            previousDayToolStripMenuItem,
            this.m_nextDayToolStripMenuItem});
            this.m_logGridContextMenuStrip.Name = "m_logGridContextMenuStrip";
            this.m_logGridContextMenuStrip.Size = new System.Drawing.Size(228, 242);
            this.m_logGridContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.m_logGridContextMenuStrip_Opening);
            // 
            // m_saveToolStripMenuItem
            // 
            this.m_saveToolStripMenuItem.Name = "m_saveToolStripMenuItem";
            this.m_saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.m_saveToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.m_saveToolStripMenuItem.Text = "Save";
            this.m_saveToolStripMenuItem.Click += new System.EventHandler(this.m_saveButton_Click);
            // 
            // m_toolStripSeparator6
            // 
            m_toolStripSeparator6.Name = "m_toolStripSeparator6";
            m_toolStripSeparator6.Size = new System.Drawing.Size(224, 6);
            // 
            // m_removeLogEntryToolStripMenuItem
            // 
            this.m_removeLogEntryToolStripMenuItem.Name = "m_removeLogEntryToolStripMenuItem";
            this.m_removeLogEntryToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.m_removeLogEntryToolStripMenuItem.Text = "Remove log entry";
            this.m_removeLogEntryToolStripMenuItem.Click += new System.EventHandler(this.removeLogEntryToolStripMenuItem_Click);
            // 
            // m_addNewLogEntryToolStripMenuItem
            // 
            this.m_addNewLogEntryToolStripMenuItem.Name = "m_addNewLogEntryToolStripMenuItem";
            this.m_addNewLogEntryToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.m_addNewLogEntryToolStripMenuItem.Text = "Add non-active task";
            this.m_addNewLogEntryToolStripMenuItem.Click += new System.EventHandler(this.addNewLogEntryToolStripMenuItem_Click);
            // 
            // m_undoToolStripMenuItem
            // 
            this.m_undoToolStripMenuItem.Name = "m_undoToolStripMenuItem";
            this.m_undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.m_undoToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.m_undoToolStripMenuItem.Text = "Undo";
            this.m_undoToolStripMenuItem.Click += new System.EventHandler(this.m_undoToolStripMenuItem_Click);
            // 
            // m_toolStripSeparator3
            // 
            m_toolStripSeparator3.Name = "m_toolStripSeparator3";
            m_toolStripSeparator3.Size = new System.Drawing.Size(224, 6);
            // 
            // m_pauseGridToolStripMenuItem
            // 
            this.m_pauseGridToolStripMenuItem.Name = "m_pauseGridToolStripMenuItem";
            this.m_pauseGridToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.m_pauseGridToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.m_pauseGridToolStripMenuItem.Text = "Pause";
            this.m_pauseGridToolStripMenuItem.Click += new System.EventHandler(this.m_pauseGridToolStripMenuItem_Click);
            // 
            // m_resumeTaskAndUpdateTimeToolStripMenuItem
            // 
            this.m_resumeTaskAndUpdateTimeToolStripMenuItem.Name = "m_resumeTaskAndUpdateTimeToolStripMenuItem";
            this.m_resumeTaskAndUpdateTimeToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.m_resumeTaskAndUpdateTimeToolStripMenuItem.Text = "Resume task and update time";
            this.m_resumeTaskAndUpdateTimeToolStripMenuItem.Click += new System.EventHandler(this.resumeTaskAndUpdateTimeToolStripMenuItem_Click);
            // 
            // m_resumeAndInsertInterruptToolStripMenuItem
            // 
            this.m_resumeAndInsertInterruptToolStripMenuItem.Name = "m_resumeAndInsertInterruptToolStripMenuItem";
            this.m_resumeAndInsertInterruptToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.m_resumeAndInsertInterruptToolStripMenuItem.Text = "Restart task";
            this.m_resumeAndInsertInterruptToolStripMenuItem.Click += new System.EventHandler(this.resumeAndInsertInterruptToolStripMenuItem_Click);
            // 
            // m_doneWithTaskToolStripMenuItem
            // 
            this.m_doneWithTaskToolStripMenuItem.Name = "m_doneWithTaskToolStripMenuItem";
            this.m_doneWithTaskToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.m_doneWithTaskToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.m_doneWithTaskToolStripMenuItem.Text = "Done with task";
            this.m_doneWithTaskToolStripMenuItem.Click += new System.EventHandler(this.doneWithTaskToolStripMenuItem_Click);
            // 
            // m_toolStripSeparator5
            // 
            m_toolStripSeparator5.Name = "m_toolStripSeparator5";
            m_toolStripSeparator5.Size = new System.Drawing.Size(224, 6);
            // 
            // previousDayToolStripMenuItem
            // 
            previousDayToolStripMenuItem.Name = "previousDayToolStripMenuItem";
            previousDayToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            previousDayToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            previousDayToolStripMenuItem.Text = "Previous day";
            previousDayToolStripMenuItem.Click += new System.EventHandler(this.previousDayToolStripMenuItem_Click);
            // 
            // m_nextDayToolStripMenuItem
            // 
            this.m_nextDayToolStripMenuItem.Name = "m_nextDayToolStripMenuItem";
            this.m_nextDayToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.m_nextDayToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.m_nextDayToolStripMenuItem.Text = "Next day";
            this.m_nextDayToolStripMenuItem.Click += new System.EventHandler(this.nextDayToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(m_summaryLabel, 0, 0);
            tableLayoutPanel1.Controls.Add(this.m_sumaryListView, 0, 1);
            tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel1.Size = new System.Drawing.Size(682, 158);
            tableLayoutPanel1.TabIndex = 15;
            // 
            // m_sumaryListView
            // 
            this.m_sumaryListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            m_customerColumnHeader,
            this.m_billiableTimePerColumnHeader,
            m_billableTimeColumnHeader,
            this.m_nonBillableTimePerColumnHeader,
            this.m_nonBillableTimeColumnHeader,
            this.m_totalTimePerColumnHeader,
            this.m_totalTimeColumnHeader});
            this.m_sumaryListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_sumaryListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.m_sumaryListView.Location = new System.Drawing.Point(3, 23);
            this.m_sumaryListView.Name = "m_sumaryListView";
            this.m_sumaryListView.Size = new System.Drawing.Size(676, 132);
            this.m_sumaryListView.TabIndex = 1;
            this.m_sumaryListView.UseCompatibleStateImageBehavior = false;
            this.m_sumaryListView.View = System.Windows.Forms.View.Details;
            // 
            // m_billiableTimePerColumnHeader
            // 
            this.m_billiableTimePerColumnHeader.Text = "Billable";
            // 
            // m_nonBillableTimePerColumnHeader
            // 
            this.m_nonBillableTimePerColumnHeader.Text = "No Bill";
            // 
            // m_nonBillableTimeColumnHeader
            // 
            this.m_nonBillableTimeColumnHeader.Text = "Time";
            this.m_nonBillableTimeColumnHeader.Width = 100;
            // 
            // m_totalTimePerColumnHeader
            // 
            this.m_totalTimePerColumnHeader.Text = "Total";
            // 
            // m_totalTimeColumnHeader
            // 
            this.m_totalTimeColumnHeader.Text = "Time";
            this.m_totalTimeColumnHeader.Width = 100;
            // 
            // m_toolStripSeparator4
            // 
            m_toolStripSeparator4.Name = "m_toolStripSeparator4";
            m_toolStripSeparator4.Size = new System.Drawing.Size(153, 6);
            // 
            // m_toolStripSeparator1
            // 
            m_toolStripSeparator1.Name = "m_toolStripSeparator1";
            m_toolStripSeparator1.Size = new System.Drawing.Size(153, 6);
            // 
            // m_toolStripSeparator2
            // 
            m_toolStripSeparator2.Name = "m_toolStripSeparator2";
            m_toolStripSeparator2.Size = new System.Drawing.Size(153, 6);
            // 
            // m_notifyIcon
            // 
            this.m_notifyIcon.ContextMenuStrip = this.m_contextMenuStrip;
            this.m_notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("m_notifyIcon.Icon")));
            this.m_notifyIcon.Text = "Personal Time Tracker";
            this.m_notifyIcon.Visible = true;
            this.m_notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_Click);
            // 
            // m_contextMenuStrip
            // 
            this.m_contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_openToolStripMenuItem,
            this.m_pauseToolStripMenuItem,
            this.m_doneWithTaskMenuItem,
            m_toolStripSeparator4,
            this.m_newCustomerToolStripMenuItem,
            this.m_newTaskToolStripMenuItem,
            m_toolStripSeparator1,
            this.m_currentTaskToolStripMenuItem,
            this.m_recentTasksToolStripMenuItem,
            m_toolStripSeparator2,
            this.m_aboutToolStripMenuItem,
            this.m_exitToolStripMenuItem});
            this.m_contextMenuStrip.Name = "m_contextMenuStrip";
            this.m_contextMenuStrip.Size = new System.Drawing.Size(157, 220);
            this.m_contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.m_contextMenuStrip_Opening);
            // 
            // m_openToolStripMenuItem
            // 
            this.m_openToolStripMenuItem.Name = "m_openToolStripMenuItem";
            this.m_openToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.m_openToolStripMenuItem.Text = "Open";
            this.m_openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // m_pauseToolStripMenuItem
            // 
            this.m_pauseToolStripMenuItem.Name = "m_pauseToolStripMenuItem";
            this.m_pauseToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.m_pauseToolStripMenuItem.Text = "Pause";
            this.m_pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // m_doneWithTaskMenuItem
            // 
            this.m_doneWithTaskMenuItem.Name = "m_doneWithTaskMenuItem";
            this.m_doneWithTaskMenuItem.Size = new System.Drawing.Size(156, 22);
            this.m_doneWithTaskMenuItem.Text = "Done with task";
            this.m_doneWithTaskMenuItem.Click += new System.EventHandler(this.m_doneWithTaskMenuItem_Click);
            // 
            // m_newCustomerToolStripMenuItem
            // 
            this.m_newCustomerToolStripMenuItem.Name = "m_newCustomerToolStripMenuItem";
            this.m_newCustomerToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.m_newCustomerToolStripMenuItem.Text = "Edit customers";
            this.m_newCustomerToolStripMenuItem.Click += new System.EventHandler(this.newCustomerToolStripMenuItem_Click);
            // 
            // m_newTaskToolStripMenuItem
            // 
            this.m_newTaskToolStripMenuItem.Name = "m_newTaskToolStripMenuItem";
            this.m_newTaskToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.m_newTaskToolStripMenuItem.Text = "Edit tasks";
            this.m_newTaskToolStripMenuItem.Click += new System.EventHandler(this.newTaskToolStripMenuItem_Click);
            // 
            // m_currentTaskToolStripMenuItem
            // 
            this.m_currentTaskToolStripMenuItem.Name = "m_currentTaskToolStripMenuItem";
            this.m_currentTaskToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.m_currentTaskToolStripMenuItem.Text = "Change task";
            // 
            // m_recentTasksToolStripMenuItem
            // 
            this.m_recentTasksToolStripMenuItem.Name = "m_recentTasksToolStripMenuItem";
            this.m_recentTasksToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.m_recentTasksToolStripMenuItem.Text = "Recent Tasks";
            // 
            // m_aboutToolStripMenuItem
            // 
            this.m_aboutToolStripMenuItem.Name = "m_aboutToolStripMenuItem";
            this.m_aboutToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.m_aboutToolStripMenuItem.Text = "About";
            this.m_aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // m_exitToolStripMenuItem
            // 
            this.m_exitToolStripMenuItem.Name = "m_exitToolStripMenuItem";
            this.m_exitToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.m_exitToolStripMenuItem.Text = "Exit";
            this.m_exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // m_errorProvider
            // 
            this.m_errorProvider.ContainerControl = this;
            // 
            // m_dateTimePicker
            // 
            this.m_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.m_dateTimePicker.Location = new System.Drawing.Point(39, 11);
            this.m_dateTimePicker.Margin = new System.Windows.Forms.Padding(2);
            this.m_dateTimePicker.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.m_dateTimePicker.Name = "m_dateTimePicker";
            this.m_dateTimePicker.Size = new System.Drawing.Size(93, 20);
            this.m_dateTimePicker.TabIndex = 1;
            this.m_dateTimePicker.ValueChanged += new System.EventHandler(this.m_dateTimePicker_ValueChanged);
            // 
            // m_dateScroll
            // 
            this.m_dateScroll.Location = new System.Drawing.Point(136, 11);
            this.m_dateScroll.Name = "m_dateScroll";
            this.m_dateScroll.Size = new System.Drawing.Size(26, 21);
            this.m_dateScroll.TabIndex = 2;
            this.m_dateScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.m_dateScroll_Scroll);
            // 
            // m_pauseCheckBox
            // 
            this.m_pauseCheckBox.AutoSize = true;
            this.m_pauseCheckBox.Enabled = false;
            this.m_pauseCheckBox.Location = new System.Drawing.Point(12, 59);
            this.m_pauseCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.m_pauseCheckBox.Name = "m_pauseCheckBox";
            this.m_pauseCheckBox.Size = new System.Drawing.Size(101, 17);
            this.m_pauseCheckBox.TabIndex = 8;
            this.m_pauseCheckBox.Text = "&Pause Tracking";
            this.m_pauseCheckBox.UseVisualStyleBackColor = true;
            this.m_pauseCheckBox.CheckedChanged += new System.EventHandler(this.m_pauseCheckBox_CheckedChanged);
            // 
            // m_editCustomersButton
            // 
            this.m_editCustomersButton.Location = new System.Drawing.Point(272, 11);
            this.m_editCustomersButton.Name = "m_editCustomersButton";
            this.m_editCustomersButton.Size = new System.Drawing.Size(108, 19);
            this.m_editCustomersButton.TabIndex = 4;
            this.m_editCustomersButton.Text = "Customer List...";
            this.m_editCustomersButton.UseVisualStyleBackColor = true;
            this.m_editCustomersButton.Click += new System.EventHandler(this.m_editCustomersButton_Click);
            // 
            // m_editTasksButton
            // 
            this.m_editTasksButton.Location = new System.Drawing.Point(387, 10);
            this.m_editTasksButton.Name = "m_editTasksButton";
            this.m_editTasksButton.Size = new System.Drawing.Size(108, 19);
            this.m_editTasksButton.TabIndex = 5;
            this.m_editTasksButton.Text = "Task List...";
            this.m_editTasksButton.UseVisualStyleBackColor = true;
            this.m_editTasksButton.Click += new System.EventHandler(this.m_editTasksButton_Click);
            // 
            // m_saveIntervalNumericUpDown
            // 
            this.m_saveIntervalNumericUpDown.Location = new System.Drawing.Point(199, 56);
            this.m_saveIntervalNumericUpDown.Name = "m_saveIntervalNumericUpDown";
            this.m_saveIntervalNumericUpDown.Size = new System.Drawing.Size(46, 20);
            this.m_saveIntervalNumericUpDown.TabIndex = 10;
            this.m_saveIntervalNumericUpDown.ValueChanged += new System.EventHandler(this.m_SaveIntervalNumericUpDown_ValueChanged);
            // 
            // m_saveButton
            // 
            this.m_saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_saveButton.Enabled = false;
            this.m_saveButton.Location = new System.Drawing.Point(14, 408);
            this.m_saveButton.Name = "m_saveButton";
            this.m_saveButton.Size = new System.Drawing.Size(75, 23);
            this.m_saveButton.TabIndex = 14;
            this.m_saveButton.Text = "&Save Now";
            this.m_saveButton.UseVisualStyleBackColor = true;
            this.m_saveButton.Click += new System.EventHandler(this.m_saveButton_Click);
            // 
            // m_addLogButton
            // 
            this.m_addLogButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_addLogButton.Location = new System.Drawing.Point(529, 408);
            this.m_addLogButton.Name = "m_addLogButton";
            this.m_addLogButton.Size = new System.Drawing.Size(109, 23);
            this.m_addLogButton.TabIndex = 17;
            this.m_addLogButton.Text = "&New Log Line";
            this.m_addLogButton.UseVisualStyleBackColor = true;
            this.m_addLogButton.Click += new System.EventHandler(this.m_addLogButton_Click);
            // 
            // m_startNewTaskButton
            // 
            this.m_startNewTaskButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_startNewTaskButton.Location = new System.Drawing.Point(122, 408);
            this.m_startNewTaskButton.Name = "m_startNewTaskButton";
            this.m_startNewTaskButton.Size = new System.Drawing.Size(109, 23);
            this.m_startNewTaskButton.TabIndex = 15;
            this.m_startNewTaskButton.Text = "Start New &Task";
            this.m_startNewTaskButton.UseVisualStyleBackColor = true;
            this.m_startNewTaskButton.Click += new System.EventHandler(this.m_startNewTaskButton_Click);
            // 
            // m_doneWithTaskButton
            // 
            this.m_doneWithTaskButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_doneWithTaskButton.Enabled = false;
            this.m_doneWithTaskButton.Location = new System.Drawing.Point(260, 408);
            this.m_doneWithTaskButton.Name = "m_doneWithTaskButton";
            this.m_doneWithTaskButton.Size = new System.Drawing.Size(109, 23);
            this.m_doneWithTaskButton.TabIndex = 16;
            this.m_doneWithTaskButton.Text = "&Done With Task";
            this.m_doneWithTaskButton.UseVisualStyleBackColor = true;
            this.m_doneWithTaskButton.Click += new System.EventHandler(this.m_doneWithTaskButton_Click);
            // 
            // m_minimizeButton
            // 
            this.m_minimizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.m_minimizeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_minimizeButton.Location = new System.Drawing.Point(666, 8);
            this.m_minimizeButton.Name = "m_minimizeButton";
            this.m_minimizeButton.Size = new System.Drawing.Size(20, 23);
            this.m_minimizeButton.TabIndex = 6;
            this.m_minimizeButton.Text = "...";
            this.m_minimizeButton.UseVisualStyleBackColor = true;
            this.m_minimizeButton.Click += new System.EventHandler(this.m_minimizeButton_Click);
            // 
            // m_statusStrip
            // 
            this.m_statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_currentTaskToolStripStatusLabel,
            this.m_statusMessageToolStripStatusLabel});
            this.m_statusStrip.Location = new System.Drawing.Point(0, 438);
            this.m_statusStrip.Name = "m_statusStrip";
            this.m_statusStrip.Size = new System.Drawing.Size(698, 22);
            this.m_statusStrip.TabIndex = 19;
            // 
            // m_currentTaskToolStripStatusLabel
            // 
            this.m_currentTaskToolStripStatusLabel.Name = "m_currentTaskToolStripStatusLabel";
            this.m_currentTaskToolStripStatusLabel.Size = new System.Drawing.Size(75, 17);
            this.m_currentTaskToolStripStatusLabel.Text = "No active task";
            // 
            // m_statusMessageToolStripStatusLabel
            // 
            this.m_statusMessageToolStripStatusLabel.Name = "m_statusMessageToolStripStatusLabel";
            this.m_statusMessageToolStripStatusLabel.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.m_statusMessageToolStripStatusLabel.Size = new System.Drawing.Size(30, 17);
            // 
            // m_gotoTodayButton
            // 
            this.m_gotoTodayButton.Location = new System.Drawing.Point(166, 12);
            this.m_gotoTodayButton.Name = "m_gotoTodayButton";
            this.m_gotoTodayButton.Size = new System.Drawing.Size(51, 23);
            this.m_gotoTodayButton.TabIndex = 3;
            this.m_gotoTodayButton.Text = "Today";
            this.m_gotoTodayButton.UseVisualStyleBackColor = true;
            this.m_gotoTodayButton.Click += new System.EventHandler(this.m_gotoTodayButton_Click);
            // 
            // m_splitContainer
            // 
            this.m_splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_splitContainer.Location = new System.Drawing.Point(9, 78);
            this.m_splitContainer.Name = "m_splitContainer";
            this.m_splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // m_splitContainer.Panel1
            // 
            this.m_splitContainer.Panel1.Controls.Add(m_gridTableLayoutPanel);
            // 
            // m_splitContainer.Panel2
            // 
            this.m_splitContainer.Panel2.Controls.Add(tableLayoutPanel1);
            this.m_splitContainer.Size = new System.Drawing.Size(682, 323);
            this.m_splitContainer.SplitterDistance = 161;
            this.m_splitContainer.TabIndex = 20;
            // 
            // m_dateLabel
            // 
            this.m_dateLabel.AutoSize = true;
            this.m_dateLabel.Location = new System.Drawing.Point(39, 41);
            this.m_dateLabel.Name = "m_dateLabel";
            this.m_dateLabel.Size = new System.Drawing.Size(40, 13);
            this.m_dateLabel.TabIndex = 7;
            this.m_dateLabel.Text = "<date>";
            // 
            // m_reportsButton
            // 
            this.m_reportsButton.Location = new System.Drawing.Point(447, 52);
            this.m_reportsButton.Name = "m_reportsButton";
            this.m_reportsButton.Size = new System.Drawing.Size(75, 23);
            this.m_reportsButton.TabIndex = 13;
            this.m_reportsButton.Text = "Reports...";
            this.m_reportsButton.UseVisualStyleBackColor = true;
            this.m_reportsButton.Click += new System.EventHandler(this.m_reportsButton_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "StartTime";
            this.dataGridViewTextBoxColumn1.HeaderText = "Start";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "StopTime";
            this.dataGridViewTextBoxColumn2.HeaderText = "Stop";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "InterruptionTime";
            this.dataGridViewTextBoxColumn3.HeaderText = "Interruptions";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Duration";
            this.dataGridViewTextBoxColumn4.HeaderText = "Duration";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewMemoColumn1
            // 
            this.dataGridViewMemoColumn1.DataPropertyName = "Details";
            this.dataGridViewMemoColumn1.FillWeight = 50F;
            this.dataGridViewMemoColumn1.HeaderText = "Details";
            this.dataGridViewMemoColumn1.Name = "dataGridViewMemoColumn1";
            this.dataGridViewMemoColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMemoColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewMemoColumn1.Width = 73;
            // 
            // m_dataGridViewTextBoxColumn1
            // 
            this.m_dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.m_dataGridViewTextBoxColumn1.DataPropertyName = "StartTime";
            this.m_dataGridViewTextBoxColumn1.HeaderText = "Start";
            this.m_dataGridViewTextBoxColumn1.Name = "m_dataGridViewTextBoxColumn1";
            // 
            // m_dataGridViewTextBoxColumn2
            // 
            this.m_dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.m_dataGridViewTextBoxColumn2.DataPropertyName = "StopTime";
            this.m_dataGridViewTextBoxColumn2.HeaderText = "Stop";
            this.m_dataGridViewTextBoxColumn2.Name = "m_dataGridViewTextBoxColumn2";
            // 
            // m_dataGridViewTextBoxColumn3
            // 
            this.m_dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.m_dataGridViewTextBoxColumn3.DataPropertyName = "InterruptionTime";
            this.m_dataGridViewTextBoxColumn3.HeaderText = "Interruptions";
            this.m_dataGridViewTextBoxColumn3.Name = "m_dataGridViewTextBoxColumn3";
            // 
            // m_dataGridViewTextBoxColumn4
            // 
            this.m_dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.m_dataGridViewTextBoxColumn4.DataPropertyName = "Duration";
            this.m_dataGridViewTextBoxColumn4.HeaderText = "Duration";
            this.m_dataGridViewTextBoxColumn4.Name = "m_dataGridViewTextBoxColumn4";
            // 
            // m_dataGridViewTextBoxColumn5
            // 
            this.m_dataGridViewTextBoxColumn5.DataPropertyName = "Details";
            this.m_dataGridViewTextBoxColumn5.FillWeight = 50F;
            this.m_dataGridViewTextBoxColumn5.HeaderText = "Details";
            this.m_dataGridViewTextBoxColumn5.Name = "m_dataGridViewTextBoxColumn5";
            this.m_dataGridViewTextBoxColumn5.Width = 54;
            // 
            // _clearOnStandyCheckBox
            // 
            this._clearOnStandyCheckBox.AutoSize = true;
            this._clearOnStandyCheckBox.Location = new System.Drawing.Point(301, 57);
            this._clearOnStandyCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this._clearOnStandyCheckBox.Name = "_clearOnStandyCheckBox";
            this._clearOnStandyCheckBox.Size = new System.Drawing.Size(134, 17);
            this._clearOnStandyCheckBox.TabIndex = 12;
            this._clearOnStandyCheckBox.Text = "&Clear Task on Standby";
            this._clearOnStandyCheckBox.UseVisualStyleBackColor = true;
            this._clearOnStandyCheckBox.CheckedChanged += new System.EventHandler(this.ClearOnStandyCheckBox_CheckedChanged);
            // 
            // PTTMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_minimizeButton;
            this.ClientSize = new System.Drawing.Size(698, 460);
            this.Controls.Add(this._clearOnStandyCheckBox);
            this.Controls.Add(this.m_dateLabel);
            this.Controls.Add(this.m_splitContainer);
            this.Controls.Add(this.m_reportsButton);
            this.Controls.Add(this.m_gotoTodayButton);
            this.Controls.Add(this.m_statusStrip);
            this.Controls.Add(this.m_doneWithTaskButton);
            this.Controls.Add(this.m_minimizeButton);
            this.Controls.Add(this.m_editTasksButton);
            this.Controls.Add(this.m_saveButton);
            this.Controls.Add(this.m_startNewTaskButton);
            this.Controls.Add(this.m_addLogButton);
            this.Controls.Add(this.m_saveIntervalNumericUpDown);
            this.Controls.Add(this.m_editCustomersButton);
            this.Controls.Add(this.m_pauseCheckBox);
            this.Controls.Add(this.m_dateScroll);
            this.Controls.Add(label1);
            this.Controls.Add(this.m_dateTimePicker);
            this.Controls.Add(label8);
            this.Controls.Add(label7);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PTTMainForm";
            this.Text = "Personal Time Tracker";
            this.Load += new System.EventHandler(this.PTTMainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PTTMainForm_FormClosing);
            this.Resize += new System.EventHandler(this.PTTMainForm_Resize);
            m_gridTableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_logDataGridView)).EndInit();
            this.m_logGridContextMenuStrip.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            this.m_contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_saveIntervalNumericUpDown)).EndInit();
            this.m_statusStrip.ResumeLayout(false);
            this.m_statusStrip.PerformLayout();
            this.m_splitContainer.Panel1.ResumeLayout(false);
            this.m_splitContainer.Panel2.ResumeLayout(false);
            this.m_splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip m_contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem m_openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_newCustomerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_newTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_exitToolStripMenuItem;
        private System.Windows.Forms.ErrorProvider m_errorProvider;
        private System.Windows.Forms.DateTimePicker m_dateTimePicker;
        private System.Windows.Forms.HScrollBar m_dateScroll;
        private System.Windows.Forms.CheckBox m_pauseCheckBox;
        private System.Windows.Forms.DataGridView m_logDataGridView;
        private System.Windows.Forms.Button m_editCustomersButton;
        private System.Windows.Forms.Button m_editTasksButton;
        private System.Windows.Forms.NumericUpDown m_saveIntervalNumericUpDown;
        private System.Windows.Forms.Button m_saveButton;
        private System.Windows.Forms.Button m_addLogButton;
        private System.Windows.Forms.ContextMenuStrip m_logGridContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem m_removeLogEntryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_addNewLogEntryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_resumeTaskAndUpdateTimeToolStripMenuItem;
        private System.Windows.Forms.Button m_startNewTaskButton;
        private System.Windows.Forms.ListView m_sumaryListView;
        private System.Windows.Forms.ColumnHeader m_billiableTimePerColumnHeader;
        private System.Windows.Forms.ToolStripMenuItem m_doneWithTaskToolStripMenuItem;
        private System.Windows.Forms.Button m_doneWithTaskButton;
        private System.Windows.Forms.ToolStripMenuItem m_pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_currentTaskToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_resumeAndInsertInterruptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_doneWithTaskMenuItem;
        private System.Windows.Forms.Button m_minimizeButton;
        private System.Windows.Forms.StatusStrip m_statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel m_currentTaskToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel m_statusMessageToolStripStatusLabel;
        private System.Windows.Forms.Button m_gotoTodayButton;
        private System.Windows.Forms.ColumnHeader m_nonBillableTimeColumnHeader;
        private System.Windows.Forms.ColumnHeader m_nonBillableTimePerColumnHeader;
        private System.Windows.Forms.NotifyIcon m_notifyIcon;
        private System.Windows.Forms.ToolStripMenuItem m_pauseGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_aboutToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader m_totalTimeColumnHeader;
        private System.Windows.Forms.ColumnHeader m_totalTimePerColumnHeader;
        private System.Windows.Forms.SplitContainer m_splitContainer;
        private System.Windows.Forms.ToolStripMenuItem m_undoToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_dataGridViewTextBoxColumn5;
        private System.Windows.Forms.ToolStripMenuItem m_nextDayToolStripMenuItem;
        private System.Windows.Forms.Label m_dateLabel;
        private System.Windows.Forms.Button m_reportsButton;
        private System.Windows.Forms.ToolStripMenuItem m_saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem m_recentTasksToolStripMenuItem;
        private System.Windows.Forms.DataGridViewComboBoxColumn m_customerColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn m_taskColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_startColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_stopColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_interruptionsColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn m_durationColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn m_billableColumn;
        private DataGridViewMemoColumn m_detailsColumn;
        private System.Windows.Forms.DataGridViewButtonColumn m_deleteColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewMemoColumn dataGridViewMemoColumn1;
        private System.Windows.Forms.CheckBox _clearOnStandyCheckBox;
    }
}

