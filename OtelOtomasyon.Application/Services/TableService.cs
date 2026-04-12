using Microsoft.EntityFrameworkCore;
using OtelOtomasyon.Application.DTOs.Table;
using OtelOtomasyon.Application.Interfaces;
using OtelOtomasyon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Application.Services
{
    public class TableService : ITableService
    {
        private readonly IAppDbContext _context;

        public TableService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<TableDto> CreateTableAsync(CreateTableDto dto)
        {
            var table = new Table
            {
                TableNumber = dto.TableNumber,
                Capacity = dto.Capacity,
                IsAvailable = true
            };

            _context.Tables.Add(table);
            await _context.SaveChangesAsync();
            return await GetTableByIdAsync(table.Id);
        }

        public async Task DeleteTableAsync(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null)
                throw new Exception($"{id} numaralı masa bulunamadı");

            _context.Tables.Remove(table);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TableDto>> GetAllTableAsync()
        {
            return await _context.Tables
                .Select(t=>new TableDto
                {
                    Id=t.Id,
                                        TableNumber = t.TableNumber,
                    Capacity = t.Capacity,
                    IsAvailable = t.IsAvailable
                })
                .ToListAsync();
        }

        public async Task<TableDto?> GetTableByIdAsync(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null) return null;

            return new TableDto
            {
                Id = table.Id,
                TableNumber = table.TableNumber,
                Capacity = table.Capacity,
                IsAvailable = table.IsAvailable
            };
        }

        public async Task<TableDto> UpdateTableAsync(int id, UpdateTableDto dto)
        {
            var table= await _context.Tables.FindAsync(id);
            if (table == null)
                throw new Exception($"{id} numaralı masa bulunamadı");

            table.TableNumber= dto.TableNumber; 
            table.Capacity= dto.Capacity;
            table.IsAvailable= dto.IsAvailable;

            await _context.SaveChangesAsync();
            return await GetTableByIdAsync(table.Id);
        }
    }
}
