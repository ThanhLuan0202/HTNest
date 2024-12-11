using AutoMapper;
using HTNest.Data.Entities;
using HTNest.Data.Interfaces;
using HTNest.Data.Model.User;
using HTNest.Data.ViewModels;
using HTNest.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HTNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        // GET: api/<UserController>
        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<IEnumerable<User>>> GetAllGetAll()

        {
            return Ok(await _userService.GetAll());
        }

        // GET api/<UserController>/5
        [HttpGet("{userName}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<User>> GetUserName(string userName)
        {
            var checkUser = await _userService.GetByUserName(userName);
            return Ok(checkUser);
        }

        // POST api/<UserController>
        [HttpPost]



        // PUT api/<UserController>/5
        [HttpPut("{userName}")]
        [Consumes("multipart/form-data")]


        public async Task<ActionResult<User>> Add([FromBody] CreateUserModel createUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (createUserModel == null)
                {
                    return BadRequest("Invalid style data");
                }
                var newUser = await _userService.Add(createUserModel);
                return Ok(_mapper.Map<UserViewModel>(newUser));
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

        // DELETE api/<UserController>/5
        [HttpDelete("{userName}")]
        [Authorize(Roles = "Admin")]

        public async Task<ActionResult<User>> Delete(string userName)
        {
            var userDelete = await _userService.Delete(userName);
            return Ok(userDelete);
        }
    }
}
