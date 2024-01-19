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

            if (result.IsSuccess && result.Value != null)
            {
                return Ok(result.Value);
            }

            switch (result.ErrorType)
            {
                case ResultErrorType.Unauthorized:
                if (!string.IsNullOrEmpty(result.ErrorMessage))
                    {
                        return Unauthorized(result.ErrorMessage);
                    }
                    return Unauthorized();

                case ResultErrorType.NotFound:
                    if (!string.IsNullOrEmpty(result.ErrorMessage))
                    {
                        return NotFound(result.ErrorMessage);
                    }
                    else if (!result.IsSuccess && result.Value == null)
                    {
                        return NotFound();
                    }
                    break;

                case ResultErrorType.BadRequest:
                    if (!string.IsNullOrEmpty(result.ErrorMessage))
                    {
                        return BadRequest(result.ErrorMessage);
                    }
                    else if (!result.IsSuccess)
                        return BadRequest(result.ErrorType.ToString());
                    break;

                default:
                    break;
            }


            return BadRequest(result.ErrorType.ToString());
        }

    }
}