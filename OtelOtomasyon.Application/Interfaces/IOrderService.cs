using OtelOtomasyon.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelOtomasyon.Application.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllOrderAsync();
        Task<OrderDto?> GetOrderByIdAsync(int id);
        Task<OrderDto> CreateOrderAsync(CreateOrderDto dto);
        Task<OrderDto> UpdateOrderAsync(int id, UpdateOrderDto dto);
        Task DeleteOrderAsync(int id);
    }
}
