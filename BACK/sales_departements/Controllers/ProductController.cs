using Microsoft.AspNetCore.Mvc;
using sales_departements.Context;
using sales_departements.Models;

namespace sales_departements.Controllers;

[Route("product")]
public class ProductController : Controller
{
    [HttpGet]
    [Route("get-all-products")]
    public  List<Product> GetAllProducts() {
        List<Product> products = null;
        try {
            SalesDepartementsContext context = new();
            products = Product.GetProducts(context);

        }
        catch(Exception ex) {
            Console.WriteLine(ex.Message);
        }

        return products;
    }



}

