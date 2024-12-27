using AutoMapper;
using SecurityGateApv.Application.DTOs.Req.CreateReq;
using SecurityGateApv.Application.DTOs.Req.UpdateReq;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SecurityGateApv.Application.Services
{
    public class GateService : IGateService
    {
        private readonly IGateRepo _gateRepo;
        private readonly ICameraRepo _cameraRepo;
        private readonly ICameraTypeRepo _cameraTypeRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GateService(IMapper mapper, IUnitOfWork unitOfWork, IGateRepo gateRepo, ICameraRepo cameraRepo, ICameraTypeRepo cameraTypeRepo)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _gateRepo = gateRepo;
            _cameraRepo = cameraRepo;
            _cameraTypeRepo = cameraTypeRepo;
        }


        public async Task<Result<List<CameraTypeRes>>> GetAllCameraType()
        {
            var cameraType = await _cameraTypeRepo.GetAllAsync();
            var res = _mapper.Map<List<CameraTypeRes>>(cameraType);
            return res;
        }

        public async Task<Result<List<GetGateRes>>> GetAllGate()
        {
            var gate = await _gateRepo.FindAsync(
                s => true,
                int.MaxValue,
                1,
                includeProperties: "Cameras.CameraType"
               );
            var res = _mapper.Map<List<GetGateRes>>(gate);
            return res;
        }
        public async Task<Result<GetGateRes>> GetGateById(int gateId)
        {
            var gate = (await _gateRepo.FindAsync(
               s => s.GateId == gateId,
               1,
               1,
               includeProperties: "Cameras.CameraType"
              )).FirstOrDefault();
            var res = _mapper.Map<GetGateRes>(gate);
            return res;
        }

        public async Task<Result<List<GetGateRes>>> GetAllGatePaging(int pageSize, int pageNumber)
        {
            var gate = await _gateRepo.FindAsync(
                s => true,
                pageSize,
                pageNumber,
                s => s.OrderByDescending(x => x.CreateDate),
                includeProperties: "Cameras.CameraType"
               );
            if (gate == null)
            {
                return Result.Failure<List<GetGateRes>>(Error.NotFound);
            }
            var result = _mapper.Map<List<GetGateRes>>(gate);
            return Result.Success(result);

        }

        public async Task<Result<List<CameraRes>>> GetCameraByGate(int gate)
        {
            var camera = await _cameraRepo.FindAsync(
                    s => s.GateId == gate,
                    int.MaxValue, 1,
                    includeProperties: "CameraType"
                );
            var res = _mapper.Map<List<CameraRes>>(camera);
            return res;
        }
        public async Task<Result<bool>> CreateGate(CreateGateCommand command)
        {

            var gate = Gate.Create(command.GateName, DateTime.Now, command.Description, true).Value;
            foreach (var item in command.Cameras)
            {
                var cameraType = await _cameraTypeRepo.GetByIdAsync(item.CameraTypeId);
                if (cameraType == null)
                {
                    return Result.Failure<bool>(Error.NotFound);
                }
                gate.AddCamera(item.CameraURL, item.Description, true, item.CameraTypeId);
            }


            await _gateRepo.AddAsync(gate);
            if (!await _unitOfWork.CommitAsync())
            {
                return Result.Failure<bool>(Error.CommitError);
            }

            return Result.Success(true);
        }

        public async Task<Result<bool>> UpdateGate(GateUpdateCommand command)
        {
            var gate = (await _gateRepo.FindAsync(
                     s => s.GateId == command.GateId,
                     1, 1,
                     includeProperties: "Cameras"
                 )).FirstOrDefault();

            if (gate == null)
            {
                return Result.Failure<bool>(Error.NotFound);
            }

            var updateResult = gate.Update(command.GateName, command.Description, true);
            if (!updateResult.IsSuccess)
            {
                return Result.Failure<bool>(updateResult.Error);
            }

            foreach (var item in command.Cameras)
            {
                var cameraType = await _cameraTypeRepo.GetByIdAsync(item.CameraTypeId);
                if (cameraType == null)
                {
                    return Result.Failure<bool>(Error.NotFound);
                }

                var existingCamera = gate.Cameras.FirstOrDefault(c => c.Id == item.CameraId);
                if (existingCamera != null)
                {
                    var cameraUpdateResult = existingCamera.Update(item.CameraURL, item.Description, true, command.GateId, item.CameraTypeId);
                    if (!cameraUpdateResult.IsSuccess)
                    {
                        return Result.Failure<bool>(cameraUpdateResult.Error);
                    }
                }
                else
                {
                    gate.AddCamera(item.CameraURL, item.Description, true, item.CameraTypeId);
                }
            }

            await _gateRepo.UpdateAsync(gate);
            if (!await _unitOfWork.CommitAsync())
            {
                return Result.Failure<bool>(Error.CommitError);
            }

            return Result.Success(true);
        }

        public async Task<Result<bool>> DeleteGate(int gateId)
        {
            var gate = (await _gateRepo.FindAsync(
                     s => s.GateId == gateId,
                     1, 1,
                     includeProperties: "Cameras"
                 )).FirstOrDefault();

            if (gate == null)
            {
                return Result.Failure<bool>(Error.NotFound);
            }
            gate.Delete();
            await _gateRepo.UpdateAsync(gate);
            if (!await _unitOfWork.CommitAsync())
            {
                return Result.Failure<bool>(Error.CommitError);
            }

            return Result.Success(true);
        }
    }
}
