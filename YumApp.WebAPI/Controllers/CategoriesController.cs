using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YumApp.WebAPI.Context;
using YumApp.WebAPI.Dtos.CategoryDtos;
using YumApp.WebAPI.Dtos.FeatureDtos;
using YumApp.WebAPI.Entities;

namespace YumApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CategoryList()
        {
            var values = _context.Categories.ToList();
            return Ok(_mapper.Map<List<ResultCategoryDto>>(values));
        }
        [HttpGet("GetCategory")]
        public IActionResult GetCategory(int id)
        {
            var values = _context.Categories.Find(id);
            return Ok(_mapper.Map<GetByIdCategoryDto>(values));
        }
        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var values = _mapper.Map<Category>(createCategoryDto);
            _context.Categories.Add(values);
            _context.SaveChanges();
            return Ok("Ekleme İşlemi Başarılı !");
        }
        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            var values = _context.Categories.Find(id);
            _context.Categories.Remove(values);
            _context.SaveChanges();
            return Ok("Silme İşlemi Başarılı !");
        }
        [HttpPut]
        public IActionResult UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var values = _mapper.Map<Category>(updateCategoryDto);
            _context.Categories.Update(values);
            return Ok("Güncelleme İşlemi Başarılı !");
        }
    }
}
