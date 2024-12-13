using InitialProject.core;
using InitialProject.Infrastracture;


namespace InitialProject
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            SomeDataEntity firstDataEntity = new SomeDataEntity("id", "6", "8");

            IRepository redis = new RedisRepository();

            redis.Update(firstDataEntity);

            var redisValue = redis.Get(firstDataEntity.ID);

            Console.WriteLine("Value from redis is " + redisValue);

            redis.Delete(firstDataEntity.ID);

            SomeDataEntity secondDataEntity = new SomeDataEntity("id2", "hello", "AAAAAAAAAA");

            IRepository postgres = new PostgresSQLRepository();

            postgres.Update(secondDataEntity);

            var postgresValue = postgres.Get(secondDataEntity.ID);

            Console.WriteLine("Value from postgres is " + postgresValue);

            postgres.Delete(secondDataEntity.ID);

            SomeDataEntity thirdDataEntity = new SomeDataEntity("id3", "hello", "BBBBBBBBBB");

            IRepository inMemory = new PostgresSQLRepository();

            postgres.Update(thirdDataEntity);

            var inMemoryValue = postgres.Get(thirdDataEntity.ID);

            Console.WriteLine("Value from postgres is " + inMemoryValue);

            postgres.Delete(thirdDataEntity.ID);
        }
    }
}