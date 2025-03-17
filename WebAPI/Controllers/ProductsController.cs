using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")] // İnsanlar bize nasıl ulaşsın
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // Loosely coupled
        // IoC Container - - Inversion of control
        // Injection
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }



        [HttpGet("getall")]

        // IActionResult - status kodlari gormek uchun
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _productService.GetAllAsync();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]

        public async Task<IActionResult> GetByIdAsync(int id) 
        {
            var result = await _productService.GetByIdAsync(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]

        public async Task<IActionResult> AddAsync(Product product)
        {
            var result = await _productService.AddAsync(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //[HttpPost("addTransactionalAsync")]
        //public async Task<IActionResult> AddTransactionalAsync(Product product)
        //{
        //      await _productService.AddTransactionalAsync(product);
        //    return Ok();
        //}

    }
}
