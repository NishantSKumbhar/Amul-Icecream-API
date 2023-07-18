using Amul.Data;
using Amul.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Amul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IcecremeController : ControllerBase
    {
        private readonly AmulDbContext amulDbContext;

        public IcecremeController(AmulDbContext amulDbContext)
        {
            this.amulDbContext = amulDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Categories>>> GetAllIcecreams()
        {
            var Categories = await amulDbContext.Categories.ToListAsync();
            return Ok(Categories);
        }
    }
}
