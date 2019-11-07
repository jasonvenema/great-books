using GreatBooks.Interfaces;
using GreatBooks.Models;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GreatBooks.Services
{
    public class OpenLibraryService : IOpenLibraryService
    {
        private HttpClient _httpClient;
        private string _requestUri;

        public OpenLibraryService(string requestUri)
        {
            _httpClient = new HttpClient();
            _requestUri = requestUri;
        }

        public async Task<Book> GetBookData(string isbn)
        {
            var fullUri = QueryHelpers.AddQueryString(_requestUri, new Dictionary<string, string>()
            {
                { "bibkeys", $"ISBN:{isbn}" },
                { "format", "json" },
                { "jscmd", "data" }
            });

            var response = await _httpClient.GetAsync(fullUri);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());
            }

            return null;
        }
    }
}
