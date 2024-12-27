using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using SecurityGateApv.Application.DTOs.Req.CreateReq;
using SecurityGateApv.Application.DTOs.Req.UpdateReq;
using SecurityGateApv.Application.DTOs.Res;
using SecurityGateApv.Application.Services.Interface;
using SecurityGateApv.Domain.Common;
using SecurityGateApv.Domain.Errors;
using SecurityGateApv.Domain.Interfaces.Jwt;
using SecurityGateApv.Domain.Interfaces.Repositories;
using SecurityGateApv.Domain.Models;
using SecurityGateApv.Domain.Shared;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.Services
{
    public class VisitorService : IVisitorService
    {
        private readonly IVisitorRepo _visitorRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwt _jwt;

        public VisitorService(IVisitorRepo visitorRepo, IMapper mapper, IUnitOfWork unitOfWork, IJwt jwt)
        {
            _visitorRepo = visitorRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _jwt = jwt;
        }
        public async Task<Result<GetVisitorCreateRes>> CreateVisitor(CreateVisitorCommand command, string token)
        {
            SixLabors.ImageSharp.Image resizeFrontImage = SixLabors.ImageSharp.Image.Load(command.VisitorCredentialFrontImageFromRequest.OpenReadStream());
            int heightFornt = (int)((300 / (float)resizeFrontImage.Width) * resizeFrontImage.Height);
            if (resizeFrontImage.Width > 300 || resizeFrontImage.Height > 200)
            {
                resizeFrontImage.Mutate(x => x.Resize(300, heightFornt));
            }
            var imageFrontString = await ImageToBase64(resizeFrontImage);
            var imageFrontEncrypt = await CommonService.Encrypt(imageFrontString);
            
            
            SixLabors.ImageSharp.Image resizeBackImage = SixLabors.ImageSharp.Image.Load(command.VisitorCredentialBackImageFromRequest.OpenReadStream());
            int heightBack = (int)((300 / (float)resizeBackImage.Width) * resizeBackImage.Height);
            if (resizeBackImage.Width > 300 || resizeBackImage.Height > 200)
            {
                resizeBackImage.Mutate(x => x.Resize(300, heightBack));
            }
            var imageBackString = await ImageToBase64(resizeBackImage);
            var imageBackEncrypt = await CommonService.Encrypt(imageBackString);

            SixLabors.ImageSharp.Image resizeBlurImage = SixLabors.ImageSharp.Image.Load(command.VisitorCredentialBlurImageFromRequest.OpenReadStream());
            int heightBlur = (int)((300 / (float)resizeBlurImage.Width) * resizeBlurImage.Height);
            if (resizeBlurImage.Width > 300 || resizeBlurImage.Height > 200)
            {
                resizeBlurImage.Mutate(x => x.Resize(300, heightBlur));
            }
            var imageBlurString = await ImageToBase64(resizeBlurImage);
            var imageBlurEncrypt = await CommonService.Encrypt(imageBlurString);

            var userId = _jwt.DecodeJwtUserId(token);

            var visitorCreate = Visitor.Create(
                command.VisitorName,
                command.CompanyName,
                command.PhoneNumber,
                DateTime.Now,
                DateTime.Now,
                command.CredentialsCard,
                imageFrontEncrypt,
                imageBackEncrypt,
                imageBlurEncrypt,
                "Active",
                command.CredentialCardTypeId,
                command.Email,
                userId
                );
            if (visitorCreate.IsFailure)
            {
                return Result.Failure<GetVisitorCreateRes>(Error.CreateVisitor);
            }
            await _visitorRepo.AddAsync(visitorCreate.Value);
            var commit = await _unitOfWork.CommitAsync();
            if (!commit)
            {
                return Result.Failure<GetVisitorCreateRes>(Error.CommitError);
            }
            var images = new List<VisitorImage>();
            foreach (var image in visitorCreate.Value.VisitorImage)
            {
                images.Add(image.DecryptResponseImage(await CommonService.Decrypt(image.ImageURL)).Value);
            }
            visitorCreate.Value.DecrypCredentialCard(images);
            var res = _mapper.Map<GetVisitorCreateRes>(visitorCreate.Value);
            
            return res;
        }

        public async Task<Result<bool>> DeleteVisitor(int visitorId)
        {
            var visitor = (await _visitorRepo.FindAsync(s => s.VisitorId == visitorId)).FirstOrDefault();
            if (visitor == null)
            {
                return Result.Failure<bool>(Error.NotFoundVisitor);
            }
            visitor.Delete();
            await _visitorRepo.UpdateAsync(visitor);
            var commit = await _unitOfWork.CommitAsync();
            if (!commit)
            {
                return Result.Failure<bool>(Error.CommitError);
            }
            return true;
        }

        public async Task<Result<List<GetVisitorRes>>> GetAllByPaging(int pageNumber, int pageSize)
        {
            var list = await _visitorRepo.FindAsync(s=> true, pageSize, pageNumber,s => s.OrderByDescending(z => z.CreateDate), includeProperties: "CredentialCardType, CreateBy");
            if(list.Count() == 0)
            {
                return Result.Failure<List<GetVisitorRes>>(Error.NotFound);
            }
            //foreach (var item in list) {
            //    try
            //    {
            //        var images = new List<VisitorImage>();
            //        foreach (var image in item.VisitorImage)
            //        {
            //            images.Add(image.DecryptResponseImage(await CommonService.Decrypt(image.ImageURL)).Value);
            //        }
            //        item.DecrypCredentialCard(images);
            //    }
            //    catch (Exception ex)
            //    {
                    
            //    }
            //}
            return _mapper.Map<List<GetVisitorRes>>(list);
        }

        public async Task<Result<GetVisitorRes>> GetByCredentialCard(string cardNumber)
        {
            var visitor = (await _visitorRepo.FindAsync(s => s.CredentialsCard == cardNumber , includeProperties: "CredentialCardType, VisitorImage,CreateBy")).FirstOrDefault();
            if (visitor == null)
            {
                return Result.Failure<GetVisitorRes>(Error.NotFoundVistor);
            }
            try
            {
                var images = new List<VisitorImage>();
                foreach(var image in visitor.VisitorImage)
                {
                    images.Add(image.DecryptResponseImage(await CommonService.Decrypt(image.ImageURL)).Value);
                }
                visitor.DecrypCredentialCard(images);
            }
            catch (Exception ex)
            {
                return Result.Failure<GetVisitorRes>(Error.DecryptError);
            }
            return _mapper.Map<GetVisitorRes>(visitor);
        }

        public async Task<Result<GetVisitorRes>> GetById(int visitorId)
        {
            var visitor = (await _visitorRepo.FindAsync(s => s.VisitorId == visitorId, includeProperties: "CredentialCardType, VisitorImage,CreateBy")).FirstOrDefault();
            if (visitor == null)
            {
                return Result.Failure<GetVisitorRes>(Error.NotFound);
            }
            try
            {
                var images = new List<VisitorImage>();
                foreach (var image in visitor.VisitorImage)
                {
                    images.Add(image.DecryptResponseImage(await CommonService.Decrypt(image.ImageURL)).Value);
                }
                visitor.DecrypCredentialCard(images);
            }
            catch (Exception ex) {
                return Result.Failure<GetVisitorRes>(Error.DecryptError);
            }
            return _mapper.Map<GetVisitorRes>(visitor);
        }

        public async Task<Result<GetVisitorCreateRes>> UpdateVisitor(int visitorId, UpdateVisitorCommand command)
        {
            var duplicateCard = await _visitorRepo.IsAny(s => s.CredentialsCard == command.CredentialsCard && s.CredentialCardTypeId == command.CredentialCardTypeId && s.VisitorId != visitorId);
            if (duplicateCard)
            {
                return Result.Failure<GetVisitorCreateRes>(Error.DuplicateCardNumber);
            }
            var visitor = (await _visitorRepo.FindAsync(s => s.VisitorId == visitorId, includeProperties: "VisitorImage")).FirstOrDefault();
            if (visitor == null)
            {
                return Result.Failure<GetVisitorCreateRes>(Error.NotFoundVistor);
            }
           // var imageEncrypt = await CommonService.Encrypt(command.VisitorCredentialImageFromRequest);
            visitor = _mapper.Map(command, visitor);
            visitor.Update(await CommonService.Encrypt(command.VisitorCredentialFrontImageFromRequest), await CommonService.Encrypt(command.VisitorCredentialBackImageFromRequest), await CommonService.Encrypt(command.VisitorCredentialBlurImageFromRequest), command.CredentialCardTypeId);
            await _visitorRepo.UpdateAsync(visitor);
            var commit = await _unitOfWork.CommitAsync();
            if (!commit)
            {
                return Result.Failure<GetVisitorCreateRes>(Error.CommitError);
            }
            var images = new List<VisitorImage>();
            foreach (var image in visitor.VisitorImage)
            {
                images.Add(image.DecryptResponseImage(await CommonService.Decrypt(image.ImageURL)).Value);
            }
            visitor.DecrypCredentialCard(images);
            var res = _mapper.Map<GetVisitorCreateRes>(visitor);
            return res;
        }
        private async Task<string> ImageToBase64(Image image)
        {
            var ms = new MemoryStream();
            await image.SaveAsync(ms, new PngEncoder());
            return System.Convert.ToBase64String(ms.ToArray());
        }
    }
}
