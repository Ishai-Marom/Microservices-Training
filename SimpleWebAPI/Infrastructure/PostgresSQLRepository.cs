using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using SimpleWebAPI.Models;

namespace SimpleWebAPI.Infrastructure
{
    public class PostgresSQLRepository : BusRepository
    {
        public static PostgresSQLRepository Instance;
        private readonly static Config CONFIG = new Config();
        public static PostgresSQLRepository GetInstance()
        {
            if (Instance == null)
            {
                Instance = new PostgresSQLRepository();
            }

            return Instance;
        }

        private NpgsqlConnection connection;

        private PostgresSQLRepository()
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
                driverName VARCHAR(50) NOT NULL,
                color VARCHAR(50) NOT NULL,
                passengersCapacity INT NOT NULL
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

        public Bus Get(string key)
        {
            string query = $"SELECT * from {CONFIG.TableName} where id = @id";

            using var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("id", key);
            using var reader = cmd.ExecuteReader();
            reader.Read();

            return new Bus(
                reader["id"].ToString(),
                reader["driverName"].ToString(),
                reader["color"].ToString(),
                Convert.ToInt32(reader["passengersCapacity"]));
        }

        public void Update(Bus value)
        {
            string query = $"""
                INSERT INTO {CONFIG.TableName} (id, driverName, color, passengersCapacity)
                VALUES (@id, @driverName, @color, @passengersCapacity)
                ON CONFLICT (id)
                DO UPDATE SET
                    driverName = EXCLUDED.driverName,
                    color = EXCLUDED.color,
                    passengersCapacity = EXCLUDED.passengersCapacity
                """;

            using var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("id", value.ID);
            cmd.Parameters.AddWithValue("driverName", value.DriverName);
            cmd.Parameters.AddWithValue("color", value.Color);
            cmd.Parameters.AddWithValue("passengersCapacity", value.PassengersCapacity);

            cmd.ExecuteNonQuery();
        }

        public bool Contains(string key)
        {
            string query = $"""
                SELECT EXISTS (SELECT 1 FROM {CONFIG.TableName} WHERE id = @id)
                """;
            // SELECT EXISTS (SELECT 1 FROM example_table WHERE id = @id

            using var cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("id", key);

            return (bool)cmd.ExecuteScalar();

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