using System;
using System.Collections.Generic;
using sales_departements.Context;

namespace sales_departements.Models;

public partial class RequestDetail
{
    public string RequestDetailsId { get; set; } = null!;

    public string? RequestId { get; set; }

    public string? ProductId { get; set; }

    public int? Quantity { get; set; }
    public string? Reason { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Request? Request { get; set; }

    public RequestDetail() {

    }
    public RequestDetail(string productId, int quantity, string reason) {
        ProductId = productId;
        Quantity = quantity;
        Reason = reason;
    }

    public void Create(SalesDepartementsContext context) {
        context.RequestDetails.Add(this);
    }

    /*insert many product for a request */
    public void Creates(SalesDepartementsContext context, List<RequestDetail> requestDetails, string requestId) {
        foreach (RequestDetail item in requestDetails)
        {
            item.RequestId = requestId;
            item.Create(context);
        }
    }
}
