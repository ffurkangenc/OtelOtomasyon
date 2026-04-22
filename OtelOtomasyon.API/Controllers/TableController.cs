using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtelOtomasyon.Application.DTOs.Table;
using OtelOtomasyon.Application.Interfaces;

namespace OtelOtomasyon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTable()
        {
            var table = await _tableService.GetAllTableAsync();
            return Ok(table);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTableById(int id)
        {
            var table = await _tableService.GetTableByIdAsync(id);
            if (table == null)
            {
                return NotFound($"{id} numaralı masa bulunamadı.");
            }
            return Ok(table);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTable([FromBody] CreateTableDto createTableDto)
        {
            var table = await _tableService.CreateTableAsync(createTableDto);
            return CreatedAtAction(nameof(GetTableById), new { id = table.Id }, table);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTable(int id, [FromBody] UpdateTableDto updateTableDto)
        {
            try
            {
                var table = await _tableService.UpdateTableAsync(id, updateTableDto);
                return Ok(table);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(int id)
        {
            try
            {
                await _tableService.DeleteTableAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
