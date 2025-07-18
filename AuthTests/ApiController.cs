using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthTests;

public class ApiController : ControllerBase
{
    [HttpGet("/protected")]
    [Authorize]
    public ActionResult<String> ProtectedHello()
    {
        return "Protected hello";
    }

    [HttpGet("private")]
    [Authorize("read:messages")]
    public ActionResult<String> PrivateHello()
    {
        return "Private hello";
    }
}