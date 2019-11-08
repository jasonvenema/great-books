using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GreatBooks.Models
{
    public class Book : Entity
    {
        [JsonProperty(PropertyName = "key")]
        public override string Id { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "subtitle")]
        public string Subtitle { get; set; }

        [JsonProperty(PropertyName = "authors")]
        public IEnumerable<BookAuthor> Authors { get; set; }

        [JsonProperty(PropertyName = "identifiers")]
        public BookIdentifiers Identifiers { get; set; }

        [JsonProperty(PropertyName = "subjects")]
        public IEnumerable<BookSubject> Subjects { get; set; }

        [JsonProperty(PropertyName = "publishers")]
        public IEnumerable<BookPublisher> Publishers { get; set; }

        [JsonProperty(PropertyName = "publish_date")]
        public string PublishDate { get; set; }

        [JsonProperty(PropertyName = "excerpts")]
        public IEnumerable<BookExcerpt> Excerpts { get; set; }

        [JsonProperty(PropertyName = "cover")]
        public BookCover Cover { get; set; }

        [JsonProperty(PropertyName = "number_of_pages")]
        public int NumberOfPages { get; set; }

        [JsonProperty(PropertyName = "weight")]
        public string Weight { get; set; }
    }
}
