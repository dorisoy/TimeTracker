using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.ComponentModel;

namespace PersonalTimeTracker
{
    public class TimeLogEntry : INotifyPropertyChanged, IComparable<TimeLogEntry>, ICloneable
    {
        #region Static Members

        static private Dictionary<DateTime, SortableBindingList<TimeLogEntry>> s_AllTimeLogEntries = new Dictionary<DateTime, SortableBindingList<TimeLogEntry>>();

        static public Dictionary<DateTime, SortableBindingList<TimeLogEntry>> AllTimeLogEntries
        {
            get { return s_AllTimeLogEntries; }
        }

        static public TimeLogEntry CreateNewLogEntryForDay(DateTime now, Customer customer, Task task)
        {
            TimeLogEntry log = new TimeLogEntry();
            DateTime timeWithoutSeconds = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0);
            log.StartTime = timeWithoutSeconds;
            log.StopTime = timeWithoutSeconds;
            log.Customer = customer;
            log.Task = task;
            log.Details = "";
            log.IsBillable = true;

            AddLogToList(log);

            return log;
        }

        /// <summary>
        /// Create a new time log entry for the day given.
        /// </summary>
        /// <returns></returns>
        static public TimeLogEntry CreateNewLogEntryForDay(DateTime now)
        {
            if (0 == Customer.AllCustomers.Count || 0 == Task.AllTasks.Count)
            {
                System.Windows.Forms.MessageBox.Show("You must first setup some customers and tasks", "Setup Required", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return null;
            }
            Customer customer = null;
            string custName = Properties.Settings.Default.DefaultCustomer;
            if (null != custName && 0 != custName.Length)
            {
                customer = Customer.FindCustomer(custName);
            }
            if (null == customer)
            {
                customer = Customer.AllCustomers[0];
            }
            Task task = null;
            string taskName = Properties.Settings.Default.DefaultTask;
            if (null != taskName && 0 != taskName.Length)
            {
                task = Task.FindTask(taskName);
            }
            if (null == task)
            {
                task = Task.AllTasks[0];
            }
            return CreateNewLogEntryForDay(now, customer, task);
        }

        private static void AddLogToList(TimeLogEntry log)
        {
            // Add this to the proper list
            SortableBindingList<TimeLogEntry> logEntries = LogsForDay(log.StartTime);
            if (null == logEntries)
            {
                logEntries = new SortableBindingList<TimeLogEntry>();
                DateTime logKey = new DateTime(log.StartTime.Year, log.StartTime.Month, log.StartTime.Day);
                AllTimeLogEntries[logKey] = logEntries;
            }
            logEntries.Add(log);
        }

        /// <summary>
        /// Adjust existing log times based on the new log entry.
        /// </summary>
        /// <param name="newLog">The log just added.</param>
        static public void AdjustTimeEntriesForNewItem(TimeLogEntry newLog)
        {
            SortableBindingList<TimeLogEntry> currentList = LogsForDay(newLog.StartTime);
            // First adjust for any overlaps with the new entry taking precedence
            for (int rowIdx = currentList.Count - 1; rowIdx >= 0; --rowIdx)
            {
                TimeLogEntry log = currentList[rowIdx];
                if (object.ReferenceEquals(log, newLog)) continue; // skip yourself

                // Is the current log is within the new one?
                if (newLog.StartTime < log.StartTime && newLog.StopTime > log.StopTime)
                {
                    // 2 Options, delete this log or split up new one
                    // (I hate putting UI into this class but it would be more work than it is worth to have a callback or something)
                    System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("An old log entry falls in the time period specified for this new log.  Do you want to delete the old log entry?  If you choose No the new entry will be split around it.",
                        "Log Overlap", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
                    if (System.Windows.Forms.DialogResult.Yes == result)
                    {
                        // Delete old line
                        currentList.RemoveAt(rowIdx);
                    }
                    else
                    {
                        // Split new line into multiple lines
                        TimeLogEntry newAdjustmentLog = TimeLogEntry.CreateNewLogEntryFromExisingLog(newLog);
                        // New log will be the last one
                        newAdjustmentLog.StartTime = newLog.StartTime;
                        newAdjustmentLog.StopTime = log.StartTime.AddSeconds(-1);
                        newLog.StartTime = log.StopTime.AddSeconds(1);
                    }
                }
                // Is the new log inside of this log?
                else if (newLog.StartTime > log.StartTime && newLog.StartTime < log.StopTime)
                {
                    // The new log starts in the middle of this log so adjust
                    // First see if the new log is entirely contained in the old log
                    if (newLog.StopTime < log.StopTime)
                    {
                        // Yes the new log is smack in the middle of the old one.
                        // So, split the old one into 2 entries
                        TimeLogEntry newAdjustmentLog = TimeLogEntry.CreateNewLogEntryFromExisingLog(log);
                        newAdjustmentLog.StartTime = newLog.StopTime.AddSeconds(1);
                        log.StopTime = newLog.StartTime.AddSeconds(-1);
                        // Swap interruption time if needed
                        TimeSpan zeroTimeSpan = new TimeSpan();
                        if (zeroTimeSpan > log.Duration)
                        {
                            if (log.InterruptionTime > newAdjustmentLog.Duration)
                            {
                                // Divide evenly between both
                                TimeSpan halfOfInterruption = new TimeSpan(log.InterruptionTime.Ticks / 2);
                                log.InterruptionTime = halfOfInterruption;
                                newAdjustmentLog.InterruptionTime = halfOfInterruption;
                            }
                            else
                            {
                                // Just swap who gets the interrupt time
                                newAdjustmentLog.InterruptionTime = log.InterruptionTime;
                                log.InterruptionTime = zeroTimeSpan;
                            }
                        }
                        break; // no need to continue
                    }
                    else
                    {
                        log.StopTime = newLog.StartTime.AddSeconds(-1);
                    }
                }
                else if (newLog.StopTime > log.StartTime && newLog.StopTime < log.StopTime)
                {
                    log.StartTime = newLog.StopTime.AddSeconds(1);
                }
            }
        }

        static public SortableBindingList<TimeLogEntry> ReplaceLogEntriesForDay(DateTime targetDay, List<TimeLogEntry> list)
        {
            SortableBindingList<TimeLogEntry> logEntries = new SortableBindingList<TimeLogEntry>();
            foreach (TimeLogEntry entry in list) logEntries.Add(entry);
            DateTime logKey = new DateTime(targetDay.Year, targetDay.Month, targetDay.Day);
            AllTimeLogEntries[logKey] = logEntries;
            return logEntries;
        }

        /// <summary>
        /// Create a new time log entry for the day given.
        /// </summary>
        /// <returns></returns>
        static public TimeLogEntry CreateNewLogEntryFromExisingLog(TimeLogEntry log)
        {
            TimeLogEntry newLog = new TimeLogEntry();
            newLog.Customer = log.Customer;
            newLog.Task = log.Task;
            newLog.Details = log.Details;
            newLog.StartTime = log.StartTime;
            newLog.StopTime = log.StopTime;
            newLog.IsBillable = log.IsBillable;
            AddLogToList(newLog);
            return newLog;
        }

        static public bool IsTheSameDay(DateTime date1, DateTime date2)
        {
            return (date1.Year == date2.Year && date1.Month == date2.Month && date1.Day == date2.Day);
        }

        static public SortableBindingList<TimeLogEntry> LogsForDay(DateTime dayOfLogs)
        {
            DateTime truncatedTime = new DateTime(dayOfLogs.Year, dayOfLogs.Month, dayOfLogs.Day);
            SortableBindingList<TimeLogEntry> list = null;
            if (AllTimeLogEntries.ContainsKey(truncatedTime))
            {
                list = AllTimeLogEntries[truncatedTime];
            }
            return list;
        }

        static public void SortLogs(SortableBindingList<TimeLogEntry> currentList)
        {
            currentList.Sort("StartTime", ListSortDirection.Ascending);
        }

        /// <summary>
        /// Read all log entries from Xml file.
        /// </summary>
        /// <param name="dataXml"></param>
        static public void ReadLogEntries(XmlDocument dataXml)
        {
            // Load indvidual time log entries
            XmlNodeList dayNodes = dataXml.SelectNodes("/ptt_data/time_logs/day");
            foreach (XmlNode dayNode in dayNodes)
            {
                XmlAttribute attr = dayNode.Attributes["date"];
                DateTime logDate = DateTime.Parse(attr.Value);
                SortableBindingList<TimeLogEntry> logEntries = new SortableBindingList<TimeLogEntry>();
                TimeLogEntry.AllTimeLogEntries[logDate] = logEntries;

                XmlNodeList logNodes = dayNode.SelectNodes("log");
                foreach (XmlNode node in logNodes)
                {
                    TimeLogEntry entry = new TimeLogEntry(node, logDate);
                    logEntries.Add(entry);
                }
            }
        }

        /// <summary>
        /// Save log entries, for current month, to Xml file.
        /// </summary>
        /// <param name="rootNode"></param>
        static public void SaveLogs(XmlNode rootNode, DateTime targetMonth)
        {
            XmlNode logsNode = rootNode.OwnerDocument.CreateNode(XmlNodeType.Element, "time_logs", rootNode.NamespaceURI);
            rootNode.AppendChild(logsNode);
            foreach (KeyValuePair<DateTime, SortableBindingList<TimeLogEntry>> dictPair in TimeLogEntry.AllTimeLogEntries)
            {
                // Skip entries not in current month
                DateTime logDate = dictPair.Key;
                if (targetMonth.Year == logDate.Year && targetMonth.Month == logDate.Month)
                {
                    XmlNode dayNode = rootNode.OwnerDocument.CreateNode(XmlNodeType.Element, "day", rootNode.NamespaceURI);
                    logsNode.AppendChild(dayNode);
                    XmlAttribute attr = rootNode.OwnerDocument.CreateAttribute("date");
                    attr.Value = logDate.ToShortDateString();
                    dayNode.Attributes.Append(attr);
                    // Save tasks
                    foreach (TimeLogEntry task in dictPair.Value)
                    {
                        task.Save(dayNode);
                    }
                }
            }
        }

        /// <summary>
        /// Tell if any log entries exist for the given customer.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns><b>true</b> if at least 1 time log entry exists for the given customer.</returns>
        static public bool DoEntriesExistForCustomer(Customer customer)
        {
            foreach (KeyValuePair<DateTime, SortableBindingList<TimeLogEntry>> dictPair in TimeLogEntry.AllTimeLogEntries)
            {
                foreach (TimeLogEntry log in dictPair.Value)
                {
                    if (customer.Equals(log.Customer) ) return true;
                }
            }
            return false;
        }

        static public bool DoEntriesExistForTask(Task task)
        {
            foreach (KeyValuePair<DateTime, SortableBindingList<TimeLogEntry>> dictPair in TimeLogEntry.AllTimeLogEntries)
            {
                foreach (TimeLogEntry log in dictPair.Value)
                {
                    if (task.Equals(log.Task)) return true;
                }
            }
            return false;
        }

        static public void DeleteLogEntry(TimeLogEntry log)
        {
            SortableBindingList<TimeLogEntry> dayLogs = LogsForDay(log.StartTime);
            if (null != dayLogs)
            {
                dayLogs.Remove(log);
            }
        }

        /// <summary>
        /// Delete all time log entries for the given customer.
        /// </summary>
        /// <param name="customer"></param>
        static public void DeleteEntriesForCustomer(Customer customer)
        {
            foreach (KeyValuePair<DateTime, SortableBindingList<TimeLogEntry>> dictPair in TimeLogEntry.AllTimeLogEntries)
            {
                // Traverse list in reverse order so can delete as we go
                for (int idx = dictPair.Value.Count - 1; idx >= 0; --idx)
                {
                    TimeLogEntry log = dictPair.Value[idx];
                    if (customer.Equals(log.Customer))
                    {
                        dictPair.Value.RemoveAt(idx);
                    }
                }
            }
        }

        static public void DeleteEntriesForTask(Task task)
        {
            foreach (KeyValuePair<DateTime, SortableBindingList<TimeLogEntry>> dictPair in TimeLogEntry.AllTimeLogEntries)
            {
                // Traverse list in reverse order so can delete as we go
                for (int idx = dictPair.Value.Count - 1; idx >= 0; --idx)
                {
                    TimeLogEntry log = dictPair.Value[idx];
                    if (task.Equals(log.Task))
                    {
                        dictPair.Value.RemoveAt(idx);
                    }
                }
            }
        }

        #endregion Static members

        #region ctors

        protected TimeLogEntry()
        {
            m_interruptionTime = new TimeSpan();
        }

        /// <summary>
        /// Intitialize a log entry from its XML data.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="dateOfLog"></param>
        public TimeLogEntry(XmlNode node, DateTime dateOfLog)
        {
            XmlAttribute attr = node.Attributes["customer"];
            if (null == attr) throw new FormatException("Log entry missing 'customer' attribute");
            m_customer = Customer.FindCustomer(attr.Value);
            if (null == m_customer) throw new FormatException("Invalid customer for log entry.");

            attr = node.Attributes["task"];
            if (null == attr) throw new FormatException("Log entry missing 'task' attribute");
            m_task = Task.FindTask(attr.Value);
            if (null == m_customer) throw new FormatException("Invalid task for log entry.");

            attr = node.Attributes["start"];
            if (null == attr) throw new FormatException("Log entry missing 'start' attribute");
            DateTime tempTime = DateTime.Parse(attr.Value);
            m_startTime = new DateTime(dateOfLog.Year, dateOfLog.Month, dateOfLog.Day, tempTime.Hour, tempTime.Minute, tempTime.Second);

            attr = node.Attributes["stop"];
            if (null == attr) throw new FormatException("Log entry missing 'stop' attribute");
            tempTime = DateTime.Parse(attr.Value);
            m_stopTime = new DateTime(dateOfLog.Year, dateOfLog.Month, dateOfLog.Day, tempTime.Hour, tempTime.Minute, tempTime.Second);

            attr = node.Attributes["billable"];
            if (null == attr) throw new FormatException("Log entry missing 'billable' attribute");
            m_isBillable = bool.Parse(attr.Value);

            attr = node.Attributes["interruptions"];
            if (null != attr)
            {
                m_interruptionTime = TimeSpan.Parse(attr.Value);
            }

            m_details = node.InnerText;
        }

        #endregion ctors

        #region Properties

        // Used for binding
        public TimeLogEntry Self
        {
            get { return this; }
        }

        /// <summary>
        /// Gets or sets the customer associated with this log entry.
        /// </summary>
        public Customer Customer
        {
            get { return m_customer; }
            set { m_customer = value; }
        }

        /// <summary>
        /// Gets or sets the details for this log entry.
        /// </summary>
        public string Details
        {
            get { return m_details; }
            set { m_details = value; }
        }

        /// <summary>
        /// Gets or sets the if this log entry is billable.
        /// </summary>
        public bool IsBillable
        {
            get { return m_isBillable; }
            set { m_isBillable = value; }
        }

        /// <summary>
        /// Gets or sets the task associated with this log.
        /// </summary>
        public Task Task
        {
            get { return m_task; }
            set { m_task = value; }
        }

        /// <summary>
        /// Gets or sets the time the task started.
        /// </summary>
        public DateTime StartTime
        {
            get { return m_startTime; }
            set { m_startTime = value; }
        }

        /// <summary>
        /// Gets or sets the time the task stopped.
        /// </summary>
        public DateTime StopTime
        {
            get { return m_stopTime; }
            set
            {
                if (m_stopTime != value)
                {
                    m_stopTime = value;
                    if (null != PropertyChanged)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("StopTime"));
                        PropertyChanged(this, new PropertyChangedEventArgs("Duration"));
                    }
                }
            }
        }

        /// <summary>
        /// Gets the time spent on this task.
        /// </summary>
        public TimeSpan Duration
        {
            get
            {
                TimeSpan diff = StopTime - StartTime;
                diff -= InterruptionTime; // adjust for any interruptions
                return diff;
            }
        }

        /// <summary>
        /// Gets or sets the interruption time for this task.
        /// </summary>
        public TimeSpan InterruptionTime
        {
            get { return m_interruptionTime; }
            set
            {
                if (m_interruptionTime != value)
                {
                    m_interruptionTime = value;
                    if (null != PropertyChanged)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("InterruptionTime"));
                        PropertyChanged(this, new PropertyChangedEventArgs("Duration"));
                    }
                }
            }
        }

        /// <summary>
        /// Tell if this log is for the current day.
        /// </summary>
        public bool IsForToday
        {
            get
            {
                return IsTheSameDay(StartTime, DateTime.Now);
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Save this log entry to the given Xml node.
        /// </summary>
        /// <param name="logsNode">The root log node for the day of this log entry.</param>
        public void Save(XmlNode logsNode)
        {
            XmlNode node = logsNode.OwnerDocument.CreateNode(XmlNodeType.Element, "log", logsNode.NamespaceURI);

            //<log customer="Customer1" task="Meeting" start="08:00" stop="08:30" billable="true">This was a dumb meeting</log>
            node.InnerText = m_details;

            XmlAttribute attr = logsNode.OwnerDocument.CreateAttribute("customer");
            attr.Value = m_customer.Name;
            node.Attributes.Append(attr);

            attr = logsNode.OwnerDocument.CreateAttribute("task");
            attr.Value = m_task.Name;
            node.Attributes.Append(attr);

            attr = logsNode.OwnerDocument.CreateAttribute("start");
            attr.Value = m_startTime.ToShortTimeString();
            node.Attributes.Append(attr);

            attr = logsNode.OwnerDocument.CreateAttribute("stop");
            attr.Value = m_stopTime.ToShortTimeString();
            node.Attributes.Append(attr);

            if (0 != InterruptionTime.TotalSeconds)
            {
                attr = logsNode.OwnerDocument.CreateAttribute("interruptions");
                attr.Value = InterruptionTime.ToString();
                node.Attributes.Append(attr);
            }

            attr = logsNode.OwnerDocument.CreateAttribute("billable");
            attr.Value = m_isBillable.ToString();
            node.Attributes.Append(attr);

            logsNode.AppendChild(node);
        }

        #endregion Methods

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion INotifyPropertyChanged Members

        #region IComparable<TimeLogEntry> Members

        int IComparable<TimeLogEntry>.CompareTo(TimeLogEntry other)
        {
            if (other.StartTime < StartTime) return 1;
            else if (other.StartTime > StartTime) return -1;
            return 0;
        }

        #endregion IComparable<TimeLogEntry> Members

        #region ICloneable Members

        public object Clone()
        {
            TimeLogEntry newLog = new TimeLogEntry();
            newLog.m_customer = m_customer;
            newLog.m_details = m_details;
            newLog.m_isBillable = m_isBillable;
            newLog.m_task = m_task;
            newLog.m_startTime = m_startTime;
            newLog.m_stopTime = m_stopTime;
            newLog.m_interruptionTime = m_interruptionTime;
            return newLog;
        }

        #endregion ICloneable Members

        #region Fields

        private Customer m_customer;
        private string m_details;
        private bool m_isBillable;
        private Task m_task;
        private DateTime m_startTime;
        private DateTime m_stopTime;
        private TimeSpan m_interruptionTime;

        #endregion Fields
    }
}
