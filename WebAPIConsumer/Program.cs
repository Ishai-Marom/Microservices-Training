using System.Net.Http.Json;

namespace WebAPIConsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var client = new HttpClient();
            var host = Environment.GetEnvironmentVariable("HOST") ?? "localhost";
            var port = Environment.GetEnvironmentVariable("") ?? "5186";

            var responseMessage = client.GetAsync($"http://{host}:{port}/WeatherForecast").Result;

            Console.WriteLine(responseMessage.Content.ReadAsStringAsync().Result.ToString());
            Console.WriteLine("Hello, World!");
        }
    }
}
