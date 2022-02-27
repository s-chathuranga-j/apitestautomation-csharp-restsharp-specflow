using System.Net;
using RestSharp;
using RestSharp.Serialization.Json;
using static ArvatoAPI.BaseAPI.Settings;

namespace ArvatoAPI.BaseAPI;

public class BaseAPITests
{
    public static RestClient Client;
    public static RestRequest Request;
    public static IRestResponse Response;
    
    /// <summary>
    /// Sets the base URL to the rest client
    /// </summary>
    public static void SetBaseUrl()
    {
        Client = new RestClient(BaseURL);
    }

    /// <summary>
    /// Sets the URI of the request where the request to directed to
    /// </summary>
    /// <param name="uri"></param>
    public static void SetRequestURI(string uri)
    {
        Request.Resource = "api/v3/validate/bank-account";
    }

    /// <summary>
    /// Maps the integer status code sent via the feature file to the HttpStatusCode
    /// and returns the HttpStatusCode accordingly
    /// </summary>
    /// <param name="statusCodeString"></param>
    /// <returns></returns>
    public static HttpStatusCode getStatusCode(int statusCodeString)
    {
        HttpStatusCode statusCode;
        switch (statusCodeString)
        {
            case 200: statusCode = HttpStatusCode.OK;
                break;
            case 400: statusCode = HttpStatusCode.BadRequest;
                break;
            case 401: statusCode = HttpStatusCode.Unauthorized;
                break;
            case 415: statusCode = HttpStatusCode.UnsupportedMediaType;
                break;
            case 405: statusCode = HttpStatusCode.MethodNotAllowed;
                break;
            default: statusCode = HttpStatusCode.OK;
                break;
        }
        return statusCode;
    }

    /// <summary>
    /// Deserializes the json response to a given POCO(Plain Old Csharp Object)
    /// and returns the relevant class instance
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T DeserializeResponse<T>()
    {
        JsonDeserializer jsonDeserializer = new JsonDeserializer();
        return jsonDeserializer.Deserialize<T>(Response);
    }
}