using Microsoft.EntityFrameworkCore;
using OtelOtomasyon.Application.DTOs.Invoice;
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
    public class InvoiceService:IInvoiceService
    {
        private readonly IAppDbContext _context;

        public InvoiceService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<InvoiceDto> CreateInvoiceAsync(CreateInvoiceDto dto)
        {
            var items = dto.Items.Select(item => new InvoiceItem
            {
                Description = item.Description,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.Quantity * item.UnitPrice
            }).ToList();

            var invoice=new Invoice
            {
                ReservationId = dto.ReservationId,
                Notes = dto.Notes,
                IssuedAt = DateTime.UtcNow,
                PaymentStatus = PaymentStatus.Pending,
                TotalAmount = items.Sum(i => i.TotalPrice),
                InvoiceItems = items
            };
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            return await GetInvoiceByIdAsync(invoice.Id);
        }

        public async Task DeleteInvoiceAsync(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if(invoice==null)
                throw new Exception($"{id} numaralı fatura bulunamadı");

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task<List<InvoiceDto>> GetAllInvoiceAsync()
        {
           return await _context.Invoices
                .Include(i=>i.Reservation).ThenInclude(r=>r.Guest)
                .Include(i=>i.Reservation).ThenInclude(r=>r.Room)
                .Include(i=>i.InvoiceItems)
                .Select(i=> new InvoiceDto
                {
                    Id = i.Id,
                    TotalAmount = i.TotalAmount,
                    IssuedAt = i.IssuedAt,
                    PaymentStatus = i.PaymentStatus.ToString(),
                    Notes = i.Notes,
                    ReservationId = i.ReservationId,
                    GuestFullName = i.Reservation.Guest.FirstName + " " + i.Reservation.Guest.LastName,
                    RoomNumber=i.Reservation.Room.RoomNumber,
                    Items=i.InvoiceItems.Select(item => new InvoiceItemDto
                    {
                        Id = item.Id,
                        Description = item.Description,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        TotalPrice = item.TotalPrice
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<InvoiceDto?> GetInvoiceByIdAsync(int id)
        {
           var invoice=await _context.Invoices
                .Include(i=>i.Reservation).ThenInclude(r=>r.Guest)
                .Include(i=>i.Reservation).ThenInclude(r=>r.Room)
                .Include(i=>i.InvoiceItems)
                .FirstOrDefaultAsync(i=>i.Id==id);

            if (invoice == null) return null;

            return new InvoiceDto
            {
                Id = invoice.Id,
                TotalAmount = invoice.TotalAmount,
                IssuedAt = invoice.IssuedAt,
                PaymentStatus = invoice.PaymentStatus.ToString(),
                Notes = invoice.Notes,
                ReservationId = invoice.ReservationId,
                GuestFullName = invoice.Reservation.Guest.FirstName + " " + invoice.Reservation.Guest.LastName,
                RoomNumber = invoice.Reservation.Room.RoomNumber,
                Items = invoice.InvoiceItems.Select(item => new InvoiceItemDto
                {
                    Id = item.Id,
                    Description = item.Description,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.TotalPrice
                }).ToList()
            };
        }

        public async Task<InvoiceDto> UpdateInvoiceAsync(int id, UpdateInvoiceDto dto)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
                throw new Exception($"{id} numaralı fatura bulunamadı");

            invoice.PaymentStatus = Enum.Parse<PaymentStatus>(dto.PaymentStatus);
            invoice.Notes = dto.Notes;

            await _context.SaveChangesAsync();

            return await GetInvoiceByIdAsync(invoice.Id);
        }
    }
}
