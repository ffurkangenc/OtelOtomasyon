using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Application.DTOs.Invoice
{
    public class CreateInvoiceDto
    {
        public int ReservationId { get; set; }
        public string? Notes { get; set; }
        public List<CreateInvoiceItemDto> Items { get; set; }
    }

    public class CreateInvoiceItemDto
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
