using Microsoft.AspNetCore.Mvc;
using FootballWebAPI.Models;
using FootballWebAPI.Data.CountryData;
using FootballWebAPI.Data.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FootballWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FootballersController : ControllerBase
    {
        public readonly FootballAPIContext _context;
        public FootballerConverter _converter;

        public FootballersController(FootballAPIContext context)
        {
            _context = context;
            _converter = new FootballerConverter(_context);
        }


        // GET: api/<FootballersController>
        [HttpGet]
        public IActionResult Get()
        {
            var allFootballers = _context.Footballers.ToList();
            var jsonFootballers = new List<object>();

            foreach (var footballer in allFootballers)
            {
                jsonFootballers.Add(_converter.ToJson(footballer));
            }
            return Ok(jsonFootballers);
        }

        // GET api/<FootballersController>/id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Footballer result = _context.Footballers.Find(id);
            return Ok(_converter.ToJson(result));
        }

        // POST api/<FootballersController>
        [HttpPost]


        //public void Post([FromBody] string name, string team, string country)
        //{
        //    var newFootballer = new Footballer();

        //    var countryData = new SqlCountryData(_context);
        //    var 
        //    if()
        //}

        // PUT api/<FootballersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FootballersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
