using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _20200522.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _20200522.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private readonly ContosouniversityContext db;

        public PersonController(ContosouniversityContext db)
        {
            this.db = db;
        }

        // GET: api/Person

        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return db.Person.ToList();
        }

        // GET: api/Person/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Person
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Person/5
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
