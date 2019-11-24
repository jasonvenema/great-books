using GreatBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatBooks.Interfaces
{
    public interface IOpenLibraryService
    {
        Task<Book> GetBookData(string isbn);

        Task<SearchResult> GetSearchResult(string query);
    }
}
