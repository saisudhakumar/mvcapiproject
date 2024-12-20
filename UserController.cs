using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mvcapitest.Models;
using Newtonsoft.Json;
using System;

namespace mvcapitest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] student person)
        {
            if (person == null)
            {
                return BadRequest("Person data is missing.");
            }
            var filePath = "data/people.json";

            var people = new List<student>();

            if (System.IO.File.Exists(filePath))
            {
                var jsonData = System.IO.File.ReadAllText(filePath);
                people = JsonConvert.DeserializeObject<List<student>>(jsonData) ?? new List<student>();
            }
            people.Add(person);

            var updatedJson = JsonConvert.SerializeObject(people, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, updatedJson);

            return Ok(new { message = "Person data saved successfully!" });
        }
    }
}
