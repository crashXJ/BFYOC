using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FunctionApp1.Model;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using System.Web.Http;

namespace FunctionApp1
{
    public static class CreateRating
    {
        [FunctionName("CreateRating")]
        public static async Task<object> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string responseMessage = "";
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string prodId = data?.productId;
            string userId = data?.userId;
            int ratingNumber = data?.rating;
            if (ratingNumber <0 || ratingNumber > 5)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            if (!string.IsNullOrWhiteSpace(prodId))
            {

                string productCheckUrl = "https://serverlessohapi.azurewebsites.net/api/GetProduct?productId=" + prodId;
                string prodResponse = BaseService.GetResponseText(productCheckUrl);
                if (string.IsNullOrEmpty(prodResponse) || prodResponse.Contains("error"))
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }
                else
                {
                    responseMessage = prodResponse;
                }
            }

            if (!string.IsNullOrWhiteSpace(userId))
            {

                string userCheckUrl = "https://serverlessohapi.azurewebsites.net/api/GetUser?userId=" + userId;
                string userResponse = BaseService.GetResponseText(userCheckUrl);
                if (!string.IsNullOrWhiteSpace(userResponse))
                    responseMessage += string.IsNullOrEmpty(userResponse) || userResponse.Contains("error")
                        ? "User not found."
                        : $"{userResponse}";
            }

            string ratingId = Guid.NewGuid().ToString();
            DateTime currentTime = DateTime.Now;

            var ratingRequest = new RatingRequest()
            {
                UserId = userId,
                ProductId = prodId,
                LocationName = data?.locationName,
                Rating = ratingNumber,
                UserNotes = data?.userNotes
            };

            var ratingResponse = new RatingResponse
            {
                Id = ratingId,
                UserId = ratingRequest.UserId,
                ProductId = ratingRequest.ProductId,
                UserNotes = ratingRequest.UserNotes,
                Timestamp = currentTime,
                LocationName = ratingRequest.LocationName,
                Rating = ratingRequest.Rating

            };


            return new OkObjectResult(ratingResponse);
        }
    }


}
