using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApiCoreWithoutORM.Data;
using WebApiCoreWithoutORM.Models;

namespace WebApiCoreWithoutORM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeoplesController : ControllerBase
    {
        public IDalPeople DalPeople { get; }

        public PeoplesController(IDalPeople dalPeople)
        {
            DalPeople = dalPeople;
        }

        [HttpGet]
        public IEnumerable<People> Get()
        {
            return DalPeople.All();
        }

        [HttpGet("{id}", Name = "Get")]
        public People Get(int id)
        {
            return DalPeople.Find(id);
        }
                
        [HttpPost]
        public IActionResult Post([FromBody] People value)
        {
            DalPeople.Add(value);
            return Ok(value);
        }
                
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] People value)
        {
            if (DalPeople.Edit(value))
            {
                return Ok(value);
            }
            return NotFound(new { id, value });
        }
                
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (DalPeople.Remove(id))
            {
                return Ok(new { id });
            }
            return NotFound(new { id });
        }
    }
}
