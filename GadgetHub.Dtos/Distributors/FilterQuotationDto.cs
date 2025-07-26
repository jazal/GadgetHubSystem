using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GadgetHub.Dtos.Distributors;

public class FilterQuotationDto
{
    public int? OrderId { get; set; }

    public int? CustomerId { get; set; }

    public int? GadgetHubOrderId { get; set; }
}
