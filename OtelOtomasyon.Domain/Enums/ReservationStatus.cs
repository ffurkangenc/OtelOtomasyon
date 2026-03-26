using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Domain.Enums
{
    public enum ReservationStatus
    {
        Pending = 1,    //Beklemede
        Confirmed = 2,  //Onaylandı
        Cancelled = 3,  //İptal edildi
        Completed = 4,  //Tamamlandı    
        NoShow = 5      //Müşteri gelmedi

    }
}
