using AutoMapper;
using HTNest.Data.Entities;
using HTNest.Data.Model.Style;
using HTNest.Data.ViewModels;
using HTNest.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HTNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StyleController : ControllerBase
    {
        private readonly IStyleService _styleService;
        private readonly IMapper _mapper;
        public StyleController(IStyleService styleService, IMapper mapper)
        {
            _styleService = styleService;
            _mapper = mapper;

        }
        // GET: api/<StyleController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Style>>> GetAll(string? styleName = null)
        {
            return Ok(await _styleService.GetAllAsync(styleName));
        }

        // GET api/<StyleController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Manager, Admin, Client")]

        public async Task<ActionResult<Style>> GetById(int id)
        {
            var style = await _styleService.GetById(id);
            if (style == null)
            {
                return NotFound();
            }
            return Ok(style);
        }

        // POST api/<StyleController>
        [Authorize(Roles = "Manager, Admin")]
        [HttpPost]
        public async Task<ActionResult<Style>> Add ([FromBody] CreateStyleModel CreateStyleModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var style = _mapper.Map<Style>(CreateStyleModel);

                if (style == null)
                {
                    return BadRequest("Invalid style data");
                }
                style = await _styleService.Add(style);
                return Ok(_mapper.Map<StyleViewModel>(style));

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });

            }

        }
        // PUT api/<StyleController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Manager, Admin")]

        public async Task<ActionResult<Style>> Update(int id, [FromBody] UpdateStyleModel updateStyleModel)
        {
            var updateStyle = _mapper.Map<Style>(updateStyleModel);

            if (updateStyle == null)
            {
                    
                    return BadRequest("Invalid Style data");
            }
            var styleUpdate = await _styleService.Update(updateStyle, id);
            return Ok(_mapper.Map<StyleViewModel>(styleUpdate));
        }

        // DELETE api/<StyleController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager, Admin")]

        public async Task<ActionResult<Style>> Delete(int id)
        {
            var style = await _styleService.GetById(id);
            if (style == null)
            {
                return NotFound();
            }
            await _styleService.Delete(id);
            return Ok(style);
        }
    }
}
