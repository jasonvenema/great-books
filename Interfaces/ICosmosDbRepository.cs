using GreatBooks.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreatBooks.Interfaces
{
    public interface ICosmosDbRepository<T> where T : Entity
    {
        Task<IEnumerable<T>> GetItemsAsync(string query);

        Task<T> GetItemAsync(string id);

        Task AddItemAsync(T item);

        Task UpdateItemAsync(T item);

        Task DeleteItemAsync(T item);
    }
}