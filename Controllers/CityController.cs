using loc_api_crud.Data;
using loc_api_crud.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace loc_api_crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityRepository _cityRepository;

        public CityController(CityRepository cityRepository)
        {
            this._cityRepository = cityRepository;
        }

        [HttpGet]
        public IActionResult GetAllCities()
        {
            var cities=this._cityRepository.GetAllCities();
            return Ok(cities);
        }

        [HttpDelete("cityID")]
        public IActionResult DeleteCity(int cityID)
        {
            try
            {
                bool isDel=this._cityRepository.DeleteCity(cityID);

                if (isDel)
                {
                    return Ok(new
                    {
                        Status = "Success",
                        Message = "City deleted successfully."
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        Status = "Error",
                        Message = "City not found or could not be deleted."
                    });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = "An unexpected error occurred.",
                    Details = ex.Message
                });
            }
        }

        [HttpPost]
        public IActionResult AddCity(CityModel cityModel)
        {
            var addCity= this._cityRepository.AddCity(cityModel);
            return Ok(addCity);
        }

        [HttpPut("cityID")]
        public IActionResult EditCity(CityModel cityModel,int cityID)
        {
            var editCity = this._cityRepository.EditCity(cityModel,cityID);
            return Ok(editCity);
        }

        [HttpGet("GetAllCountries")]
        public IActionResult GetAllCountries()
        {
            var countries = _cityRepository.GetAllCountries();
            return Ok(countries);
        }

        [HttpGet("countryID")]
        public IActionResult GetAllStates(int countryID)
        {
            var states = _cityRepository.GetAllStatesByCountryID(countryID);
            return Ok(states);
        }

        [HttpGet("{cityID}")]
        public IActionResult GetByID(int cityID)
        {
            var city = _cityRepository.GetByIDCity(cityID);
            return Ok(city);
        }
    }
}
