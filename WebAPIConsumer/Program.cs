using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;

namespace WebAPIConsumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var client = new HttpClient();
            var host = Environment.GetEnvironmentVariable("HOST") ?? "localhost";
            var port = Environment.GetEnvironmentVariable("PORT") ?? "5186";

            // POST
            // var postRoute = $"http://{host}:{port}/Bus";
            // var postData = new{ id = "Q", driverName = "AAAAAAAA", color = "BBBBBB", passengersCapacity = 15};
            // var postResponse = client.PostAsJsonAsync(postRoute, postData).Result;

            // Console.WriteLine(postResponse.Content.ReadAsStringAsync().Result.ToString());

            // // PUT
            // var putRoute = $"http://{host}:{port}/Bus/Q";
            // var putData = new{ id = "Q", driverName = "AAAAAAAA", color = "EEEEEEEEE", passengersCapacity = 30};
            // var putResponse = client.PutAsJsonAsync(putRoute, putData).Result;

            // Console.WriteLine(putResponse.Content.ReadAsStringAsync().Result.ToString());

            // // GET
            // var getRoute = $"http://{host}:{port}/Bus/Q";
            // var getResponse = client.GetAsync(getRoute).Result;

            // Console.WriteLine(getResponse.Content.ReadAsStringAsync().Result.ToString());

            // // DELETE
            // var deleteRoute = $"http://{host}:{port}/Bus/Q";
            // var deleteResponse = client.DeleteAsync(deleteRoute).Result;

            // Console.WriteLine(deleteResponse.Content.ReadAsStringAsync().Result.ToString());

            Console.WriteLine("Strarting passengers entry app.");

            while (true)
            {
                try {
                    Thread.Sleep(5000);

                    string busToUpdate = "Q";

                    var getRoute = $"http://{host}:{port}/Bus/{busToUpdate}";
                    var getResponse = client.GetAsync(getRoute);

                    if (getResponse.Result.StatusCode.Equals(HttpStatusCode.OK)) {
                        Console.WriteLine("Bus found, adding 1 passenger");
                        
                        var putRoute = $"http://{host}:{port}/Bus/{busToUpdate}/add-passenger";
                        var putResponse = client.PostAsync(putRoute, null).Result;
                        Console.WriteLine(putResponse.Content.ReadAsStringAsync().Result.ToString());                 
                    } else {
                        Console.WriteLine("Bus not found");
                    }
                } catch (Exception) {
                    Console.WriteLine("Waiting");
                }
            }
        }
    }
}
