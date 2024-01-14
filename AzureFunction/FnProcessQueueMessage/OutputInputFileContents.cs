using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
namespace FnProcessQueueMessage
{
    public static class OutputInputFileContents
    {
        [FunctionName("OutputInputFileContents")]
        public static async Task<HttpResponseMessage> Run([QueueTrigger("input-items", Connection = "AzureWebJobsStorage")]string myQueueItem, 
            [Blob("generic-himacs-input-file/{queueTrigger}", FileAccess.Read,Connection = "AzureWebJobsStorage")] Stream inputFile
            ,ILogger log)
        {
                StreamReader reader = new StreamReader(inputFile);
                DataTable dt = CsvProfile.ConvertCSVtoDataTable(reader);
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(dt, Formatting.Indented), Encoding.UTF8, "application/json")
                };   
        }
    }
    
}
