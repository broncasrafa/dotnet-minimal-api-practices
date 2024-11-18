using Newtonsoft.Json;

namespace Api.Models.DTO.ApiResult;


/// <summary>
/// Classe padrão para retorno dos dados da api
/// </summary>
/// <typeparam name="T">objeto para retornar</typeparam>
[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
public class ApiResponse<T>
{
    private readonly T _value;


    [JsonProperty(Order = -3)]
    public bool Succeeded { get; }

    [JsonProperty(Order = -2)]
    public T Data
    {
        get
        {
            return _value!;
        }

        private init => _value = value;
    }

    [JsonProperty(Order = -1)]
    public List<string> Errors { get; }



    private ApiResponse(T value)
    {
        Data = value;
        Succeeded = true;
        Errors = null;
    }
    private ApiResponse(string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(errorMessage))
            throw new ArgumentException("invalid error", nameof(errorMessage));

        Succeeded = false;
        Errors = new List<string> { errorMessage };
    }


    public static ApiResponse<T> Success(T value) => new ApiResponse<T>(value);
    public static ApiResponse<T> Failure(string error) => new ApiResponse<T>(error);
}
