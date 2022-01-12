using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace FunctionApp1.Model
{
    public class ProductsResponse
    {
        public List<ProductResponse> Products { get; set; }
    }

    /*
     * {"productId":"75542e38-563f-436f-adeb-f426f1dabb5c","productName":"Starfruit Explosion","productDescription":"This starfruit ice cream is out of this world!"}
     */
    public class ProductResponse
    {
        [DataMember(Name = "productId")]
        public string ProductId { get; set; }

        [DataMember(Name = "productName")]
        public string ProductName { get; set; }

        [DataMember(Name = "productDescription")]
        public string ProductDescription { get; set; }

    }
}
