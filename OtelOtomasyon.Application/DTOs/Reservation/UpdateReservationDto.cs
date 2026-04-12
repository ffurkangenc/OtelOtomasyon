using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Application.DTOs.Reservation
{
    public class UpdateReservationDto
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int GuestCount { get; set; }
        public string? Notes { get; set; }
        public string Status { get; set; }
        public int RoomId { get; set; }
    }
}
