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
    public class VisitCardService : IVisitCardService
    {
        private readonly IVisitDetailRepo _visitDetailRepo;
        private readonly ICardRepo _cardRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVisitCardRepo _visitCardRepo;
        private readonly IMapper _mapper;

        public VisitCardService(IVisitDetailRepo visitDetailRepo, ICardRepo cardRepo, IUnitOfWork unitOfWork, IVisitCardRepo visitCardRepo, IMapper mapper)
        {
            _visitDetailRepo = visitDetailRepo;
            _cardRepo = cardRepo;
            _unitOfWork = unitOfWork;
            _visitCardRepo = visitCardRepo;
            _mapper = mapper;
        }
        public async Task<Result<VisitCardRes>> CreateVisitCard(int visitDetailId, string cardVerification)
        {
            var visitDetail = (await _visitDetailRepo.FindAsync(s => s.VisitDetailId == visitDetailId, includeProperties: "Visit")).FirstOrDefault();
            if (visitDetail == null)
            {
                return Result.Failure<VisitCardRes>(Error.NotFoundVisitDetail);
            }
            var card = (await _cardRepo.FindAsync(
                s => s.CardVerification == cardVerification
                               )).FirstOrDefault();
            if (card == null)
            {
                return Result.Failure<VisitCardRes>(Error.NotFoundCard);
            }

            //var visitCard = VisitCard.Create(DateTime.Now, visitDetail.Visit.ExpectedEndTime, "Issue", visitDetailId, card.CardId);

            //await _visitCardRepo.AddAsync(visitCard);
            //await _unitOfWork.CommitAsync();
            //var result = _mapper.Map<VisitCardRes>(visitCard);
            return null;
        }
    }
}
