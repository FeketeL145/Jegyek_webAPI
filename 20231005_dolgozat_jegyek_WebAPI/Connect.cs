using MySql.Data.MySqlClient;

namespace _20231005_dolgozat_jegyek_WebAPI
{
    public class Connect
    {
        public MySqlConnection connection;
        private string Host;
        private string DbName;
        private string UserName;
        private string Password;
        private string ConnectionString;

        public Connect()
        {
            Host = "localhost";
            DbName = "jegyekdb";
            UserName = "root";
            Password = "";

            ConnectionString = $"Host={Host};Database={DbName};User={UserName};Password={Password}";

            connection = new MySqlConnection(ConnectionString);

        }

    }
}