using InitialProject.core;
using InitialProject.Infrastracture;


namespace InitialProject
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            while (true) 
            {
                SomeDataEntity firstDataEntity = new SomeDataEntity("id", "6", "8");
                Thread.Sleep(5000);
                
                // Redis
                Console.WriteLine("Redis Start");

                IRepository redis = new RedisRepository();
                redis.Update(firstDataEntity);
                var redisValue = redis.Get(firstDataEntity.ID);
                Console.WriteLine("Value from redis is " + redisValue);
                redis.Delete(firstDataEntity.ID);

                Console.WriteLine("Redis Start");

                // Postgres
                Console.WriteLine("Postgres Start");

                SomeDataEntity secondDataEntity = new SomeDataEntity("id2", "hello", "AAAAAAAAAA");
                IRepository postgres = new PostgresSQLRepository();
                postgres.Update(secondDataEntity);
                var postgresValue = postgres.Get(secondDataEntity.ID);
                Console.WriteLine("Value from postgres is " + postgresValue);
                postgres.Delete(secondDataEntity.ID);

                Console.WriteLine("Postgres Start");


                // In Memory
                Console.WriteLine("In Memory Start");

                SomeDataEntity thirdDataEntity = new SomeDataEntity("id3", "hello", "BBBBBBBBBB");
                IRepository inMemory = new InMemoryRepository();
                inMemory.Update(thirdDataEntity);
                var inMemoryValue = inMemory.Get(thirdDataEntity.ID);
                Console.WriteLine("Value from in memory is " + inMemoryValue);
                inMemory.Delete(thirdDataEntity.ID);

                Console.WriteLine("In Memory End");
            }
        }
    }
}