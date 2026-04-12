using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Application.DTOs.Invoice
{
    public class UpdateInvoiceDto
    {
        public string PaymentStatus { get; set; }
        public string? Notes { get; set; }
    }
}
