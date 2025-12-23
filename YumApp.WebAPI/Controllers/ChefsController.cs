using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YumApp.WebAPI.Context;
using YumApp.WebAPI.Entities;

namespace YumApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChefsController : ControllerBase
    {
        private readonly ApiContext _context;

        public ChefsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ChefList()
        {
            var values = _context.Chefs.ToList();
            return Ok(values);
        }
        [HttpGet("GetChef")]
        public IActionResult GetChef(int id)
        {
            var values = _context.Chefs.Find(id);
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreateChef(Chef chef)
        {
            var values = _context.Chefs.Add(chef);
            _context.SaveChanges();
            return Ok("Şef Başarılı Bir Şekilde Eklendi");
        }
        [HttpDelete]
        public IActionResult DeleteChef(int id)
        {
            var values = _context.Chefs.Find(id);
            _context.Remove(values);
            _context.SaveChanges();
            return Ok("Şef Başarılı Bir Şekilde Silindi");
        }
        [HttpPut]
        public IActionResult UpdateChef(Chef chef)
        {
            var values = _context.Chefs.Update(chef);
            _context.SaveChanges();
            return Ok("Şef Başarılı Bir Şekilde Güncellendi");
        }
        [HttpGet("GetChefCount")]
        public IActionResult GetChefCount()
        {
            var values = _context.Chefs.Count();
            return Ok(values);
        }
    }
}
