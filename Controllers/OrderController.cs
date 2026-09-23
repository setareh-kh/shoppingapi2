using Microsoft.AspNetCore.Mvc;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Services;

namespace shoppingapi2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }


    [HttpPost("Add")]
    public async Task<IActionResult> Create(CreateOrderDto dto)
    {
        var order= await _orderService.CreateAsync(dto);

        if (order == null)
            return BadRequest("Unable to create order.");

        return Ok(order);
    }

}