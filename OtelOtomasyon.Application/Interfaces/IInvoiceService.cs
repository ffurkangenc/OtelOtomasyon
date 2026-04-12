using OtelOtomasyon.Application.DTOs.Invoice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Application.Interfaces
{
    public interface IInvoiceService
    {
        Task <List<InvoiceDto>> GetAllInvoiceAsync();
        Task<InvoiceDto?> GetInvoiceByIdAsync(int id);
        Task<InvoiceDto> CreateInvoiceAsync(CreateInvoiceDto dto);
        Task<InvoiceDto> UpdateInvoiceAsync(int id, UpdateInvoiceDto dto);
        Task DeleteInvoiceAsync(int id);
    }
}
