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
        private DateTime? _firstPublishDate;
        private IEnumerable<string> _publishDates;

        private const string COVER_URL_FORMAT = "http://covers.openlibrary.org/b/{2}/{0}-{1}.jpg";

        [JsonProperty(PropertyName = "title_suggest")]
        public string TitleSuggest { get; set; }

        [JsonProperty(PropertyName = "author_name")]
        public IEnumerable<string> AuthorName { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "first_publish_date")]
        public DateTime? FirstPublishDate
        {
            get { return _firstPublishDate; }
        }

        [JsonProperty(PropertyName = "publish_date")]
        public IEnumerable<string> PublishDate
        {
            get => _publishDates;
            set
            {
                _publishDates = value;
                _firstPublishDate = GetFirstPublishDate(_publishDates);
            }
        }


        private DateTime? GetFirstPublishDate(IEnumerable<string> pubDates)
        {
            DateTime first = DateTime.MaxValue;
            DateTime next;
            int year = default(int);
            int firstYear = Int32.MaxValue;

            foreach (var pd in pubDates)
            {
                if (DateTime.TryParse(pd, out next))
                {
                    if (DateTime.Compare(next, first) > 0)
                    {
                        first = next;
                    }
                }
                if (Int32.TryParse(pd, out year))
                {
                    if (year < firstYear)
                    {
                        year = firstYear;
                    }
                }
            }

            if (year != default(int))
            {
                first = new DateTime(year, 1, 1);
            }

            return first;
        }

        [JsonProperty(PropertyName = "isbn")]
        public IEnumerable<string> Isbn { get; set; }

        [JsonProperty(PropertyName = "lccn")]
        public IEnumerable<string> Lccn { get; set; }

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
            return PopulateCoverUrls(coverSizeIndicator);
        }

        private IEnumerable<string> PopulateCoverUrls(string coverSizeIndicator)
        {
            if (this.Isbn != null)
            {
                return GetCoverUrlsFromIsbn(coverSizeIndicator);
            }
            else if (this.Lccn != null)
            {
                return GetCoverUrlsFromLccn(coverSizeIndicator);
            }

            return new List<string>();
        }

        private IEnumerable<string> GetCoverUrlsFromLccn(string coverSizeIndicator)
        {
            List<string> covers = new List<string>();
            foreach (var i in this.Lccn)
            {
                covers.Add(String.Format(COVER_URL_FORMAT, i, coverSizeIndicator, "lccn"));
            }
            return covers;
        }

        private IEnumerable<string> GetCoverUrlsFromIsbn(string coverSizeIndicator)
        {
            List<string> covers = new List<string>();
            var isbn13 = this.Isbn.Where(i => i.Length == 13);

            foreach (var i in isbn13)
            {
                covers.Add(String.Format(COVER_URL_FORMAT, i, coverSizeIndicator, "isbn"));
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
