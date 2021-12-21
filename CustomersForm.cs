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
    public partial class CustomersForm : Form
    {
        public CustomersForm()
        {
            m_isDirty = false;

            // Make a temp copy of the customer names
            m_customers = new BindingList<Customer>();
            m_deletedCustomers = new List<Customer>();
            m_newToOldMap = new Dictionary<Customer, Customer>();
            foreach (Customer customer in Customer.AllCustomers)
            {
                Customer newCust = customer.Clone() as Customer;
                m_newToOldMap[newCust] = customer;
                m_customers.Add(newCust);
            }

            InitializeComponent();

            m_dataGridView.AutoGenerateColumns = false;
            m_dataGridView.AutoSize = true;
            m_dataGridView.DataSource = m_customers;
        }

        private void m_moveToTopButton_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = m_dataGridView.CurrentRow;
            if (null != row)
            {
                if (row.Index > 0)
                {
                    row.Selected = false;
                    Customer customer = m_customers[row.Index];
                    customer.DoNotSave = false;
                    m_customers.RemoveAt(row.Index);
                    int newIndex = 0;
                    m_customers.Insert(newIndex, customer);
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
                    Customer customer = m_customers[row.Index];
                    customer.DoNotSave = false;
                    int newIndex = row.Index - 1;
                    row.Selected = false;
                    m_customers.RemoveAt(row.Index);
                    m_customers.Insert(newIndex, customer);
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
                if (row.Index < m_customers.Count - 1)
                {
                    int newIndex = row.Index + 1;
                    row.Selected = false;
                    Customer customer = m_customers[row.Index];
                    customer.DoNotSave = false;
                    m_customers.RemoveAt(row.Index);
                    m_customers.Insert(newIndex, customer);
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
                if (row.Index < m_customers.Count - 1)
                {
                    row.Selected = false;
                    Customer customer = m_customers[row.Index];
                    customer.DoNotSave = false;
                    m_customers.RemoveAt(row.Index);
                    m_customers.Add(customer);
                    int newIndex = m_customers.Count - 1;
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
                Customer customer = m_customers[row.Index];
                // See if this customer has associated time logs
                if (TimeLogEntry.DoEntriesExistForCustomer(customer))
                {
                    string msg = string.Format("Delete all logs for the customer '{0}'.", customer.Name);
                    if (DialogResult.No == MessageBox.Show(this, "Customer Has Log Entries", msg,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        return;
                    }
                }
                row.Selected = false;
                m_newToOldMap.Remove(customer);
                m_deletedCustomers.Add(customer);
                m_customers.RemoveAt(row.Index);
                // set dirty flag
                m_isDirty = true;
            }
        }

        private void m_addButton_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Name = "New Customer";
            m_newToOldMap[customer] = null;
            m_customers.Add(customer);
            m_dataGridView.Rows[m_customers.Count - 1].Selected = true;
            m_dataGridView.CurrentCell = m_dataGridView.Rows[m_customers.Count - 1].Cells[0];
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

        public BindingList<Customer> Customers
        {
            get { return m_customers; }
            set { m_customers = value; }
        }

        public List<Customer> DeletedCustomers
        {
            get { return m_deletedCustomers; }
            set { m_deletedCustomers = value; }
        }

        public Dictionary<Customer, Customer> NewToOldMap
        {
            get { return m_newToOldMap; }
            set { m_newToOldMap = value; }
        }
        #endregion Properties

        #region Fields

        private BindingList<Customer> m_customers;
        private List<Customer> m_deletedCustomers;
        private Dictionary<Customer, Customer> m_newToOldMap;
        private bool m_isDirty;

        #endregion Fields
    }
}
