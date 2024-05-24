using System.Text.Json.Serialization;

namespace EbisconDemo.Services.Models
{
    public class RatingDto
    {

        [JsonPropertyName("rate")]
        public double Rate { get; set; }


        [JsonPropertyName("count")]
        public int Count { get; set; }

    }
}
