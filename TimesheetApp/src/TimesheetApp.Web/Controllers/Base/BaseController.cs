using Microsoft.AspNetCore.Mvc;

namespace TimesheetApp.Controllers.Base
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        
    }
}