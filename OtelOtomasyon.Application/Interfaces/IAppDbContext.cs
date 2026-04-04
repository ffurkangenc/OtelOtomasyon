using Microsoft.EntityFrameworkCore;
using OtelOtomasyon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Application.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Room> Rooms { get; set; }
        DbSet<RoomType> RoomTypes { get; set; } 
        DbSet<Guest> Guests { get; set; }
        DbSet<Reservation> Reservations { get; set; }
        DbSet<Invoice> Invoices { get; set; }
        DbSet<InvoiceItem> InvoiceItems { get; set; }
        DbSet<Table> Tables { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
