using Microsoft.AspNetCore.Mvc;
using FootballWebAPI.Models;
using FootballWebAPI.Data;
using FootballWebAPI.Data.CountryData;
using FootballWebAPI.Data.TeamData;
using FootballWebAPI.Data.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FootballWebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FootballersController : ControllerBase
    {
        private readonly IFootballerData _data;
        private FootballerConverter _converter;
        public readonly FootballAPIContext _context;

        public FootballersController(FootballAPIContext context, IFootballerData data)
        {
            _context = context;
            _data = data;
            _converter = new FootballerConverter(context);
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
        public IActionResult Get(Guid id)
        {
            Footballer result = _data.GetFootballerById(id);
            if (result == null)
                return NotFound($"Footballer with id: {id} not found");
            return Ok(_converter.ToJson(result));
        }

        // POST api/<FootballersController>
        [HttpPost]


        public IActionResult Post(string name, string country)
        {
            if (name == null || country == null)
                return BadRequest("footballer name and country cannot be null");
            if (_data.AlreadyExist(name))
                return BadRequest("footballer with current name already exist");

            var newFootballer = new Footballer();

            newFootballer.FootballerId = Guid.NewGuid();
            newFootballer.Name = name;
            //add country
            Guid newCountryId=SetCountry(country);
            newFootballer.CountryId = newCountryId;
            _data.AddFootballer(newFootballer);

            var currentRequest = HttpContext.Request;
            string createdPath = currentRequest.Scheme + "://" + currentRequest.Host + currentRequest.Path
                                                       + "/" + newFootballer.FootballerId;
            return Created(createdPath, _converter.ToJson(newFootballer));
         
        }

        // PUT api/<FootballersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, string teamName)
        {
            if (teamName == null)
                return BadRequest("teamName argument cannot be null");
            Team team;
            var teamData = new SqlTeamData(_context);
            if (!teamData.AlreadyExist(teamName))
                return BadRequest("current team doesn't exist");
            team = teamData.GetTeamByName(teamName);
            var editableFootballer = _data.GetFootballerById(id);
            if(editableFootballer==null)
                return NotFound($"Team with id: {id} not Found");
            if (_data.EditFootballer(editableFootballer, team))
                return Ok(_converter.ToJson(editableFootballer));
            return BadRequest("Edit Failed");

        }

        // DELETE api/<FootballersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var footballer = _data.GetFootballerById(id);
            if (footballer != null)
            {
                _data.DeleteFootballer(footballer);
                return Ok(_converter.ToJson(footballer));
            }
            return BadRequest($"Footballer with id: {id} not Found");
        }

        private Guid SetCountry(string country)
        {
            var countryData = new SqlCountryData(_context);
            Guid newCountryId;
            if (countryData.AlreadyExist(country))
                newCountryId = countryData.GetCountry(country).CountryId;
            else
            {
                Country addedCountry = countryData.AddCountry(country);
                newCountryId = addedCountry.CountryId;
            }
            return newCountryId;
        }
    }
}
