
using EbisconDemo.Data.Models;
using System.Text.Json.Serialization;

namespace EbisconDemo.Services.Models
{
    public class ProductDto
    {
        public ProductDto()
        {
            Rating = new RatingDto();
        }

        [JsonIgnore]
        public string SourceName { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("category")]
        public string Category { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }


        [JsonPropertyName("rating")]
        public RatingDto Rating { get; set; }
    }
}
