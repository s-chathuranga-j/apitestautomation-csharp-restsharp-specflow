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
    
    public static void SetBaseUrl()
    {
        Client = new RestClient(BaseURL);
    }

    public static void SetRequestURI(string uri)
    {
        Request.Resource = "api/v3/validate/bank-account";
    }

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

    public static T DeserializeResponse<T>()
    {
        JsonDeserializer jsonDeserializer = new JsonDeserializer();
        return jsonDeserializer.Deserialize<T>(Response);
    }
}