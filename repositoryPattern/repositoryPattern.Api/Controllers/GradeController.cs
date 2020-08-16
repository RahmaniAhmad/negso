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
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpGet]
        public IEnumerable<Grade> Get()
        {
            var grade = new Grade()
            {
                Title = "اول",
                Courses = new List<Course>(){
                    new Course(){Title="فارسی"},
                    new Course(){Title="ریاضی"}
                                    }
            };
            var result = _gradeService.Add(grade);
            return _gradeService.GetAll();
        }
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var data = _gradeService.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        [HttpPost]
        public virtual IActionResult Create([FromBody] Grade Grade)
        {
            var result = _gradeService.Add(Grade);
            if (!result)
            {
                return BadRequest("Can't create the requested record.");
            }
            return CreatedAtAction(nameof(Get), new { Grade.Id }, Grade);
        }

        [HttpDelete("{id}")]
        public virtual ActionResult Delete(int id)
        {
            var result = _gradeService.Delete(id);
            if (result)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public virtual ActionResult Update(int id, [FromBody] Grade data)
        {
            var updated = _gradeService.Update(id, data);
            if (updated)
            {
                return Ok(data);
            }
            return NotFound();
        }
    }
}
