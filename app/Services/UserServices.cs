using System.Data.SqlClient;

namespace PasswordManager.app.Services
{
    internal class UserServices
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bbdnet2782\Documents\BBD\Grad_program\PasswordManager\database\PasswordManagerDB.mdf;Integrated Security=True";

        #region Constructors
        public UserServices()
        {
        }

        public UserServices(CrudOperation operation, int userID, string username, string password, string service)
        {
            Operation = operation;
            Execute(userID, username, password, service);
        }
        #endregion

        public void Execute(int userID, string username, string password, string service)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand();
            command.Connection = connection;

            switch (Operation)
            {
                case CrudOperation.Add:
                    command.CommandText = "INSERT INTO Entries (userID, username, password, service) VALUES (@userID, @username, @password, @service)";
                    break;
                case CrudOperation.Update:
                    command.CommandText = "";
                    break;
                case CrudOperation.Delete:
                    command.CommandText = "";
                    break;
                default:
                    throw new InvalidOperationException("Invalid operation specified.");
            }

            command.Parameters.AddWithValue("@userID", userID);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@service", service);


            connection.Open();
            command.ExecuteNonQuery();
        }
        public CrudOperation Operation { get; }

    }
    internal enum CrudOperation
    {
        Add,
        Update,
        Delete
    }
}
