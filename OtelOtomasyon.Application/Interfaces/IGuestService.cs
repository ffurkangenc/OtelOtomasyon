using OtelOtomasyon.Application.DTOs.Guest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Application.Interfaces
{
    public interface IGuestService
    {
        Task<List<GuestDto>> GetAllGuestAsync();
        Task<GuestDto?> GetGuestByIdAsync(int id);
        Task<GuestDto> CreateGuestAsync(CreateGuestDto dto);
        Task<GuestDto> UpdateGuestAsync(int id, UpdateGuestDto dto);
        Task DeleteGuestAsync(int id);
    }
}
