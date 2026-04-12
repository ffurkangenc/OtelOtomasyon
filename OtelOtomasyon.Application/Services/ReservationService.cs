using Microsoft.EntityFrameworkCore;
using OtelOtomasyon.Application.DTOs.Reservation;
using OtelOtomasyon.Application.Interfaces;
using OtelOtomasyon.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IAppDbContext _context;

        public ReservationService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ReservationDto> CreateReservationAsync(CreateReservationDto dto)
        {
            var isReservationAvaliable = !await _context.Reservations
                .AnyAsync(r => r.RoomId == dto.RoomId
                && r.Status != ReservationStatus.Cancelled
                && r.CheckInDate < dto.CheckOutDate
                && r.CheckOutDate > dto.CheckInDate);

            if (!isReservationAvaliable)
                throw new Exception("Seçilen tarihler arasında bu oda için mevcut bir rezervasyon bulunmaktadır.");

            var reservation = new Domain.Entities.Reservation
            {
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                GuestCount = dto.GuestCount,
                Notes = dto.Notes,
                RoomId = dto.RoomId,
                GuestId = dto.GuestId,
                Status = ReservationStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return await GetReservationByIdAsync(reservation.Id);
        }

        public async Task DeleteReservationAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
                throw new Exception($"{id} numaralı rezervasyon bulunamadı");

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

        }

        public async Task<List<ReservationDto>> GetAllReservationAsync()
        {
            return await _context.Reservations
                .Include(r => r.Room).ThenInclude(r => r.RoomType)
                .Include(r => r.Guest)
                .Select(r => new ReservationDto
                {
                    Id = r.Id,
                    CheckInDate = r.CheckInDate,
                    CheckOutDate = r.CheckOutDate,
                    GuestCount = r.GuestCount,
                    Status = r.Status.ToString(),
                    Notes = r.Notes,
                    CreatedAt = r.CreatedAt,
                    RoomNumber = r.Room.RoomNumber,
                    RoomTypeName = r.Room.RoomType.Name,
                    GuestId = r.GuestId,
                    GuestFullName = r.Guest.FirstName + " " + r.Guest.LastName
                })
                .ToListAsync();
        }

        public async Task<ReservationDto?> GetReservationByIdAsync(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Room).ThenInclude(r => r.RoomType)
                .Include(r => r.Guest)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null) return null;

            return new ReservationDto
            {
                Id = reservation.Id,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                GuestCount = reservation.GuestCount,
                Status = reservation.Status.ToString(),
                Notes = reservation.Notes,
                CreatedAt = reservation.CreatedAt,
                RoomNumber = reservation.Room.RoomNumber,
                RoomTypeName = reservation.Room.RoomType.Name,
                GuestId = reservation.GuestId,
                GuestFullName = reservation.Guest.FirstName + " " + reservation.Guest.LastName
            };
        }

        public async Task<ReservationDto> UpdateReservationAsync(int id, UpdateReservationDto dto)
        {
            var reservation = _context.Reservations.FirstOrDefault(r => r.Id == id);
            if (reservation == null)
                throw new Exception($"{id} numaralı rezervasyon bulunamadı.");

            reservation.CheckInDate = dto.CheckInDate;
            reservation.CheckOutDate = dto.CheckOutDate;
            reservation.GuestCount = dto.GuestCount;
            reservation.Notes = dto.Notes;
            reservation.Status = Enum.Parse<ReservationStatus>(dto.Status);

            await _context.SaveChangesAsync();
            return await GetReservationByIdAsync(reservation.Id);
        }
    }
}
