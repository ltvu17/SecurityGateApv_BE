using SecurityGateApv.Application.DTOs.Req.CreateReq;
using SecurityGateApv.Application.DTOs.Req.UpdateReq;
using SecurityGateApv.Application.DTOs.Res;
using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.Services.Interface
{
    public interface IVisitorService
    {
        public Task<Result<List<GetVisitorRes>>> GetAllByPaging(int pageNumber, int pageSize);
        public Task<Result<GetVisitorRes>> GetById(int visitorId);
        public Task<Result<GetVisitorRes>> GetByCredentialCard(string cardNumber);
        public Task<Result<GetVisitorCreateRes>> CreateVisitor(CreateVisitorCommand command, string token);
        public Task<Result<GetVisitorCreateRes>> UpdateVisitor(int visitorId,UpdateVisitorCommand command);
        public Task<Result<bool>> DeleteVisitor(int visitorId);
    }
}
