using System;
using System.IO;
using System.Threading.Tasks;
using FunctionApp1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionApp1
{
    public static class GetRating
    {
        [FunctionName("GetRating")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string ratingId = req.Query["ratingId"];

            /* based on the ratingId passed in, then reading from data source
             * {
  "id": "79c2779e-dd2e-43e8-803d-ecbebed8972c",
  "userId": "cc20a6fb-a91f-4192-874d-132493685376",
  "productId": "4c25613a-a3c2-4ef3-8e02-9c335eb23204",
  "timestamp": "2018-05-21 21:27:47Z",
  "locationName": "Sample ice cream shop",
  "rating": 5,
  "userNotes": "I love the subtle notes of orange in this ice cream!"
}
             */
            RatingResponse ratingResponse = new RatingResponse
            {
                Id = "79c2779e-dd2e-43e8-803d-ecbebed8972c",
                UserId = "cc20a6fb-a91f-4192-874d-132493685376",
                ProductId = "4c25613a-a3c2-4ef3-8e02-9c335eb23204",
                LocationName = "Sample ice cream shop",
                Timestamp = DateTime.Now,
                Rating = 5,
                UserNotes = "I love the subtle notes of orange in this ice cream!"
            };

            return new OkObjectResult(ratingResponse);
        }
    }
}
