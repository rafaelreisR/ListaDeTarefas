using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using InfraEstrutura.Repository;
using InfraEstrutura.Model;

namespace FunctionAzure
{
    public static class PutFunction
    {
        [FunctionName("PutFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            TodoItem dataToUpdate = JsonConvert.DeserializeObject<TodoItem>(requestBody);

            var repository = new TodoItemRepositoryCosmosDb();

            await repository.Update(dataToUpdate);

            return new OkObjectResult(dataToUpdate);
        }
    }
}