using System;
using System.Data;
using System.Data.SqlClient;

namespace QiPOS
{
    public sealed class UserRepository
    {
        private readonly string connectionString;

        public UserRepository()
        {
            connectionString = new Connect().ConnectionStr;
        }

        public UserAccount GetUserByName(string username)
        {
            const string query = "SELECT TOP 1 user_id, name, password, priority FROM users WHERE name = @Username";
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                        return null;

                    return new UserAccount
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("user_id")),
                        Name = reader.GetString(reader.GetOrdinal("name")),
                        PasswordHash = reader.GetString(reader.GetOrdinal("password")),
                        Priority = reader.GetInt32(reader.GetOrdinal("priority"))
                    };
                }
            }
        }

        public DataTable GetUsers()
        {
            const string query = "SELECT user_id, name, priority FROM users ORDER BY name";
            using (var connection = new SqlConnection(connectionString))
            using (var adapter = new SqlDataAdapter(query, connection))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public void AddUser(string username, string passwordHash, int priority)
        {
            const string query = "INSERT INTO users (name, password, priority) VALUES (@Username, @Password, @Priority)";
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", passwordHash);
                command.Parameters.AddWithValue("@Priority", priority);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdatePassword(int userId, string passwordHash)
        {
            const string query = "UPDATE users SET password = @Password WHERE user_id = @UserId";
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Password", passwordHash);
                command.Parameters.AddWithValue("@UserId", userId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdatePriority(int userId, int priority)
        {
            const string query = "UPDATE users SET priority = @Priority WHERE user_id = @UserId";
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Priority", priority);
                command.Parameters.AddWithValue("@UserId", userId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
