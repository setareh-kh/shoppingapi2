
using Microsoft.AspNetCore.Mvc;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Repositories;

namespace shoppingapi2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddAsync([FromForm] AddOrderDto addOrderDto)
        {
            var order = await _orderRepository.AddAsync(addOrderDto);
            return Ok(order);
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            if (orders != null)
                //var responses = orders?.Select(x => _mapper.Map<AdminProductResponseDto>(x)).ToList();
                return Ok(orders);
            else
                return Ok("No Any exisit user");
        }
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return order == null ? Ok($"product by {id} is not found!!") : Ok(order);

        }
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] UpdateOrderDto updateorderDto)
        {
            var order = await _orderRepository.UpdateAsync(id, updateorderDto);
            if (!order)
                return Ok($"{id} number is not found!!");
            return Ok($"{id} number is updated Succseefully");
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var order = await _orderRepository.DeleteAsync(id);
            if (!order)
                return Ok($"{id}number is not found!!");
            return Ok($"{id} number is removed now");

        }
    }
}