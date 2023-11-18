using System;
using System.Collections.Generic;
using Npgsql;

namespace sales_departements.Models;

public partial class Supplier
{
    public string SupplierId { get; set; } = null!;

    public string? Name { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Proforma> Proformas { get; } = new List<Proforma>();

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; } = new List<PurchaseOrder>();

    public virtual ICollection<SupplierProduct> SupplierProducts { get; } = new List<SupplierProduct>();

    public static List<Supplier> GetSuppliersByIds(List<string> supplierIds, NpgsqlConnection connection)
        {
            List<Supplier> suppliers = new List<Supplier>();

                string inClause = string.Join(",", supplierIds.ConvertAll(id => $"'{id}'"));

                string query = $@"
                    SELECT * FROM supplier WHERE supplier_id IN ({inClause});";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Supplier supplier = new Supplier
                            {
                                SupplierId = reader["supplier_id"].ToString(),
                                Name = reader["name"].ToString(),
                                ContactEmail = reader["contact_email"].ToString(),
                                ContactPhone = reader["contact_phone"].ToString(),
                                Address = reader["address"].ToString()
                            };

                            suppliers.Add(supplier);
                        }
                    }
                }
            return suppliers;
        }
}
