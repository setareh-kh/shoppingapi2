using Microsoft.AspNetCore.Mvc;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Services;
using shoppingapi2.Setting;
using shoppingapi2.Validators;

namespace shoppingapi2.Controllers.Admin;
[Authorize(Type = new int[] { 0 })]
[ApiController]
[Route(ApiRoutes.Admin.Order)]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IRedisService _redisService;

    public OrderController(IOrderService orderService, IRedisService redisService)
    {
        _orderService = orderService;
        _redisService = redisService;
    }

    [HttpGet("Filter")]
    public async Task<IActionResult> Filter([FromQuery] OrderFilterDto filterDto)
    {
        var result = await _redisService.
            GetValueAsync<PaginateResponseDto<OrderFilterResponseDto>>($"Order_result_Filter");
        if (result != null) return Ok(result);
        result = await _orderService.Filter(filterDto);
        await _redisService.SetValueAsync($"Order_result_Filter", result, TimeSpan.FromMinutes(10));
        return Ok(result);
    }
    [HttpPost("Add")]
    public async Task<IActionResult> Create(CreateOrderDto dto)
    {
        var order = await _orderService.CreateAsync(dto);
        if (order == null)
            return BadRequest("Unable to create order.");
        await _redisService.SetValueAsync($"Order_{order.Id}", order, TimeSpan.FromMinutes(10));
        return Ok(order);
    }

}