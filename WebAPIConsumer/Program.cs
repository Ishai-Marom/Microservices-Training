using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text;

namespace WebAPIConsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var client = new HttpClient();
            var host = Environment.GetEnvironmentVariable("HOST") ?? "localhost";
            var port = Environment.GetEnvironmentVariable("") ?? "5186";

            // POST
            var postRoute = $"http://{host}:{port}/AttemptNew";
            var postData = new{ id = "Q", firstName = "AAAAAAAA", lastName = "BBBBBB"};
            var postResponse = client.PostAsJsonAsync(postRoute, postData).Result;

            Console.WriteLine(postResponse.Content.ReadAsStringAsync().Result.ToString());

            // PUT
            var putRoute = $"http://{host}:{port}/AttemptNew/Q";
            var putData = new{ id = "Q", firstName = "AAAAAAAA", lastName = "TTTTTTTTTtt"};
            var putResponse = client.PutAsJsonAsync(putRoute, putData).Result;

            Console.WriteLine(putResponse.Content.ReadAsStringAsync().Result.ToString());

            // GET
            var getRoute = $"http://{host}:{port}/AttemptNew/Q";
            var getResponse = client.GetAsync(getRoute).Result;

            Console.WriteLine(getResponse.Content.ReadAsStringAsync().Result.ToString());

            // DELETE
            var deleteRoute = $"http://{host}:{port}/AttemptNew/Q";
            var deleteResponse = client.DeleteAsync(deleteRoute).Result;

            Console.WriteLine(deleteResponse.Content.ReadAsStringAsync().Result.ToString());

            Console.WriteLine("Hello, World!");
        }
    }
}
