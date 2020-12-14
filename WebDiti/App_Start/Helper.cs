using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebDiti.App_Start
{
    public class Helper
    {
        /// <summary>
        /// ERRORS LOG
        /// </summary>
        /// <param name="erreur"></param>
        public static void WriteLogSystem(string erreur)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "DItI2020";
                eventLog.WriteEntry(string.Format("date: {0}, libelle: {1}, description {2}", DateTime.Now, "DItI2020", erreur), EventLogEntryType.Information, 101, 1);
            }
        }
    }
}