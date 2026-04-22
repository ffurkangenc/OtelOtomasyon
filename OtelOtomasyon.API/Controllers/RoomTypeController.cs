using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OtelOtomasyon.Domain.Entities;
using OtelOtomasyon.Infrastructure.Persistence;

namespace OtelOtomasyon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoomTypeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoomTypeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomType roomType)
        {
            _context.RoomTypes.Add(roomType);
            await _context.SaveChangesAsync();
            return Ok(roomType);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roomTypes= await _context.RoomTypes.ToListAsync();
            return Ok(roomTypes);
        }
    }
}
