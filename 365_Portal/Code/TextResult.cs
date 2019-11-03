using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

/// <summary>
/// Summary description for TextResult
/// </summary>
public class APIResult : IHttpActionResult
{
    string _value;
    HttpRequestMessage _request;

    public APIResult(IEnumerable<object> result, HttpRequestMessage request)
    {
        _value = JsonConvert.SerializeObject(result);
        _request = request;
    }

    public APIResult(HttpRequestMessage request, string result)
    {
        _value = result;
        _request = request;
    }


    public APIResult(object result, HttpRequestMessage request)
    {
        _value = JsonConvert.SerializeObject(result);
        _request = request;
    }

    public static void AuthenticationFailed()
    {
        HttpContext.Current.Session["UserId"] = null;
        HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.ProxyAuthenticationRequired);
        msg.Content = new StringContent("Authentication Failed");
        throw new HttpResponseException(msg);
    }

    public static void InvalidDetailsProvided()
    {
        HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.Unauthorized);
        msg.Content = new StringContent("Invalid Details Provided");
        throw new HttpResponseException(msg);
    }

    public static void UnauthorizedAccess()
    {
        HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.Unauthorized);
        msg.Content = new StringContent("Unauthorized Access");
        throw new HttpResponseException(msg);
    }

    public static void ThrowException(Exception ex)
    {
        HttpResponseMessage msg = new HttpResponseMessage(HttpStatusCode.InternalServerError);
        msg.Content = new StringContent(ex.Message);
        throw new HttpResponseException(msg);
    }

    public APIResult(string statusCode, string statusDescription, HttpRequestMessage request)
    {
        KeyValuePair<string, string> response = new KeyValuePair<string, string>(statusCode, statusDescription);
        _value = JsonConvert.SerializeObject(response);
        _request = request;
    }

    public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
    {
        var response = new HttpResponseMessage()
        {
            Content = new StringContent(_value),
            RequestMessage = _request
        };
        return Task.FromResult(response);
    }
}