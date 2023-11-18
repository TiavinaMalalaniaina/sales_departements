using System;
using System.Collections.Generic;
using Npgsql;

namespace sales_departements.Models;

public partial class PurchaseOrder
{
    public string PurchaseOrderId { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public int? DeliveryDays { get; set; }

    public string? SupplierId { get; set; }

    public int? Validation { get; set; }

    public virtual Supplier? Supplier { get; set; }

    public static List<string> InsertPurchaseOrder(PurchaseOrder purchaseOrder, NpgsqlConnection connection)
        {
            try
            {
                string query = @"
                    INSERT INTO purchase_order (purchase_order_id, supplier_id)
                    VALUES (default, @SupplierId)
                    RETURNING purchase_order_id;";

                List<string> insertedIds = new List<string>();

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SupplierId", purchaseOrder.SupplierId);

                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            insertedIds.Add(reader["purchase_order_id"].ToString());
                        }
                    }
                }

                return insertedIds;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'insertion de la commande d'achat : {ex.Message}");
                throw;
            }
        }
}
