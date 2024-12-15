using InitialProject.core;
using InitialProject.Infrastracture;


namespace InitialProject
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            // while (true) 
            // {
            //     Bus firstDataEntity = new Bus("id", "6", "8", 2);
            //     Thread.Sleep(5000);
                
            //     // Redis
            //     Console.WriteLine("Redis Start");

            //     IRepository redis = new RedisRepository();
            //     redis.Update(firstDataEntity);
            //     var redisValue = redis.Get(firstDataEntity.ID);
            //     Console.WriteLine("Value from redis is " + redisValue);
            //     redis.Delete(firstDataEntity.ID);

            //     Console.WriteLine("Redis Start");

            //     // Postgres
            //     Console.WriteLine("Postgres Start");

            //     Bus secondDataEntity = new Bus("id2", "hello", "AAAAAAAAAA", 4);
            //     IRepository postgres = new PostgresSQLRepository();
            //     postgres.Update(secondDataEntity);
            //     var postgresValue = postgres.Get(secondDataEntity.ID);
            //     Console.WriteLine("Value from postgres is " + postgresValue);
            //     postgres.Delete(secondDataEntity.ID);

            //     Console.WriteLine("Postgres Start");


            //     // In Memory
            //     Console.WriteLine("In Memory Start");

            //     Bus thirdDataEntity = new Bus("id3", "hello", "BBBBBBBBBB", 6);
            //     IRepository inMemory = new InMemoryRepository();
            //     inMemory.Update(thirdDataEntity);
            //     var inMemoryValue = inMemory.Get(thirdDataEntity.ID);
            //     Console.WriteLine("Value from in memory is " + inMemoryValue);
            //     inMemory.Delete(thirdDataEntity.ID);

            //     Console.WriteLine("In Memory End");
            // }

            Bus bus = new Bus("id2", "Noam", "Yellow", 30);
            IRepository repository = new PostgresSQLRepository();

            repository.Delete("id2");
        }
    }
}