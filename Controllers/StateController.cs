using loc_api_crud.Data;
using loc_api_crud.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace loc_api_crud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly StateRepository _stateRepository;

        public StateController(StateRepository _repo)
        {
            this._stateRepository = _repo;
        }

        [HttpGet]
        public IActionResult GetAllStates()
        {
            List<StateModel> states=this._stateRepository.GetAllStates();
            return Ok(states);
        }

        [HttpDelete("stateID")]
        public IActionResult DeleteCity(int stateID)
        {
            try
            {
                bool isDel = this._stateRepository.DeleteState(stateID);

                if (isDel)
                {
                    return Ok(new
                    {
                        Status = "Success",
                        Message = "State deleted successfully."
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        Status = "Error",
                        Message = "State not found or could not be deleted."
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
        public IActionResult AddState(StateModel stateModel)
        {
            var addstate= this._stateRepository.AddState(stateModel);
            return Ok(addstate);
        }

        [HttpPut("stateID")]
        public IActionResult EditState(StateModel stateModel,int stateID)
        {
            var editState= this._stateRepository.EditState(stateModel, stateID);
            return Ok(editState);
        }

        [HttpGet("{stateID}")]
        public IActionResult GetByID(int stateID)
        {
            var state = _stateRepository.GetByIDState(stateID);
            return Ok(state);
        }

        [HttpGet("GetAllCountries")]
        public IActionResult GetAllCountries()
        {
            var countries = _stateRepository.GetAllCountries();
            return Ok(countries);
        }
    }
}
