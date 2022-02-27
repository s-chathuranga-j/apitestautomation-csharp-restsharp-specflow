using System.Net;
using APITAP.DataEntities;
using ArvatoAPI.BaseAPI;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using static ArvatoAPI.BaseAPI.BaseAPITests;
namespace ArvatoAPI.Steps;

[Binding]
public class CheckAccountValidityStepDefinitions
{
    private string AuthType;
    private string AuthKey;

    [Then(@"the response returns with responseCode (.*)")]
    public void ThenTheResponseReturnsWithResponseCode(int responseCode)
    {
        Assert.That(Response.StatusCode, Is.EqualTo(getStatusCode(responseCode)));
    }

    [When(@"the request posted with bankAccount (.*)")]
    public void WhenTheRequestSentWithBankAccount(string accountNumber)
    {
        Request.AddHeader(AuthType, AuthKey);
        JsonObject body = new JsonObject {{"bankAccount", accountNumber}};
        Request.RequestFormat = DataFormat.Json;
        Request.AddBody(JsonConvert.SerializeObject(body));
        Response = Client.Post(Request);
    }

    [Then(@"isValid as (.*)")]
    public void ThenIsValidAs(string isValid)
    {
        if (Response.StatusCode == HttpStatusCode.OK)
        {
            var deserializeResponse = DeserializeResponse<AccountStatusSuccessResponse>();
            Assert.That(deserializeResponse.IsValid.ToString(), Is.EqualTo(isValid));
        }
        else
        {
            Assert.True(true);
        }
    }

    [Given(@"the contentType is (.*)")]
    public void GivenTheContentTypeIs(string contentType)
    {
        if (contentType == "null")
            contentType = null;
        SetBaseUrl();
        Request = new RestRequest();
        SetRequestURI(Settings.BankAccountValidationURI);
        Request.AddHeader("Content-Type", contentType);
    }

    [Given(@"the authKey is (.*)")]
    public void GivenTheAuthKeyIs(string authKey)
    {
        if (authKey == "null")
            AuthKey = null;
        else
            AuthKey = authKey;
    }

    [Given(@"the authType is (.*)")]
    public void GivenTheAuthTypeIs(string authType)
    {
        AuthType = authType;
    }
    
    [When(@"the request is posted")]
    public void WhenTheRequestIsSent()
    {
        Request.AddHeader(AuthType, AuthKey);
        Response = Client.Post(Request);
    }

    [When(@"the request is posted without headers and bankAccount (.*)")]
    public void WhenTheRequestIsSentWithoutHeadersAndBankAccount(string accountNumber)
    {
        JsonObject body = new JsonObject {{"bankAccount", accountNumber}};
        Request.RequestFormat = DataFormat.Json;
        Request.AddBody(JsonConvert.SerializeObject(body));
        Response = Client.Post(Request);
    }

    [Then(@"the response message is (.*)")]
    public void ThenTheResponseMessageIs(string responseMessage)
    {
        if (responseMessage == "null")
            responseMessage = null;
        
        if (Response.StatusCode == HttpStatusCode.OK)
        {
            if (responseMessage != null)
            {
                var deserializeResponse = DeserializeResponse<AccountStatusSuccessResponse>();
                Assert.True(deserializeResponse.RiskCheckMessages[0].Message.Contains(responseMessage));
            }
            else
            {
                Assert.True(true);
            }
        }
        else if(Response.StatusCode == HttpStatusCode.BadRequest)
        {
            if (responseMessage != null)
            {
                List<AccountStatusErrorResponse> responseList;
                var deserializeResponse = DeserializeResponse<List<AccountStatusErrorResponse>>();
                Assert.True(deserializeResponse[0].Message.Contains(responseMessage));
            }
            else
            {
                Assert.True(true);
            }
        }
        else if(Response.StatusCode == HttpStatusCode.Unauthorized)
        {
            if (responseMessage != null)
            {
                var deserializeResponse = DeserializeResponse<AccountStatusErrorResponse>();
                Assert.True(deserializeResponse.Message.Contains(responseMessage));
            }
            else
            {
                Assert.True(true);
            }
        }
    }

    [Then(@"the response type is (.*)")]
    public void ThenTheResponseTypeIs(string type)
    {
        var deserializeResponse = DeserializeResponse<AccountStatusSuccessResponse>();
        Assert.That(deserializeResponse.RiskCheckMessages[0].Type.ToString(), Is.EqualTo(type));
    }

    [Then(@"the response code contains (.*)")]
    public void ThenTheResponseCodeContains(string responseCode)
    {
        var deserializeResponse = DeserializeResponse<AccountStatusSuccessResponse>();
        Assert.True(deserializeResponse.RiskCheckMessages[0].Code.Contains(responseCode));
    }

    [Then(@"the response actionCode is (.*)")]
    public void ThenTheResponseActionCodeIs(string actionCode)
    {
        var deserializeResponse = DeserializeResponse<AccountStatusSuccessResponse>();
        Assert.That(deserializeResponse.RiskCheckMessages[0].ActionCode, Is.EqualTo(actionCode));
    }

    [Then(@"the response fieldReference is (.*)")]
    public void ThenTheResponseFieldReferenceIs(string fieldReference)
    {
        var deserializeResponse = DeserializeResponse<AccountStatusSuccessResponse>();
        Assert.That(deserializeResponse.RiskCheckMessages[0].FieldReference, Is.EqualTo(fieldReference));
    }
}