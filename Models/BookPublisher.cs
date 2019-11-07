using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatBooks.Models
{
    public class BookPublisher
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
