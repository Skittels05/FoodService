using DeliveryService.BLL.DTOs;
using DeliveryService.BLL.Enums;
using DeliveryService.BLL.Mappers.Interfaces;
using DeliveryService.BLL.Models;

namespace DeliveryService.BLL.Mappers;

public class AddOrderItemDtoToEntityMapper : IMapper<AddOrderItemDto, OrderItem>
{
    public OrderItem Map(AddOrderItemDto input)
    {
        return new OrderItem
        {
            OrderId = input.OrderId, 
            MenuItemId = input.MenuItemId,
            Name = input.Name,
            Price = input.Price,
            Quantity = input.Quantity
        };
    }
}

public class OrderItemToDtoMapper : IMapper<OrderItem, OrderItemDto>
{
    public OrderItemDto Map(OrderItem input)
    {
        return new OrderItemDto(
            MenuItemId: input.MenuItemId,
            Name: input.Name,
            Price: input.Price,
            Quantity: input.Quantity
        );
    }
}

public class CreateOrderDtoToEntityMapper : IMapper<CreateOrderDto, Order>
{
    public Order Map(CreateOrderDto input)
    {
        return new Order
        {
            CustomerId = input.CustomerId,
            RestaurantId = input.RestaurantId,
            RestaurantLocationId = input.RestaurantLocationId,
            Status = OrderStatus.Created,
            IsPaid = false,
            DeliveryAddress = input.DeliveryAddress,
            CustomerComment = input.CustomerComment,
            TotalAmount = 0,
            Items = []
        };
    }
}

public class OrderToDtoMapper(IMapper<OrderItem, OrderItemDto> itemMapper)
    : IMapper<Order, OrderDto>
{
    public OrderDto Map(Order input)
    {
        return new OrderDto(
            Id: input.Id,
            CustomerId: input.CustomerId,
            RestaurantId: input.RestaurantId,
            RestaurantLocationId: input.RestaurantLocationId,
            CourierId: input.CourierId,
            Status: input.Status,
            IsPaid: input.IsPaid,
            TotalAmount: input.TotalAmount,
            DeliveryAddress: input.DeliveryAddress,
            CustomerComment: input.CustomerComment,
            CancellationReason: input.CancellationReason,
            CancellationComment: input.CancellationComment,
            CreatedAt: input.CreatedAt,
            Items: input.Items?.Select(itemMapper.Map).ToList() ?? []
        );
    }
}
