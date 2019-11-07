using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreatBooks.Interfaces;
using GreatBooks.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;

namespace GreatBooks.Interfaces
{
    public abstract class CosmosDbRepository<T> : ICosmosDbRepository<T> where T : Entity
    {
        protected Container _container;

        public CosmosDbRepository(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(T item)
        {
            await this._container.CreateItemAsync<T>(item, ResolvePartitionKey(item.Id).Value);
        }

        public async Task DeleteItemAsync(T item)
        {
            await this._container.DeleteItemAsync<T>(item.Id, ResolvePartitionKey(item.Id).Value);
        }

        public async Task<T> GetItemAsync(string id)
        {
            ItemResponse<T> response = await this._container.ReadItemAsync<T>(id, ResolvePartitionKey(id).Value);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            return response.Resource;
        }

        public async Task<IEnumerable<T>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(T item)
        {
            await this._container.UpsertItemAsync<T>(item, ResolvePartitionKey(item.Id).Value);
        }

        public virtual PartitionKey? ResolvePartitionKey(string key) => null;
    }
}