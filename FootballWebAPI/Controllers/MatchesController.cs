using FootballWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using FootballWebAPI.Data;
using FootballWebAPI.Data.TeamData;
using FootballWebAPI.Data.TournamentData;
using FootballWebAPI.Data.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FootballWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly IMatchData _data;
        private readonly FootballAPIContext _context;
        private MatchConverter _converter;

        public MatchesController(FootballAPIContext context, IMatchData data)
        {
            _context = context;
            _data = data;
            _converter = new MatchConverter(context);

        }
        // GET: api/<MatchesController>
        [HttpGet]
        public IActionResult Get()
        {
            var allMatches = _data.GetMatches();
            var jsonMatches = new List<object>();
            foreach (var match in allMatches)
                jsonMatches.Add(_converter.ToJson(match));
            return Ok(jsonMatches);
        }

        // GET api/<MatchesController>/5
        [HttpGet("{byTime}")]
        public IActionResult Get(DateTime time)
        {
            if (DateTime.MinValue == time)
                time = DateTime.Now.AddDays(7);
            var nearestMatches = _data.GetNearestMatches(time);
            var jsonResults = new List<object>();
            foreach(var match in nearestMatches)
                jsonResults.Add(_converter.ToJson(match));
            return Ok(jsonResults);
        }

        // POST api/<MatchesController>
        [HttpPost]
        public IActionResult Post(string homeTeam, string guestTeam)
        {
            if (homeTeam == null || guestTeam == null)
                return BadRequest("team names cannot be null");
            var teamData = new SqlTeamData(_context);
            if (!teamData.AlreadyExist(homeTeam) || !teamData.AlreadyExist(guestTeam))
                return BadRequest("No such teams");
            Match newMatch = new Match();
            newMatch.MatchId = Guid.NewGuid();

            newMatch.HomeTeamId = teamData.GetTeamByName(homeTeam).TeamId;
            newMatch.GuestTeamId = teamData.GetTeamByName(guestTeam).TeamId;
            if (!_data.AddMatch(newMatch))
                return BadRequest("error while add to DB");

            var currentRequest = HttpContext.Request;
            string createdPath = currentRequest.Scheme + "://" + currentRequest.Host + currentRequest.Path
                                                       + "/" + newMatch.MatchId;
            return Created(createdPath,_converter.ToJson(newMatch));
        }

        // PUT api/<MatchesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id,[FromBody] MatchInfo info)
        {
            DateTime? timeInfo = info.time;
            string tournamentInfo = info.tournament;

            Match match = _data.GetMatchById(id);
            if (match == null)
                return BadRequest($"Match with id:{id} not Found");
            //Set tournament
            Guid? tournamentId = null;
            if (tournamentInfo != null)
            {
                var tournamentData = new SqlTournamentData(_context);
                var tournament = tournamentData.GetTournamentByName(tournamentInfo);
                if (tournament != null)
                    tournamentId = tournament.TournamentId;
            }
            //Set Date
            if (timeInfo == DateTime.MinValue)
                timeInfo = null;

            if (_data.EditMatch(match, timeInfo, tournamentId))
                return Ok(_converter.ToJson(match));
            return BadRequest("Edit failed");

        }

        // DELETE api/<MatchesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var match = _data.GetMatchById(id);
            if (match != null)
            {
                _data.DeleteMatch(match);
                return Ok(_converter.ToJson(match));
            }
            return BadRequest($"Match with id: { id} not Found");
        }
    }
    public class MatchInfo
    {
        public DateTime time { get; set; }
        public string tournament { get; set; }
    }
}
