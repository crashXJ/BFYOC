using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using System.Runtime.Serialization;

namespace FunctionApp1.Model
{
    /*
     
  "userId": "cc20a6fb-a91f-4192-874d-132493685376",
  "productId": "4c25613a-a3c2-4ef3-8e02-9c335eb23204",
  "locationName": "Sample ice cream shop",
  "rating": 5,
  "userNotes": "I love the subtle notes of orange in this ice cream!"
}
     */
    public class RatingRequest
    {
        [DataMember(Name = "userId")]
        public string UserId { get; set; }

        [DataMember(Name = "productId")]
        public string ProductId { get; set; }

        [DataMember(Name = "locationName")]
        public string LocationName { get; set; }

        [DataMember(Name = "rating")]
        public int Rating { get; set; }

        [DataMember(Name = "userNotes")]
        public string UserNotes { get; set; }


    }
}
