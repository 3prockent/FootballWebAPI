using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootballWebAPI.Models;
using FootballWebAPI.Data.CountryData;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FootballWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryData _data;

        public CountriesController(ICountryData data)
        {
            _data = data;
        }
        // GET: api/<CountriesController>
        [HttpGet]
        public List<Country> Get()
        {
            return _data.GetCountries();
        }

        // GET api/<CountriesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            Country country = _data.GetCountry(id);
            if (country == null)
                return NotFound("country with that id not found");
            return Ok(country);
        }

        // POST api/<CountriesController>
        [HttpPost]
        public IActionResult Post(string countryName)
        {
            var newCountry = _data.AddCountry(countryName);
            if (newCountry == null)
                return BadRequest("Cannot create county with current name");
            if (_data.AlreadyExist(countryName))
                return BadRequest("Country with that name already exist");
            string createdPath = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path
                                                            + "/" + newCountry.CountryId;
            return Created(createdPath, newCountry);
        }


        // DELETE api/<CountriesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var country = _data.GetCountry(id);
            if (country != null)
            {
                _data.DeleteCountry(country);
                return Ok(country);
            }
            return BadRequest($"Country with id:{id} not found");
        }

        // PUT api/<CountriesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, Country country)
        {
            var editableCountry = _data.GetCountry(id);
            if (editableCountry!=null)
            {
                if(_data.EditCountry(id,country))
                    return Ok(editableCountry);
                return BadRequest("Edit Failed");
            }
            return BadRequest($"Country with id: {id} not found");
        }
    }
}
