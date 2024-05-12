using Microsoft.AspNetCore.Mvc;

namespace AttendanceManagement.WebAPI.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class CustomControllersAdminBase : ControllerBase
    {
    }
}
