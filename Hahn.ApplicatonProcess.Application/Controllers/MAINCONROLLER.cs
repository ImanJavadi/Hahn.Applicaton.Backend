
using Hahn.ApplicatonProcess.December2020.Api.Utilities.Validators;
using Hahn.ApplicatonProcess.December2020.Data;
using Hahn.ApplicatonProcess.December2020.Data.Actions;
using Hahn.ApplicatonProcess.December2020.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MAINCONROLLER : ControllerBase
    {
      //  private readonly  MYACT;
        private readonly Database db = new Database();
        private MYActions act = new MYActions();
        private readonly ILogger<MAINCONROLLER> _logger;

        public MAINCONROLLER(ILogger<MAINCONROLLER> logger)
        {
            _logger = logger;
        }
        //---------------------------------------------------------------------
        [HttpPost("CreateUser")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody]ModelDB newuser)
        {
            try
            {
                if (newuser.IsValid(out IEnumerable<string> errors))
                {
                    var result = await act.Create(newuser);
                    if (result==true)
                    {
                        _logger.LogInformation("Succesfully created NewUser @{object}, ID :@{url}", newuser,+ newuser.Id);
                        return StatusCode(StatusCodes.Status201Created, "Succesfully created "+"api/Applicant/" + newuser.Id);
                    }                  
                    else
                    {
                      //  _logger.LogError("Internal Server error at CreateApplicant {@exception}", ex);
                        return StatusCode(StatusCodes.Status500InternalServerError, "Not Succesfully created");
                    }       
                }
                else
                {
                    _logger.LogError("Invalid params at CreateApplicant Errors: {@errors}, Object:{@object}", errors, newuser);
                    return BadRequest(errors);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal Server error at CreateApplicant {@exception}", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }


        //-----------------------------------------------------------------------
        [HttpPut("UpdateApplicant")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateApplicant([FromBody] ModelDB euser)
        {
            try
            {
                if (euser.IsValid(out IEnumerable<string> errors))
                {
                    var result =  await act.Update(euser);

                    if (result==true)
                    {
                        _logger.LogInformation("Succesfully created NewUser @{object}, ID :@{url}", euser, +euser.Id);
                        return StatusCode(StatusCodes.Status201Created, "Succesfully Edit " + "api/Applicant/" + euser.Id);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, "Not Succesfully Edit");
                    }
                }
                else
                {
                    _logger.LogError("Invalid params at UpdateApplicant Errors: {@errors}, Applicant:{@object} ", errors, euser);
                    return BadRequest(errors);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal Server error at UpdateApplicant {@Exception} ", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }

        //---------------------------------------------------------------------
        [HttpGet("GetApplicantById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<ModelDB> GetApplicantById(int id)
        {
            try
            {
                if (id != 0 && id >0)
                {
                    var result = act.Get(id);
                    if (result == null)
                        return Ok("No Content");
                    _logger.LogInformation("Successfully retrieved applicant by Id {@object}", result);
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("BadRequest at GetApplicantById @{id}", id);
                    return BadRequest("Invalid parameters");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal Server error at GetApplicantById @{exception} ", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }
        //----------------------------------------------------------------------
        [HttpDelete("DeleteApplicantById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteApplicantById(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result = await act.Delete(id);

                    if (!result)
                        return Ok("No Content");

                    _logger.LogInformation("Successfully deleted applicant by Id {@Id}", id);
                    return Ok("Success");
                }
                else
                {
                    _logger.LogError("BadRequest at DeleteApplicantById {@Id} ", id);
                    return BadRequest("Invalid parameter");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Internal Server error at DeleteApplicantById {@exception} ", ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }







    }
}
