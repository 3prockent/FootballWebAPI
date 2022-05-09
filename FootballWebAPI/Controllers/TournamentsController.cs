using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootballWebAPI.Models;
using FootballWebAPI.Data.TournamentData;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FootballWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly ITournamentData _data;

        public TournamentsController(ITournamentData data)
        {
            _data = data;
        }
        // GET: api/<TournamentsController>
        [HttpGet]
        public List<Tournament> Get()
        {
            return _data.GetTournaments();
        }

        // GET api/<TournamentsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            Tournament tournament = _data.GetTournament(id);
            if (tournament == null)
                return NotFound("tournament with that id not found");
            return Ok(tournament);
        }

        // POST api/<TournamentsController>
        [HttpPost]
        public IActionResult Post(string tournamentName)
        {
            var newTournament = _data.AddTournament(tournamentName);
            if (newTournament == null)
                return NotFound("Cannot create tournament with current name");
            if (_data.AlreadyExist(tournamentName))
                return BadRequest("Country with that name already exist");
            string createdPath = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path
                                                            + "/" + newTournament.TournamentId;
            return Created(createdPath, newTournament);
        }


        // DELETE api/<TournamentsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var tournament = _data.GetTournament(id);
            if (tournament != null)
            {
                _data.DeleteTournament(tournament);
                return Ok(tournament);
            }
            return BadRequest($"Tournament with id:{id} not found");
        }

        // PUT api/<TournamentsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Tournament tournament)
        {
            var editableTournament = _data.GetTournament(id);
            if (editableTournament!=null)
            {
                if(_data.EditTournament(id,tournament))
                    return Ok(editableTournament);
                return BadRequest("Edit Failed");
            }
            return BadRequest($"Tournament with id: {id} not found");
        }
    }
}
