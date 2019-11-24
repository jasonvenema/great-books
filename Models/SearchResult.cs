using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatBooks.Models
{
    public enum CoverSize
    {
        Small,
        Medium,
        Large
    }

    public class SearchResult
    {
        [JsonProperty(PropertyName = "numFound")]
        public int NumFound { get; set; }

        [JsonProperty(PropertyName = "docs")]
        public IEnumerable<SearchDoc> Documents { get; set; }
    }
}
