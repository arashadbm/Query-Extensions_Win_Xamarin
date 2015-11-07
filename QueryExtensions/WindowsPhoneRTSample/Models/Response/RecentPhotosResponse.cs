using Newtonsoft.Json;

namespace WindowsPhoneRTSample.Models.Response
{

    public class RecentPhotosResponse
    {
        [JsonProperty("photos")]
        public Photos Photos { get; set; }
        [JsonProperty("stat")]
        public string Stat { get; set; }
    }
}
