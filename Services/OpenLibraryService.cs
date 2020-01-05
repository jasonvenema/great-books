using GreatBooks.Interfaces;
using GreatBooks.Models;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GreatBooks.Services
{
    public class OpenLibraryService : IOpenLibraryService
    {
        private HttpClient _httpClient;

        private const string OPEN_LIBRARY_REQUEST_PATH = "api/books";
        private const string OPEN_LIBRARY_SEARCH_PATH = "search.json";

        public OpenLibraryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private async Task<string> GetHttpResponse(string fullUri)
        {
            var response = await _httpClient.GetAsync(fullUri);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            return null;
        }

        public async Task<Book> GetBookData(string isbn)
        {
            var requestUri = _httpClient.BaseAddress.ToString();
            var builder = new UriBuilder(requestUri);
            builder.Path = OPEN_LIBRARY_REQUEST_PATH;
            var fullUri = QueryHelpers.AddQueryString(builder.ToString(), new Dictionary<string, string>()
            {
                { "bibkeys", $"ISBN:{isbn}" },
                { "format", "json" },
                { "jscmd", "data" }
            });

            var response = await GetHttpResponse(fullUri);
            if (response != null)
            {
                return JsonConvert.DeserializeObject<Book>(response);
            }

            return null;
        }

        public async Task<SearchResult> GetSearchResult(string query)
        {
            var requestUri = _httpClient.BaseAddress.ToString();
            var uriBuilder = new UriBuilder(requestUri);
            uriBuilder.Path = OPEN_LIBRARY_SEARCH_PATH;
            var fullUri = QueryHelpers.AddQueryString(uriBuilder.ToString(), new Dictionary<string, string>()
            {
                { "q", query }
            });

            var response = await GetHttpResponse(WebUtility.UrlDecode(fullUri));
            if (response != null)
            {
                try
                {
                    var result = JsonConvert.DeserializeObject<SearchResult>(response);
                    result.Documents = result.Documents.Where(
                        d => d.PublishDate != null && (d.Isbn != null || d.Lccn != null));
                    return result;
                }
                catch (Exception ex)
                {
                    var x = ex.Message;
                    throw;
                }
            }

            return null;
        }
    }
}
