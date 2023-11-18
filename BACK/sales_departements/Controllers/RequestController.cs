using Microsoft.AspNetCore.Mvc;
using sales_departements.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using sales_departements.Context;



namespace sales_departements.Controllers;

[Route("request")]
public class RequestController : Controller
{
    [HttpGet]
    [Route("get-all-requests")]
    public string GetAllRequests() {
        string exception = null;
        try
        {
            SalesDepartementsContext context = new ();
            List<Request> requests = new Request().GetRequests(context);
            List<object> requestsObject = new (requests);
            return Service.Serialize(requestsObject);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            exception = e.Message;
        }
        return exception;
    }

    [HttpGet]
    [Route("create")]
    public string? Create(string requestDetailJson) {
        string? strings = null;
        try {
            SalesDepartementsContext context = new();

            string? departmentId = HttpContext.Session.GetString("personId");

            DateTime createdAt = DateTime.Now;

            string requestId = new Request(departmentId, createdAt).Create(context);

            List<RequestDetail> requestDetails = (List<RequestDetail>)JsonConvert.DeserializeObject(requestDetailJson);
            new RequestDetail().Creates(context, requestDetails, requestId);

            context.SaveChanges();
        }
        catch(Exception ex) {
            Console.WriteLine(ex.Message);
            strings = ex.Message;
        }
        return strings;
    }

    [HttpGet]
    [Route("validate")]
    public string? Validate(string request_id) {
        string? strings = null;
        try {
            SalesDepartementsContext context = new();
            new Request().UpdateIsValidate(context, request_id);
            context.SaveChanges();
        }
        catch(Exception ex) {
            Console.WriteLine(ex.Message);
            strings = ex.Message;
        }
        return strings;
    }

    // [HttpGet]
    // [Route("add-in-request-detail")]
    // public void AddInRequestDetail(string product_id, int quantity, string reason) {
    //     string requestDetailJson = HttpContext.Session.GetString("requestDetailJson");

    //     RequestDetail requestDetail = new (product_id, quantity, reason);
    //     string newRequestDetailJson = JsonConvert.SerializeObject(requestDetail);

    //     if(string.IsNullOrEmpty(requestDetailJson)) {
    //         HttpContext.Session.SetString("requestDetailJson", newRequestDetailJson);
    //     }
    //     else {
    //         List<RequestDetail> requestDetails =  JsonConvert.DeserializeObject<List<RequestDetail>>(requestDetailJson);
    //         requestDetails.Add(requestDetail);
    //         newRequestDetailJson = JsonConvert.SerializeObject(requestDetails);
    //         HttpContext.Session.SetString("requestDetailJson", newRequestDetailJson);
    //     }
    // }

}
