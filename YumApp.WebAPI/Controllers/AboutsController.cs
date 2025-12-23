using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YumApp.WebAPI.Context;
using YumApp.WebAPI.Dtos.AboutDtos;
using YumApp.WebAPI.Entities;

namespace YumApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public AboutsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult AboutList()
        {
            var values = _context.Abouts.ToList();
            return Ok(_mapper.Map<List<ResultAboutDto>>(values));
        }
        [HttpPost]
        public IActionResult CreateAbout(CreateAboutDto createAboutDto)
        {
            var values = _mapper.Map<About>(createAboutDto);
            _context.Abouts.Add(values);
            _context.SaveChanges();
            return Ok("Ekleme İşlemi Başarılı !");
        }
        [HttpDelete]
        public IActionResult CreateAbout(int id)
        {
            var values = _context.Abouts.Find(id);
            _context.Abouts.Remove(values);
            _context.SaveChanges();
            return Ok("Silme İşlemi Başarılı !");
        }
        [HttpGet("GetAbout")]
        public IActionResult GetAbout(int id)
        {
            var values = _context.Abouts.Find(id);
            return Ok(_mapper.Map<GetByIdAboutDto>(values));
        }
        [HttpPut]
        public IActionResult UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            var values = _mapper.Map<About>(updateAboutDto);
            _context.Abouts.Update(values);
            return Ok("Güncelleme İşlemi Başarılı !");
        }
    }
}
