using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YumApp.WebAPI.Context;
using YumApp.WebAPI.Dtos.EventDtos;
using YumApp.WebAPI.Entities;

namespace YumApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApiContext _context;

        public EventsController(IMapper mapper, ApiContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult EventList()
        {
            var values = _context.Events.ToList();
            return Ok(_mapper.Map<List<ResultEventDto>>(values));
        }
        [HttpPost]
        public IActionResult CreateEvent(CreateEventDto createEventDto)
        {
            var values = _mapper.Map<Event>(createEventDto);
            _context.Events.Add(values);
            _context.SaveChanges();
            return Ok("Ekleme İşlemi Başarılı !");
        }
        [HttpDelete]
        public IActionResult CreateEvent(int id)
        {
            var values = _context.Events.Find(id);
            _context.Events.Remove(values);
            _context.SaveChanges();
            return Ok("Silme İşlemi Başarılı !");
        }
        [HttpGet("GetEvent")]
        public IActionResult GetEvent(int id)
        {
            var values = _context.Events.Find(id);
            return Ok(_mapper.Map<GetByIdEventDto>(values));
        }
        [HttpPut]
        public IActionResult UpdateEvent(UpdateEventDto updateEventDto)
        {
            var values = _mapper.Map<Event>(updateEventDto);
            _context.Events.Update(values);
            return Ok("Güncelleme İşlemi Başarılı !");
        }
        [HttpGet("GetActiveEventCount")]
        public IActionResult GetActiveEventCount()
        {
            var values= _context.Events.Count();
            return Ok(values);
        }
    }
}
