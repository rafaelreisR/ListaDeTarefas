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

namespace FunctionAzure
{
    public static class DeleteFunction
    {
        [FunctionName("DeleteFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            Guid id = new Guid(req.Query["id"]);

            var repository = new TodoItemRepositoryCosmosDb();

            var todo = repository.GetById(id.ToString());

            repository.Delete(todo);

            if (todo == null)
                return new NotFoundObjectResult(new { message = "Deletado" });

            return new OkObjectResult(todo);
        }
    }
}
