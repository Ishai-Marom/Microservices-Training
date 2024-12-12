using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using InitialProject.core;
using StackExchange.Redis;

namespace InitialProject.Infrastracture
{
    public class RedisRepository : Repository
    {

        private IDatabase _database;

        public RedisRepository()
        {
            var connection = ConnectionMultiplexer.Connect("localhost:6379");
            _database = connection.GetDatabase();
        }

        public TryingClass get(string key)
        {
            var value = _database.HashGetAll(key);
            var dict = value.ToDictionary();

            return new TryingClass(
                            dict[new RedisValue("FirstName")].ToString(),
                            dict[new RedisValue("LastName")].ToString());
        }

        public void put(string key, TryingClass value)
        {
            _database.HashSet(
                key,
                [
                    new ("FirstName", value.FirstName),
                    new ("LastName", value.LastName)
                ]);
        }

        public void delete(string key)
        {
            _database.KeyDelete(key);
        }
    }
}