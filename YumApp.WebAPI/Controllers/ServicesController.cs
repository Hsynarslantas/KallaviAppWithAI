using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YumApp.WebAPI.Context;
using YumApp.WebAPI.Dtos.ServiceDtos;
using YumApp.WebAPI.Entities;

namespace YumApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApiContext _context;

        public ServicesController(IMapper mapper, ApiContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult ServiceList()
        {
            var values = _context.Services.ToList();
            return Ok(_mapper.Map<List<ResultServiceDto>>(values));
        }
        [HttpPost]
        public IActionResult CreateService(CreateServiceDto createServiceDto)
        {
            var values = _mapper.Map<Service>(createServiceDto);
            _context.Services.Add(values);
            _context.SaveChanges();
            return Ok("Ekleme İşlemi Başarılı !");
        }
        [HttpDelete]
        public IActionResult CreateService(int id)
        {
            var values = _context.Services.Find(id);
            _context.Services.Remove(values);
            _context.SaveChanges();
            return Ok("Silme İşlemi Başarılı !");
        }
        [HttpGet("GetService")]
        public IActionResult GetService(int id)
        {
            var values = _context.Services.Find(id);
            return Ok(_mapper.Map<GetByIdServiceDto>(values));
        }
        [HttpPut]
        public IActionResult UpdateService(UpdateServiceDto updateServiceDto)
        {
            var values = _mapper.Map<Service>(updateServiceDto);
            _context.Services.Update(values);
            return Ok("Güncelleme İşlemi Başarılı !");
        }
    }
}
