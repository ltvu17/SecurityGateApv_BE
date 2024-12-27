using SecurityGateApv.Application.DTOs.Req;
using SecurityGateApv.Application.DTOs.Res;
using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.Services.Interface
{
    public interface IDepartmentService
    {
        public Task<Result<List<GetDepartmentRes>>> GetAllByPaging(int pageNumber, int pageSize);
        public Task<Result<GetDepartmentRes>> GetById(int departmentId);
        public Task<Result<DepartmentCreateCommand>> CreateDepartment(DepartmentCreateCommand command);
        public Task<Result<DepartmentCreateCommand>> UpdateDepartment(int departmentId,DepartmentCreateCommand command);
        public Task<Result<bool>> UnactiveDepartment(int departmentId);

    }
}
