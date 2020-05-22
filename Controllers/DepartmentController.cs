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

        // GET: api/Department
        [HttpGet]
        public IEnumerable<Department> Get()
        {
            return db.Department.ToList();
        }

        // GET: api/Department/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Department
        [HttpPost]
        public ActionResult<Department> Post(Department department)
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

        // PUT: api/Department/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
