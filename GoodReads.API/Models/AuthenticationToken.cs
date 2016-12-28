using Newtonsoft.Json;
using System;

namespace GoodReads.API.Models
{
    [Serializable]
    [JsonObject]
    public class AuthenticationToken
    {
        public string Key { get; set; }
        public string Secret { get; set; }
    }
}
