using System;
using InitialProject.core;
using InitialProject.Infrastracture;


namespace InitialProject
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            TryingClass a = new TryingClass("id", "6", "8");

            Repository redis = new RedisRepository();

            redis.update(a);

            var redisValue = redis.get(a.ID);

            Console.WriteLine("Value from redis is " + redisValue);

            redis.delete(a.ID);

            TryingClass b = new TryingClass("id2", "hello", "AAAAAAAAAA");

            Repository postgres = new PostgresSQLRepository();

            postgres.update(b);

            var postgresValue = postgres.get(b.ID);

            Console.WriteLine("Value from postgres is " + postgresValue);

            postgres.delete(b.ID);
        }
    }
}