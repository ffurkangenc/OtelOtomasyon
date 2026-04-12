using OtelOtomasyon.Application.DTOs.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Application.Interfaces
{
    public interface IReservationService
    {
        Task<List<ReservationDto>> GetAllReservationAsync();
        Task<ReservationDto?> GetReservationByIdAsync(int id);
        Task<ReservationDto> CreateReservationAsync(CreateReservationDto dto);
        Task<ReservationDto> UpdateReservationAsync(int id, UpdateReservationDto dto);
        Task DeleteReservationAsync(int id);
    }
}
