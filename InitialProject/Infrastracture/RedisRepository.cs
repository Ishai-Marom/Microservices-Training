using InitialProject.core;
using StackExchange.Redis;

namespace InitialProject.Infrastracture
{
    /*
    * A Redis implementation of the IRepository interface.
    */
    internal class RedisRepository : IRepository
    {
        private readonly static Config CONFIG = new Config(); 

        private IDatabase database;

        public RedisRepository()
        {
            var connection = ConnectionMultiplexer.Connect(CONFIG.ConnectionConfiguration);
            database = connection.GetDatabase();
        }

        public SomeDataEntity Get(string key)
        {
            var value = database.HashGetAll(key);
            var dict = value.ToDictionary();

            /*Converting the (field-name, field-value) dictionary read from redis to data for the class.*/
            return new SomeDataEntity(
                            dict[new RedisValue("id")].ToString(),
                            dict[new RedisValue("FirstName")].ToString(),
                            dict[new RedisValue("LastName")].ToString());
        }

        public void Update(SomeDataEntity value)
        {
            /* Creating a hash-set pf key-value data that represent the fields in redis.
                left value = field name. right value = field data.
            */
            database.HashSet(
                value.ID,
                [
                    new ("id", value.ID),
                    new ("FirstName", value.FirstName),
                    new ("LastName", value.LastName)
                ]);
        }

        public void Delete(string key)
        {
            database.KeyDelete(key);
        }


        /*
        * A helper class with configuration for logging in to redis.
        */
        private class Config {
            // The configuration for connecting to redis.
            public string ConnectionConfiguration {get; private set;} = "localhost:6379";
        }
    }
}