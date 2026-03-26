using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Domain.Enums
{
    public enum OrderStatus
    {
        Pending = 1,    //Beklemede
        Preparing = 2,  //Hazırlanıyor
        Delivered = 3,  //Teslim edildi
        Cancelled = 4   //İptal edildi
    }
}
