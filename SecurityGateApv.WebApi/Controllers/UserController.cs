using Microsoft.AspNetCore.Mvc;
using SecurityGateApv.Application.DTOs.Req;
using SecurityGateApv.Application.DTOs.Req.UpdateReq;
using SecurityGateApv.Application.DTOs.Res;
using SecurityGateApv.Application.Services.Interface;
using SecurityGateApv.Domain.Errors;
using SecurityGateApv.Domain.Shared;
using System.ComponentModel.DataAnnotations;
using Error = SecurityGateApv.Domain.Errors.Error;

namespace SecurityGateApv.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginModel loginModel)
        {
            var result = await _userService.Login(loginModel);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPost("User/OTP")]
        public async Task<IActionResult> SendOTP([Required(ErrorMessage = "Yêu cầu nhập email")]string email)
        {
            var result = await _userService.SendOTPResetPassword(email);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPost("User/ConfirmOTP")]
        public async Task<IActionResult> ConfirmOTP([Required(ErrorMessage = "Yêu cầu nhập email")] string email, [Required(ErrorMessage = "Yêu cầu nhập OTP")] string OTP)
        {
            var result = await _userService.ConfirmOTPResetPassword(email, OTP);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPost("User/SetNewPassword")]
        public async Task<IActionResult> SetNewPassword([Required(ErrorMessage = "Yêu cầu nhập email")] string email, [Required(ErrorMessage = "Yêu cầu nhập OTP")] string OTP, [Required(ErrorMessage = "Yêu cầu nhập mật khẩu mới")] string password)
        {
            if(password.Length < 6)
            {
                return BadRequest(new Error("SetNewPassword", "Mật khẩu phải lớn hơn 6 kí tự"));
            }
            var result = await _userService.SetNewPassword(email, OTP, password);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUserPaging(int pageNumber, int pageSize, string role)
        {
            if (pageNumber == -1 || pageSize == -1)
            {
                var resultAll = await _userService.GetUserByRolePaging(1, int.MaxValue, role);
                return Ok(resultAll.Value);
            }
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var result = await _userService.GetUserByRolePaging(pageNumber, pageSize, role);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("{userId}")]
        public async Task<ActionResult> GetAllUserDetail(int userId)
        {
            var result = await _userService.GetUserById(userId);
            if (result.IsFailure) {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("Staff/{phonenumber}")]
        public async Task<ActionResult> GetStaffByPhone(string phonenumber)
        {
            var result = await _userService.GetStaffByPhone(phonenumber);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpGet("Staff/DepartmentManager/{departmentManagerId}")]
        public async Task<ActionResult> GetAllStaffPagingByDepartmentManagerId(int pageNumber, int pageSize, int departmentManagerId)
        {
            if (pageNumber == -1 || pageSize == -1)
            {
                var resultAll = await _userService.GetAllStaffPagingByDepartmentManagerId(1, int.MaxValue, departmentManagerId);
                return Ok(resultAll.Value);
            }
            if (pageNumber <= 0 || pageSize <= 0 || departmentManagerId == null)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var result = await _userService.GetAllStaffPagingByDepartmentManagerId(pageNumber, pageSize, departmentManagerId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);

        }

        [HttpGet("Department/{departmentId}")]
        public async Task<ActionResult> GetAllUserByDepartmentId(int pageNumber, int pageSize, int departmentId)
        {
            if ( departmentId == null)
            {
                return BadRequest(Result.Failure<List<GetUserRes>>(Error.NullInput));
            }
            var result = await _userService.GetAllUserPagingByDepartmentId(pageNumber, pageSize, departmentId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserComman command)
        {
            var token = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new Error("CreateUser", "Invalid Token"));
            }
            var result = await _userService.CreateUser(command, token);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult> UpdateUser(int userId, [FromBody] UpdateUserCommand command)
        {
            var token = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new Error("CreateUser", "Invalid Token"));
            }
            var result = await _userService.UpdateUser(userId, command, token);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("Password/{userId}")]
        public async Task<ActionResult> UpdatePassWord(int userId, [FromBody] UpdateUserPasswordCommand command)
        {
            var token = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new Error("UpdatePassWord", "Invalid Token"));
            }
            var result = await _userService.UpdateUserPassword(userId, command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPut("NoDepartmentId/{userId}")]
        public async Task<ActionResult> UpdateUserNodepartmentId(int userId, [FromBody] UpdateUserNoDepartmentIdCommand command)
        {
            var token = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new Error("UpdateUserNodepartmentId", "Invalid Token"));
            }
            var result = await _userService.UpdateUserNodepartmentId(userId, command, token);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpDelete("{userId}")]
        public async Task<ActionResult> UnactiveUser(int userId)
        {
            var token = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new Error("CreateUser", "Invalid Token"));
            }
            var result = await _userService.UnactiveUser(userId, token);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
        [HttpPost("Testemail")]
        public async Task<ActionResult> TestEmail()
        {
            var result = await _userService.SendEmailTest("");

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }
            return Ok(result.Value);
        }
    }
}
