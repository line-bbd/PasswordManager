using System.Data;
using System.Data.SqlClient;
using PasswordManager.app.Common;

namespace PasswordManager.app.Services
{
    internal class AuthServices
    {
        private string loggedInUser = "";
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + getDBPath() + ";Integrated Security=True";

        #region Constructors
        public AuthServices()
        {
        }

        public AuthServices(AuthOperation operation)
        {
            Operation = operation;
            Aggregator.Instance.Subscribe(nameof(logout), logout);
        }
        #endregion

        public bool Execute(string username, string password)
        {
            switch (Operation)
            {
                case AuthOperation.LOGIN:
                    return ValidateLogin(username, password);
                case AuthOperation.REGISTER:
                    return ValidateRegister(username, password);
                default:
                    throw new InvalidOperationException("Invalid operation specified.");
            }
        }

        private bool ValidateLogin(string username, string password)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = new SqlCommand();
            command.Connection = connection;

            command.CommandText = "SELECT * FROM Users WHERE username = @username AND password = @password";
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);
            connection.Close();

            if (dataTable.Rows.Count == 1)
            {
                loggedInUser = username;
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidateRegister(string username, string password)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = new SqlCommand();
            command.Connection = connection;

            command.CommandText = "SELECT * FROM Users WHERE username = @username";
            command.Parameters.AddWithValue("@username", username);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                // user already exists
                return false;
            }

            // insert new user
            command.CommandText = "INSERT INTO Users (username, password) VALUES (@username, @password)";
            command.Parameters.AddWithValue("@password", password);

            command.ExecuteNonQuery();
            connection.Close();

            return true;
        }
        public AuthOperation Operation { get; }

        // method to get true file path of db
        private static string getDBPath()
        {
            string systemPath = Path.GetFullPath("database\\");
            systemPath = systemPath.Substring(0, systemPath.IndexOf("bin")) + "database\\PasswordManagerDB.mdf";
            return systemPath;
        }

        private void logout()
        {
            loggedInUser = "";
        }
    }
    internal enum AuthOperation
    {
        LOGIN,
        REGISTER,
    }

}
