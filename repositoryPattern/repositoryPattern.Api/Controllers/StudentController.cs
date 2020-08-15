using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using repositoryPattern.Business;
using repositoryPattern.Entities;

namespace repositoryPattern.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return _studentService.GetAll();
        }

        [HttpGet("test")]
        public IEnumerable<Student> GetTest()
        {
            return new List<Student>()
            { new Student() { FirstName = "1", LastName = "2" } };
            //return  _studentService.GetAllFromStudentRepository();
        }

        [HttpPost("create")]
        public virtual IActionResult Create([FromBody] Student student)
        {
            var result = _studentService.Add(student);
            if (!result)
            {
                return BadRequest("Can't create the requested record.");
            }
            return CreatedAtAction(nameof(Get), new { student.Id }, student);
        }

        [HttpDelete("{id}")]
        public virtual ActionResult Delete(int id)
        {
            var result = _studentService.Delete(id);
            if (result)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public virtual ActionResult Update(int id, [FromBody] Student data)
        {
            var updated = _studentService.Update(id, data);
            if (updated)
            {
                return Ok(data);
            }
            return NotFound();
        }
    }
}
