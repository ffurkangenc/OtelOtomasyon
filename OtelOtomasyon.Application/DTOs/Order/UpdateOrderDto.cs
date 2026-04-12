using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Application.DTOs.Order
{
    public class UpdateOrderDto
    {
        public string Status { get; set; }
        public string? Notes { get; set; }
    }
}
