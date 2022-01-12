using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace FunctionApp1.Model
{
    public class RatingsResponse
    {
        public List<RatingResponse> Ratings { get; set; }
    }
}
