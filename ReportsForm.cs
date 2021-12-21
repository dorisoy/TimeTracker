using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PersonalTimeTracker
{
    /// <summary>
    /// Displays the reports and report controls.
    /// </summary>
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                // Fill Report Types combo
                m_reportTypeToolStripComboBox.Items.Add(Properties.Resources.ReportStrActivity);
                m_reportTypeToolStripComboBox.Items.Add(Properties.Resources.ReportStrActivityWithTasks);
                m_reportTypeToolStripComboBox.Items.Add(Properties.Resources.ReportStrSummary);
                m_reportTypeToolStripComboBox.Items.Add(Properties.Resources.ReportStrSummaryWithTasks);

                m_zoomToolStripComboBox.SelectedIndex = 1;

                // Initialize to default report (weekly w/details)
                DateTime now = DateTime.Now;
                DateTime startOfWeek = now.AddDays(-1 * (int)now.DayOfWeek);
                DateTime endOfWeek = now.AddDays(6 - (int)now.DayOfWeek);
                m_startToolStripTextBox.Text = startOfWeek.ToShortDateString();
                m_endToolStripTextBox.Text = endOfWeek.ToShortDateString();
                m_reportTypeToolStripComboBox.SelectedIndex = 1;

                GenerateReport();
                m_printDocument.EndPrint += new PrintEventHandler(m_printDocument_EndPrint);
            }
        }

        #region Methods

        /// <summary>
        /// Produce the report.
        /// </summary>
        private void GenerateReport()
        {
            // Set report properties
            DateTime date;
            if (!DateTime.TryParse(m_startToolStripTextBox.Text, out date))
            {
                MessageBox.Show(this, Properties.Resources.ErrStrBadDateFormat, Properties.Resources.ErrStrBadFormatTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            m_printDocument.BeginDate = date;
            if (!DateTime.TryParse(m_endToolStripTextBox.Text, out date))
            {
                MessageBox.Show(this, Properties.Resources.ErrStrBadDateFormat, Properties.Resources.ErrStrBadFormatTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            m_printDocument.EndDate = date;
            m_printDocument.SkipDaysWithoutTasks = m_skipEmptyToolStripButton.Checked;
            bool isSummaryReport = false;
            bool showTasks = false;
            switch (m_reportTypeToolStripComboBox.SelectedIndex)
            {
                case 0: // Activity no details
                    isSummaryReport = false;
                    showTasks = false;
                    break;
                case 1: // Activity with details
                    isSummaryReport = false;
                    showTasks = true;
                    break;
                case 2: // Summary no details
                    isSummaryReport = true;
                    showTasks = false;
                    break;
                case 3: // Summary with details
                    isSummaryReport = true;
                    showTasks = true;
                    break;
            }
            m_printPreviewControl.StartPage = 0;
            m_printDocument.IsSummaryReport = isSummaryReport;
            m_printDocument.ShowTasks = showTasks;
            m_printPreviewControl.InvalidatePreview();
        }

        /// <summary>
        /// Update page Y of Z label.
        /// </summary>
        private void UpdatePageLabel()
        {
            m_pageToolStripLabel.Text = string.Format(Properties.Resources.StrPageYOfZ,
                (m_printPreviewControl.StartPage + 1).ToString(),
                m_printDocument.TotalNumberOfPages.ToString());
            // Update next/prev buttons at the same time
            m_prevPageToolStripButton.Enabled = (m_printPreviewControl.StartPage > 0);
            m_nextPageToolStripButton.Enabled = (m_printPreviewControl.StartPage < m_printDocument.TotalNumberOfPages - 1);
        }

        #endregion Methods

        #region Event Handlers

        private void m_updateReportToolStripButton_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void m_zoomToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string zoomStr = m_zoomToolStripComboBox.Text;
            zoomStr = zoomStr.Replace('%', ' ');
            double zoomFactor;
            if (double.TryParse(zoomStr, out zoomFactor))
            {
                m_printPreviewControl.Zoom = zoomFactor / 100;
            }
        }

        private void ReportsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.ReportsIsMaximized = (WindowState == FormWindowState.Maximized);
            // Must restore to get actual position
            WindowState = FormWindowState.Normal;
            Properties.Settings.Default.ReportsPos = DesktopBounds;
            Properties.Settings.Default.Save();
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            UpdatePageLabel();

            // Determine total display area
            Rectangle totalResolution = new Rectangle();
            foreach (Screen screen in Screen.AllScreens)
            {
                totalResolution = Rectangle.Union(totalResolution, screen.Bounds);
            }
            if (!Properties.Settings.Default.ReportsPos.IsEmpty
                && totalResolution.Contains(Properties.Settings.Default.ReportsPos))
            {
                DesktopBounds = Properties.Settings.Default.ReportsPos;
                if (Properties.Settings.Default.ReportsIsMaximized) WindowState = FormWindowState.Maximized;
            }
        }

        private void m_prevPageToolStripButton_Click(object sender, EventArgs e)
        {
            if (0 < m_printPreviewControl.StartPage)
            {
                m_printPreviewControl.StartPage -= 1;
                m_printPreviewControl.Invalidate();
                UpdatePageLabel();
            }
        }

        private void m_nextPageToolStripButton_Click(object sender, EventArgs e)
        {
            if (m_printPreviewControl.StartPage < m_printDocument.TotalNumberOfPages - 1)
            {
                m_printPreviewControl.StartPage += 1;
                m_printPreviewControl.Invalidate();
                UpdatePageLabel();
            }
        }

        void m_printDocument_EndPrint(object sender, PrintEventArgs e)
        {
            UpdatePageLabel();
        }

        private void m_printToolStripButton_Click(object sender, EventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();
            printDlg.Document = m_printDocument;
            printDlg.AllowCurrentPage = false;
            printDlg.AllowSelection = false;
            printDlg.AllowSomePages = false;
            if (DialogResult.OK == printDlg.ShowDialog())
            {
                m_printDocument.PrinterSettings = printDlg.PrinterSettings;
                m_printDocument.Print();
            }
        }

        #endregion Event Handlers
    }
}
