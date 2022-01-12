using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace FunctionApp1.Model
{
    public class RatingResponse
    {
        /*
         {
  "id": "79c2779e-dd2e-43e8-803d-ecbebed8972c",
  "userId": "cc20a6fb-a91f-4192-874d-132493685376",
  "productId": "4c25613a-a3c2-4ef3-8e02-9c335eb23204",
  "timestamp": "2018-05-21 21:27:47Z",
  "locationName": "Sample ice cream shop",
  "rating": 5,
  "userNotes": "I love the subtle notes of orange in this ice cream!"
}
         */
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "userId")]
        public string UserId { get; set; }
        [DataMember(Name = "productId")]
        public string ProductId { get; set; }

        [DataMember(Name = "timestamp")]
        public DateTime Timestamp { get; set; }
        [DataMember(Name = "locationName")]
        public string LocationName { get; set; }
        [DataMember(Name = "rating")]
        public int Rating { get; set; }
        [DataMember(Name = "userNotes")]
        public string UserNotes { get; set; }
    }
}
