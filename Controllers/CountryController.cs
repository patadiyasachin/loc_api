using loc_api_crud.Data;
using loc_api_crud.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace loc_api_crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private CountryRepository _countryRepository;
        public CountryController(CountryRepository CountryRepo)
        {
            this._countryRepository = CountryRepo;
        }
        [HttpGet]
        public IActionResult GetAllCountry()
        {
            var country = _countryRepository.GetAllCountry();
            return Ok(country);
        }

        [HttpDelete("countryID")]
        public IActionResult DeleteCity(int countryID)
        {
            try
            {
                bool isDel = this._countryRepository.DeleteCountry(countryID);

                if (isDel)
                {
                    return Ok(new
                    {
                        Status = "Success",
                        Message = "Country deleted successfully."
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        Status = "Error",
                        Message = "Country not found or could not be deleted."
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
        public IActionResult AddCountry(CountryModel countryModel)
        {
            var addCountry = this._countryRepository.AddCountry(countryModel);
            return Ok(addCountry);
        }

        [HttpPut("countryID")]
        public IActionResult EditCountry(CountryModel countryModel,int countryID)
        {
            var editCountry = this._countryRepository.EditCountry(countryModel, countryID);
            return Ok(editCountry);
        }

        [HttpGet("{countryID}")]
        public IActionResult GetByID(int countryID)
        {
            var country = _countryRepository.GetByIDCountry(countryID);
            return Ok(country);
        }
    }
}
