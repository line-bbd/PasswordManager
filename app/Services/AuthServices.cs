using System.Data;
using System.Data.SqlClient;

namespace PasswordManager.app.Services
{
    internal class AuthServices
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + getDBPath() + ";Integrated Security=True";

        #region Constructors
        public AuthServices()
        {
        }

        public AuthServices(AuthOperation operation, string username, string password)
        {
            Operation = operation;
            Execute(username, password);
        }
        #endregion

        private void Execute(Operation operation, string username, string password)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand();
            command.Connection = connection;

            switch (Operation)
            {
                case AuthOperation.LOGIN:
                    command.CommandText = "SELECT * FROM Users WHERE username = @username AND password = @password";
                    ValidateLogin(command, connection);
                    break;
                case AuthOperation.REGISTER:
                    command.CommandText = "";
                    break;
                default:
                    throw new InvalidOperationException("Invalid operation specified.");
            }


        }

        private bool ValidateLogin(SqlCommand command, SqlConnection connection)
        {
            DataTable dataTable = new DataTable();

            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dataTable);
            connection.Close();

            if (dataTable.Rows.Count == 1)
            {
                // login success
                return true;
            }
            else
            {
                // login failed
                return false;
            }
        }
        public AuthOperation Operation { get; }

        // method to get true file path of db
        private static string getDBPath()
        {
            string systemPath = Path.GetFullPath("database\\");
            systemPath = systemPath.Substring(0, systemPath.IndexOf("bin")) + "database\\PasswordManagerDB.mdf";
            return systemPath;
        }
    }
    internal enum AuthOperation
    {
        LOGIN,
        REGISTER,
    }

}
