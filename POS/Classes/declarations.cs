using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace POS.Classes
{
    public static class declarations
    {
        public static int userid;
        public static string userFullName;
        public static string privilege;
        public static int shiftId;
        public static Dictionary<string, object> systemOptions;
    }
}
