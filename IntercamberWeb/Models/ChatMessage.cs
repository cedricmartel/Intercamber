using System;
using Newtonsoft.Json;

namespace CML.Intercamber.Web.Models
{
    public class ChatMessage
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "msg")]
        public string Message { get; set;  }

        [JsonProperty(PropertyName = "usr")]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "idu")]
        public long IdUser { get; set; }

        [JsonProperty(PropertyName = "d")]
        public DateTime Date { get; set; }

    }
}