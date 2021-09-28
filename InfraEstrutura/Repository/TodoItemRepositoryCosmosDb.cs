using InfraEstrutura.Model;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraEstrutura.Repository
{
    public class TodoItemRepositoryCosmosDb
    {
        private string ConnectionString = "AccountEndpoint=https://todo-cosmo-db.documents.azure.com:443/;AccountKey=lEBsosc4z88RgHyfYutb6wYMF5y4BhfBNDadz6LegRCuBgDzCfcPviOvtCSex0L55VwDKSMGABuJsfVog4a9MA==;";
        private string Container = "todo-container";
        private string Database = "todo-db-amigo";

        private CosmosClient CosmosClient { get; set; }

        public TodoItemRepositoryCosmosDb()
        {
            this.CosmosClient = new CosmosClient(this.ConnectionString);
        }

        public List<TodoItem> GetAll()
        {
            var container = this.CosmosClient.GetContainer(Database, Container);

            QueryDefinition queryDefinition = new QueryDefinition("SELECT * FROM c");

            var result = new List<TodoItem>();

            var queryResult = container.GetItemQueryIterator<TodoItem>(queryDefinition);

            while (queryResult.HasMoreResults)
            {
                FeedResponse<TodoItem> currentResultSet = queryResult.ReadNextAsync().Result;
                result.AddRange(currentResultSet.Resource);
            }

            return result;

        }

        public TodoItem GetById(string id)
        {
            var container = this.CosmosClient.GetContainer(Database, Container);

            QueryDefinition queryDefinition = new QueryDefinition($"SELECT * FROM c where c.id = '{id}'");

            var queryResult = container.GetItemQueryIterator<TodoItem>(queryDefinition);

            TodoItem item = null;

            while (queryResult.HasMoreResults)
            {
                FeedResponse<TodoItem> currentResultSet = queryResult.ReadNextAsync().Result;
                item = currentResultSet.Resource.FirstOrDefault();
            }

            return item;
        }

        public async Task Save(TodoItem item)
        {
            var container = this.CosmosClient.GetContainer(Database, Container);
            await container.CreateItemAsync<TodoItem>(item, new PartitionKey(item.PartitionKey));
        }

        public async Task Update(TodoItem item)
        {
            var container = this.CosmosClient.GetContainer(Database, Container);
            await container.ReplaceItemAsync<TodoItem>(item, item.Id.ToString(), new PartitionKey(item.PartitionKey));
        }

        public async Task Delete(TodoItem item)
        {
            var container = this.CosmosClient.GetContainer(Database, Container);
            await container.DeleteItemAsync<TodoItem>(item.Id.ToString(), new PartitionKey(item.PartitionKey));
        }
    }
}
