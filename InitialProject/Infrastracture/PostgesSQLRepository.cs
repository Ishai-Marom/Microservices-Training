using InitialProject.core;
using Npgsql;

namespace InitialProject.Infrastracture
{
    /*
    * A PostgreSQL implementation of the IRepository interface.
    */
    internal class PostgresSQLRepository : IRepository
    {

        private readonly static Config CONFIG = new Config(); 
        private NpgsqlConnection connection;
        
        public PostgresSQLRepository()
        {
            var connString = $"Host={CONFIG.Host};Username={CONFIG.Username};Password={CONFIG.Password};Database={CONFIG.DatabaseName};";
            connection = new NpgsqlConnection(connString);
            connection.Open();

            // In order to write/read from a postgres table, it needs to be created first.
            dropOldTable();
            createTable();
        }

        private void dropOldTable()
        {
            var query = $@"DROP TABLE IF EXISTS {CONFIG.TableName}";
            using var cmd = new NpgsqlCommand(query, connection);

            cmd.ExecuteNonQuery();
        }

        private void createTable() {
            var query = $@"
            CREATE TABLE {CONFIG.TableName} (
                id VARCHAR(50) PRIMARY KEY,
                first_name VARCHAR(50) NOT NULL,
                last_name VARCHAR(50) NOT NULL
            )";
            using var cmd = new NpgsqlCommand(query, connection);

            cmd.ExecuteNonQuery();
        }

        public void Delete(string key)
        {
            var query = $"DELETE FROM {CONFIG.TableName} WHERE id = @id";

            using var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("id", key);

            cmd.ExecuteNonQuery();
        }

        public SomeDataEntity Get(string key)
        {
            string query = $"SELECT * from {CONFIG.TableName} where id = @id";

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
            string query = $"""
                INSERT INTO {CONFIG.TableName} (id, first_name, last_name)
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

        /*
        * A helper class with configuration for logging in to postgres.
        */
        private class Config {
            // The database server host.
            public string Host {get; private set;} = Environment.GetEnvironmentVariable("HOST") ?? "localhost";
            // The username defined for logining in to postgres.
            public string Username {get; private set;} = "postgres";
            // The password defined for logining in to postgres.
            public string Password {get; private set;} = "example";
            // The name of the database we want to connect to.
            public string DatabaseName {get; private set;} = "postgres";

            // The name of the table within the database will write to and read from.
            public string TableName {get; private set;} = "mytable";
        }
    }
}