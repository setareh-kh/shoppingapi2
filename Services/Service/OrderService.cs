using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Models;
using shoppingapi2.Repositories;

namespace shoppingapi2.Services.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        public OrderService(IOrderRepository orderRepository, IOrderDetailsRepository orderDetailsRepository, IUserRepository userRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailsRepository = orderDetailsRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }
        public async Task<OrderResponseDto?> CreateAsync(CreateOrderDto dto)
        {
            // 1. بررسی User
            var user = await _userRepository.GetByIdAsync(dto.UserId);
            if (user == null)
                return null;
            // 2. ایجاد Order
            var order = new Order
            {
                UserId = dto.UserId,
                CreateAt = DateTime.UtcNow,
                TotalPrice = 0
            };
            await _orderRepository.Insert(order);
            await _orderRepository.SaveChangesAsync();
            decimal totalPrice = 0;
            // 3. پردازش محصولات
            foreach (var item in dto.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product == null)
                    return null;
                // 4. بررسی موجودی
                if (product.Quantity < item.Quantity)
                    return null;
                // محاسبه تخفیف
                decimal discountAmount = product.Price * product.Discount / 100m;
                // قیمت نهایی هر واحد
                decimal finalUnitPrice = product.Price - discountAmount;
                // قیمت کل این محصول
                decimal itemTotal = finalUnitPrice * item.Quantity;
                // اضافه کردن به مجموع سفارش
                totalPrice += itemTotal;
                // 5. ایجاد OrderDetails
                var orderDetail = new OrderDetails
                {
                    OrderId = order.Id,
                    Order = order,
                    ProductId = product.Id,
                    UnitPrice = product.Price,
                    Discount = product.Discount,
                    Quantity = item.Quantity,
                    CreateAt = DateTime.UtcNow
                };
                await _orderDetailsRepository.Insert(orderDetail);
                // 6. کاهش موجودی
                product.Quantity -= item.Quantity;
                product.Available = product.Quantity > 0;
                product.UpdateDate = DateTime.UtcNow;
                _productRepository.Update(product);
            }
            // قیمت نهایی کل سفارش
            order.TotalPrice = totalPrice;
            _orderRepository.Update(order);
            // 7. ذخیره تمام تغییرات
            await _orderRepository.SaveChangesAsync();
            // 8. ساخت Response
            return new OrderResponseDto
            {
                Id = order.Id,
                UserId = order.UserId,
                CreateAt = order.CreateAt,
                Items = dto.Items.Select(x => new OrderItemResponseDto
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity
                }).ToList()
            };
        }

    }

}