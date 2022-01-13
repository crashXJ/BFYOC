using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BFYOC
{
    public static class CreateItem
    {
        [FunctionName("CreateItem")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(databaseName: "BFYOC",collectionName: "Data-Container", 
            ConnectionStringSetting = "CosmosDbConnectionString")]
            IAsyncCollector<dynamic> documentsOut, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string userId = req.Query["userId"];
            string productId = req.Query["productId"];
            string locationName = req.Query["locationName"];
            string rating = req.Query["rating"];
            string userNotes = req.Query["userNotes"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            userId = userId ?? data?.userId;
            productId = productId ?? data?.productId;
            locationName = locationName ?? data?.locationName;
            rating = rating ?? data?.rating;
            userNotes = userNotes ?? data?.userNotes;

            if (!string.IsNullOrEmpty(userId))
            {
                // Add a JSON document to the output container.
                await documentsOut.AddAsync(new
                {
                    // create a random ID
                    id = System.Guid.NewGuid().ToString(),
                    userId = userId,
                    productId = productId,
                    locationName = locationName,
                    rating = rating,
                    userNotes = userNotes
                });
            }   

            string responseMessage = string.IsNullOrEmpty(userId)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {userId}. This HTTP triggered function executed successfully.  You selected this Product: {productId} from this location: {locationName}.";

            return new OkObjectResult(responseMessage);
        }
    }
}
