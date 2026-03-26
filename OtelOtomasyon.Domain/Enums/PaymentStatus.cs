using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Domain.Enums
{
    public enum PaymentStatus
    {
        Pending = 1,       //Beklemede
        Paid = 2,          //Ödendi
        PartiallyPaid = 3, //Kısmen ödendi
        Refunded = 4,      //İade edildi
    }
}
