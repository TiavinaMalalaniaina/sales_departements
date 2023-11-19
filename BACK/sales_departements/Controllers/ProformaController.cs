using Microsoft.AspNetCore.Mvc;
using sales_departements.Models;
using Npgsql;
using sales_departements.Context;

namespace sales_departements.Controllers
{

    [Route("proformaDetail")]
    public class ProformaController : Controller
    {

        [HttpGet]
        [Route("select")]
        public IActionResult Test() {
            Dictionary<string, List<VProforma>> dico = new Dictionary<string, List<VProforma>>();
            try {
                SalesDepartementsContext context = new ();
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        List<string> productIds = new List<string> { "PRO00001", "PRO00002" };
                        List<VProforma> vProformas = context.VProformas.Where(p => productIds.Contains(p.ProductId)).ToList();
                        foreach (var item in vProformas)
                        {
                            if (dico.ContainsKey(item.SupplierId)) {
                                List<VProforma> v = dico[item.SupplierId];
                                v.Add(item);
                            } else {
                                List<VProforma> v = new();
                                v.Add(item);
                                dico.Add(item.SupplierId, v);
                            }
                        }
                        var items = dico.Values;
                        foreach (List<VProforma> vProformaPluriel in items)
                        {
                            string supplierId = vProformaPluriel[0].SupplierId;
                            PurchaseOrder purchaseOrder = new PurchaseOrder{
                                SupplierId = supplierId,
                            };
                            context.PurchaseOrders.Add(purchaseOrder);
                            context.SaveChanges();
                            foreach (VProforma vProforma in vProformaPluriel)
                            {
                                PurchaseOrderDetail purchaseOrderDetail = new PurchaseOrderDetail{
                                    PurchaseOrderId = purchaseOrder.PurchaseOrderId,
                                    ProductId = vProforma.ProductId,
                                    Quantity = vProforma.Quantity,
                                    Price = vProforma.Price,
                                };
                                context.PurchaseOrderDetails.Add(purchaseOrderDetail);
                            }
                            context.SaveChanges();
                        }
                        context.SaveChanges();

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
                
                context.SaveChanges();
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }
            return null;
        }


        [HttpGet]
        [Route("select-2")]
        public IActionResult Index()
        {
            List<ProformaDetail> proformaDetails = null;
            List<Proforma> proformas = null;
            List<Supplier> suppliers = null;
            List<string> insertedPurchaseOrder = null;
            List<string> proformaDetailIds = null;
            List<ProformaDetail> purchaseOrderDetails = null;
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

                    insertedPurchaseOrder = PurchaseOrder.InsertPurchaseOrder(purchaseOrder, connection);

                    Console.WriteLine($"@PurchaseOrderId : {string.Join(", ", insertedPurchaseOrder)}");
                }

                proformaDetailIds = proformaDetails.Select(pd => pd.ProformaDetailsId).ToList();
                //purchaseOrderDetails = ProformaDetail.GetFromProformaOrder(insertedPurchaseOrder, proformaDetailIds, connection);

                foreach (var proformaDetailId in proformaDetailIds)
                {
                    Console.WriteLine($"ProformaDetailsId: {proformaDetailId}");
                }

                // List<PurchaseOrderDetail> newPurchaseOrderDetails = new();
                // foreach (var orderDetail in proformaDetails)
                // {
                //     foreach (var purchaseOrderId in insertedPurchaseOrder)
                //     {
                //         PurchaseOrderDetail newDetail = new PurchaseOrderDetail
                //         {
                //         };
                //         newPurchaseOrderDetails.Add(newDetail);
                //     }
                // }

                //             PurchaseOrderId = insertedPurchaseOrder,
                //             ProductId = proformaDetails.ProductId,
                //             Quantity = proformaDetails.Quantity,
                //             Price = proformaDetails.Price
                
                // Console.WriteLine(proformaDetails);
                // foreach (var item in newPurchaseOrderDetails)
                // {
                //     Console.WriteLine(item);                    
                // }

                // List<string> insertPurchaseDetail = PurchaseOrderDetail.InsertPurchaseOrderDetail(newPurchaseOrderDetails, connection);

                // Console.WriteLine($"Identifiants insérés -------------------- : {string.Join(", ", insertPurchaseDetail)}");


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
