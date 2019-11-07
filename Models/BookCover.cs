using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatBooks.Models
{
    public class BookCover
    {
        [JsonProperty(PropertyName = "small")]
        public string Small { get; set; }

        [JsonProperty(PropertyName = "medium")]
        public string Medium { get; set; }

        [JsonProperty(PropertyName = "large")]
        public string Large { get; set; }
    }
}
