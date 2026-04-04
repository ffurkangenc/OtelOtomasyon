using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Application.DTOs.Room
{
    public class UpdateRoomDto
    {
        public string RoomNumber { get; set; }
        public int Floor { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public int RoomTypeId { get; set; }

    }
}
