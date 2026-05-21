using DeliveryService.BLL.DTOs;
using DeliveryService.BLL.Enums;
using DeliveryService.BLL.Mappers.Interfaces;
using DeliveryService.BLL.Models;

namespace DeliveryService.BLL.Mappers;

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

public class CreateOrderItemDtoToEntityMapper : IMapper<CreateOrderItemDto, OrderItem>
{
    public OrderItem Map(CreateOrderItemDto input)
    {
        return new OrderItem
        {
            MenuItemId = input.MenuItemId,
            Name = input.Name,
            Price = input.Price,
            Quantity = input.Quantity,
            OrderId = Guid.Empty
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

public class CreateOrderDtoToEntityMapper(IMapper<CreateOrderItemDto, OrderItem> itemMapper)
    : IMapper<CreateOrderDto, Order>
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
            Items = input.Items.Select(itemMapper.Map).ToList(),
            TotalAmount = 0 
        };
    }
}
