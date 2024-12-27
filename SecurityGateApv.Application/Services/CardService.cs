using AutoMapper;
using Microsoft.AspNetCore.Http;
using SecurityGateApv.Application.DTOs.Res;
using SecurityGateApv.Application.Services.Interface;
using SecurityGateApv.Domain.Errors;
using SecurityGateApv.Domain.Interfaces.AWS;
using SecurityGateApv.Domain.Interfaces.DomainDTOs;
using SecurityGateApv.Domain.Interfaces.ExtractImage;
using SecurityGateApv.Domain.Interfaces.Repositories;
using SecurityGateApv.Domain.Shared;
using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityGateApv.Application.DTOs.Req.CreateReq;
using Microsoft.AspNetCore.Http.Metadata;
using SecurityGateApv.Domain.Enums;

namespace SecurityGateApv.Application.Services
{
    public class CardService : ICardService
    {
        private readonly IExtractQRCode _extractQRCode;
        private readonly ICardRepo _qrRCardRepo;
        private readonly ICardTypeRepo _cardTypeRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAWSService _awsService;
        private readonly IPrivateKeyRepo _privateKeyRepo;
        private readonly IVisitCardRepo _visitCardRepo;


        public CardService(IExtractQRCode extractQRCode, IMapper mapper, IUnitOfWork unitOfWork,
            IAWSService awsService, ICardRepo qrRCardRepo, IPrivateKeyRepo privateKeyRepo, ICardTypeRepo cardTypeRepo, IVisitCardRepo visitCardRepo)
        {
            _extractQRCode = extractQRCode;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _qrRCardRepo = qrRCardRepo;
            _awsService = awsService;
            _privateKeyRepo = privateKeyRepo;
            _qrRCardRepo = qrRCardRepo;
            _cardTypeRepo = cardTypeRepo;
            _visitCardRepo = visitCardRepo;
        }



        public string DecodeQRCodeFromImage(IFormFile imageStream)
        {
            var result = _extractQRCode.ExtractQrCodeFromImage(imageStream);
            return result;
        }

        public async Task<Result<GetCardRes>> GenerateCard(CreateCardCommand command)
        {
            var card = (await _qrRCardRepo.FindAsync(
                s => s.CardVerification.Equals(command.CardVerified)
                )).FirstOrDefault();
            if (card != null)
            {
                return Result.Failure<GetCardRes>(Error.DuplicateCard);
            }

            var cardType = (await _cardTypeRepo.FindAsync(
                s => s.CardTypeId.Equals(command.CardTypeId)
                )).FirstOrDefault();
            if (cardType == null)
            {
                return Result.Failure<GetCardRes>(Error.NotFoundCardType);
            }
            //var qrCard = QRCard.Create(1, 2, cardGuid, );
            var cardGenerate = await _qrRCardRepo.GenerateQRCard(command.CardVerified, command.ImageLoGo, cardType.CardTypeName);
            var qrCoder = _mapper.Map<GetCardRes>(cardGenerate);
            return qrCoder;
        }

        public async Task<Result<List<GetCardRes>>> GetAllByPaging(int pageNumber, int pageSize)
        {
            var card = await _qrRCardRepo.FindAsync(
                s => true, pageSize, pageNumber, includeProperties: "CardType"
                );
            if (card == null)
            {
                return Result.Failure<List<GetCardRes>>(Error.NotFoundCard);
            }

            var result = _mapper.Map<List<GetCardRes>>(card);
            return result;



        }

        public async Task<Result<AWSDomainDTO>> DetectShoe(IFormFile image)
        {
            var result = new AWSDomainDTO();
            var key = (await _privateKeyRepo.GetAllAsync()).FirstOrDefault();
            var label = await _awsService.DetectLabelService(image, key);
            label = label.Where(s => s.Label.Equals("Sandal", StringComparison.OrdinalIgnoreCase) || s.Label.Equals("Shoe", StringComparison.OrdinalIgnoreCase)).ToArray();
            if (label.Count == 0)
            {
                return Result.Failure<AWSDomainDTO>(Error.DetectionError);
            }
            result = label.ToArray()[0];
            foreach (var item in label)
            {
                if (result.Label == "Sandal")
                {
                    result = item;
                    break;
                }
                else
                {
                    result = item;
                }
            }
            if(result.Label == "Sandal")
            {
                return Result.Failure<AWSDomainDTO>(Error.DetectionError);
            }
            return result;
        }

        public async Task<Result<GetCardRes>> GetQrCardByCardVerification(string cardVerified)
        {
            var card = (await _qrRCardRepo.FindAsync(
                s => s.CardVerification.Equals(cardVerified), includeProperties: "CardType"
                )).FirstOrDefault();
            if (card == null)
            {
                return Result.Failure<GetCardRes>(Error.NotFoundCard);
            }

            var result = _mapper.Map<GetCardRes>(card);
            return result;
        }
        public async Task<Result<GetCardRes>> GetQrCardByCardCredentialCard(string credentialCard)
        {
            var card = (await _qrRCardRepo.FindAsync(
                 s => s.VisitCards.Any(s => s.Visitor.CredentialsCard == credentialCard) && s.VisitCards.Any(s => s.VisitCardStatus == VisitCardStatusEnum.Issue.ToString()), includeProperties: "CardType"
                 )).FirstOrDefault();
            if (card == null)
            {
                return Result.Failure<GetCardRes>(Error.NotFoundCardByCardVerification);
            }

            var result = _mapper.Map<GetCardRes>(card);
            return result;
        }
        public async Task<Result<bool>> CreateCard(CreateCardCommand command)
        {
            var card = (await _qrRCardRepo.FindAsync(
                s => s.CardVerification.Equals(command.CardVerified),
                includeProperties: "CardType"
                )).FirstOrDefault();
            if (card != null)
            {
                return Result.Failure<bool>(Error.DuplicateCard);
            }

            var cardType = (await _cardTypeRepo.FindAsync(
               s => s.CardTypeId.Equals(command.CardTypeId)
               )).FirstOrDefault();
            if (cardType == null)
            {
                return Result.Failure<bool>(Error.NotFoundCardType);
            }
            var qrCoder = _qrRCardRepo.GenerateQRCard(command.CardVerified, command.ImageLoGo, cardType.CardTypeName).Result;

            var qrCard = Card.Create(command.CardTypeId, command.CardVerified, qrCoder.CardImage);
            await _qrRCardRepo.AddAsync(qrCard);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<Result<bool>> UpdateCardStatusLost(string credentialCard, string newCard)
        {
            var visitCard = (await _visitCardRepo.FindAsync(
                               s => s.Visitor.CredentialsCard == credentialCard
                               && s.VisitCardStatus == VisitCardStatusEnum.Issue.ToString(),
                               includeProperties: "Card"
                                              )).FirstOrDefault();
            if (visitCard == null)
            {
                return Result.Failure<bool>(Error.CardNotIssue);
            }
            var newCardEntiry = (await _qrRCardRepo.FindAsync(
                    s => s.CardVerification == newCard
                )).FirstOrDefault();
            if (visitCard == null)
            {
                return Result.Failure<bool>(Error.NotFoundCard);
            }
            visitCard.CancelCardLost();
            visitCard = VisitCard.Create(DateTime.Now, visitCard.ExpiryDate, "Issue", visitCard.VisitorId, newCardEntiry.CardId);

            await _visitCardRepo.UpdateAsync(visitCard);
            if (!await _unitOfWork.CommitAsync())
            {
                return Result.Failure<bool>(Error.CommitError);

            }

            return true;
        }
    }
}
