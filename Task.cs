using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.ComponentModel;

namespace PersonalTimeTracker
{
    /// <summary>
    /// Represents a task that can be performed for a <see cref="Customer"/>.
    /// </summary>
    public class Task
    {
        #region Static Members

        static private List<Task> s_AllTasks = new List<Task>();

        static public List<Task> AllTasks
        {
            get { return s_AllTasks; }
        }

        /// <summary>
        /// Find a task given its name.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <returns></returns>
        static public Task FindTask(string name)
        {
            return AllTasks.Find(delegate(Task t) { return 0 == string.CompareOrdinal(name, t.Name); } );
        }

        /// <summary>
        /// Read all tasks from Xml file.
        /// </summary>
        /// <param name="dataXml"></param>
        static public void ReadTasks(XmlDocument dataXml)
        {
            // Don't save these if they are not for the current month
            bool doNotSave = (0 != AllTasks.Count);
            // Read tasks
            XmlNodeList taskNodes = dataXml.SelectNodes("/ptt_data/tasks/task");
            foreach (XmlNode node in taskNodes)
            {
                Task task = new Task(node);
                if (null == FindTask(task.Name))
                {
                    task.DoNotSave = doNotSave;
                    AllTasks.Add(task);
                }
            }
        }

        /// <summary>
        /// Saves all tasks to the Xml file.
        /// </summary>
        /// <param name="rootNode"></param>
        static public void SaveTasks(XmlNode rootNode)
        {
            XmlNode tasksNode = rootNode.OwnerDocument.CreateNode(XmlNodeType.Element, "tasks", rootNode.NamespaceURI);
            rootNode.AppendChild(tasksNode);
            foreach (Task task in AllTasks)
            {
                task.Save(tasksNode);
            }
        }

        /// <summary>
        /// Update task list to match list just updated by user.
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="newToOldMap"></param>
        static public void SyncChanges(BindingList<Task> tasks,
            List<Task> deletedTasks,
            Dictionary<Task, Task> newToOldMap)
        {
            s_AllTasks = new List<Task>(); // First just replace old list to keep the ordering specified
            foreach (Task c in tasks) s_AllTasks.Add(c);

            // Now replace items in new list with old objects if they still exist.
            // This is to maintain reference links in TimeLogEntries
            for (int idx = 0; idx < s_AllTasks.Count; idx++)
            {
                Task newTask = s_AllTasks[idx];
                if (newToOldMap.ContainsKey(newTask))
                {
                    Task oldTask = newToOldMap[newTask];
                    if (null != oldTask)
                    {
                        oldTask.CopySettingsFrom(newTask);
                        // Put old task object back in list
                        s_AllTasks[idx] = oldTask;
                    }
                }
            }
            foreach (Task task in deletedTasks)
            {
                // Clean up all log entries for deleted tasks
                TimeLogEntry.DeleteEntriesForTask(task);
            }
        }

        #endregion Static members

        #region ctors

        public Task()
        {
        }

        public Task(XmlNode node)
        {
            m_name = node.InnerText;
        }

        #endregion ctors

        #region Properties

        // Used for binding
        public Task Self
        {
            get { return this; }
        }

        public bool DoNotSave
        {
            get { return m_doNotSave; }
            set { m_doNotSave = value; }
        }

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        #endregion Properties

        #region Methods

        public void CopySettingsFrom(Task newTask)
        {
            m_name = newTask.Name;
            m_doNotSave = newTask.DoNotSave;
        }

        public void Save(XmlNode tasksNode)
        {
            XmlNode node = tasksNode.OwnerDocument.CreateNode(XmlNodeType.Element, "task", tasksNode.NamespaceURI);

            node.InnerText = m_name;

            tasksNode.AppendChild(node);
        }

        public override string ToString()
        {
            return Name;
        }

        #region ICloneable Members

        public object Clone()
        {
            Task newTask = new Task();
            newTask.Name = Name;
            return newTask;
        }

        #endregion ICloneable Members

        #endregion Methods

        #region Fields

        private string m_name;
        private bool m_doNotSave;

        #endregion Fields
    }
}
