using System;
using System.IO;
using System.Net;

namespace kunze_prüfer.Models
{
    public static class ErrorLogger
    {
        public static void Log(Exception ex)
        {
            if (!File.Exists("./error.log"))
            {
                File.Create("./error.log");

            }
            
            if (File.Exists("./error.log"))
            {
                File.AppendAllText("./error.log", "--- ERROR START ---" + Environment.NewLine);
                File.AppendAllText("./error.log", "DATE: " + DateTime.Now.ToString() + Environment.NewLine + Environment.NewLine);
                File.AppendAllText("./error.log", ex.ToString() + Environment.NewLine + Environment.NewLine);
                File.AppendAllText("./error.log", "--- ERROR END ---" + Environment.NewLine);
            }
        }
    }
}