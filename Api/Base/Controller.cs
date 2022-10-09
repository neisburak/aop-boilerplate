using Microsoft.AspNetCore.Mvc;

namespace Api.Base;

public class BaseController : ControllerBase
{
    protected IActionResult FromResult(Core.Utilities.Results.Abstract.IResult result)
    {
        if(result.Success) return Ok(result);
        return BadRequest(result);
    }
}