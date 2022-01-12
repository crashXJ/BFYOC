using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace FunctionApp1.Model
{
    public class UsersResponse
    {
        public List<UserResponse> Users { get; set; }
    }
    /*
     * {"userId":"cc5581ff-6be1-4418-a8d8-55a29c24b995","userName":"garry.thornburg","fullName":"Garry Thornburg"}
     */
    public class UserResponse
    {
        [DataMember(Name = "userId")]
        public string UserId { get; set; }

        [DataMember(Name = "userId")]
        public string UserName { get; set; }

        [DataMember(Name = "fullName")]
        public string FullName { get; set; }
    }
}
