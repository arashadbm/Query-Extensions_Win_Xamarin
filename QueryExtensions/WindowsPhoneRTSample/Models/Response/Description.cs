using Newtonsoft.Json;

namespace WindowsPhoneRTSample.Models.Response
{
    public class Description
    {
        [JsonProperty("_content")]
        public string Content { get; set; }
    }
}