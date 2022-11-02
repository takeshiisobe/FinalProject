using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharpClient.DataModels
{
    public partial class TokenUser
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

    }

    public partial class Token1
    {
        [JsonProperty("token")]
        public string Token { get; set; }


    }


}
