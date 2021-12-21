namespace PersonalTimeTracker
{
    partial class TasksForm
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
            System.Windows.Forms.Label label1;
            this.m_moveToTopButton = new System.Windows.Forms.Button();
            this.m_moveToBottomButton = new System.Windows.Forms.Button();
            this.m_moveUpButton = new System.Windows.Forms.Button();
            this.m_moveDownButton = new System.Windows.Forms.Button();
            this.m_removeButton = new System.Windows.Forms.Button();
            this.m_okButton = new System.Windows.Forms.Button();
            this.m_cancelButton = new System.Windows.Forms.Button();
            this.m_dataGridView = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_addButton = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(13, 19);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(300, 13);
            label1.TabIndex = 0;
            label1.Text = "To add a task click on the blank line and enter the task name.";
            // 
            // m_moveToTopButton
            // 
            this.m_moveToTopButton.Location = new System.Drawing.Point(258, 41);
            this.m_moveToTopButton.Name = "m_moveToTopButton";
            this.m_moveToTopButton.Size = new System.Drawing.Size(75, 23);
            this.m_moveToTopButton.TabIndex = 2;
            this.m_moveToTopButton.Text = "Top";
            this.m_moveToTopButton.UseVisualStyleBackColor = true;
            this.m_moveToTopButton.Click += new System.EventHandler(this.m_moveToTopButton_Click);
            // 
            // m_moveToBottomButton
            // 
            this.m_moveToBottomButton.Location = new System.Drawing.Point(259, 245);
            this.m_moveToBottomButton.Name = "m_moveToBottomButton";
            this.m_moveToBottomButton.Size = new System.Drawing.Size(75, 23);
            this.m_moveToBottomButton.TabIndex = 5;
            this.m_moveToBottomButton.Text = "Bottom";
            this.m_moveToBottomButton.UseVisualStyleBackColor = true;
            this.m_moveToBottomButton.Click += new System.EventHandler(this.m_moveToBottomButton_Click);
            // 
            // m_moveUpButton
            // 
            this.m_moveUpButton.Location = new System.Drawing.Point(259, 125);
            this.m_moveUpButton.Name = "m_moveUpButton";
            this.m_moveUpButton.Size = new System.Drawing.Size(75, 23);
            this.m_moveUpButton.TabIndex = 3;
            this.m_moveUpButton.Text = "Up";
            this.m_moveUpButton.UseVisualStyleBackColor = true;
            this.m_moveUpButton.Click += new System.EventHandler(this.m_moveUpButton_Click);
            // 
            // m_moveDownButton
            // 
            this.m_moveDownButton.Location = new System.Drawing.Point(259, 155);
            this.m_moveDownButton.Name = "m_moveDownButton";
            this.m_moveDownButton.Size = new System.Drawing.Size(75, 23);
            this.m_moveDownButton.TabIndex = 4;
            this.m_moveDownButton.Text = "Down";
            this.m_moveDownButton.UseVisualStyleBackColor = true;
            this.m_moveDownButton.Click += new System.EventHandler(this.m_moveDownButton_Click);
            // 
            // m_removeButton
            // 
            this.m_removeButton.Location = new System.Drawing.Point(177, 274);
            this.m_removeButton.Name = "m_removeButton";
            this.m_removeButton.Size = new System.Drawing.Size(75, 23);
            this.m_removeButton.TabIndex = 7;
            this.m_removeButton.Text = "Remove";
            this.m_removeButton.UseVisualStyleBackColor = true;
            this.m_removeButton.Click += new System.EventHandler(this.m_removeButton_Click);
            // 
            // m_okButton
            // 
            this.m_okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_okButton.Location = new System.Drawing.Point(12, 303);
            this.m_okButton.Name = "m_okButton";
            this.m_okButton.Size = new System.Drawing.Size(75, 23);
            this.m_okButton.TabIndex = 8;
            this.m_okButton.Text = "OK";
            this.m_okButton.UseVisualStyleBackColor = true;
            this.m_okButton.Click += new System.EventHandler(this.m_okButton_Click);
            // 
            // m_cancelButton
            // 
            this.m_cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_cancelButton.Location = new System.Drawing.Point(259, 303);
            this.m_cancelButton.Name = "m_cancelButton";
            this.m_cancelButton.Size = new System.Drawing.Size(75, 23);
            this.m_cancelButton.TabIndex = 9;
            this.m_cancelButton.Text = "Cancel";
            this.m_cancelButton.UseVisualStyleBackColor = true;
            this.m_cancelButton.Click += new System.EventHandler(this.m_cancelButton_Click);
            // 
            // m_dataGridView
            // 
            this.m_dataGridView.AllowUserToResizeRows = false;
            this.m_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_dataGridView.ColumnHeadersVisible = false;
            this.m_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameColumn});
            this.m_dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_dataGridView.Location = new System.Drawing.Point(0, 0);
            this.m_dataGridView.Name = "m_dataGridView";
            this.m_dataGridView.RowHeadersVisible = false;
            this.m_dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.m_dataGridView.Size = new System.Drawing.Size(240, 227);
            this.m_dataGridView.TabIndex = 0;
            this.m_dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.m_dataGridView_CellValueChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.m_dataGridView);
            this.panel1.Location = new System.Drawing.Point(12, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 227);
            this.panel1.TabIndex = 1;
            // 
            // m_addButton
            // 
            this.m_addButton.Location = new System.Drawing.Point(24, 274);
            this.m_addButton.Name = "m_addButton";
            this.m_addButton.Size = new System.Drawing.Size(75, 23);
            this.m_addButton.TabIndex = 6;
            this.m_addButton.Text = "Add";
            this.m_addButton.UseVisualStyleBackColor = true;
            this.m_addButton.Click += new System.EventHandler(this.m_addButton_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // NameColumn
            // 
            this.NameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NameColumn.DataPropertyName = "Name";
            this.NameColumn.HeaderText = "Name";
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // TasksForm
            // 
            this.AcceptButton = this.m_okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.m_cancelButton;
            this.ClientSize = new System.Drawing.Size(342, 338);
            this.Controls.Add(label1);
            this.Controls.Add(this.m_addButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.m_cancelButton);
            this.Controls.Add(this.m_okButton);
            this.Controls.Add(this.m_removeButton);
            this.Controls.Add(this.m_moveDownButton);
            this.Controls.Add(this.m_moveUpButton);
            this.Controls.Add(this.m_moveToBottomButton);
            this.Controls.Add(this.m_moveToTopButton);
            this.Name = "TasksForm";
            this.Text = "Tasks";
            ((System.ComponentModel.ISupportInitialize)(this.m_dataGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button m_moveToTopButton;
        private System.Windows.Forms.Button m_moveToBottomButton;
        private System.Windows.Forms.Button m_moveUpButton;
        private System.Windows.Forms.Button m_moveDownButton;
        private System.Windows.Forms.Button m_removeButton;
        private System.Windows.Forms.Button m_okButton;
        private System.Windows.Forms.Button m_cancelButton;
        private System.Windows.Forms.DataGridView m_dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_addButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    }
}