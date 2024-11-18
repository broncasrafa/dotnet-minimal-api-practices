using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Validations.Filters;

public class ValidationProblemDetails : ProblemDetails
{
    [JsonProperty(Order = 4)]
    public List<string> Errors { get; set; } = new List<string>();
}
