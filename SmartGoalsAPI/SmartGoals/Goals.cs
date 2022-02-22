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
    public class Goals
    {
        private readonly ILogger<Goals> _logger;

        private readonly GoalsService _goalsService;

        public Goals(ILogger<Goals> log)
        {
            _logger = log;
            _goalsService = new GoalsService();
        }

        [FunctionName("GoalsGetAll")]
        [OpenApiOperation(operationId: "GetAll", tags: new[] { "Goals" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> GetAll(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Goals/GetAll")] HttpRequest req)
        {
            _logger.LogInformation("Goals GetAll was triggered!");

            var goals = _goalsService.GetAll();

            return new OkObjectResult(goals);
        }

        [FunctionName("GoalsGetById")]
        [OpenApiOperation(operationId: "GetById", tags: new[] { "Goals" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(Guid), Description = "The **Id** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> GetById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Goals/GetById")] HttpRequest req)
        {
            _logger.LogInformation("Goals GetById was triggered!");

            string id = req.Query["Id"];

            var result = _goalsService.GetById(id);

            return new OkObjectResult(result);
        }

        [FunctionName("GoalsAdd")]
        [OpenApiOperation(operationId: "Add", tags: new[] { "Goals" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody("json", typeof(GoalAddModel))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Add(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Goals/Add")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<GoalAddModel>(requestBody);

            _goalsService.AddGoal(data);

            return new OkObjectResult("The goal was added successfully!");
        }

        [FunctionName("GoalsUpdate")]
        [OpenApiOperation(operationId: "Update", tags: new[] { "Goals" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody("json", typeof(GoalModel))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Update(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "Goals/Update")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<GoalModel>(requestBody);
            
            _goalsService.UpdateGoal(data.Id.ToString(), data);

            return new OkObjectResult("The goal was updated successfully!");
        }

        [FunctionName("GoalsDelete")]
        [OpenApiOperation(operationId: "Delete", tags: new[] { "Goals" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(Guid), Description = "The **Id** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Delete(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "Goals/Delete")] HttpRequest req)
        {
            _logger.LogInformation("Goals Delete was triggered!");

            string id = req.Query["Id"];

            _goalsService.DeleteGoal(id);
                
            return new OkObjectResult("The goal was deleted successfully!");
        }
    }
}
