using System.Data;
using System.Data.SqlClient;
using PasswordManager.app.Common;
using System.Text;

namespace PasswordManager.app.Services
{
    internal class UserServices
    {
        private static string loggedInUser = "";
        private int loggedInUserID;

        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Utils.Utils.getDBPath() + ";Integrated Security=True";

        #region Constructors
        public UserServices(CrudOperation operation)
        {
            Operation = operation;
            loggedInUser = (string)Aggregator.Instance.Raise("GetLoggedInUser");
            GetUserID();
        }
        #endregion

        public bool Execute(string username, string? password, string service)
        {
            switch (Operation)
            {
                case CrudOperation.ADD:
                    return AddEntry(username, password, service);
                case CrudOperation.DELETE:
                    return DeleteEntry(username, service);
                case CrudOperation.RETRIEVE:
                    RetrieveAll();
                    break;
                default:
                    throw new InvalidOperationException("Invalid operation specified.");
            }
            return true;
        }
        public CrudOperation Operation { get; }

        private bool AddEntry(string username, string password, string service)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            connection.Open();

            // check if entry already exists
            command.CommandText = "SELECT * FROM Entries WHERE userID = @userID AND username = @username AND service = @service";
            command.Parameters.AddWithValue("@userID", loggedInUserID);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@service", service);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                Console.WriteLine("Entry already exists.\n");
                return false;
            }

            // add entry
            command.CommandText = "INSERT INTO Entries (userID, username, password, service) VALUES (@userID, @username, @password, @service)";
            command.Parameters.AddWithValue("@password", password);
            command.ExecuteNonQuery();

            connection.Close();

            return true;
        }

        private bool DeleteEntry(string username, string service)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            connection.Open();

            // check if entry exists
            command.CommandText = "SELECT * FROM Entries WHERE userID = @userID AND username = @username AND service = @service";
            command.Parameters.AddWithValue("@userID", loggedInUserID);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@service", service);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            if (dataTable.Rows.Count == 0)
            {
                Console.WriteLine("No such entry exists.\n");
                return false;
            }

            // delete entry
            command.CommandText = "DELETE FROM Entries WHERE userID = @userID AND username = @username AND service = @service";
            command.ExecuteNonQuery();

            connection.Close();

            return true;
        }

        public string RetrieveAll()
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand();

            command.Connection = connection;
            connection.Open();

            command.CommandText = "SELECT e.* FROM Users as u INNER JOIN Entries as e on e.userID = u.userID WHERE u.username = @username";
            command.Parameters.AddWithValue("@username", loggedInUser);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();

            adapter.Fill(dataTable);

            string data = (dataTable.Rows.Count == 0) ? "" : GetFormattedData(dataTable);

            connection.Close();

            return data;
        }

        private string GetFormattedData(DataTable data)
        {
            int max = 25;
            int pad = 10;

            string format = "{0,-" + pad + "} {1,-" + pad + "} {2,-" + pad + "}";

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < data.Rows.Count; i++)
            {
                for (int k = 2; k < data.Columns.Count; k++)
                {
                    string value = data.Rows[i][k].ToString().PadRight(max);

                    sb.AppendFormat(format, value, "", "");
                }
                sb.Append("\n");
            }

            return sb.ToString();
        }

        private void GetUserID()
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand();

            DataTable dataTable = new DataTable();

            command.Connection = connection;
            connection.Open();

            command.CommandText = "SELECT userID FROM Users WHERE username = @username";
            command.Parameters.AddWithValue("@username", loggedInUser);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dataTable);

            connection.Close();

            loggedInUserID = (int)dataTable.Rows[0][0];
        }
    }

    internal enum CrudOperation
    {
        ADD,
        DELETE,
        RETRIEVE
    }

}
