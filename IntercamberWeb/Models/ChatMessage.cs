using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace CML.Intercamber.Web.Models
{
    public class ChatMessage
    {
        [JsonProperty(PropertyName = "msg")]
        public string Message { get; set;  }

        [JsonProperty(PropertyName = "usr")]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "d")]
        public string Date { get; set; }

    }
}