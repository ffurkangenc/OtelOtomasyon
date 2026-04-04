using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtelOtomasyon.Application.DTOs.Room;
using OtelOtomasyon.Application.Interfaces;

namespace OtelOtomasyon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rooms=await _roomService.GetAllRoomAsync();
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult > GetById(int id)
        {
            var room=await _roomService.GetRoomByIdAsync(id);
            if (room == null)
            {
                return NotFound($"{id} numaralı oda bulunamadı.");
            }
            return Ok(room);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoomDto dto)
        {
            var room=await _roomService.CreateRoomAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRoomDto dto)
        {
            try
            {
                var room =await _roomService.UpdateRoomAsync(id, dto);
                return Ok(room);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult > Delete(int id)
        {
            try
            {
                await _roomService.DeleteRoomAsync(id);
                return NoContent();
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}
