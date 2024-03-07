using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Repositories;

namespace shoppingapi2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatogoryController : ControllerBase
    {
        private readonly ICatogoryRepository _catogoryRepository;
        //private readonly IMapper _mapper;
        public CatogoryController(ICatogoryRepository catogoryRepository)
        {
            _catogoryRepository = catogoryRepository;
        }
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddAsync([FromBody] CatogoryDto catogoryDto)
        {
            var catogory = await _catogoryRepository.AddAsync(catogoryDto);
            return Ok(catogory);
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var catogories = await _catogoryRepository.GetAllAsync();
            return Ok(catogories!=null ? catogories:"No Any exisit catogory");
        }
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var catogory = await _catogoryRepository.GetByIdAsync(id);
            return Ok(catogory!= null ? catogory:$"catogory {id} not found");

        }
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, CatogoryDto catogoryDto)
        {
            var result = await _catogoryRepository.UpdateAsync(id, catogoryDto);
            return Ok(result == true ? $"{id} number is updated" : $"{id}number is not found!!");
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _catogoryRepository.DeleteAsync(id);
            return Ok(result == true ? $"{id} number is deleted" : $"{id}number is not found!!");
        }
    }
}