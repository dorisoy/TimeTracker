using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace PersonalTimeTracker
{
    /// <summary>
    /// Manages reading and saving log files.
    /// </summary>
    public static class LogFileManager
    {
        private const string DATA_FILE_NAME = "PTT Data";

        static LogFileManager()
        {
            s_MonthsLoaded = new List<DateTime>();
            s_MonthsEdited = new List<DateTime>();
            s_CanSaveToPrimaryDataFile = false;
        }

        #region Properties

        /// <summary>
        /// Gets the time the data file was last saved.
        /// </summary>
        public static DateTime LastSaveTime
        {
            get { return LogFileManager.s_LastSaveTime; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Loads the data file for the current month.
        /// </summary>
        static public void LoadInitialDataFile()
        {
            DateTime monthToLoad = DateTime.Now;
            string dataFileName = GetDataFileName(monthToLoad);
            if (File.Exists(dataFileName))
            {
                s_CanSaveToPrimaryDataFile = LoadDataFile(monthToLoad);
            }
            else
            {
                s_CanSaveToPrimaryDataFile = true; // The primary file does not exist, so it is OK to create a new one
                // Look back as far as 6 months to see if any old data files exist
                for (int idx = 0; !File.Exists(dataFileName) && idx <= 6; ++idx)
                {
                    monthToLoad = monthToLoad.AddMonths(-1);
                    dataFileName = GetDataFileName(monthToLoad);
                }
                LoadDataFile(monthToLoad);
            }
        }

        /// <summary>
        /// Load the data file for the given month.
        /// </summary>
        /// <param name="monthToLoad"></param>
        /// <returns><b>true</b> if loaded sucessfully.</returns>
        static public bool LoadDataFile(DateTime monthToLoad)
        {
            bool retVal = false;
            string dataFileName = GetDataFileName(monthToLoad);
            if (File.Exists(dataFileName))
            {
                XmlDocument dataXml = new XmlDocument();
                try
                {
                    dataXml.Load(dataFileName);

                    Customer.ReadCustomers(dataXml);
                    Task.ReadTasks(dataXml);
                    TimeLogEntry.ReadLogEntries(dataXml);

                    // Update last save time
                    s_CanSaveToPrimaryDataFile = true;
                    s_LastSaveTime = DateTime.Now;
                    retVal = true;
                }
                catch (IOException ex)
                {
                    string msg = string.Format("Error Loading Data File.  No data will be saved until the error is corrected and PTT is restarted.  Error: {0}", ex.Message);
                    MessageBox.Show("Error Loading File", msg, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (s_CanSaveToPrimaryDataFile)
            {
                s_MonthsLoaded.Add(monthToLoad);
            }
            return retVal;
        }

        /// <summary>
        /// Loads the data file for the given month if it was not already loaded.
        /// </summary>
        /// <param name="monthToLoad">The month to load.</param>
        /// <returns><b>true</b> if the file was to be loaded.</returns>
        static public bool LoadDataFileIfNotAlreadyLoaded(DateTime monthToLoad)
        {
            bool retVal = false;
            if (!s_MonthsLoaded.Exists(delegate(DateTime loadedMonth) { return (loadedMonth.Year == monthToLoad.Year && loadedMonth.Month == monthToLoad.Month); }))
            {
                retVal = LogFileManager.LoadDataFile(monthToLoad);
            }
            return retVal;
        }

        /// <summary>
        /// Add this month to the list of months that have been edited.
        /// </summary>
        /// <param name="monthEdited"></param>
        static public void MarkMonthAsDirty(DateTime monthEdited)
        {
            if (!DoesMonthHaveEditedTimes(monthEdited))
            {
                s_MonthsEdited.Add(monthEdited);
            }
        }

        /// <summary>
        /// Save the tasks for the current month.
        /// </summary>
        /// <returns><b>true</b> if sucessful.</returns>
        static public bool SaveDataFile()
        {
            bool retVal = true;
            if (s_CanSaveToPrimaryDataFile)
            {
                List<DateTime> monthsThatFailedToSave = new List<DateTime>();
                foreach (DateTime monthLoaded in s_MonthsLoaded)
                {
                    if (DoesMonthHaveEditedTimes(monthLoaded))
                    {
                        // See if we need rolled over to a new month file
                        string dataFileName = GetDataFileName(monthLoaded);
                        XmlDocument dataXml = new XmlDocument();
                        try
                        {
                            XmlNode rootNode = dataXml.CreateNode(XmlNodeType.Element, "ptt_data", dataXml.NamespaceURI);
                            dataXml.AppendChild(rootNode);
                            Customer.SaveCustomers(rootNode);
                            Task.SaveTasks(rootNode);
                            TimeLogEntry.SaveLogs(rootNode, monthLoaded);

                            using (XmlTextWriter writer = new XmlTextWriter(dataFileName, Encoding.UTF8))
                            {
                                writer.Formatting = Formatting.Indented;
                                dataXml.Save(writer);
                            }
                            // Update last save time
                            s_LastSaveTime = DateTime.Now;
                        }
                        catch (IOException ex)
                        {
                            monthsThatFailedToSave.Add(monthLoaded);
                            MessageBox.Show("Error Saving Data File", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            retVal = false;
                        }
                    }
                }
                // Clear dirty months
                s_MonthsEdited.Clear();
                // Add back any months that had save errors
                s_MonthsEdited.AddRange(monthsThatFailedToSave);
            }
            return retVal;
        }

        static private bool DoesMonthHaveEditedTimes(DateTime monthLoaded)
        {
            foreach (DateTime dt in s_MonthsEdited)
            {
                if (dt.Year == monthLoaded.Year && dt.Month == monthLoaded.Month) return true;
            }
            return false;
        }

        /// <summary>
        /// Generate the file name for the current month's log file.
        /// </summary>
        /// <returns>The fully qualified name of the log file.</returns>
        static private string GetDataFileName()
        {
            return GetDataFileName(DateTime.Now);
        }

        /// <summary>
        /// Generate the file name for the given month's log file.
        /// </summary>
        /// <returns>The fully qualified name of the log file.</returns>
        static private string GetDataFileName(DateTime monthToLoad)
        {
            string dirName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string baseName = string.Format("{0} {1}.xml", DATA_FILE_NAME, monthToLoad.ToString("yyyy MMM"));
            return Path.Combine(dirName, baseName);
        }

        #endregion Methods

        #region Fields
        static private List<DateTime> s_MonthsLoaded;
        static private List<DateTime> s_MonthsEdited;
        static private bool s_CanSaveToPrimaryDataFile; // Used to prevent truncation of a partially corrupt file
        static private DateTime s_LastSaveTime;
        #endregion Fields
    }
}
