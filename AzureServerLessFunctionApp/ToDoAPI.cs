using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace AzureServerLessFunctionApp
{
    public static class ToDoAPI
    {
        static List<TodoItem> items = new List<TodoItem>();

        [FunctionName("Create")]
        public static async Task<IActionResult> Create(
            [HttpTrigger(AuthorizationLevel.Function, "post" , Route = "todo")] HttpRequest req,
            ILogger log)
        {
            string reqBody = await new StreamReader(req.Body).ReadToEndAsync().ConfigureAwait(false);
            var input = JsonConvert.DeserializeObject<ToDoCreateModel>(reqBody);

            TodoItem newItem = new TodoItem(input.Description);
            newItem.Id = Guid.NewGuid().ToString();
            newItem.IsCompleted = false;
            items.Add(newItem);

            return new OkObjectResult(newItem);

        }

        [FunctionName("Display")]
        public static async Task<IActionResult> Display(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "todo")] HttpRequest req,
    ILogger log)
        {
            return new OkObjectResult(items);
        }

        [FunctionName("GetItemById")]
        public static async Task<IActionResult> GetItemById(
[HttpTrigger(AuthorizationLevel.Function, "get", Route = "todo/{id}")] HttpRequest req, string id,
ILogger log)
        {
            var currentItem = items.Where(x => x.Id == id).FirstOrDefault();

            if(currentItem == null)
            {
                return new NotFoundObjectResult($"Item not present with id {id}");
            }

            return new OkObjectResult(currentItem);

        }
    }
}
