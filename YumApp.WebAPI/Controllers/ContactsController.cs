using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YumApp.WebAPI.Context;
using YumApp.WebAPI.Dtos.ContactDtos;
using YumApp.WebAPI.Entities;

namespace YumApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ApiContext _context;

        public ContactsController(ApiContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult ContactList()
        {
            var values = _context.Contacts.ToList();
            return Ok(values);
        }
        [HttpGet("GetContact")]
        public IActionResult GetContact(int id)
        {
            var values = _context.Contacts.Find(id);
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreateContact(CreateContactDto createContactDto)
        {
            Contact contact = new Contact();
           contact.Mail=createContactDto.Mail;
            contact.PhoneNumber=createContactDto.PhoneNumber;
            contact.MapLocation=createContactDto.MapLocation;
            contact.Address=createContactDto.Address;
            contact.OpenHours=createContactDto.OpenHours;
            _context.Add(contact);
            _context.SaveChanges();
            return Ok("İletişim Bilgisi Başarılı Bir Şekilde Eklendi");
        }
        [HttpDelete]
        public IActionResult DeleteContact(int id)
        {
            var values = _context.Contacts.Find(id);
            _context.Remove(values);
            _context.SaveChanges();
            return Ok("İletişim Bilgisi Başarılı Bir Şekilde Silindi");
        }
        [HttpPut]
        public IActionResult UpdateContact(UpdateContactDto updateContactDto)
        {
            Contact contact = new Contact();
            contact.Mail = updateContactDto.Mail;
            contact.PhoneNumber = updateContactDto.PhoneNumber;
            contact.MapLocation = updateContactDto.MapLocation;
            contact.Address = updateContactDto.Address;
            contact.OpenHours = updateContactDto.OpenHours;
            _context.Update(contact);
            _context.SaveChanges();
            return Ok("İletişim Bilgisi Başarılı Bir Şekilde Güncellendi");
        }
    }
}
