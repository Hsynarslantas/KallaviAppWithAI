using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YumApp.WebAPI.Context;
using YumApp.WebAPI.Dtos.GroupDtos;
using YumApp.WebAPI.Entities;

namespace YumApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupReservationsController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public GroupReservationsController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GroupReservationList()
        {
            var values = _context.GroupReservations.ToList();
            return Ok(_mapper.Map<List<ResultGroupReservationDto>>(values));
        }
        [HttpPost]
        public IActionResult CreateGroupReservation(CreateGroupReservationDto createGroupReservationDto)
        {
            var values = _mapper.Map<GroupReservation>(createGroupReservationDto);
            _context.GroupReservations.Add(values);
            _context.SaveChanges();
            return Ok("Ekleme İşlemi Başarılı !");
        }
        [HttpDelete]
        public IActionResult DeleteGroupReservation(int id)
        {
            var values = _context.GroupReservations.Find(id);
            _context.GroupReservations.Remove(values);
            _context.SaveChanges();
            return Ok("Silme İşlemi Başarılı !");
        }
        [HttpGet("GetGroupReservation")]
        public IActionResult GetGroupReservation(int id)
        {
            var values = _context.GroupReservations.Find(id);
            return Ok(_mapper.Map<GetByIdGroupReservationDto>(values));
        }
        [HttpPut]
        public IActionResult UpdateGroupReservation(UpdateGroupReservationDto updateGroupReservationDto)
        {
            var values = _mapper.Map<GroupReservation>(updateGroupReservationDto);
            _context.GroupReservations.Update(values);
            return Ok("Güncelleme İşlemi Başarılı !");
        }
    }
}
