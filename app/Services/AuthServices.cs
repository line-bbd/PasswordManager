using System.Data;
using System.Data.SqlClient;
using PasswordManager.app.Common;

namespace PasswordManager.app.Services
{
    internal class AuthServices
    {
        private static string loggedInUser = "";
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Utils.Utils.getDBPath() + ";Integrated Security=True";

        #region Constructors
        public AuthServices(AuthOperation operation)
        {
            Operation = operation;
            Aggregator.Instance.Subscribe(nameof(logout), logout);
            Aggregator.Instance.Subscribe(AggregatorMethodNames.GET_LOGGED_IN_USER, GetLoggedInUser);
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
            command.Parameters.AddWithValue("@password", (string) Aggregator.Instance.Raise("GetHash", password));

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);
            connection.Close();

            if (dataTable.Rows.Count == 1)
            {
                // set active user
                loggedInUser = username;
                return true;
            }
            return false;
        }

        private bool ValidateRegister(string username, string password)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = new SqlCommand();
            command.Connection = connection;

            // first check if user doesn't already exist
            command.CommandText = "SELECT * FROM Users WHERE username = @username";
            command.Parameters.AddWithValue("@username", username);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            if (dataTable.Rows.Count == 0)
            {
                // insert new user
                command.CommandText = "INSERT INTO Users (username, password) VALUES (@username, @password)";
                command.Parameters.AddWithValue("@password", (string)Aggregator.Instance.Raise("GetHash", password));
                command.ExecuteNonQuery();
                return true;
            }

            connection.Close();

            return false;
        }

        public string GetLoggedInUser()
        {
            return loggedInUser;
        }
        public AuthOperation Operation { get; }

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
