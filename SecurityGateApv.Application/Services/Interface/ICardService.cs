using Microsoft.AspNetCore.Http;
using SecurityGateApv.Application.DTOs.Res;
using SecurityGateApv.Domain.Shared;
using SecurityGateApv.Domain.Interfaces.DomainDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityGateApv.Application.DTOs.Req.CreateReq;

namespace SecurityGateApv.Application.Services.Interface
{
    public interface ICardService
    {
        public string DecodeQRCodeFromImage(IFormFile imageStream);
        public Task<Result<AWSDomainDTO>> DetectShoe(IFormFile image);
        public Task<Result<GetCardRes>> GenerateCard(CreateCardCommand command);
        public Task<Result<bool>> CreateCard( CreateCardCommand command);
        public Task<Result<bool>> UpdateCardStatusLost( string credentialCard, string newCard);
        public Task<Result<List<GetCardRes>>> GetAllByPaging(int pageNumber, int pageSize);
        public Task<Result<GetCardRes>> GetQrCardByCardVerification(string cardVerified);
        public Task<Result<GetCardRes>> GetQrCardByCardCredentialCard(string credentialCard);

    }
}
