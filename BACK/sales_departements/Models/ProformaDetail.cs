using Npgsql;
using sales_departements.Context;
using System;
using System.Collections.Generic;
using System.Data;

namespace sales_departements.Models;

public partial class ProformaDetail
{
    public string ProformaDetailsId { get; set; } = null!;

    public string? ProformaId { get; set; }

    public string? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Proforma? Proforma { get; set; }


    public static List<ProformaDetail> GetProformaDetailsMoinsDisans(List<string> productIds, NpgsqlConnection connection)
    {
            string inClause = string.Join(",", productIds.Select((_, index) => $"@ProductId{index}"));

            string query = $@"
            SELECT proforma_details_id, proforma_id, product_id, quantity, price
            FROM (
                SELECT proforma_details_id, proforma_id, product_id, quantity, price,
                    ROW_NUMBER() OVER (PARTITION BY product_id ORDER BY price) AS rank
                FROM proforma_details
            ) ranked_products
            WHERE rank = 1 AND product_id IN ({inClause});";


            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                for (int i = 0; i < productIds.Count; i++)
                {
                    command.Parameters.AddWithValue($"@ProductId{i}", productIds[i]);
                }

                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    List<ProformaDetail> proformaDetails = new List<ProformaDetail>();

                    foreach (DataRow row in dataTable.Rows)
                    {
                        ProformaDetail proformaDetail = new ProformaDetail
                        {
                            ProformaDetailsId = row["proforma_details_id"].ToString(),
                            ProformaId = row["proforma_id"].ToString(),
                            ProductId = row["product_id"].ToString(),
                            Quantity = Convert.ToInt32(row["quantity"]),
                            Price = Convert.ToDecimal(row["price"])
                        };

                        proformaDetails.Add(proformaDetail);
                    }

                    return proformaDetails;
                }
            }
    }

    // public static List<ProformaDetail> GetFromProformaOrder(List<string> purchaseOrderIds, List<string> proformaDetailIds, NpgsqlConnection connection)
    // {
    //     string purchaseOrderIdClause = string.Join(",", purchaseOrderIds.Select((_, index) => $"@PurchaseOrderId{index}"));
    //     string proformaDetailIdClause = string.Join(",", proformaDetailIds.Select((_, index) => $"@ProformaDetailId{index}"));
    //     Console.WriteLine(purchaseOrderIdClause);

    //     string query = $@"
    //         SELECT pd.product_id, quantity, price
    //         FROM proforma_details pd
    //         JOIN proforma p ON p.proforma_id = pd.proforma_id
    //         JOIN supplier s ON s.supplier_id = p.supplier_id
    //         JOIN purchase_order po ON po.supplier_id = s.supplier_id
    //         JOIN product pr on pr.product_id = pd.product_id
    //         WHERE po.purchase_order_id IN ({purchaseOrderIdClause}) and pd.proforma_details_id IN ({proformaDetailIdClause});";

    //         Console.WriteLine(query);

    //     using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
    //     {
    //         for (int i = 0; i < purchaseOrderIds.Count; i++)
    //         {
    //             command.Parameters.AddWithValue($"@PurchaseOrderId{i}", purchaseOrderIds[i]);
    //             Console.WriteLine(purchaseOrderIds[i]);
    //         }

    //         for (int i = 0; i < proformaDetailIds.Count; i++)
    //         {
    //             command.Parameters.AddWithValue($"@ProformaDetailId{i}", proformaDetailIds[i]);
    //         }
    //         using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
    //         {
    //             DataTable dataTable = new DataTable();
    //             adapter.Fill(dataTable);

    //             List<ProformaDetail> proformaOrderDetails = new List<ProformaDetail>();

    //             foreach (DataRow row in dataTable.Rows)
    //             {
    //                 ProformaDetail proformaDetail = new ProformaDetail
    //                     {
    //                         ProductId = row["product_id"].ToString(),
    //                         Quantity = Convert.ToInt32(row["quantity"]),
    //                         Price = Convert.ToDecimal(row["price"])
    //                     };

    //                 proformaOrderDetails.Add(proformaDetail);
    //             }

    //             return proformaOrderDetails;
    //         }
    //     }
    // }

}
