using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using sales_departements.Context;

namespace sales_departements.Models;

public partial class Request
{
    public string RequestId { get; set; } = null!;

    public string? DepartmentId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public bool? IsValidated { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<RequestDetail> RequestDetails { get; } = new List<RequestDetail>();

    public Request(string? departementId, DateTime? createdAt) {
        DepartmentId = departementId;
        CreatedAt = createdAt;
    }

    public Request() {

    }

    public string Create(SalesDepartementsContext context) {
        context.Requests.Add(this);
        context.SaveChanges();
        return this.RequestId;
    }

    public void UpdateIsValidate(SalesDepartementsContext context, String requestId) {
        var requestToUpdate = context.Requests.Find(requestId);
        if (requestToUpdate != null) {
            requestToUpdate.IsValidated = true;
        }
    }

    public List<Request> GetRequests(SalesDepartementsContext context) {
        return context.Requests.Include(r => r.Department).Include(r => r.RequestDetails).ToList();
    }
}
