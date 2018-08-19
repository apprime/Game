using MySql.Data.MySqlClient;
using System;

namespace Data.DataProviders.MySqlHelpers
{
    public class MySqlDb : IDisposable
    {
        public MySqlConnection Connection;

        public MySqlDb(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}
