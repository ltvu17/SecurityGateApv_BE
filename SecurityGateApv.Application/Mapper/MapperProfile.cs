using AutoMapper;
using SecurityGateApv.Application.DTOs.Req;
using SecurityGateApv.Application.DTOs.Req.CreateReq;
using SecurityGateApv.Application.DTOs.Req.UpdateReq;
using SecurityGateApv.Application.DTOs.Res;
using SecurityGateApv.Domain.Enums;
using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region User
            CreateMap<User, CreateByRes>().ReverseMap();
            CreateMap<User, SenderRes>().ReverseMap();
            CreateMap<User, GetUserRes>().ReverseMap();
            CreateMap<SecurityRes, User>().ReverseMap();
            CreateMap<UserGetScheduleUserRes, User>().ReverseMap();
            CreateMap<UserScheduleUserRes, User>().ReverseMap();
            #endregion

            #region Role
            CreateMap<Role, RoleRes>().ReverseMap();
            #endregion

            #region Gate
            CreateMap<Gate, GetGateRes>().ReverseMap();
            CreateMap<Gate, CreateGateCommand>().ReverseMap();
            CreateMap<Camera, CameraCommand>().ReverseMap();
            CreateMap<Camera, CameraRes>().ReverseMap();
            CreateMap<CameraType, CameraTypeRes>().ReverseMap();
            #endregion
            CreateMap<GetDepartmentRes, Department>().ReverseMap();
            CreateMap<ScheduleResForVisit, Schedule>().ReverseMap();
            CreateMap<CreateUserComman, User>().ReverseMap();
            CreateMap<DepartmentCreateCommand, Department>().ReverseMap();
            CreateMap<VisitCreateCommand, Visit>().ReverseMap();
            CreateMap<GetVisitorRes, Visitor>().ReverseMap();
            CreateMap<User, UpdateUserCommand>().ReverseMap();
            CreateMap<User, UpdateUserNoDepartmentIdCommand>().ReverseMap();
            CreateMap<VisitDetailOldCommand, VisitDetail>().ReverseMap();
            CreateMap<Department, DeparmentRes>().ReverseMap();
            CreateMap<GetNotificationRes, NotificationUsers>().ReverseMap();
            CreateMap<GetNotification, Notification>().ReverseMap();
            CreateMap<CredentialCardTypeRes, CredentialCardType>().ReverseMap();
            CreateMap<NotificationTypeRes, NotificationType>().ReverseMap();
            CreateMap<UserGetVisitorRes, User>().ReverseMap();

            #region ScheduleUser
            //CreateMap<GetScheduleRes, Schedule>().ReverseMap();
            CreateMap<Schedule, GetScheduleRes>().ReverseMap()
            .ForMember(dest => dest.ScheduleUser, opt => opt.MapFrom(src => src.ScheduleUser));
            CreateMap<GetScheduleUserRes, ScheduleUser>().ReverseMap()
                .ForMember(dest => dest.AssignFrom, opt => opt.MapFrom(src => src.Schedule.CreateBy));
            CreateMap<ScheduleUserRes, ScheduleUser>().ReverseMap()
                .ForMember(dest => dest.AssignFrom, opt => opt.MapFrom(src => src.Schedule.CreateBy));
            #endregion

            #region Visitor map
            CreateMap<CreateVisitorRes, Visitor>().ReverseMap();
            #endregion


            #region Visit map
            CreateMap<GetVisitRes, Visit>().ReverseMap()
                .AfterMap((src, dest) =>
                {
                    if (src.ScheduleUser == null)
                    {
                        dest.ScheduleUser = null;
                    }
                }); 
            CreateMap<Visit, GetVisitNoDetailRes>().ReverseMap()
                 .AfterMap((src, dest) =>
                 {
                     if (src.ScheduleUser == null)
                     {
                         dest.ScheduleUser = null;
                     }
                 });
            CreateMap<VisitDetail, VisitDetailRes>().ReverseMap();
            CreateMap<VisitDetail, VisitDetailSessionRes>().ReverseMap();
            CreateMap<UpdateVisitBeforeStartDateCommand, Visit>().ReverseMap();
            CreateMap<UpdateVisitAfterStartDateCommand, Visit>().ReverseMap();
            CreateMap<GetVisitByDateRes, Visit>().ReverseMap()
                .ForMember(dest => dest.ScheduleTypeName, opt => opt.MapFrom(src => src.ScheduleUser.Schedule.ScheduleType.ScheduleTypeName))
                .ForMember(dest => dest.CreateByname, opt => opt.MapFrom(src => src.CreateBy.FullName));
            CreateMap<VisitRes, Visit>().ReverseMap()
                .ForMember(dest => dest.ScheduleTypeName, opt => opt.MapFrom(src => src.ScheduleUser.Schedule.ScheduleType.ScheduleTypeName))
                .ForMember(dest => dest.CreateByname, opt => opt.MapFrom(src => src.CreateBy.FullName));
            #endregion

            #region VisitDetail map
            CreateMap<GetVisitDetailRes, VisitDetail>().ReverseMap();
            CreateMap<GetVisitByCredentialCardRes, VisitDetail>().ReverseMap();
            CreateMap<ValidCheckinRes, VisitDetail>().ReverseMap();
            CreateMap<VisitDetaiUpdateVisitAfterStartDateCommand, VisitDetail>().ReverseMap();
            CreateMap<UpdateAppendTimeForVisitCommand, Visit>().ReverseMap();
            #endregion

            #region Visitor map
            CreateMap<Visitor, VisitorRes>().ReverseMap()
               /* .AfterMap((src, dest) =>
                {
                    if (dest.VisitorImage != null)
                    {
                        var frontImage = dest.VisitorImage.FirstOrDefault(s => s.ImageType.Contains("FRONT"));
                        src.VisitorCredentialFrontImage = frontImage?.ImageURL ?? string.Empty;
                    }
                })*/; 
            CreateMap<Visitor, CreateVisitorCommand>().ReverseMap();
            CreateMap<VisitorDetailRes, Visitor>().ReverseMap();
            CreateMap<GetVisitorCreateRes, Visitor>().ReverseMap();
            CreateMap<UpdateVisitorCommand, Visitor>().ReverseMap();
            CreateMap<VisitorImageRes1, VisitorImage>().ReverseMap();
            #endregion

            #region ScheduleType
            CreateMap<GetScheduleTypeRes, ScheduleType>().ReverseMap();
            CreateMap<ScheduleGetScheduleUserRes, Schedule>().ReverseMap();
            CreateMap<ScheduleTypeGetScheduleUserRes, ScheduleType>().ReverseMap();
            CreateMap<ScheduleTypeGetScheduleUserRes, ScheduleType>().ReverseMap();
            CreateMap<ScheduleType, ScheduleTypeRes>().ReverseMap();
            #endregion

            #region Schedule
            CreateMap<Schedule, ScheduleRes>().ReverseMap();
            #endregion

            #region SessionsImageRes
            CreateMap<SessionsImageRes, VisitorSessionsImage>().ReverseMap();
            CreateMap<VisitorSessionsImageCheckinCommand, VisitorSessionsImage>().ReverseMap();

            #endregion

            #region Gate
            CreateMap<GateRes, Gate>().ReverseMap();
            #endregion

            #region VisitSession
            CreateMap<GetVisitorSessionRes, VisitorSession>().ReverseMap()
                .ForMember(dest => dest.IsVehicleSession, opt => opt.MapFrom(src => src.VehicleSession != null)); // Map IsVehicleSession
            CreateMap<SessionCheckOutRes, VisitorSession>().ReverseMap();
            CreateMap<VisitorSession, GetVisitorSessionGraphQLRes>()
                .ForMember(dest => dest.Visitor, opt => opt.MapFrom(src => src.VisitDetail.Visitor))
                .ForMember(dest => dest.Visit, opt => opt.MapFrom(src => src.VisitDetail.Visit))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.VisitorSessionsImages))
                .ReverseMap();

            //CreateMap<GetVisitorSessionGraphQLRes, VisitorSession>().ReverseMap()
            //    //.ForMember(dest => dest.Visitor, opt => opt.MapFrom(src => src.VisitDetail.Visitor))
            //    /*.ForMember(dest => dest.Visit, opt => opt.MapFrom(src => src.VisitDetail.Visit))*/;
            CreateMap<SessionsRes, VisitorSession>().ReverseMap();
            CreateMap<GraphQlGetVisitRes, Visit>().ReverseMap();
            CreateMap<GraphQlVisitorRes, Visitor>().ReverseMap()
                .ForMember(dest => dest.VisitCard, opt => opt.MapFrom(src => src.VisitCard.Where(s => s.VisitCardStatus == VisitCardStatusEnum.Issue.ToString())));
            CreateMap<VehicleSessionRes, VehicleSession>().ReverseMap();
            CreateMap<VehicleSessionImageRes, VehicleSessionImage>().ReverseMap();
            CreateMap<VisitorSession, VisitorSessionCheckOutCommand>().ReverseMap();
            CreateMap<VehicleSession, VehicleSessionComand>().ReverseMap();

            #endregion

            #region Card
            CreateMap<Card, GetCardRes>().ReverseMap();
            CreateMap<VisitCard, VisitCardRes>().ReverseMap();
            CreateMap<GetCardRes, Card>().ReverseMap()
                .ForMember(dest => dest.QrCardTypename, opt => opt.MapFrom(src => src.CardType.CardTypeName));
            CreateMap<CardRes, Card>().ReverseMap()
                .ForMember(dest => dest.QrCardTypename, opt => opt.MapFrom(src => src.CardType.CardTypeName));
            #endregion

            #region Images
            CreateMap<VisitorSessionsImage, VisitorSessionImageRes>().ReverseMap();

            #endregion
        }
    }
}
