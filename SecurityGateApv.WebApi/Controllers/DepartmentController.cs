using Microsoft.AspNetCore.Mvc;
using SecurityGateApv.Application.DTOs.Req;
using SecurityGateApv.Application.Services;
using SecurityGateApv.Application.Services.Interface;
using SecurityGateApv.Domain.Models;

namespace SecurityGateApv.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet("{deparmentId}")]
        public async Task<ActionResult> GetAllDepartmentPaging(int deparmentId)
        {
            var result = await _departmentService.GetById(deparmentId);
            return Ok(result.Value);
        }
        [HttpGet]
        public async Task<ActionResult> GetAllDepartmentPaging( int pageNumber, int pageSize)
        {
            if (pageNumber == -1 || pageSize == -1)
            {
                var resultAll = await _departmentService.GetAllByPaging(1, int.MaxValue);
                return Ok(resultAll.Value);
            }
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }

            var result = await _departmentService.GetAllByPaging(pageNumber, pageSize);
            return Ok(result.Value);
        }
        [HttpPost]
        public async Task<ActionResult> CreateDepartment(DepartmentCreateCommand command)
        {
            var result = await _departmentService.CreateDepartment(command);
            return Ok(result.Value);
        }
        [HttpPut("{departmentId}")]
        public async Task<ActionResult> UpdateDepartment(int departmentId, DepartmentCreateCommand command)
        {
            var result = await _departmentService.UpdateDepartment(departmentId,command);
            return Ok(result.Value);
        }        
        [HttpDelete("{departmentId}")]
        public async Task<ActionResult> DeleteDepartment(int departmentId)
        {
            var result = await _departmentService.UnactiveDepartment(departmentId);
            return Ok(result.Value);
        }
    }
}
