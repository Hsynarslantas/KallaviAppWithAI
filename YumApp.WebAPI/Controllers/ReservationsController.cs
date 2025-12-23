using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YumApp.WebAPI.Context;
using YumApp.WebAPI.Dtos.ReservationDtos;
using YumApp.WebAPI.Entities;

namespace YumApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApiContext _context;

        public ReservationsController(IMapper mapper, ApiContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult ReservationList()
        {
            var values = _context.Reservations.ToList();
            return Ok(_mapper.Map<List<ResultReservationDto>>(values));
        }
        [HttpPost]
        public IActionResult CreateReservation(CreateReservationDto createReservationDto)
        {
            var values = _mapper.Map<Reservation>(createReservationDto);
            _context.Reservations.Add(values);
            _context.SaveChanges();
            return Ok("Ekleme İşlemi Başarılı !");
        }
        [HttpDelete]
        public IActionResult CreateReservation(int id)
        {
            var values = _context.Reservations.Find(id);
            _context.Reservations.Remove(values);
            _context.SaveChanges();
            return Ok("Silme İşlemi Başarılı !");
        }
        [HttpGet("GetReservation")]
        public IActionResult GetReservation(int id)
        {
            var values = _context.Reservations.Find(id);
            return Ok(_mapper.Map<GetByIdReservationDto>(values));
        }
        [HttpPut]
        public IActionResult UpdateReservation(UpdateReservationDto updateReservationDto)
        {
            var values = _mapper.Map<Reservation>(updateReservationDto);
            _context.Reservations.Update(values);
            return Ok("Güncelleme İşlemi Başarılı !");
        }
        [HttpGet("TotalReservationCount")]
        public IActionResult TotalReservationCount()
        {
            var values=_context.Reservations.Count();
            return Ok(values);
        }
        [HttpGet("TotalReservationPeopleCount")]
        public IActionResult TotalReservationPeopleCount()
        {
            var values = _context.Reservations.Sum(x=>x.PeopleCount);
            return Ok(values);
        }
        [HttpGet("GetReservationByCompleted")]
        public IActionResult GetReservationByCompleted()
        {
            var values = _context.Reservations.Count(x => x.Status == true);
            return Ok(values);
        }
        [HttpGet("GetReservationByOld")]
        public IActionResult GetReservationByOld()
        {
            var values = _context.Reservations.Count(x => x.Status==false);
            return Ok(values);
        }

        [HttpGet("GetReservationStats")]
        public IActionResult GetReservationStats()
        {
            DateTime today = DateTime.Today;
            DateTime fourMonthsAgo = today.AddMonths(-3);

            var rawData = _context.Reservations
                .Where(r => r.ReservationDate >= fourMonthsAgo)
                .GroupBy(r => new { r.ReservationDate.Year, r.ReservationDate.Month })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Month,
                    Approved = g.Count(x => x.Status == true),
                    Old = g.Count(x => x.Status == false),
                })
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .ToList();

            var result = rawData.Select(x => new RezervationChartDto
            {
                Month = new DateTime(x.Year, x.Month, 1).ToString("MMM yyyy"),
                CompletedRezervation = x.Approved,
                OldRezervation = x.Old,
            }).ToList();

            return Ok(result);
        }
    }
}
