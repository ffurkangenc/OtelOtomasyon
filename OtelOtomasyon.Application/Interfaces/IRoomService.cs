using OtelOtomasyon.Application.DTOs.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Application.Interfaces
{
    public interface IRoomService
    {
        Task<List<RoomDto>> GetAllRoomAsync();
        Task<RoomDto?> GetRoomByIdAsync(int id);
        Task<RoomDto> CreateRoomAsync(CreateRoomDto dto);
        Task<RoomDto> UpdateRoomAsync(int id, UpdateRoomDto dto);
        Task DeleteRoomAsync(int id);
    }
}
