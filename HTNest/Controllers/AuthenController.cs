using AutoMapper;
using HTNest.Data.Entities;
using HTNest.Data.Model.Login;
using HTNest.Data.ViewModels;
using HTNest.Service.Interfaces;
using HTNest.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_Cursus_Group3.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HTNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {

        private readonly IAuthenService _authenService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenController(IMapper mapper, IAuthenService authenService, IUnitOfWork unitOfWork)
        {
            _authenService = authenService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null || string.IsNullOrEmpty(loginModel.Email) || string.IsNullOrEmpty(loginModel.Password))
            {
                return BadRequest(new { code = 400, message = "Invalid username or password" });
            }

            
            try
            {
                _unitOfWork.BeginTransaction();
                var token = await _authenService.Login(loginModel);
                if (string.IsNullOrEmpty((string?)token))
                {
                    return Unauthorized(new { code = 401, message = "Invalid username or password" });
                }
                // Lưu token vào cookie HTTP-only
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = false,
                    Secure = true,   // Chạy trên HTTPS
                    SameSite = SameSiteMode.None, // Hoặc SameSiteMode.Lax nếu chỉ cần GET requests
                    Expires = DateTime.Now.AddDays(7),
                    Path = "/",
                    //Domain = "coursev1.vercel.app"
                };
                Response.Cookies.Append("authToken", (string)token, cookieOptions);
                _unitOfWork.CommitTransaction();

                return Ok(new { code = 200, token = token, message = "Login successful" });
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                return StatusCode(StatusCodes.Status500InternalServerError, new { code = 500, message = ex.Message });
            }
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterLoginModel registerLoginModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new { Errors = errors });
            }

            var newAccount = _mapper.Map<User>(registerLoginModel);
            if (newAccount == null)
            {
                return BadRequest("Invalid data");
            }
            newAccount = await _authenService.Register(newAccount);

            return Ok(_mapper.Map<RegisterViewModel>(newAccount));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _authenService.Logout(HttpContext);
            if (result)
            {
                return Ok(new { message = "Logout successful" });
            }
            return BadRequest(new { message = "Logout failed" });
        }
    }
}
