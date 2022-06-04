using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRun.Db
{
    public abstract class DbConnection
    {
        private readonly string connectionString;
        public DbConnection()
        {
           connectionString = @"SERVER=localhost;" + "DATABASE=graduationwork;" + "UID=savichev;" + "PASSWORD=admin;" + "connection timeout = 180";
        }

        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
