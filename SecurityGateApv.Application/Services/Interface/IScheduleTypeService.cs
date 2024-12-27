using SecurityGateApv.Application.DTOs.Res;
using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.Services.Interface
{
    public interface IScheduleTypeService
    {
        public Task<Result<List<GetScheduleTypeRes>>> GetAllScheduleType();

    }
}
