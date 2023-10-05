using MySql.Data.MySqlClient;

namespace _20231005_dolgozat_jegyek_WebAPI.Controllers
{
    public class Connect
    {
        public MySqlConnection connection;
        public string Host;
        public string DbName;
        private string Username;
        public string Password;
        private string ConnectionString;
        public Connect()
        {
            Host = "localhost";
            DbName = "jegyekdb";
            Username = "root";
            Password = "";

            ConnectionString = $"Host={Host};Database={DbName};User={Username};Password={Password};SslMode=none";
            connection = new MySqlConnection(ConnectionString);
        }
    }
}
