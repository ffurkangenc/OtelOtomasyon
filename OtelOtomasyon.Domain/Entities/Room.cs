using OtelOtomasyon.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int Floor { get; set; }
        public RoomStatus Status { get; set; } = RoomStatus.Available;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
