namespace PersonalTimeTracker
{
    partial class ReportsForm
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
            System.Windows.Forms.ToolStripLabel toolStripLabel1;
            System.Windows.Forms.ToolStripLabel toolStripLabel2;
            System.Windows.Forms.ToolStripLabel toolStripLabel3;
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.m_printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.m_zoomToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.m_reportTypeToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.m_startToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.m_endToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.m_updateReportToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.m_skipEmptyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.m_prevPageToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.m_nextPageToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.m_printPreviewControl = new System.Windows.Forms.PrintPreviewControl();
            this.m_printDocument = new PersonalTimeTracker.ReportPrintDocument();
            this.m_pageToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new System.Drawing.Size(33, 22);
            toolStripLabel1.Text = "Begin";
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new System.Drawing.Size(25, 22);
            toolStripLabel2.Text = "End";
            // 
            // toolStripLabel3
            // 
            toolStripLabel3.Name = "toolStripLabel3";
            toolStripLabel3.Size = new System.Drawing.Size(40, 22);
            toolStripLabel3.Text = "Report";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_printToolStripButton,
            this.m_zoomToolStripComboBox,
            toolStripLabel3,
            this.m_reportTypeToolStripComboBox,
            toolStripLabel1,
            this.m_startToolStripTextBox,
            toolStripLabel2,
            this.m_endToolStripTextBox,
            this.toolStripSeparator1,
            this.m_updateReportToolStripButton,
            this.m_skipEmptyToolStripButton,
            this.m_pageToolStripLabel,
            this.m_prevPageToolStripButton,
            this.m_nextPageToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(889, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // m_printToolStripButton
            // 
            this.m_printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_printToolStripButton.Image = global::PersonalTimeTracker.Properties.Resources.PrintBtn;
            this.m_printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_printToolStripButton.Name = "m_printToolStripButton";
            this.m_printToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.m_printToolStripButton.Text = "Print";
            this.m_printToolStripButton.Click += new System.EventHandler(this.m_printToolStripButton_Click);
            // 
            // m_zoomToolStripComboBox
            // 
            this.m_zoomToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_zoomToolStripComboBox.DropDownWidth = 75;
            this.m_zoomToolStripComboBox.Items.AddRange(new object[] {
            "200%",
            "100%",
            "75%",
            "50%",
            "25%"});
            this.m_zoomToolStripComboBox.Name = "m_zoomToolStripComboBox";
            this.m_zoomToolStripComboBox.Size = new System.Drawing.Size(75, 25);
            this.m_zoomToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.m_zoomToolStripComboBox_SelectedIndexChanged);
            // 
            // m_reportTypeToolStripComboBox
            // 
            this.m_reportTypeToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_reportTypeToolStripComboBox.Name = "m_reportTypeToolStripComboBox";
            this.m_reportTypeToolStripComboBox.Size = new System.Drawing.Size(121, 25);
            // 
            // m_startToolStripTextBox
            // 
            this.m_startToolStripTextBox.Name = "m_startToolStripTextBox";
            this.m_startToolStripTextBox.Size = new System.Drawing.Size(100, 25);
            // 
            // m_endToolStripTextBox
            // 
            this.m_endToolStripTextBox.Name = "m_endToolStripTextBox";
            this.m_endToolStripTextBox.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // m_updateReportToolStripButton
            // 
            this.m_updateReportToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_updateReportToolStripButton.Image = global::PersonalTimeTracker.Properties.Resources.RefreshPageBtn;
            this.m_updateReportToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_updateReportToolStripButton.Name = "m_updateReportToolStripButton";
            this.m_updateReportToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.m_updateReportToolStripButton.Text = "Generate";
            this.m_updateReportToolStripButton.ToolTipText = "Generate report";
            this.m_updateReportToolStripButton.Click += new System.EventHandler(this.m_updateReportToolStripButton_Click);
            // 
            // m_skipEmptyToolStripButton
            // 
            this.m_skipEmptyToolStripButton.Checked = true;
            this.m_skipEmptyToolStripButton.CheckOnClick = true;
            this.m_skipEmptyToolStripButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.m_skipEmptyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_skipEmptyToolStripButton.Image = global::PersonalTimeTracker.Properties.Resources.SkipEmptyBtn;
            this.m_skipEmptyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_skipEmptyToolStripButton.Name = "m_skipEmptyToolStripButton";
            this.m_skipEmptyToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.m_skipEmptyToolStripButton.Text = "Skip days with no tasks";
            // 
            // m_prevPageToolStripButton
            // 
            this.m_prevPageToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_prevPageToolStripButton.Image = global::PersonalTimeTracker.Properties.Resources.LeftArrowBtn;
            this.m_prevPageToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_prevPageToolStripButton.Name = "m_prevPageToolStripButton";
            this.m_prevPageToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.m_prevPageToolStripButton.Text = "Previous page";
            this.m_prevPageToolStripButton.Click += new System.EventHandler(this.m_prevPageToolStripButton_Click);
            // 
            // m_nextPageToolStripButton
            // 
            this.m_nextPageToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.m_nextPageToolStripButton.Image = global::PersonalTimeTracker.Properties.Resources.RightArrowBtn;
            this.m_nextPageToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.m_nextPageToolStripButton.Name = "m_nextPageToolStripButton";
            this.m_nextPageToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.m_nextPageToolStripButton.Text = "Next page";
            this.m_nextPageToolStripButton.Click += new System.EventHandler(this.m_nextPageToolStripButton_Click);
            // 
            // m_printPreviewControl
            // 
            this.m_printPreviewControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_printPreviewControl.AutoZoom = false;
            this.m_printPreviewControl.Document = this.m_printDocument;
            this.m_printPreviewControl.Location = new System.Drawing.Point(12, 45);
            this.m_printPreviewControl.Name = "m_printPreviewControl";
            this.m_printPreviewControl.Size = new System.Drawing.Size(865, 399);
            this.m_printPreviewControl.TabIndex = 0;
            this.m_printPreviewControl.Zoom = 1;
            // 
            // m_printDocument
            // 
            this.m_printDocument.BeginDate = new System.DateTime(((long)(0)));
            this.m_printDocument.EndDate = new System.DateTime(((long)(0)));
            this.m_printDocument.IsSummaryReport = false;
            this.m_printDocument.ShowTasks = false;
            this.m_printDocument.SkipDaysWithoutTasks = false;
            this.m_printDocument.TotalNumberOfPages = 0;
            // 
            // m_pageToolStripLabel
            // 
            this.m_pageToolStripLabel.Name = "m_pageToolStripLabel";
            this.m_pageToolStripLabel.Size = new System.Drawing.Size(35, 22);
            this.m_pageToolStripLabel.Text = "1 of 1";
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 456);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.m_printPreviewControl);
            this.Name = "ReportsForm";
            this.Text = "ReportsForm";
            this.Load += new System.EventHandler(this.ReportsForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReportsForm_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ReportPrintDocument m_printDocument;
        private System.Windows.Forms.PrintPreviewControl m_printPreviewControl;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton m_printToolStripButton;
        private System.Windows.Forms.ToolStripComboBox m_zoomToolStripComboBox;
        private System.Windows.Forms.ToolStripComboBox m_reportTypeToolStripComboBox;
        private System.Windows.Forms.ToolStripTextBox m_startToolStripTextBox;
        private System.Windows.Forms.ToolStripTextBox m_endToolStripTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton m_updateReportToolStripButton;
        private System.Windows.Forms.ToolStripButton m_skipEmptyToolStripButton;
        private System.Windows.Forms.ToolStripButton m_prevPageToolStripButton;
        private System.Windows.Forms.ToolStripButton m_nextPageToolStripButton;
        private System.Windows.Forms.ToolStripLabel m_pageToolStripLabel;
    }
}