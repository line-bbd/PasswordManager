using System.Data.SqlClient;

namespace PasswordManager.app.Services
{
    internal class UserServices
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\bbdnet2782\Documents\BBD\Grad_program\C_sharp\PasswordManager\database\passwords.mdf;Integrated Security=True;Connect Timeout=30";

        #region Constructors
        public UserServices()
        {
        }

        public UserServices(CrudOperation operation)
        {
            Operation = operation;
        }
        #endregion

        public void Execute(string username, string password, string service)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand();
            command.Connection = connection;

            switch (Operation)
            {
                case CrudOperation.Add:
                    command.CommandText = "INSERT INTO passwordManager (username, password, service) VALUES (@username, @password, @service)";
                    break;
                case CrudOperation.Update:
                    command.CommandText = "UPDATE passwords SET password = @password WHERE username = @ssername AND service = @service";
                    break;
                case CrudOperation.Delete:
                    command.CommandText = "DELETE FROM passwords WHERE service = @service";
                    break;
                default:
                    throw new InvalidOperationException("Invalid operation specified.");
            }

            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);
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
