using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using SmartGoals.Models;
using SmartGoals.Services;

namespace SmartGoals
{
    public class Objectives
    {
        private readonly ILogger<Goals> _logger;

        private readonly ObjectivesService _objectivesService;

        public Objectives(ILogger<Goals> log)
        {
            _logger = log;
            _objectivesService = new ObjectivesService();
        }

        [FunctionName("ObjectivesAdd")]
        [OpenApiOperation(operationId: "Add", tags: new[] { "Objectives" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody("json", typeof(ObjectiveAddModel))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> ObjectiveAdd(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Objectives/Add")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<ObjectiveAddModel>(requestBody);

            _objectivesService.Add(data);

            return new OkObjectResult("The objective was added successfully!");
        }
        
        [FunctionName("ObjectivesUpdate")]
        [OpenApiOperation(operationId: "Update", tags: new[] { "Objectives" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody("json", typeof(ObjectiveModel))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> ObjectiveUpdate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "Objectives/Update")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
        
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<ObjectiveModel>(requestBody);
            
            _objectivesService.Update(data.Id.ToString(), data);
        
            return new OkObjectResult("The objective was updated successfully!");
        }

        [FunctionName("ObjectivesUpdateIsFinished")]
        [OpenApiOperation(operationId: "UpdateIsFinished", tags: new[] { "Objectives" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(Guid), Description = "The **Id** parameter")]
        [OpenApiParameter(name: "isFinished", In = ParameterLocation.Query, Required = true, Type = typeof(bool), Description = "The **IsFinished** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> ObjectiveUpdateIsFinished(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "Objectives/UpdateIsFinished")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string id = req.Query["Id"];
            string isFinished = req.Query["IsFinished"];

            _objectivesService.UpdateIsFinished(id, isFinished);

            return new OkObjectResult("The objective was updated successfully!");
        }

        [FunctionName("ObjectivesDelete")]
        [OpenApiOperation(operationId: "Delete", tags: new[] { "Objectives" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(Guid), Description = "The **Id** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> ObjectiveDelete(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "Objectives/Delete")] HttpRequest req)
        {
            _logger.LogInformation("Goals Delete was triggered!");
        
            string id = req.Query["Id"];
        
            _objectivesService.Delete(id);
                
            return new OkObjectResult("The objective was deleted successfully");
        }
    }
}
