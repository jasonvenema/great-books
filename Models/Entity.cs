using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatBooks.Models
{
    public class Entity
    {
        /// <summary>
        /// Entity identifier
        /// </summary>
        /// <example>5fe3fc2a-cbac-4df0-8031-fdca0f682989</example>
        [JsonProperty(PropertyName = "id")]
        public virtual string Id { get; set; }
    }
}
