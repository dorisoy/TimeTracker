using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PersonalTimeTracker
{
    public partial class TasksForm : Form
    {
        public TasksForm()
        {
            m_isDirty = false;

            // Make a temp copy of the task names
            m_tasks = new BindingList<Task>();
            m_deletedTasks = new List<Task>();
            m_newToOldMap = new Dictionary<Task, Task>();
            foreach (Task task in Task.AllTasks)
            {
                Task newCust = task.Clone() as Task;
                m_newToOldMap[newCust] = task;
                m_tasks.Add(newCust);
            }

            InitializeComponent();

            m_dataGridView.AutoGenerateColumns = false;
            m_dataGridView.AutoSize = true;
            m_dataGridView.DataSource = m_tasks;
        }

        private void m_moveToTopButton_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = m_dataGridView.CurrentRow;
            if (null != row)
            {
                if (row.Index > 0)
                {
                    row.Selected = false;
                    Task task = m_tasks[row.Index];
                    task.DoNotSave = false;
                    m_tasks.RemoveAt(row.Index);
                    int newIndex = 0;
                    m_tasks.Insert(newIndex, task);
                    m_dataGridView.Rows[newIndex].Selected = true;
                    m_dataGridView.CurrentCell = m_dataGridView.Rows[newIndex].Cells[0];
                    // set dirty flag
                    m_isDirty = true;
                }
            }
        }

        private void m_moveUpButton_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = m_dataGridView.CurrentRow;
            if (null != row)
            {
                if (row.Index > 0)
                {
                    Task task = m_tasks[row.Index];
                    task.DoNotSave = false;
                    int newIndex = row.Index - 1;
                    row.Selected = false;
                    m_tasks.RemoveAt(row.Index);
                    m_tasks.Insert(newIndex, task);
                    m_dataGridView.Rows[newIndex].Selected = true;
                    m_dataGridView.CurrentCell = m_dataGridView.Rows[newIndex].Cells[0];
                    // set dirty flag
                    m_isDirty = true;
                }
            }
        }

        private void m_moveDownButton_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = m_dataGridView.CurrentRow;
            if (null != row)
            {
                if (row.Index < m_tasks.Count - 1)
                {
                    int newIndex = row.Index + 1;
                    row.Selected = false;
                    Task task = m_tasks[row.Index];
                    task.DoNotSave = false;
                    m_tasks.RemoveAt(row.Index);
                    m_tasks.Insert(newIndex, task);
                    m_dataGridView.Rows[newIndex].Selected = true;
                    m_dataGridView.CurrentCell = m_dataGridView.Rows[newIndex].Cells[0];
                    // set dirty flag
                    m_isDirty = true;
                }
            }
        }

        private void m_moveToBottomButton_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = m_dataGridView.CurrentRow;
            if (null != row)
            {
                if (row.Index < m_tasks.Count - 1)
                {
                    row.Selected = false;
                    Task task = m_tasks[row.Index];
                    task.DoNotSave = false;
                    m_tasks.RemoveAt(row.Index);
                    m_tasks.Add(task);
                    int newIndex = m_tasks.Count - 1;
                    m_dataGridView.Rows[newIndex].Selected = true;
                    m_dataGridView.CurrentCell = m_dataGridView.Rows[newIndex].Cells[0];
                    // set dirty flag
                    m_isDirty = true;
                }
            }
        }

        private void m_removeButton_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = m_dataGridView.CurrentRow;
            if (null != row)
            {
                Task task = m_tasks[row.Index];
                // See if this task has associated time logs
                if (TimeLogEntry.DoEntriesExistForTask(task))
                {
                    string msg = string.Format("Delete all logs for the task '{0}'.", task.Name);
                    if (DialogResult.No == MessageBox.Show(this, "Task Has Log Entries", msg,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        return;
                    }
                }
                row.Selected = false;
                m_newToOldMap.Remove(task);
                m_deletedTasks.Add(task);
                m_tasks.RemoveAt(row.Index);
                // set dirty flag
                m_isDirty = true;
            }
        }

        private void m_addButton_Click(object sender, EventArgs e)
        {
            Task task = new Task();
            task.Name = "New Task";
            m_newToOldMap[task] = null;
            m_tasks.Add(task);
            m_dataGridView.Rows[m_tasks.Count - 1].Selected = true;
            m_dataGridView.CurrentCell = m_dataGridView.Rows[m_tasks.Count - 1].Cells[0];
            // set dirty flag
            m_isDirty = true;
        }

        private void m_dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // set dirty flag
                m_isDirty = true;
            }
        }

        private void m_okButton_Click(object sender, EventArgs e)
        {
            // If nothing changed then pretend Cancel was pressed
            if (!m_isDirty) DialogResult = DialogResult.Cancel;
        }

        private void m_cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            if (m_isDirty
                    && DialogResult.No == MessageBox.Show(this, "You have unsaved changes.  Abandon these changes?", "Save Changes?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) )
            {
                DialogResult = DialogResult.None;
            }
        }

        #region Properties

        public BindingList<Task> Tasks
        {
            get { return m_tasks; }
            set { m_tasks = value; }
        }

        public List<Task> DeletedTasks
        {
            get { return m_deletedTasks; }
            set { m_deletedTasks = value; }
        }

        public Dictionary<Task, Task> NewToOldMap
        {
            get { return m_newToOldMap; }
            set { m_newToOldMap = value; }
        }
        #endregion Properties

        #region Fields

        private BindingList<Task> m_tasks;
        private List<Task> m_deletedTasks;
        private Dictionary<Task, Task> m_newToOldMap;
        private bool m_isDirty;

        #endregion Fields
    }
}
