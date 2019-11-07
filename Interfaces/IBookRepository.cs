using GreatBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreatBooks.Interfaces;

namespace GreatBooks.Interfaces
{
    public interface IBookRepository : ICosmosDbRepository<Book>
    {
    }
}
