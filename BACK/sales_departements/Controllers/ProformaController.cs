using Microsoft.AspNetCore.Mvc;
using sales_departements.Models;
using Npgsql;

namespace sales_departements.Controllers
{

    [Route("proformaDetail")]
    public class ProformaController : Controller
    {
        [HttpGet]
        [Route("select")]
        public IActionResult Index()
        {
            List<ProformaDetail> proformaDetails = null;
            List<Proforma> proformas = null;
            List<Supplier> suppliers = null;
            NpgsqlConnection connection = null;

            try
            {
                connection = new PostgresConnection().Connect();

                List<string> productIds = new List<string> { "PRO00001", "PRO00002" };
                proformaDetails = ProformaDetail.GetProformaDetailsMoinsDisans(productIds, connection);

                List<string> proformaIds = proformaDetails.Select(pd => pd.ProformaId).ToList();
                proformas = Proforma.GetProformasByIds(proformaIds, connection);

                List<string> supplierIds = proformas.Select(proforma => proforma.SupplierId).ToList();
                suppliers = Supplier.GetSuppliersByIds(supplierIds, connection);

                foreach (string supplierId in supplierIds)
                {
                    PurchaseOrder purchaseOrder = new PurchaseOrder
                    {
                        SupplierId = supplierId
                    };

                    List<string> insertedIds = PurchaseOrder.InsertPurchaseOrder(purchaseOrder, connection);

                    Console.WriteLine($"Identifiants insérés : {string.Join(", ", insertedIds)}");
                }

                ProformaDetailViewModel viewModel = new ProformaDetailViewModel
                {
                    ProformaDetails = proformaDetails,
                    Proformas = proformas,
                    Suppliers = suppliers 
                };

                return Json(viewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Une erreur interne s'est produite.");
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
