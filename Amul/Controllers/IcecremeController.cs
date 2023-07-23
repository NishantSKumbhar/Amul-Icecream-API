using Amul.Data;
using Amul.Models.Domain;
using Amul.Models.DTO;
using Amul.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace Amul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class IcecremeController : ControllerBase
    {
        private readonly AmulDbContext amulDbContext;
        private readonly IMapper mapper;
        private readonly IIcecreamRepository icecreamRepository;
        private readonly ILogger<IcecremeController> logger;

        public IcecremeController(AmulDbContext amulDbContext, IMapper mapper, IIcecreamRepository icecreamRepository, ILogger<IcecremeController> logger)
        {
            this.amulDbContext = amulDbContext;
            this.mapper = mapper;
            this.icecreamRepository = icecreamRepository;
            this.logger = logger;
        }

        [HttpGet]
        //[Authorize(Roles ="Reader")]
        public async Task<ActionResult<List<IcecreamSendDTO>>> GetAllIcecreams(
            [FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
        {


            //logger.LogInformation("Get all insode Icecream controller was just invoked.");
            //logger.LogWarning("Warning log");
            //logger.LogError("Error log");

            //var Icecreams = await amulDbContext.Icecreams.Include("Category").ToListAsync();
            // select pis of code and Ctrl + k + s and type try

            // you can use below exception handling as well, but we are usig Global Exception handling in Middleware folder.
            //try
            //{
            //    throw new Exception("This was the Error");
            //    var IcecreamsModel = await icecreamRepository.GetAllIcecreamsAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            //    if (IcecreamsModel == null)
            //    {
            //        return NotFound(new { Message = "Icecreams are not available in the Database." });
            //    }
            //    var IcecreamSenDTO = mapper.Map<List<IcecreamSendDTO>>(IcecreamsModel);

            //    logger.LogInformation($"Finished GetAll : {JsonSerializer.Serialize(IcecreamSenDTO)}");
            //    return Ok(IcecreamSenDTO);
            //}
            //catch (Exception)
            //{
            //    logger.LogError("Error log");
            //    return Problem("Something Went wrong ", null, (int?)(HttpStatusCode.InternalServerError));
            //}
            var IcecreamsModel = await icecreamRepository.GetAllIcecreamsAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            if (IcecreamsModel == null)
            {
                return NotFound(new { Message = "Icecreams are not available in the Database." });
            }
            var IcecreamSenDTO = mapper.Map<List<IcecreamSendDTO>>(IcecreamsModel);
            //throw new Exception("This was the Error");
            // after this Middleware handles
            //logger.LogInformation($"Finished GetAll : {JsonSerializer.Serialize(IcecreamSenDTO)}");
            return Ok(IcecreamSenDTO);
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="Reader")]
        public async Task<ActionResult<IcecreamSendDTO>> GetIcecreamWithId([FromRoute]Guid id)
        {
            var IcecreamModel = await icecreamRepository.GetIcecreamAsync(id);
            if(IcecreamModel == null)
            {
                return NotFound(new { Message = "Icecream with id , Not Found in Database." });
            }
            return Ok(mapper.Map<IcecreamSendDTO>(IcecreamModel));
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<ActionResult<IcecreamSendDTO>> PostIcecream([FromBody] IcecreamGetDTO icecreamGetDTO)
        {
            if(ModelState.IsValid)
            {
                var IcecreamModel = mapper.Map<Icecream>(icecreamGetDTO);

                var Icecream = await icecreamRepository.PostIcecreamAsync(IcecreamModel);
                if (Icecream == null)
                {
                    return BadRequest(new { Message = "Please send a Correct object, Adding to Database." });
                }

                return Ok(mapper.Map<IcecreamSendDTO>(Icecream));
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Writer")]
        public async Task<ActionResult<IcecreamSendDTO>> PutIcecream([FromRoute] Guid id, [FromBody] IcecreamGetDTO icecreamGetDTO)
        {
            var IcecreamModel = mapper.Map<Icecream>(icecreamGetDTO);

            var Icecream = await icecreamRepository.UpdateIcecreamAsync(id, IcecreamModel);
            if (Icecream == null)
            {
                return BadRequest(new { Message = "Please send a Correct object or id for Updation" });
            }
            return Ok(mapper.Map<IcecreamSendDTO>(Icecream));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Writer, Reader")]
        public async Task<ActionResult<IcecreamSendDTO>> DeleteIcecream([FromRoute] Guid id)
        {
            var Icecream = await icecreamRepository.DeleteIcecreamAsync(id);
            if (Icecream == null)
            {
                return BadRequest(new { Message = "Please send a  id for Deletion" });
            }
            return Ok(mapper.Map<IcecreamSendDTO>(Icecream));
        }
    }
}
