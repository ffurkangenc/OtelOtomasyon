using Microsoft.EntityFrameworkCore;
using OtelOtomasyon.Application.DTOs.Room;
using OtelOtomasyon.Application.Interfaces;
using OtelOtomasyon.Domain.Entities;
using OtelOtomasyon.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IAppDbContext _context;

        public RoomService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<RoomDto> CreateRoomAsync(CreateRoomDto dto)
        {
            var room = new Room
            {
                RoomNumber = dto.RoomNumber,
                Floor = dto.Floor,
                Description = dto.Description,
                RoomTypeId = dto.RoomTypeId,
                Status = RoomStatus.Available,
                IsActive = true
            };
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return await GetRoomByIdAsync(room.Id);
        }

        public async Task DeleteRoomAsync(int id)
        {
            var room= await _context.Rooms.FindAsync(id);

            if(room==null)
            {
                throw new Exception($"{id} numaralı oda bulunamadı");
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }

        public async Task<List<RoomDto>> GetAllRoomAsync()
        {
           return await _context.Rooms
                .Include(r=>r.RoomType)
                .Select (r=> new RoomDto
                {
                    Id=r.Id,
                    RoomNumber=r.RoomNumber,
                    Floor=r.Floor,
                    Status=r.Status.ToString(),
                    Description=r.Description,
                    IsActive=r.IsActive,
                    RoomTypeName=r.RoomType.Name,
                    PricePerNight=r.RoomType.PricePerNight,
                    Capacity=r.RoomType.Capacity
                })
                .ToListAsync();
        }

        public async Task<RoomDto?> GetRoomByIdAsync(int id)
        {
           var room =await _context.Rooms
                .Include(r=>r.RoomType)
                .FirstOrDefaultAsync(r=>r.Id==id);

            if ((room==null))
            {
                return null;
            }
            return new RoomDto
            {
                Id = room.Id,
                RoomNumber = room.RoomNumber,
                Floor = room.Floor,
                Status = room.Status.ToString(),
                Description = room.Description,
                IsActive = room.IsActive,
                RoomTypeName = room.RoomType.Name,
                PricePerNight = room.RoomType.PricePerNight,
                Capacity = room.RoomType.Capacity
            };
        }

        public async Task<RoomDto> UpdateRoomAsync(int id, UpdateRoomDto dto)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                throw new Exception($"{id} numaralı oda bulunamadı");
            }

            room.RoomNumber = dto.RoomNumber;
            room.Floor = dto.Floor;
            room.Description = dto.Description;
            room.IsActive = dto.IsActive;
            room.RoomTypeId = dto.RoomTypeId;
            room.Status=Enum.Parse<RoomStatus>(dto.Status);

            await _context.SaveChangesAsync();
            return await GetRoomByIdAsync(room.Id);
        }
    }
}
