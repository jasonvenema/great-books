using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatBooks.Models
{
    public class BookIdentifiers
    {
        [JsonProperty(PropertyName = "lccn")]
        public IEnumerable<string> LCCN { get; set; }

        [JsonProperty(PropertyName = "openlibrary")]
        public IEnumerable<string> OpenLibrary { get; set; }

        [JsonProperty(PropertyName = "isbn_10")]
        public IEnumerable<string> ISBN10 { get; set; }

        [JsonProperty(PropertyName = "wikidata")]
        public IEnumerable<string> WikiData { get; set; }

        [JsonProperty(PropertyName = "goodreads")]
        public IEnumerable<string> GoodReads { get; set; }

        [JsonProperty(PropertyName = "libraryanything")]
        public IEnumerable<string> LibraryAnything { get; set; }
    }
}
