using GreatBooks.Interfaces;
using GreatBooks.Models;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatBooks.Data
{
    public class BookRepository : CosmosDbRepository<Book>, IBookRepository
    {
        public BookRepository(
            CosmosClient dbClient,
            string databaseName,
            string containerName) : base(dbClient, databaseName, containerName)
        { }


        public override PartitionKey? ResolvePartitionKey(string id) => new PartitionKey(id);
    }
}
