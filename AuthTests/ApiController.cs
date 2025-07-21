using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthTests;

public class ApiController : ControllerBase
{
    [HttpGet("protected")]
    [Authorize]
    public ActionResult<String> ProtectedHello()
    {
        Console.WriteLine("IN PROTECTED HELLO");
        return "Protected hello";
    }

    [HttpGet("private")]
    [Authorize("read:messages")]
    public ActionResult<String> PrivateHello()
    {
        Console.WriteLine("IN PRIVATE HELLO");
        return "Private hello";
    }
}