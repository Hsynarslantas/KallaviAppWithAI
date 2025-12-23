using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YumApp.WebAPI.Context;
using YumApp.WebAPI.Dtos.ImageDtos;
using YumApp.WebAPI.Entities;

namespace YumApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApiContext _context;

        public ImageController(IMapper mapper, ApiContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        [HttpGet]
        public IActionResult ImageList()
        {
            var values = _context.Images.ToList();
            return Ok(_mapper.Map<List<ResultImageDto>>(values));
        }
        [HttpPost]
        public IActionResult CreateImage(CreateImageDto createImageDto)
        {
            var values = _mapper.Map<Image>(createImageDto);
            _context.Images.Add(values);
            _context.SaveChanges();
            return Ok("Ekleme İşlemi Başarılı !");
        }
        [HttpDelete]
        public IActionResult CreateImage(int id)
        {
            var values = _context.Images.Find(id);
            _context.Images.Remove(values);
            _context.SaveChanges();
            return Ok("Silme İşlemi Başarılı !");
        }
        [HttpGet("GetImage")]
        public IActionResult GetImage(int id)
        {
            var values = _context.Images.Find(id);
            return Ok(_mapper.Map<GetByIdImageDto>(values));
        }
        [HttpPut]
        public IActionResult UpdateImage(UpdateImageDto updateImageDto)
        {
            var values = _mapper.Map<Image>(updateImageDto);
            _context.Images.Update(values);
            return Ok("Güncelleme İşlemi Başarılı !");
        }
    }
}
