using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YumApp.WebAPI.Context;
using YumApp.WebAPI.Dtos.ProductDtos;
using YumApp.WebAPI.Entities;

namespace YumApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IValidator<Product> _validator;
        private readonly ApiContext _context;
        private readonly IMapper _mapper;
        public ProductsController(IValidator<Product> validator, ApiContext context, IMapper mapper)
        {
            _validator = validator;
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult ProductList()
        {

            var values = _context.Products.ToList();
            return Ok(values);
        }
        [HttpGet("GetProduct")]
        public IActionResult GetProduct(int id)
        {
            
            var values = _context.Products.Find(id);
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            var validationResult = _validator.Validate(product);
            if (validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x=>x.ErrorMessage));
            }
            else
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return Ok("Ürün Başarılı Bir Şekilde Eklendi !");
            }
        }
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            var values = _context.Products.Find(id);
            _context.Remove(values);
            _context.SaveChanges();
            return Ok("Ürün Başarılı Bir Şekilde Silindi");
        }
        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            var validationResult = _validator.Validate(product);
            if (validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));
            }
            else
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return Ok("Ürün Başarılı Bir Şekilde Güncellendi !");
            }
        }
        [HttpPost("CreateProductWithCategory")]
        public IActionResult CreateProductWithCategory(CreateProductDto createProductDto)
        {
            var values=_mapper.Map<Product>(createProductDto);
            _context.Products.Add(values);
            _context.SaveChanges();
            return Ok("Ürün Başarılı Bir Şekilde Eklendi ! ");   
        }
        [HttpGet("ProductListWithCategory")]
        public IActionResult ProductListWithCategory()
        {
            var values = _context.Products
         .Include(x => x.Category)
         .ToList();

            var dto = _mapper.Map<List<ResultProductWithCategoryDto>>(values);

            return Ok(dto);
        }
        [HttpGet("GetProductCount")]
        public IActionResult GetProductCount()
        {
           var values=_context.Products.Count();
            return Ok(values);
     
        }
        [HttpGet("GetProductRandom")]
        public IActionResult GetProductRandom()
        {

            var values =_context.Products.OrderBy(x=>Guid.NewGuid()).ToList();
            return Ok(values);
        }

    }
}
