using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YumApp.WebAPI.Context;
using YumApp.WebAPI.Dtos.TestimonialDtos;
using YumApp.WebAPI.Entities;

namespace YumApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApiContext _context;

        public TestimonialsController(IMapper mapper, ApiContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult TestimonialList()
        {
            var values = _context.Testimonials.ToList();
            return Ok(_mapper.Map<List<ResultTestimonialDto>>(values));
        }
        [HttpPost]
        public IActionResult CreateTestimonial(CreateTestimonialDto createTestimonialDto)
        {
            var values = _mapper.Map<Testimonial>(createTestimonialDto);
            _context.Testimonials.Add(values);
            _context.SaveChanges();
            return Ok("Ekleme İşlemi Başarılı !");
        }
        [HttpDelete]
        public IActionResult CreateTestimonial(int id)
        {
            var values = _context.Testimonials.Find(id);
            _context.Testimonials.Remove(values);
            _context.SaveChanges();
            return Ok("Silme İşlemi Başarılı !");
        }
        [HttpGet("GetTestimonial")]
        public IActionResult GetTestimonial(int id)
        {
            var values = _context.Testimonials.Find(id);
            return Ok(_mapper.Map<GetByIdTestimonialDto>(values));
        }
        [HttpPut]
        public IActionResult UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto)
        {
            var values = _mapper.Map<Testimonial>(updateTestimonialDto);
            _context.Testimonials.Update(values);
            return Ok("Güncelleme İşlemi Başarılı !");
        }
    }
}
