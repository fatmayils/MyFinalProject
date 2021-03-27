using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]// route bu isteği yaparken insanlar bize nasıl ulaşsın demek
    [ApiController]//Attribute denir,javadaki karşılığı annotation
    public class ProductsController : ControllerBase
    {
        //loosely coupled-gevşek bağlılık
        //IoC container-Inversion of Control
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult Get()
        {
            var result=_productService.GetAll();
            if(result.Success==true)
            {
                return Ok(result.Data);//Get request
            }
            return BadRequest(result.Message);

        }
        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if(result.Success==true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if(result.Success==true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
