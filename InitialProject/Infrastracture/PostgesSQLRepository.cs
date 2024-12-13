using InitialProject.core;
using Npgsql;

namespace InitialProject.Infrastracture
{
    internal class PostgresSQLRepository : IRepository
    {
        private NpgsqlConnection connection;
        
        public PostgresSQLRepository()
        {
            string host = "localhost";
            string username = "postgres";
            string password = "example";
            string database = "postgres";

            var connString = $"Host={host};Username={username};Password={password};Database={database}";
            connection = new NpgsqlConnection(connString);
            connection.Open();
        }

        public void Delete(string key)
        {
            string table = "mytable";
            var query = $"DELETE FROM {table} WHERE id = @id";

            using var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("id", key);

            cmd.ExecuteNonQuery();
        }

        public SomeDataEntity Get(string key)
        {
            string table = "mytable";
            string query = $"SELECT * from {table} where id = @id";

            using var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("id", key);
            using var reader = cmd.ExecuteReader();
            reader.Read();

            return new SomeDataEntity(
                reader["id"].ToString(),
                reader["first_name"].ToString(),
                reader["last_name"].ToString());
        }

        public void Update(SomeDataEntity value)
        {
            string table = "mytable";
            //string query = $"UPDATE {table} SET first_name = {value.ID}, last_name = {value.FirstName} WHERE id = {value.LastName}";
            string query = $"""
                INSERT INTO {table} (id, first_name, last_name)
                VALUES (@id, @first_name, @last_name)
                ON CONFLICT (id)
                DO UPDATE SET
                    first_name = EXCLUDED.first_name,
                    last_name = EXCLUDED.last_name;
                """;

            using var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("id", value.ID);
            cmd.Parameters.AddWithValue("first_name", value.FirstName);
            cmd.Parameters.AddWithValue("last_name", value.LastName);

            cmd.ExecuteNonQuery();
        }
    }
}