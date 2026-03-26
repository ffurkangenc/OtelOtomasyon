using OtelOtomasyon.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Domain.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public Decimal TotalAmount { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
        public string? Notes { get; set; }

        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
    }
}
