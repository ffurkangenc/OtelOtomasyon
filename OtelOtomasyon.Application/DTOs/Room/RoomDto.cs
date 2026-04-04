using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Application.DTOs.Room
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } 
        public int Floor { get; set; }
        public string Status { get; set; } 
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string RoomTypeName { get; set; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
    }
}
