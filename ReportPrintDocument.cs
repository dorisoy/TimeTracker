using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;

namespace PersonalTimeTracker
{
    /// <summary>
    /// Used to help generate reports on time logs.
    /// </summary>
    class ReportPrintDocument : PrintDocument
    {
        const int BILLABLE_TIME_INDEX = 0;
        const int NON_BILLABLE_TIME_INDEX = 1;

        #region Properties

        public int TotalNumberOfPages
        {
            get { return m_totalNumberOfPages; }
            set { m_totalNumberOfPages = value; }
        }

        public bool IsSummaryReport
        {
            get { return m_isSummaryReport; }
            set { m_isSummaryReport = value; }
        }

        public bool ShowTasks
        {
            get { return m_showTasks; }
            set { m_showTasks = value; }
        }

        public DateTime BeginDate
        {
            get { return m_beginDate; }
            set { m_beginDate = value; }
        }

        public DateTime EndDate
        {
            get { return m_endDate; }
            set { m_endDate = value; }
        }

        public bool SkipDaysWithoutTasks
        {
            get { return m_skipDaysWithoutTasks; }
            set { m_skipDaysWithoutTasks = value; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Print the header for a new day.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="drawLocation"></param>
        private void PrintDayHeaderLine(PrintPageEventArgs e, ref Point drawLocation)
        {
            if (drawLocation.Y > e.MarginBounds.Top)
            {
                drawLocation.Y += m_headerFont.Height; // skip a line
            }
            string headerStr = string.Format(Properties.Resources.ReportStrDayHeader, m_currentDateBeingPrinted.ToLongDateString());
            e.Graphics.DrawString(headerStr, m_headerFont, m_headerBrush, drawLocation);
            drawLocation.Y += m_headerFont.Height;
        }

        /// <summary>
        /// Draw the header line for a list summary lines.
        /// </summary>
        /// <param name="drawLocation"></param>
        /// <param name="e"></param>
        /// <param name="itemTypeHeader"></param>
        private void DrawSummaryHeader(ref Point drawLocation, PrintPageEventArgs e, string itemTypeHeader)
        {
            Point currPos = drawLocation;
            e.Graphics.DrawString(itemTypeHeader, m_summaryFont, m_summaryBrush, currPos);
            currPos.X = m_columnStarts[1];
            e.Graphics.DrawString(Properties.Resources.ReportStrBillableColHeader, m_summaryFont, m_summaryBrush, currPos);
            currPos.X = m_columnStarts[2];
            e.Graphics.DrawString(Properties.Resources.ReportStrNonBillableColHeader, m_summaryFont, m_summaryBrush, currPos);
            currPos.X = m_columnStarts[3];
            e.Graphics.DrawString(Properties.Resources.ReportStrTotalColHeader, m_summaryFont, m_summaryBrush, currPos);
            drawLocation.Y += m_summaryFont.Height;
        }

        /// <summary>
        /// Print a summary line (title, billable, non-billable, total)
        /// </summary>
        /// <param name="drawLocation"></param>
        /// <param name="e"></param>
        /// <param name="title"></param>
        /// <param name="billableTime"></param>
        /// <param name="nonBillableTime"></param>
        private void DrawSummaryLine(ref Point drawLocation, PrintPageEventArgs e, string title, TimeSpan billableTime, TimeSpan nonBillableTime)
        {
            // Billable
            int hours = (int)billableTime.TotalHours;
            string billableDurationStr = string.Format("{0}:{1}",
                hours.ToString("00"), billableTime.Minutes.ToString("00"));
            string billableDurationPercentStr = string.Format("{0}.{1}",
                hours.ToString(), ((int)(billableTime.Minutes * 100) / 60).ToString("00"));
            // Non-Billable
            hours = (int)nonBillableTime.TotalHours;
            string nonBillableDurationStr = string.Format("{0}:{1}",
                hours.ToString("00"), nonBillableTime.Minutes.ToString("00"));
            string nonBillableDurationPercentStr = string.Format("{0}.{1}",
                hours.ToString(), ((int)(nonBillableTime.Minutes * 100) / 60).ToString("00"));
            // Total
            TimeSpan totalTime = billableTime + nonBillableTime;
            hours = (int)totalTime.TotalHours;
            string totalDurationStr = string.Format("{0}:{1}",
                hours.ToString("00"), totalTime.Minutes.ToString("00"));
            string totalDurationPercentStr = string.Format("{0}.{1}",
                hours.ToString(), ((int)(totalTime.Minutes * 100) / 60).ToString("00"));

            Point currPos = drawLocation;
            e.Graphics.DrawString(title, m_summaryFont, m_summaryBrush, currPos);
            currPos.X = m_columnStarts[1];
            string tempStr = string.Format(Properties.Resources.ReportStrTimeFormat, billableDurationStr, billableDurationPercentStr);
            e.Graphics.DrawString(tempStr, m_summaryFont, m_summaryBrush, currPos);
            currPos.X = m_columnStarts[2];
            tempStr = string.Format(Properties.Resources.ReportStrTimeFormat, nonBillableDurationStr, nonBillableDurationPercentStr);
            e.Graphics.DrawString(tempStr, m_summaryFont, m_summaryBrush, currPos);
            currPos.X = m_columnStarts[3];
            tempStr = string.Format(Properties.Resources.ReportStrTimeFormat, totalDurationStr, totalDurationPercentStr);
            e.Graphics.DrawString(tempStr, m_summaryFont, m_summaryBrush, currPos);

            drawLocation.Y += m_summaryFont.Height;
        }

        /// <summary>
        /// Run through all tasks in given time period and calculate sums.
        /// </summary>
        private void CalculateSummaries()
        {
            if (m_showTasks)
            {
                m_taskSummaryData = new Dictionary<string, Dictionary<string, TimeSpan[]>>();
            }
            while (m_currentDateBeingPrinted <= m_endDate)
            {
                LogFileManager.LoadDataFileIfNotAlreadyLoaded(m_currentDateBeingPrinted);
                SortableBindingList<TimeLogEntry> logs = TimeLogEntry.LogsForDay(m_currentDateBeingPrinted);
                if (null != logs && 0 != logs.Count)
                {
                    foreach (TimeLogEntry log in logs)
                    {
                        if (m_showTasks)
                        {
                            // Update task level totals
                            Dictionary<string, TimeSpan[]> taskList = null;
                            // Get the task list for this customer (or create one)
                            if (m_taskSummaryData.ContainsKey(log.Customer.Name))
                            {
                                taskList = m_taskSummaryData[log.Customer.Name];
                            }
                            else
                            {
                                taskList = new Dictionary<string, TimeSpan[]>();
                                m_taskSummaryData[log.Customer.Name] = taskList;
                            }
                            // See if this task exists yet
                            TimeSpan[] taskTimes = null;
                            if (taskList.ContainsKey(log.Task.Name))
                            {
                                taskTimes = taskList[log.Task.Name];
                            }
                            else
                            {
                                taskTimes = new TimeSpan[2];
                            }
                            if (log.IsBillable) taskTimes[0] += log.Duration;
                            else taskTimes[1] += log.Duration;
                            taskList[log.Task.Name] = taskTimes;
                        }
                        // Update customer level totals
                        TimeSpan[] customerTotals = null;
                        if (m_customerSummaryData.ContainsKey(log.Customer.Name))
                        {
                            customerTotals = m_customerSummaryData[log.Customer.Name];
                        }
                        else
                        {
                            customerTotals = new TimeSpan[2];
                            m_customerSummaryData[log.Customer.Name] = customerTotals;
                        }
                        if (log.IsBillable) customerTotals[0] += log.Duration;
                        else customerTotals[1] += log.Duration;
                    }
                }
                // Move to next day
                m_currentDateBeingPrinted = m_currentDateBeingPrinted.AddDays(1);
            }
            // Sort customer names
            foreach (string s in m_customerSummaryData.Keys) m_customersSeenForDay.Add(s);
            m_customersSeenForDay.Sort();
            // Get ready to run through the list
            m_customerEnumerator = m_customersSeenForDay.GetEnumerator();
            m_needNextCustomer = true;
            m_needToShowSummary = true;
        }

        #endregion Methods

        #region Event Handlers

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            m_totalNumberOfPages = 0;

            m_detailFont = new Font("Arial", 10, FontStyle.Regular);
            m_detailBrush = Brushes.DarkGray;

            m_headerFont = new Font("Arial", 14, FontStyle.Bold);
            m_headerBrush = Brushes.Black;

            m_summaryFont = new Font("Arial", 12, FontStyle.Regular);
            m_summaryBrush = Brushes.Black;

            m_linePen = new Pen(m_summaryBrush, 2);

            // Reset totals
            m_overallBillableTime = new TimeSpan();
            m_overallNonBillableTime = new TimeSpan();
            m_needToShowOverallTotals = false;
            m_needToShowSummary = false;

            m_indexOfTaskInCurrentDay = 0;
            // Start at midnight to proper comparison with end date
            m_currentDateBeingPrinted = new DateTime(m_beginDate.Year, m_beginDate.Month, m_beginDate.Day);
            m_lastDayDisplayed = m_currentDateBeingPrinted.AddDays(-1); // set to before actual valid range
            m_customersSeenForDay = new List<string>();
            m_customerSummaryData = new Dictionary<string, TimeSpan[]>();

            if (IsSummaryReport) CalculateSummaries();

            base.OnBeginPrint(e);
        }

        protected override void OnEndPrint(PrintEventArgs e)
        {
            m_detailFont.Dispose();
            m_headerFont.Dispose();
            m_summaryFont.Dispose();
            m_linePen.Dispose();
            m_taskEnumerator.Dispose();
            m_customerEnumerator.Dispose();
            base.OnEndPrint(e);
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            ++m_totalNumberOfPages;
            if (null == m_columnStarts)
            {
                // Determine the longest customer name and base the first column width off that
                // Yes, this may include customer names which are not actually in the report but that is acceptable
                int maxLength = 0;
                foreach (Customer c in Customer.AllCustomers)
                {
                    SizeF size = e.Graphics.MeasureString(c.Name, m_summaryFont);
                    if (size.Width > maxLength) maxLength = (int)size.Width;
                }
                m_firstColumnWidth = maxLength;

                // Calculate where our columns will start
                m_columnStarts = new int[4];
                m_columnStarts[0] = e.MarginBounds.Left;
                m_columnStarts[1] = m_columnStarts[0] + m_firstColumnWidth;
                int columnWidth = (e.MarginBounds.Right - e.MarginBounds.Left - m_firstColumnWidth) / 3;
                for (int idx = 2; idx < 4; ++idx)
                {
                    m_columnStarts[idx] = m_columnStarts[idx - 1] + columnWidth;
                }
            }

            // Draw page header
            Point drawLocation = new Point(e.MarginBounds.Left, e.MarginBounds.Top);
            string titleFmt = null;
            if (m_isSummaryReport)
            {
                if (m_showTasks)
                {
                    titleFmt = Properties.Resources.ReportStrSummaryWithTasksTitle;
                }
                else
                {
                    titleFmt = Properties.Resources.ReportStrSummaryTitle;
                }
            }
            else
	        {
                if (m_showTasks)
                {
                    titleFmt = Properties.Resources.ReportStrActivityWithTasksTitle;
                }
                else
                {
                    titleFmt = Properties.Resources.ReportStrActivityTitle;
                }
            }
            string titleHeader = string.Format(titleFmt, m_beginDate.ToShortDateString(), m_endDate.ToShortDateString());
            e.Graphics.DrawString(titleHeader, m_headerFont, m_headerBrush, e.MarginBounds.Location);
            string pageHeader = string.Format(Properties.Resources.StrPageY, m_totalNumberOfPages.ToString());
            SizeF pageSize = e.Graphics.MeasureString(pageHeader, m_detailFont);
            Point pagePos = new Point((int)(e.MarginBounds.Right - pageSize.Width), e.MarginBounds.Top);
            e.Graphics.DrawString(pageHeader, m_detailFont, m_detailBrush, pagePos);
            drawLocation.Y += m_headerFont.Height + (m_headerFont.Height / 2);  // Give a 1/2 line gap before first line of report

            if (IsSummaryReport) PrintSummaryPage(e, drawLocation);
            else PrintActivityPage(e, drawLocation);
        }

        private bool m_needNextCustomer;

        /// <summary>
        /// Print a summary of all customer activity (optional showing summaries for each task)
        /// </summary>
        /// <param name="e"></param>
        private void PrintSummaryPage(PrintPageEventArgs e, Point drawLocation)
        {
            bool skipRestOfPage = m_needToShowOverallTotals;

            // Set up enumerators
            string customerName = null;
            Dictionary<string, TimeSpan[]> taskDetails = null;
            while (!m_needToShowOverallTotals && drawLocation.Y < e.MarginBounds.Bottom)
            {
                if (m_needNextCustomer)
                {
                    if (!m_customerEnumerator.MoveNext())
                    {
                        m_needNextCustomer = false;
                        m_needToShowOverallTotals = true; // Ready for overall totals
                        break; // finished with report
                    }
                    customerName = m_customerEnumerator.Current;
                    if (m_showTasks)
                    {
                        // That means we need a new task enumerator
                        if (m_taskSummaryData.ContainsKey(customerName))
                        {
                            taskDetails = m_taskSummaryData[customerName];
                            m_hasTaskEnumerator = false;
                            if (0 != taskDetails.Count)
                            {
                                m_taskEnumerator.Dispose();
                                m_taskEnumerator = taskDetails.GetEnumerator();
                                m_needToShowSummary = true;
                                m_hasTaskEnumerator = true;
                            }
                        }
                    }
                }
                customerName = m_customerEnumerator.Current;
                if (drawLocation.Y + 2 > e.MarginBounds.Top
                    && drawLocation.Y + 2 * m_headerFont.Height < e.MarginBounds.Bottom)
                {
                    drawLocation.Y += m_headerFont.Height; // Blank between customers
                    e.Graphics.DrawString(customerName, m_headerFont, m_headerBrush, drawLocation);
                    drawLocation.Y += m_headerFont.Height;
                }

                if (drawLocation.Y + m_summaryFont.Height < e.MarginBounds.Bottom && m_showTasks)
                {
                    if (m_hasTaskEnumerator)
                    {
                        if (m_needToShowSummary)
                        {
                            DrawSummaryHeader(ref drawLocation, e, Properties.Resources.ReportStrTaskColHeader);
                            m_needToShowSummary = false;
                        }
                        while (drawLocation.Y < e.MarginBounds.Bottom && m_taskEnumerator.MoveNext())
                        {
                            KeyValuePair<string, TimeSpan[]> pair = m_taskEnumerator.Current;
                            DrawSummaryLine(ref drawLocation, e, pair.Key, pair.Value[0], pair.Value[1]);
                        }
                    }
                }
                if (drawLocation.Y < e.MarginBounds.Bottom)
                {
                    TimeSpan[] timeSpans = m_customerSummaryData[customerName];
                    DrawSummaryLine(ref drawLocation, e, Properties.Resources.ReportStrTotalColHeader,
                        timeSpans[0], timeSpans[1]);
                    m_needNextCustomer = true;
                    m_overallBillableTime += timeSpans[0];
                    m_overallNonBillableTime += timeSpans[1];
                }
            }
            if (m_needToShowOverallTotals && drawLocation.Y + 2 * m_summaryFont.Height < e.MarginBounds.Bottom)
            {
                m_needToShowOverallTotals = false;

                // Draw a dividing line between details and summary (if showing details)
                Point startPoint = drawLocation;
                startPoint.Y += m_summaryFont.Height / 2;
                Point endPoint = new Point(e.MarginBounds.Right, drawLocation.Y + m_summaryFont.Height / 2);
                e.Graphics.DrawLine(m_linePen, startPoint, endPoint);
                drawLocation.Y += m_summaryFont.Height;
                
                DrawSummaryLine(ref drawLocation, e, Properties.Resources.ReportStrTotalColHeader,
                    m_overallBillableTime, m_overallNonBillableTime);
            }
            e.HasMorePages = m_needToShowOverallTotals || (drawLocation.Y + m_summaryFont.Height >= e.MarginBounds.Bottom);
        }

        /// <summary>
        /// Print Report showing daily activity (optionally showing all tasks)
        /// </summary>
        /// <param name="e"></param>
        private void PrintActivityPage(PrintPageEventArgs e, Point drawLocation)
        {
            bool skipRestOfPage = m_needToShowOverallTotals;
            while (!skipRestOfPage && drawLocation.Y < e.MarginBounds.Bottom && m_currentDateBeingPrinted <= m_endDate)
            {
                LogFileManager.LoadDataFileIfNotAlreadyLoaded(m_currentDateBeingPrinted);
                SortableBindingList<TimeLogEntry> logs = TimeLogEntry.LogsForDay(m_currentDateBeingPrinted);
                if (null == logs || 0 == logs.Count)
                {
                    if (!m_skipDaysWithoutTasks)
                    {
                        PrintDayHeaderLine(e, ref drawLocation);
                        e.Graphics.DrawString(Properties.Resources.ReportStrNoActivity, m_detailFont, m_detailBrush, drawLocation);
                        drawLocation.Y += m_detailFont.Height;
                        m_lastDayDisplayed = m_currentDateBeingPrinted;
                    }
                    // Move to next day
                    m_currentDateBeingPrinted = m_currentDateBeingPrinted.AddDays(1);
                    m_customersSeenForDay.Clear();
                    m_indexOfTaskInCurrentDay = 0;
                    continue;
                }
                // If not already past end and do not need to show summary from last page
                else if (!m_needToShowSummary && m_indexOfTaskInCurrentDay < logs.Count)
                {
                    TimeLogEntry log = logs[m_indexOfTaskInCurrentDay];
                    if (!m_customersSeenForDay.Contains(log.Customer.Name) )
                    {
                        m_customersSeenForDay.Add(log.Customer.Name);
                    }
                    // Add to overall totals
                    TimeSpan[] timeSpans = null;
                    if (!m_customerSummaryData.ContainsKey(log.Customer.Name))
                    {
                        timeSpans = new TimeSpan[2];
                        m_customerSummaryData[log.Customer.Name] = timeSpans;
                    }
                    else
                    {
                        timeSpans = m_customerSummaryData[log.Customer.Name];
                    }
                    if (log.IsBillable) timeSpans[BILLABLE_TIME_INDEX] += log.Duration;
                    else timeSpans[NON_BILLABLE_TIME_INDEX] += log.Duration;

                    if (m_lastDayDisplayed != m_currentDateBeingPrinted)
                    {
                        PrintDayHeaderLine(e, ref drawLocation);
                        m_lastDayDisplayed = m_currentDateBeingPrinted;
                    }
                    if (m_showTasks)
                    {
                        //		{0} for {1} from {2} to {3}. Duration{4}({5})  Billable:{6}	
                        string durationStr = string.Format("{0}:{1}",
                            log.Duration.Hours.ToString(), log.Duration.Minutes.ToString("00"));
                        string durationPercentStr = string.Format("{0}.{1}",
                            log.Duration.Hours.ToString(), ((int)(log.Duration.Minutes * 100) / 60).ToString("00") );
                        string billiableFlagStr = "";
                        if (!log.IsBillable) billiableFlagStr = Properties.Resources.ReportStrNotBillableReportTag;
                        string taskLine = string.Format(Properties.Resources.ReportStrActivityLine,
                            log.Task.Name,
                            log.Customer.Name,
                            log.StartTime.ToShortTimeString(),
                            log.StopTime.ToShortTimeString(),
                            durationStr,
                            durationPercentStr,
                            billiableFlagStr);
                        e.Graphics.DrawString(taskLine, m_detailFont, m_detailBrush, drawLocation);
                        drawLocation.Y += m_detailFont.Height;
                    }
                    // Move to next item (could bump to next day)
                    ++m_indexOfTaskInCurrentDay;
                }
                Point startPoint;
                Point endPoint;
                if (null != logs && m_indexOfTaskInCurrentDay >= logs.Count)
                {
                    if (0 < m_customersSeenForDay.Count)
                    {
                        m_needToShowSummary = true;
                        // See if there is enough room to display the summary (# customers + 1 for totals + 1 for horz line + 1 for header)
                        int summaryHeight = m_summaryFont.Height * (3 + m_customersSeenForDay.Count);
                        if (drawLocation.Y + summaryHeight > e.MarginBounds.Bottom)
                        {
                            // Must wait for next page
                            skipRestOfPage = true;
                            break;
                        }
                        m_needToShowSummary = false; // we are going to print it so clear flag
                        // OK, we have enough room for the summary

                        DrawSummaryHeader(ref drawLocation, e, Properties.Resources.ReportStrCustomerColHeader);

                        TimeSpan dailyTotalTime = new TimeSpan();
                        TimeSpan dailyBillableTime = new TimeSpan();
                        TimeSpan dailyNonBillableTime = new TimeSpan();
                        // Sort customers alphabetically
                        m_customersSeenForDay.Sort();
                        foreach (string customerName in m_customersSeenForDay)
                        {
                            TimeSpan billableTime = new TimeSpan();
                            TimeSpan nonBillableTime = new TimeSpan();
                            // Total up time for this customer
                            foreach (TimeLogEntry log in logs)
                            {
                                if (log.Customer.Name == customerName)
                                {
                                    if (log.IsBillable)
                                    {
                                        billableTime += log.Duration;
                                        dailyBillableTime += log.Duration;
                                    }
                                    else
                                    {
                                        nonBillableTime += log.Duration;
                                        dailyNonBillableTime += log.Duration;
                                    }
                                }
                            }
                            DrawSummaryLine(ref drawLocation, e, customerName, billableTime, nonBillableTime);
                        }
                        // Update the overall totals

                        m_overallBillableTime += dailyBillableTime;
                        m_overallNonBillableTime += dailyNonBillableTime;

                        DrawSummaryLine(ref drawLocation, e, Properties.Resources.ReportStrTotalTitle,
                            dailyBillableTime, dailyNonBillableTime);
                        // Draw a dividing line between details and summary (if showing details)
                        startPoint = drawLocation;
                        startPoint.Y += m_summaryFont.Height / 2;
                        endPoint = new Point(e.MarginBounds.Left + e.MarginBounds.Width / 2, drawLocation.Y + m_summaryFont.Height / 2);
                        e.Graphics.DrawLine(m_linePen, startPoint, endPoint);
                        drawLocation.Y += m_summaryFont.Height;
                    }
                    m_indexOfTaskInCurrentDay = 0;
                    m_currentDateBeingPrinted = m_currentDateBeingPrinted.AddDays(1);
                    m_customersSeenForDay.Clear();
                }
            }
            e.HasMorePages = skipRestOfPage || (m_currentDateBeingPrinted < m_endDate);
            // See if we need to display the grand totals
            if (m_needToShowOverallTotals || !e.HasMorePages)
            {
                m_needToShowOverallTotals = true;
                // See if there is enough room to display the summary (# customers + 1 for totals + 1 for horz line + 1 for header)
                int summaryHeight = m_summaryFont.Height * (3 + m_customerSummaryData.Count);
                if (drawLocation.Y + summaryHeight > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true; // Must put summary on last page
                }
                else
                {
                    m_needToShowOverallTotals = false; // we are going to print it so clear flag

                    if (drawLocation.Y > e.MarginBounds.Top)
                    {
                        drawLocation.Y += m_summaryFont.Height;
                    }
                    // draw a horz line all the way across
                    Point startPoint = drawLocation;
                    startPoint.Y += m_summaryFont.Height / 2;
                    Point endPoint = new Point(e.MarginBounds.Right, drawLocation.Y + m_summaryFont.Height / 2);
                    e.Graphics.DrawLine(m_linePen, startPoint, endPoint);
                    drawLocation.Y += m_summaryFont.Height;

                    DrawSummaryHeader(ref drawLocation, e, Properties.Resources.ReportStrCustomerColHeader);

                    // Sort customers alphabetically
                    List<string> allCustomerNames = new List<string>();
                    foreach (string key in m_customerSummaryData.Keys)
                    {
                        allCustomerNames.Add(key);
                    }
                    allCustomerNames.Sort();
                    // Now show each customer's totals
                    TimeSpan overallBillable = new TimeSpan();
                    TimeSpan overallNonBillable = new TimeSpan();
                    TimeSpan overallTotal = new TimeSpan();
                    foreach (string name in allCustomerNames)
                    {
                        TimeSpan[] timeSpans = m_customerSummaryData[name];

                        DrawSummaryLine(ref drawLocation, e, name,
                            timeSpans[BILLABLE_TIME_INDEX],
                            timeSpans[NON_BILLABLE_TIME_INDEX]);
                        // Add to global total
                        overallBillable += timeSpans[BILLABLE_TIME_INDEX];
                        overallNonBillable += timeSpans[NON_BILLABLE_TIME_INDEX];
                        overallTotal += timeSpans[BILLABLE_TIME_INDEX] + timeSpans[NON_BILLABLE_TIME_INDEX];
                    }
                    DrawSummaryLine(ref drawLocation, e, Properties.Resources.ReportStrTotalTitle,
                        overallBillable, overallNonBillable);

                    e.HasMorePages = false; // this is the end
                }
            }
        }

        #endregion Event Handlers

        #region Fields
        private Font m_detailFont;
        private Brush m_detailBrush;
        private Font m_headerFont;
        private Brush m_headerBrush;
        private Font m_summaryFont;
        private Brush m_summaryBrush;
        private Pen m_linePen;
        private DateTime m_currentDateBeingPrinted;
        private DateTime m_lastDayDisplayed;
        private int m_indexOfTaskInCurrentDay;
        private List<string> m_customersSeenForDay;
        private TimeSpan m_overallBillableTime;
        private TimeSpan m_overallNonBillableTime;
        private bool m_needToShowSummary;
        private bool m_needToShowOverallTotals;
        private Dictionary<string, Dictionary<string, TimeSpan[]>> m_taskSummaryData;
        private Dictionary<string, TimeSpan[]> m_customerSummaryData;
        private List<string>.Enumerator m_customerEnumerator;
        private bool m_hasTaskEnumerator;
        private Dictionary<string, TimeSpan[]>.Enumerator m_taskEnumerator;
        private int[] m_columnStarts;
        private int m_totalNumberOfPages;
        private int m_firstColumnWidth;
        // report settings
        private DateTime m_beginDate;
        private DateTime m_endDate;
        private bool m_showTasks;
        private bool m_isSummaryReport;
        private bool m_skipDaysWithoutTasks;
        #endregion Fields
    }
}
