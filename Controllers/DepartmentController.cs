using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20200522.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace _20200522.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ContosouniversityContext db;

        public DepartmentController(ContosouniversityContext db)
        {
            this.db = db;
        }

          // GET: api/Departments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetDepartment()
        {
            return await db.Department.Where(c => c.isDeleted == false).ToListAsync();
        }

        // GET: api/Departments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var department = await db.Department.FindAsync(id);

            if (department == null || ( department.isDeleted))
            {
                return NotFound();
            }

            return department;
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<ActionResult> PutDepartment(int id, Department department)
        {
            if (id != department.DepartmentId)
            {
                return BadRequest();
            }

            byte[] _rowVersion = db.Department.Where(c => c.DepartmentId == id).Select(c => c.RowVersion).FirstOrDefault();
            department.RowVersion = _rowVersion;
            department.DateModified = DateTime.Now;
            db.Entry(department).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();

               
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(id))
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

        // POST: api/Departments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<Department> PostDepartment(Department department)
        {
           
            #region Use stored procedure
            SqlParameter name = new SqlParameter("@Name", department.Name);
            SqlParameter budget = new SqlParameter("@Budget", department.Budget);
            SqlParameter startDate = new SqlParameter("@StartDate", department.StartDate);
            SqlParameter instructorID = new SqlParameter("@InstructorID", department.InstructorId);
            department.DepartmentId = db.Department.FromSqlRaw("execute Department_Insert @Name,@Budget,@StartDate,@InstructorID",
                name, budget, startDate, instructorID).Select(c => c.DepartmentId).ToList().First();
            #endregion

            return CreatedAtAction("GetDepartment", new { id = department.DepartmentId }, department);
        }

        // DELETE: api/Departments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Department>> DeleteDepartment(int id)
        {
            var department = db.Department.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            department.isDeleted = true;
            //db.Department.Remove(department);
            await db.SaveChangesAsync();

            return department;
        }

        private bool DepartmentExists(int id)
        {
            return db.Department.Any(e => e.DepartmentId == id);
        }


        [HttpGet("GetDepartmentCourseCount")]
        public async Task<ActionResult<IEnumerable<VwDepartmentCourseCount>>> GetDepartmentCourseCount()
        {
            return await db.VwDepartmentCourseCount.FromSqlRaw("SELECT * FROM [dbo].[vwDepartmentCourseCount]").ToListAsync();
        }
    }
}
