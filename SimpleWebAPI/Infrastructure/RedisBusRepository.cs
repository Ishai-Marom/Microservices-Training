using SimpleWebAPI.Models;
using StackExchange.Redis;

namespace SimpleWebAPI.Infrastructure
{
    public class RedisBusRepository : BusRepository
    {
        public static RedisBusRepository Instance;
        private readonly static Config CONFIG = new Config();
        public static RedisBusRepository GetInstance()
        {
            if (Instance == null)
            {
                Instance = new RedisBusRepository();
            }

            return Instance;
        }

        private IDatabase database;

        private RedisBusRepository()
        {
            var connection = ConnectionMultiplexer.Connect(CONFIG.ConnectionConfiguration);
            database = connection.GetDatabase();
        }

        public bool Contains(string key)
        {
            return database.KeyExists(key);
        }

        public void Delete(string key)
        {
            database.KeyDelete(key);
        }

        public Bus Get(string key)
        {
            var value = database.HashGetAll(key);
            var dict = value.ToDictionary();

            /*Converting the (field-name, field-value) dictionary read from redis to data for the class.*/
            dict[new RedisValue("passengersCapacity")].TryParse(out int passengersCapacity);
            return new Bus(
                            dict[new RedisValue("id")].ToString(),
                            dict[new RedisValue("driverName")].ToString(),
                            dict[new RedisValue("color")].ToString(),
                            passengersCapacity);
        }

        public void Update(Bus value)
        {
            /* Creating a hash-set pf key-value data that represent the fields in redis.
                left value = field name. right value = field data.
            */
            database.HashSet(
                value.ID,
                [
                    new ("id", value.ID),
                    new ("driverName", value.DriverName),
                    new ("color", value.Color),
                    new ("passengersCapacity", value.PassengersCapacity)
                ]);
        }

        /*
        * A helper class with configuration for logging in to redis.
        */
        private class Config
        {
            // The database server host and port.
            public string ConnectionConfiguration { get; private set; } = $"{Environment.GetEnvironmentVariable("HOST") ?? "localhost"}:6379";
        }
    }
}
