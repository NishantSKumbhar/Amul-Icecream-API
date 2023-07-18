using Amul.Data;
using Amul.Models.Domain;
using Amul.Models.DTO;
using Amul.Repositories;
using AutoMapper;
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
        private readonly IMapper mapper;
        private readonly IIcecreamRepository icecreamRepository;

        public IcecremeController(AmulDbContext amulDbContext, IMapper mapper, IIcecreamRepository icecreamRepository)
        {
            this.amulDbContext = amulDbContext;
            this.mapper = mapper;
            this.icecreamRepository = icecreamRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<IcecreamSendDTO>>> GetAllIcecreams()
        {
            //var Icecreams = await amulDbContext.Icecreams.Include("Category").ToListAsync();
            var IcecreamsModel = await icecreamRepository.GetAllIcecreamsAsync();
            
            if(IcecreamsModel == null)
            {
                return NotFound(new { Message = "Icecreams are not available in the Database."});
            }
            var IcecreamSenDTO = mapper.Map<List<IcecreamSendDTO>>(IcecreamsModel);
            
            return Ok(IcecreamSenDTO);
        }

        [HttpGet("{id}")]
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
