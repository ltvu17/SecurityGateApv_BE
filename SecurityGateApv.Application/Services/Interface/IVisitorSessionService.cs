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
    public interface IVisitorSessionService
    {
        public Task<Result<SessionCheckOutRes>> CheckOutWithCard(VisitorSessionCheckOutCommand command, string qrCardVerifi);
        public Task<Result<SessionCheckOutRes>> CheckOutWithCredentialCard(VisitorSessionCheckOutCommand command, string credentialCard);
        public Task<Result<ValidCheckinRes>> CheckInWithCredentialCard(VisitSessionCheckInCommand command);
        public Task<Result<List<VisitorSessionImageRes>>> GetAllImagesByVisitorSessionId(int visitorSessionId);
        public Task<Result<List<VehicleSessionRes>>> GetVehicleSessionByvisitorId(int visitorSessionId);
        public Task<Result<List<VehicleSessionRes>>> GetVehicleSessionByVisitId(int visitId);
        public Task<Result<List<VehicleSessionRes>>> GetVehicleSessionByVisitorId(int visitId, int visitorId);
        public Task<Result<ValidCheckinRes>> CheckInWithoutCredentialCard(VisitSessionCheckInCommand command);
        public Task<Result<ValidCheckinRes>> ValidCheckWithQRCardVerification(ValidCheckInCommand command);
        public Task<Result<ValidCheckinRes>> ValidCheckWithoutQRCardVerification(ValidCheckInCommand command);
        public Task<Result<ICollection<GetVisitorSessionRes>>> GetAllVisitorSession(int pageNumber, int pageSize, string token);
        public Task<Result<ICollection<GetVisitorSessionRes>>> GetVisitorSessionByDate(int pageNumber, int pageSize, DateTime date, string token);
        public Task<Result<ICollection<GetVisitorSessionGraphQLRes>>> GetAllVisitorSessionGraphQL(int pageNumber, int pageSize, string token);
        public Task<Result<ICollection<GetVisitorSessionRes>>> GetAllVisitorSessionByVisitorId(int pageNumber, int pageSize, int VisitorId);
        public Task<Result<ICollection<GetVisitorSessionRes>>> GetAllVisitorSessionByVisitId(int pageNumber, int pageSize, int visitId);
        public Task<Result<SessionCheckOutRes>> GetVisitSessionStatusCheckInByCardVerification(string qrCardVerified);
        public Task<Result<SessionCheckOutRes>> GetVisitorSessionStatusCheckInByCredentialIdId(string credentialId);


    }
}
