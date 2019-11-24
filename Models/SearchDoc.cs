using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatBooks.Models
{
    public class SearchDoc
    {
        private const string COVER_URL_ISBN_FORMAT = "http://covers.openlibrary.org/b/isbn/{0}-{1}.jpg";

        [JsonProperty(PropertyName = "title_suggest")]
        public string TitleSuggest { get; set; }

        [JsonProperty(PropertyName = "author_name")]
        public IEnumerable<string> AuthorName { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "publish_date")]
        public IEnumerable<string> PublishDate { get; set; }

        [JsonProperty(PropertyName = "isbn")]
        public IEnumerable<string> Isbn { get; set; }

        [JsonProperty(PropertyName = "coverUrl")]
        public IEnumerable<string> CoverUrl
        {
            get
            {
                return GetCoverUrls(CoverSize.Small);
            }
        }

        public IEnumerable<string> GetCoverUrls(CoverSize size)
        {
            var coverSizeIndicator = GetCoverSizeIndicator(size);
            var covers = new List<string>();
            var isbn13 = this.Isbn.Where(i => i.Length == 13);

            foreach (var i in isbn13)
            {
                covers.Add(String.Format(COVER_URL_ISBN_FORMAT, i, coverSizeIndicator));
            }
            return covers;
        }

        private string GetCoverSizeIndicator(CoverSize size)
        {
            if (size == CoverSize.Large)
            {
                return "L";
            }
            else if (size == CoverSize.Medium)
            {
                return "M";
            }
            else
            {
                return "S";
            }
        }
    }
}
