using System;
using System.Collections.Generic;
using Npgsql;
using System.Data;

namespace sales_departements.Models;

public partial class PurchaseOrderDetail
{
    public string PurchaseOrderDetailsId { get; set; } = null!;

    public string? PurchaseOrderId { get; set; }

    public string? ProductId { get; set; }

    public double? Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual Product? Product { get; set; }

    /*public static List<PurchaseOrderDetail> GetFromPurchaseOrder(List<string> purchaseOrderIds, List<string> proformaDetailIds, NpgsqlConnection connection)
        {
            string purchaseOrderIdClause = string.Join(",", purchaseOrderIds.Select((_, index) => $"@PurchaseOrderId{index}"));
            string proformaDetailIdClause = string.Join(",", proformaDetailIds.Select((_, index) => $"@ProformaDetailId{index}"));

            string query = $@"
                SELECT pod.purchase_order_details_id, pod.purchase_order_id, pod.product_id, pod.quantity, pod.price
                FROM purchase_order_details pod
                JOIN purchase_order po ON po.purchase_order_id = pod.purchase_order_id
                JOIN product pr ON pr.product_id = pod.product_id
                WHERE po.purchase_order_id IN ({purchaseOrderIdClause}) AND pod.purchase_order_details_id IN ({proformaDetailIdClause});";

                Console.WriteLine(query);

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                for (int i = 0; i < purchaseOrderIds.Count; i++)
                {
                    command.Parameters.AddWithValue($"@PurchaseOrderId{i}", purchaseOrderIds[i]);
                }

                for (int i = 0; i < proformaDetailIds.Count; i++)
                {
                    command.Parameters.AddWithValue($"@ProformaDetailId{i}", proformaDetailIds[i]);
                }

                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    List<PurchaseOrderDetail> purchaseOrderDetails = new List<PurchaseOrderDetail>();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        PurchaseOrderDetail purchaseOrderDetail = new PurchaseOrderDetail
                        {
                            PurchaseOrderDetailsId = row["purchase_order_details_id"].ToString(),
                            PurchaseOrderId = row["purchase_order_id"].ToString(),
                            ProductId = row["product_id"].ToString(),
                            Quantity = Convert.ToDouble(row["quantity"]),
                            Price = Convert.ToDecimal(row["price"])
                        };

                        purchaseOrderDetails.Add(purchaseOrderDetail);
                    }

                    return purchaseOrderDetails;
                }
            }
        }*/

    public static List<string> InsertPurchaseOrderDetail(List<PurchaseOrderDetail> purchaseOrderDetails, NpgsqlConnection connection)
    {
        List<string> insertedIds = new();

        string query = @"
            INSERT INTO purchase_order_details (purchase_order_id, product_id, quantity, price)
            VALUES (@PurchaseOrderId, @ProductId, @Quantity, @Price)
            RETURNING purchase_order_details_id;";

            Console.WriteLine(query);

        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            foreach (var purchaseOrderDetail in purchaseOrderDetails)
            {
                command.Parameters.AddWithValue("@PurchaseOrderId", purchaseOrderDetail.PurchaseOrderId);
                command.Parameters.AddWithValue("@ProductId", purchaseOrderDetail.ProductId);
                command.Parameters.AddWithValue("@Quantity", purchaseOrderDetail.Quantity);
                command.Parameters.AddWithValue("@Price", purchaseOrderDetail.Price);

                object insertedId = command.ExecuteScalar();
                insertedIds.Add(insertedId.ToString());
            }
        }

        return insertedIds;
    }


    public override string ToString() {
        return "purchaseOrderId:"+PurchaseOrderId +";;ProductId:"+ProductId;
    }

}
