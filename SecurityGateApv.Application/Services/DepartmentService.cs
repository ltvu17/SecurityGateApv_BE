using AutoMapper;
using SecurityGateApv.Application.DTOs.Req;
using SecurityGateApv.Application.DTOs.Res;
using SecurityGateApv.Application.Services.Interface;
using SecurityGateApv.Domain.Errors;
using SecurityGateApv.Domain.Interfaces.Repositories;
using SecurityGateApv.Domain.Models;
using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.Services
{
    public class DepartmentService : IDepartmentService
    {

        private readonly IDepartmentRepo _departmentRepo1;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentService( IMapper mapper, IUnitOfWork unitOfWork, IDepartmentRepo departmentRepo)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _departmentRepo1 = departmentRepo;
        }

        public async Task<Result<DepartmentCreateCommand>> CreateDepartment(DepartmentCreateCommand command)
        {
            var department = Department.Create(command.DepartmentName,
                command.Description,
                command.AcceptLevel);
            if (department.IsFailure)
            {
                return Result.Failure<DepartmentCreateCommand> (Error.CreateDepartment);
            }
            await _departmentRepo1.AddAsync(department.Value);
            if(!await _unitOfWork.CommitAsync())
            {
                return Result.Failure<DepartmentCreateCommand>(Error.SaveToDBError);

            }
            await _unitOfWork.CommitAsync();
            return command;
        }

        public async Task<Result<List<GetDepartmentRes>>> GetAllByPaging(int pageNumber, int pageSize)
        {
            var departments = await _departmentRepo1.FindAsync(
                s => true, pageSize, pageNumber
                );
            var result = _mapper.Map<List<GetDepartmentRes>>(departments);

            if (result.Count == 0)
            {
                return Result.Failure<List<GetDepartmentRes>>(Error.NotFoundDepartment);
            }
            return result;
        }

        public async Task<Result<GetDepartmentRes>> GetById(int departmentId)
        {
            var department = (await _departmentRepo1.FindAsync(s => s.DepartmentId == departmentId)).FirstOrDefault();
            return _mapper.Map<GetDepartmentRes>(department); 
        }

        public async Task<Result<bool>> UnactiveDepartment(int departmentId)
        {
            var department = (await _departmentRepo1.FindAsync(s => s.DepartmentId == departmentId)).FirstOrDefault();
            if (department == null)
            {
                return Result.Failure<bool>(Error.NotFoundDepartment);
            }
            department.Delete();
            await _departmentRepo1.UpdateAsync(department);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<Result<DepartmentCreateCommand>> UpdateDepartment(int departmentId, DepartmentCreateCommand command)
        {
            var department = (await _departmentRepo1.FindAsync(s => s.DepartmentId == departmentId)).FirstOrDefault();
            if (department == null)
            {
                Result.Failure<DepartmentCreateCommand>(Error.NotFoundDepartment);
            }
            department = _mapper.Map(command, department);
            department.Update();
            await _departmentRepo1.UpdateAsync(department);
            await _unitOfWork.CommitAsync();
            return command;
        }
    }
}
