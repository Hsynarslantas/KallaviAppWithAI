using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YumApp.WebAPI.Context;
using YumApp.WebAPI.Dtos.EmployeeDtos;
using YumApp.WebAPI.Entities;

namespace YumApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApiContext _context;

        public EmployeesController(IMapper mapper, ApiContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public IActionResult EmployeeList()
        {
            var values = _context.Employees.ToList();
            return Ok(_mapper.Map<List<ResultEmployeeDto>>(values));
        }
        [HttpPost]
        public IActionResult CreateEmployee(CreateEmployeeDto createEmployeeDto)
        {
            var values = _mapper.Map<Employee>(createEmployeeDto);
            _context.Employees.Add(values);
            _context.SaveChanges();
            return Ok("Ekleme İşlemi Başarılı !");
        }
        [HttpDelete]
        public IActionResult DeleteEmployee(int id)
        {
            var values = _context.Employees.Find(id);
            _context.Employees.Remove(values);
            _context.SaveChanges();
            return Ok("Silme İşlemi Başarılı !");
        }
        [HttpGet("GetEmployee")]
        public IActionResult GetEmployee(int id)
        {
            var values = _context.Employees.Find(id);
            return Ok(_mapper.Map<GetByIdEmployeeDto>(values));
        }
        [HttpPut]
        public IActionResult UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
        {
            var values = _mapper.Map<Employee>(updateEmployeeDto);
            _context.Employees.Update(values);
            return Ok("Güncelleme İşlemi Başarılı !");
        }

         [HttpGet("GetEmployeeCount")]
        public IActionResult GetEmployeeCount()
        {
            var values = _context.Employees.Count();
            return Ok(values);
          
        }
    }
}
