using InitialProject.core;
using StackExchange.Redis;

namespace InitialProject.Infrastracture
{
    internal class RedisRepository : IRepository
    {

        private IDatabase _database;

        public RedisRepository()
        {
            var connection = ConnectionMultiplexer.Connect("localhost:6379");
            _database = connection.GetDatabase();
        }

        public SomeDataEntity Get(string key)
        {
            var value = _database.HashGetAll(key);
            var dict = value.ToDictionary();

            return new SomeDataEntity(
                            dict[new RedisValue("id")].ToString(),
                            dict[new RedisValue("FirstName")].ToString(),
                            dict[new RedisValue("LastName")].ToString());
        }

        public void Update(SomeDataEntity value)
        {
            _database.HashSet(
                value.ID,
                [
                    new ("id", value.ID),
                    new ("FirstName", value.FirstName),
                    new ("LastName", value.LastName)
                ]);
        }

        public void Delete(string key)
        {
            _database.KeyDelete(key);
        }
    }
}