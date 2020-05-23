using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AzureServerLessFunctionApp
{
    public static class Fileprocessor
    {
        [FunctionName("Fileprocessor")]
        [return : Table("vehicle")]        
        public static Vehicle Run([TimerTrigger("0 */2 * * * *")]TimerInfo myTimer,[Blob("vehicles/input.txt", FileAccess.Read, Connection = "AzureWebJobsStorage")] Stream stream,  ILogger log)
        {
            StreamReader reader = new StreamReader(stream);
            JObject jObject = JsonConvert.DeserializeObject<JObject>(reader.ReadToEnd());

            return new Vehicle() { data = jObject };
        }
    }

    public class Vehicle 
    {
        public Vehicle()
        {
            this.PartitionKey = new Guid().ToString();
            this.RowKey = "maruti";
        }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }

        public JObject data { get; set; }
    }
}
