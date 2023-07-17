using Amul.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Amul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IcecremeController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Icecream>>> GetAllIcecreams()
        {
            return Ok();
        }
    }
}
