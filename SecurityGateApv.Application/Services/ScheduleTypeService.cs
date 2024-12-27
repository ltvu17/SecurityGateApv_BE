using AutoMapper;
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
    public class ScheduleTypeService : IScheduleTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IScheduleTypeRepo _scheduleTypeRepo;
        private readonly IMapper _mapper;

        public ScheduleTypeService(IUnitOfWork unitOfWork, IMapper mapper, IScheduleTypeRepo scheduleTypeRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _scheduleTypeRepo = scheduleTypeRepo;
        }

        public async Task<Result<List<GetScheduleTypeRes>>> GetAllScheduleType()
        {
           var scheduleType = await _scheduleTypeRepo.FindAsync(
                    s => s.Status == true,
                    int.MaxValue,1
                );
            if (scheduleType == null)
            {
                return Result.Failure<List<GetScheduleTypeRes>>(Error.NotFoundSchedule);
            }
            var result = _mapper.Map<IEnumerable<GetScheduleTypeRes>>(scheduleType);
            return result.ToList();
        }
    }
}
