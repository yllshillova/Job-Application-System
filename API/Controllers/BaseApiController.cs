using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result == null) return null;
            // If none of the specific cases match, return Ok with the value if successful
            if (result.IsSuccess && result.Value != null)
            {
                return Ok(result.Value);
            }
            switch (result.ErrorType)
            {
                case ResultErrorType.Unauthorized:
                    return Unauthorized();

                case ResultErrorType.NotFound:
                    if (result.IsSuccess && result.Value == null)
                    {
                        return NotFound();
                    }
                    break;

                case ResultErrorType.BadRequest:
                    if (!result.IsSuccess)
                    {
                        return BadRequest(result.ErrorType.ToString());
                    }
                    break;

                // Add more cases for other error types as needed

                default:
                    // Handle other cases if necessary
                    break;
            }



            // Default to BadRequest if no specific case matches
            return BadRequest(result.ErrorType.ToString());
        }

    }
}