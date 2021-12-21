using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml;

namespace PersonalTimeTracker
{
    public class Customer : ICloneable
    {
        #region Static Members

        static private List<Customer> s_AllCustomers = new List<Customer>();

        static public List<Customer> AllCustomers
        {
            get { return s_AllCustomers; }
        }

        /// <summary>
        /// Find a customer given its name.
        /// </summary>
        /// <param name="customerName">The name of the customer to find.</param>
        /// <returns></returns>
        static public Customer FindCustomer(string customerName)
        {
            return AllCustomers.Find(delegate(Customer t) { return 0 == string.CompareOrdinal(customerName, t.Name); });
        }

        /// <summary>
        /// Read in all customers from Xml file.
        /// </summary>
        /// <param name="dataXml"></param>
        /// <remarks>This may involve merging in customers from older month files.</remarks>
        static public void ReadCustomers(XmlDocument dataXml)
        {
            // If we are reading in an older month then these customers should not be saved
            bool isTemporaryCustomer = (AllCustomers.Count > 0);
            // Read customers
            XmlNodeList customerNodes = dataXml.SelectNodes("/ptt_data/customers/customer");
            foreach (XmlNode node in customerNodes)
            {
                Customer cust = new Customer(node);
                cust.DoNotSave = isTemporaryCustomer;
                if (null == FindCustomer(cust.Name))
                {
                    AllCustomers.Add(cust);
                }
            }
        }

        /// <summary>
        /// Save all customers to the Xml file
        /// </summary>
        /// <param name="rootNode"></param>
        public static void SaveCustomers(XmlNode rootNode)
        {
            XmlNode customersNode = rootNode.OwnerDocument.CreateNode(XmlNodeType.Element, "customers", rootNode.NamespaceURI);
            rootNode.AppendChild(customersNode);
            foreach (Customer customer in AllCustomers)
            {
                if (!customer.DoNotSave)
                {
                    customer.Save(customersNode);
                }
            }
        }

        /// <summary>
        /// Update customer list to match list just updated by user.
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="newToOldMap"></param>
        static public void SyncChanges(BindingList<Customer> customers,
            List<Customer> deletedCustomers,
            Dictionary<Customer, Customer> newToOldMap)
        {
            s_AllCustomers = new List<Customer>(); // First just replace old list to keep the ordering specified
            foreach (Customer c in customers) s_AllCustomers.Add(c);

            // Now replace items in new list with old objects if they still exist.
            // This is to maintain reference links in TimeLogEntries
            for (int idx = 0; idx < s_AllCustomers.Count; idx++)
            {
                Customer newCustomer = s_AllCustomers[idx];
                if (newToOldMap.ContainsKey(newCustomer))
                {
                    Customer oldCustomer = newToOldMap[newCustomer];
                    if (null != oldCustomer)
                    {
                        oldCustomer.CopySettingsFrom(newCustomer);
                        // Put old customer object back in list
                        s_AllCustomers[idx] = oldCustomer;
                    }
                }
            }
            foreach (Customer customer in deletedCustomers)
            {
                // Clean up all log entries for deleted customers
                TimeLogEntry.DeleteEntriesForCustomer(customer);
            }
        }

        private void CopySettingsFrom(Customer newCustomer)
        {
            Name = newCustomer.Name;
            DoNotSave = newCustomer.DoNotSave;
        }

        #endregion Static members

        public Customer()
        {
        }

        public Customer(XmlNode node) : this()
        {
            m_name = node.InnerText;
        }

        #region Properties

        // Used for binding
        public Customer Self
        {
            get { return this; }
        }

        public string Name
        {
            get { return m_name; }
            set
            {
                m_name = value;
                // If modified then save it
                DoNotSave = false;
            }
        }

        public bool DoNotSave
        {
            get { return m_doNotSave; }
            set { m_doNotSave = value; }
        }

        #endregion Properties

        #region Methods

        public void Save(XmlNode customersNode)
        {
            XmlNode node = customersNode.OwnerDocument.CreateNode(XmlNodeType.Element, "customer", customersNode.NamespaceURI);
            node.InnerText = Name;
            customersNode.AppendChild(node);
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion Methods

        #region ICloneable Members

        public object Clone()
        {
            Customer newCust = new Customer();
            newCust.Name = Name;
            return newCust;
        }

        #endregion ICloneable Members

        #region Fields

        private string m_name;
        private bool m_doNotSave;

        #endregion Fields
    }
}
