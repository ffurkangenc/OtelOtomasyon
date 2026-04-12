using Microsoft.EntityFrameworkCore;
using OtelOtomasyon.Application.DTOs.Guest;
using OtelOtomasyon.Application.Interfaces;
using OtelOtomasyon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Application.Services
{
    public class GuestService : IGuestService
    {
        private readonly IAppDbContext _context;

        public GuestService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<GuestDto> CreateGuestAsync(CreateGuestDto dto)
        {
            var guest = new Guest
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                NationalId = dto.NationalId,
                Nationality = dto.Nationality,
                DateOfBirth = dto.DateOfBirth,
                CreatedAt = DateTime.UtcNow
            };

            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();
            return await GetGuestByIdAsync(guest.Id);

        }

        public async Task DeleteGuestAsync(int id)
        {
            var guest= await _context.Guests.FindAsync(id);
            if (guest == null)
                throw new Exception($"{id} numaralı misafir bulunamadı.");

            _context.Guests.Remove(guest);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GuestDto>> GetAllGuestAsync()
        {
            return await _context.Guests
                .Select(g => new GuestDto
                {
                    Id = g.Id,
                    FirstName = g.FirstName,
                    LastName = g.LastName,
                    Email = g.Email,
                    Phone = g.Phone,
                    NationalId = g.NationalId,
                    Nationality = g.Nationality,
                    DateOfBirth = g.DateOfBirth,
                    CreatedAt = g.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<GuestDto?> GetGuestByIdAsync(int id)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest == null) return null;

            return new GuestDto
            {
                Id = guest.Id,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                Email = guest.Email,
                Phone = guest.Phone,
                NationalId = guest.NationalId,
                Nationality = guest.Nationality,
                DateOfBirth = guest.DateOfBirth,
                CreatedAt = guest.CreatedAt
            };
        }

        public async Task<GuestDto> UpdateGuestAsync(int id, UpdateGuestDto dto)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest == null)
                throw new Exception($"{id} numaralı misafir bulunamadı.");

            guest.FirstName = dto.FirstName;
            guest.LastName = dto.LastName;
            guest.Email = dto.Email;
            guest.Phone = dto.Phone;
            guest.NationalId = dto.NationalId;
            guest.Nationality = dto.Nationality;
            guest.DateOfBirth = dto.DateOfBirth;

            await _context.SaveChangesAsync();
            return await GetGuestByIdAsync(guest.Id);
        }
    }
}
