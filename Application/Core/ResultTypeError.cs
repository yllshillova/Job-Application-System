using System.ComponentModel;

namespace Application.Core;

public enum ResultErrorType
{
    [Description("Unauthorized attempt!")]
    Unauthorized,
    [Description("Not found!")]
    NotFound,
    [Description("Bad Request!")]
    BadRequest
}
