using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Domain.Enums
{
    public enum OdaDurumu
    {
        Available = 1,
        Occupied = 2,
        Cleaning = 3,
        Maintenance = 4,
        OutOfService = 5
    }
}
