using MySql.Data.MySqlClient;
using System.Data;

namespace WebApiCoreWithoutORM.Data
{
    public sealed class MySQLConnection : IMySQLConnect
    {      
        public MySqlConnection Connect { get; set; }
        public MySQLConnection(string connectionString)
        {
            Connect = new MySqlConnection(connectionString);
        }
        public MySQLConnection(MySqlConnection connect)
        {
            Connect = connect;
        }

        public void Close()
        {
            Connect?.Close();
        }

        public MySqlCommand CreateCommand()
        {
            return Open().CreateCommand();
        }       

        public MySqlConnection Open()
        {
            if (Connect == null)
            {
                throw new DataException("Connection no instance");
            }
            if (Connect?.State == ConnectionState.Closed)
            {
                Connect?.Open();
            }
            return Connect;
        }

        public void Dispose()
        {
            Close();
            Connect.Dispose();
        }
    }
}
