using System.Data.SqlClient;

namespace PasswordManager.app.Services
{
    internal class UserServices
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + getDBPath() + ";Integrated Security=True";

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
                case CrudOperation.ADD:
                    command.CommandText = "INSERT INTO Entries (userID, username, password, service) VALUES (@userID, @username, @password, @service)";
                    break;
                case CrudOperation.UPDATE:
                    command.CommandText = "";
                    break;
                case CrudOperation.DELETE:
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

        // method to get true file path of db
        private static string getDBPath()
        {
            string systemPath = Path.GetFullPath("database\\");
            systemPath = systemPath.Substring(0, systemPath.IndexOf("bin")) + "database\\PasswordManagerDB.mdf";
            return systemPath;
        }
    }
    internal enum CrudOperation
    {
        ADD,
        UPDATE,
        DELETE
    }

}
