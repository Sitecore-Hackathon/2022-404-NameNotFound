using Newtonsoft.Json;
using System;

namespace BasicCompany.Feature.BasicContent.Models.Json
{
    [Serializable]
    public class BaseResultJson
    {
        [JsonProperty("Status")]
        public bool Status { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("RedirectUrl")]
        public string RedirectUrl { get; set; }

        [JsonProperty("Id")]
        public string Id { get; set; }
    }
}