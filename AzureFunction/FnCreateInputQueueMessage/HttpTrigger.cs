using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Data;

namespace FnCreateInputQueueMessage
{
    public static class HttpTrigger
    {
        [FunctionName("HttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            string name = req.Query["itemName"];
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.itemName;


            BlobProfile blobObj = new BlobProfile("generic-himacs-input-file", name);
            StreamReader reader= await blobObj.ListBlobsAsync();
            DataTable dt = CsvProfile.ConvertCSVtoDataTable(reader);
            return (ActionResult)new OkObjectResult("{ \"items\" :" +JsonConvert.SerializeObject(dt)+"}");
        }
    }
}