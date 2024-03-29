
using Microsoft.AspNetCore.Mvc;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Repositories;

namespace shoppingapi2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        public OrderDetailsController(IOrderDetailsRepository orderDetailsRepository)
        {
            _orderDetailsRepository = orderDetailsRepository;
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddAsync([FromForm] AddOrderDetailsDto addOrderDetailsDto)
        {
            var orderDetails = await _orderDetailsRepository.AddAsync(addOrderDetailsDto);
            return Ok(orderDetails);
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var orderDetails = await _orderDetailsRepository.GetAllAsync();
            if (orderDetails != null)
                return Ok(orderDetails);
            else
                return Ok("No Any exisit orderItems");
        }
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var orderDetails = await _orderDetailsRepository.GetByIdAsync(id);
            return orderDetails == null ? Ok($"OrderDetails by {id} is not found!!") : Ok(orderDetails);

        }
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] UpdateOrderDetailsDto updateOrderDetailsDto)
        {
            var orderDetails = await _orderDetailsRepository.UpdateAsync(id, updateOrderDetailsDto);
            if (!orderDetails)
                return Ok($"{id} number is not found!!");
            return Ok($"{id} number is updated Succseefully");
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var orderDetails = await _orderDetailsRepository.DeleteAsync(id);
            if (!orderDetails)
                return Ok($"{id}number is not found!!");
            return Ok($"{id} number is removed now");

        }
    }
}