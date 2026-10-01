using Microsoft.AspNetCore.Mvc;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Services;
using shoppingapi2.Setting;

namespace shoppingapi2.Controllers.Admin;

[ApiController]
[Route(ApiRoutes.Admin.Order)]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet("Filter")]
    public async Task<IActionResult> Filter([FromQuery] OrderFilterDto filterDto)
    {
        var result = await _orderService.Filter(filterDto);
        return Ok(result);
    }
    [HttpPost("Add")]
    public async Task<IActionResult> Create(CreateOrderDto dto)
    {
        var order = await _orderService.CreateAsync(dto);

        if (order == null)
            return BadRequest("Unable to create order.");

        return Ok(order);
    }

}