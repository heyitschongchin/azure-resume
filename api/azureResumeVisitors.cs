using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Api.Function
{
    public class azureResumeVisitors
    {
        private readonly ILogger<azureResumeVisitors> _logger;

        public azureResumeVisitors(ILogger<azureResumeVisitors> logger)
        {
            _logger = logger;
        }

        [Function("azureResumeVisitors")]
        [CosmosDBOutput("AzureResume", "Counter", Connection = "CosmosDbConnectionString")]
        public async Task<Counter> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
            [CosmosDBInput("AzureResume", "Counter", Connection = "CosmosDbConnectionString", Id = "1", PartitionKey = "1")] Counter counter)
        {
            _logger.LogInformation("Processing request...");

            counter.Count++;

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json; charset=utf-8");
            var jsonString = JsonSerializer.Serialize(new { id = counter.Id, count = counter.Count });
            await response.WriteStringAsync(jsonString);

            return counter; // This is what will be saved back to Cosmos DB
        }
    }

    public class Counter
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}