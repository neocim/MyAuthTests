using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicAuth;

public class ApiController : ControllerBase
{
    [HttpGet("protected")]
    [Authorize]
    public ActionResult<string> ProtectedHello()
    {
        Console.WriteLine("IN PROTECTED HELLO");
        return "Protected hello";
    }

    [HttpGet("private")]
    [Authorize("read:messages")]
    public ActionResult<string> PrivateHello()
    {
        Console.WriteLine("IN PRIVATE HELLO");
        return "Private hello";
    }
}