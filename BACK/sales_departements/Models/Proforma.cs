using System;
using System.Collections.Generic;
using Npgsql;

namespace sales_departements.Models;

public partial class Proforma
{
    public string ProformaId { get; set; } = null!;

    public DateOnly? IssueDate { get; set; }

    public DateOnly? DueDate { get; set; }

    public string? SupplierId { get; set; }

    public virtual ICollection<ProformaDetail> ProformaDetails { get; } = new List<ProformaDetail>();

    public virtual Supplier? Supplier { get; set; }


        public static List<Proforma> GetProformasByIds(List<string> proformaIds, NpgsqlConnection connection)
        {
            List<Proforma> proformas = new List<Proforma>();

                string inClause = string.Join(",", proformaIds.ConvertAll(id => $"'{id}'"));

                string query = $@"
                    SELECT * FROM proforma WHERE proforma_id IN ({inClause});";

                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Proforma proforma = new Proforma
                            {
                                ProformaId = reader["proforma_id"].ToString(),
                                IssueDate = reader["issue_date"] as DateOnly?,
                                DueDate = reader["due_date"] as DateOnly?,
                                SupplierId = reader["supplier_id"].ToString()
                            };

                            proformas.Add(proforma);
                        }
                    }
                }
            return proformas;
        }
    }
