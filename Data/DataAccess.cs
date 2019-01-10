using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AppDatabasesGuide.Data
{
    public class DataAccess
    {

        private static string connection;

        public static  string Connection
        {
            get {
                return "Server=.;Database=DatabaseGuideDB;uid=sa;pwd=123";
            }
        }

    }
}
