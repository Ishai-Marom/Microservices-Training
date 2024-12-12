using System;
using InitialProject.core;
using InitialProject.Infrastracture;


namespace InitialProject
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine("Hello, World!");
            Console.WriteLine("Hello, World!");
            Console.WriteLine("Hello, World!");

            TryingClass a = new TryingClass("5", "bi");
            a.FirstName = "5";
            a.LastName = "bi";

            Repository m = new InMemoryRepository();

            m.put("1", a);
            var v = m.get("1");
            m.delete("1");

            Console.WriteLine("The returned value is " + v);

            Repository redis = new RedisRepository();

            var redisValue = redis.get("Ron2");

            Console.WriteLine("Value from redis is " + redisValue);

            redis.delete("Ron2");
        }
    }
}