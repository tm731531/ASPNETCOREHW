using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _20200522.Models;
using Microsoft.EntityFrameworkCore;

namespace _20200522.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        
        private readonly ContosouniversityContext db;

        public CourseController(ContosouniversityContext db)
        {
            this.db = db;
        }

        // GET: api/Course
        [HttpGet]
        public IEnumerable<Course> Get()
        {
           return db.Course.ToList();
        }

       // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await db.Course.FindAsync(id);

            if (course == null || (course.isDeleted))
            {
                return NotFound();
            }

            return course;
        }

 // PUT: api/Courses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.CourseId)
            {
                return BadRequest();
            }
            course.DateModified = DateTime.Now;
            db.Entry(course).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            db.Course.Add(course);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Course>> DeleteCourse(int id)
        {
            var course = await db.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            course.isDeleted = true;
            //_context.Course.Remove(course);
            await db.SaveChangesAsync();

            return course;
        }

        private bool CourseExists(int id)
        {
            return db.Course.Any(e => e.CourseId == id);
        }

        [HttpGet("GetCourseStudentCount")]
        public async Task<ActionResult<IEnumerable<VwCourseStudentCount>>> GetCourseStudentCount()
        {
            return await db.VwCourseStudentCount.ToListAsync();
        }

        [HttpGet("GetCourseStudents")]
        public async Task<ActionResult<IEnumerable<VwCourseStudents>>> GetCourseStudents()
        {
            return await db.VwCourseStudents.ToListAsync();
        }
    }
}
