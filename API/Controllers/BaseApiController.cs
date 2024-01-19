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
                        return NotFound();
                    break;

                case ResultErrorType.BadRequest:
                    if (!result.IsSuccess)
                        return BadRequest(result.ErrorType.ToString());
                    break;

                default:
                    break;
            }


            return BadRequest(result.ErrorType.ToString());
        }

    }
}