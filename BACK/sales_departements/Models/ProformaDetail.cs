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
            // Construire la clause IN pour les paramètres
            string inClause = string.Join(",", productIds.Select((_, index) => $"@ProductId{index}"));

            // Construire la requête avec la clause IN
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
                // Ajouter les paramètres pour chaque product_id
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

}
