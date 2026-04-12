using Microsoft.EntityFrameworkCore;
using OtelOtomasyon.Application.DTOs.Order;
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
    public class OrderService: IOrderService
    {
        private readonly IAppDbContext _context;

        public OrderService(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto dto)
        {
            var items=dto.Items.Select(item=> new OrderItem
            {
                ProductName=item.ProductName,
                Quantity=item.Quantity,
                UnitPrice=item.UnitPrice,
                TotalPrice=item.Quantity*item.UnitPrice
            }).ToList();

            var order = new Order
            {
                TableId = dto.TableId,
                Notes = dto.Notes,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                TotalAmount = items.Sum(i => i.TotalPrice),
                OrderItems = items
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return await GetOrderByIdAsync(order.Id);
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order=await _context.Orders.FindAsync(id);
            if(order== null) throw new Exception($"{id} numaralı sipariş bulunamadı");

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderDto>> GetAllOrderAsync()
        {
            return await _context.Orders
                .Include(o=>o.Table)
                .Include(o=>o.OrderItems)
                .Select(o=> new OrderDto
                {
                    Id=o.Id,
                    CreatedAt=o.CreatedAt,
                    Status=o.Status.ToString(),
                    TotalAmount=o.TotalAmount,
                    Notes=o.Notes,
                    TableId=o.TableId, 
                    TableNumber=o.Table.TableNumber,
                    Items=o.OrderItems.Select(item=> new OrderItemDto
                    {
                        Id=item.Id,
                        ProductName=item.ProductName,
                        Quantity=item.Quantity,
                        UnitPrice=item.UnitPrice,
                        TotalPrice=item.TotalPrice
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<OrderDto?> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Table)
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);

            if(order == null) return null;

            return new OrderDto
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                Status = order.Status.ToString(),
                TotalAmount = order.TotalAmount,
                Notes = order.Notes,
                TableId = order.TableId,
                TableNumber = order.Table.TableNumber,
                Items = order.OrderItems.Select(item => new OrderItemDto
                {
                    Id = item.Id,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.TotalPrice
                }).ToList()
            };
        }

        public async Task<OrderDto> UpdateOrderAsync(int id, UpdateOrderDto dto)
        {
            var order =await _context.Orders.FindAsync(id);
            if(order== null) throw new Exception("Order not found");

            order.Status = Enum.Parse<OrderStatus>(dto.Status);
            order.Notes = dto.Notes;

            await _context.SaveChangesAsync();
            return await GetOrderByIdAsync(order.Id);
        }
    }
}
