using OtelOtomasyon.Application.DTOs.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Application.Interfaces
{
    public interface ITableService
    {
        Task<List<TableDto>> GetAllTableAsync();
        Task<TableDto?> GetTableByIdAsync(int id);
        Task<TableDto> CreateTableAsync(CreateTableDto dto);
        Task<TableDto> UpdateTableAsync(int id, UpdateTableDto dto);
        Task DeleteTableAsync(int id);
    }
}
