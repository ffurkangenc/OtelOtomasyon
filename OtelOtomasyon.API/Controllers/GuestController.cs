using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtelOtomasyon.Application.DTOs.Guest;
using OtelOtomasyon.Application.Interfaces;
using OtelOtomasyon.Domain.Entities;

namespace OtelOtomasyon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGuest()
        {
            var guest= await _guestService.GetAllGuestAsync();
            return Ok(guest);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGuestById(int id)
        {
            var guest=await _guestService.GetGuestByIdAsync(id);
            if (guest == null)
            {
                return NotFound($"{id} numaralı misafir bulunamadı");
            }
            return Ok(guest);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGuest([FromBody] CreateGuestDto createGuestDto)
        {
            var guest =await _guestService.CreateGuestAsync(createGuestDto);
            return CreatedAtAction(nameof(GetGuestById),new {id=guest.Id},guest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGuest(int id, [FromBody]UpdateGuestDto updateGuestDto)
        {
            try
            {
                var guest=await _guestService.UpdateGuestAsync(id, updateGuestDto);
                return Ok(guest);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuest(int id)
        {
            try
            {
                await _guestService.DeleteGuestAsync(id);
                return NoContent();
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
