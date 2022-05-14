using FootballWebAPI.Data.CountryData;
using FootballWebAPI.Data.TeamData;
using FootballWebAPI.Data.Json;
using FootballWebAPI.Models;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FootballWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {

        private readonly ITeamData _data;
        private TeamConverter _converter;
        private readonly FootballAPIContext _context;

        public TeamsController(FootballAPIContext context, ITeamData data)
        {
            _context = context;
            _data = data;
            _converter = new TeamConverter(context);
        }

        // GET: api/<TeamsController>
        [HttpGet]
        public IActionResult Get()
        {
            var allTeams = _data.GetTeams();
            var jsonTeams = new List<object>();

            foreach (var team in allTeams)
            {
                jsonTeams.Add(_converter.ToJson(team));
            }
            return Ok(jsonTeams);
        }

        // GET api/<TeamsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {

            Team result = _data.GetTeamById(id);
            if (result == null)
                return BadRequest($"Team with id: {id} not found");
            return Ok(_converter.ToJson(result));
        }

        // POST api/<TeamsController>
        [HttpPost]
        public IActionResult Post(string teamName, string countryName)
        {
            if (teamName == null || countryName == null)
                return BadRequest("team name and country cannot be null");
            if (_data.AlreadyExist(teamName))
                return BadRequest("Team with current name already exist");
            var newTeam = new Team();
            newTeam.TeamId = Guid.NewGuid();

            var countryData = new SqlCountryData(_context);

            var country = new Country();
            if (!countryData.AlreadyExist(countryName))
                country = countryData.AddCountry(countryName);
            else
                country = countryData.GetCountry(countryName);
            newTeam.CountryId = country.CountryId;
            newTeam.Name = teamName;
            if (!_data.AddTeam(newTeam))
                return BadRequest("error while add to Database");
            var currentRequest = HttpContext.Request;
            string createdPath = currentRequest.Scheme + "://" + currentRequest.Host + currentRequest.Path
                                                       + "/" + newTeam.TeamId;
            return Created(createdPath, _converter.ToJson(newTeam));
        }
        // PUT api/<TeamsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, int year)
        {
            if (year < 1869 || year > DateTime.Now.Year)
                return BadRequest("Team year must be between 1869 and 2022");
            var editableTeam = _data.GetTeamById(id);
            if (editableTeam != null)
            {
                if (_data.EditTeam(editableTeam, year))
                    return Ok(_converter.ToJson(editableTeam));
                return BadRequest("Edit Failed");
            }
            return BadRequest($"Team with id: {id} not Found");
        }

        // DELETE api/<TeamsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var team = _data.GetTeamById(id);
            if (team != null)
            {
                _data.DeleteTeam(team);
                return Ok(_converter.ToJson(team));
            }
            return BadRequest($"Team with id: { id} not Found");
        }
        

        
    }
}
