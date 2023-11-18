using Microsoft.AspNetCore.Mvc;
using sales_departements.Context;
using sales_departements.Models;

namespace sales_departements.Controllers;

[Route("employee")]
public class EmployeeController : Controller
{
    [HttpGet]
    [Route("log-in")]
    public string LogIn(string email, string password) {
        string strings = null;
        try
        {
            SalesDepartementsContext context = new ();
            Employee employee = new Employee().GetEmployeeByEmailAndPassword(context, email, password);
            HttpContext.Session.SetString("personId", employee.PersonId);
            string personId = HttpContext.Session.GetString("personId");
            Console.WriteLine(personId+"");
            //List<object> departmentObjects = new(departments);
            //return Service.Serialize(departmentObjects);
        }
        catch (Exception e)
        {
            strings = e.Message;
        }

        Console.WriteLine(strings);
        return strings;
    }
}
