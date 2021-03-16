using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Utility
{
    public class UtilityFunctions
    {
        public static string ReturnFormattedConnectionString(string OriginalConn)
        {
            string connString = OriginalConn;
            StringBuilder _strConn = new StringBuilder();
            if (connString.IndexOf("provider connection string") > -1)
            {
                connString = connString.Substring(connString.IndexOf("provider connection string") + "provider connection string".Length);
                connString = connString.Replace("MultipleActiveResultSets=True;App=EntityFramework", "");
                connString = connString.Remove(0, 2);
                connString = connString.Remove(connString.Length - 1);
                _strConn = new StringBuilder(connString + ";Persist Security Info=True;");
            }
            return _strConn.ToString();
        }
    }
}
