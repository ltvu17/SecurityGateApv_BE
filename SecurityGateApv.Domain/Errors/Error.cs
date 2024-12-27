using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Errors
{
    public class Error
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null");
        public static readonly Error CommitError = new("Error.CommitError", "Lỗi trong quá trình lưu trữ dữ liệu.");
        public static readonly Error DecryptError = new("Error.DecryptError", "Error when decrypt text");
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; }
        public string Message { get; }
        public static implicit operator string(Error error) { return error.Code; }
        //Domain Errors
        public static readonly Error NotFound = new("Error.Notfound", "Không tìm thấy");
        public static readonly Error NotFoundVistor = new("Error.Notfound", "Không tìm thấy khách này");
        public static readonly Error SaveToDBError = new("Error.CommitDataBase", "Save to DB error");
        #region Valid input
        public static readonly Error NullInput = new("Error.Input", "Input param can't null");
        #endregion

        #region Author error
        public static readonly Error CreateStaffError = new("Error.CreateStaff", "This role can not create staff");
        public static readonly Error PasswordNotMatch = new("Error.PasswordNotMatch", "Password doesn't match");
        public static readonly Error CheckPasswordError = new("Error.CheckPasswordError", "Password double check doesn't match");
        public static readonly Error Unauthorized = new("Error.Unauthorized", "Unauthorized Access.");
        #endregion


        #region UserError
        //User validation
        public static readonly Error NotFoundUser = new("Error.NotfoundUser", "Not found this user");
        public static readonly Error NotFoundUserLogin = new("Error.NotfoundUser", "Tài khoản hoặc mật khẩu không chính xác xin vui lòng thử lại");
        //Not found department manager
        public static readonly Error NotFoundDepartmentManager = new("Error.NotfoundDepartmentManager", "Not found this department manager");
        public static readonly Error CanNotUpdateUserName = new("Error.CanNotUpdateUserName", "Username can not change");
        public static readonly Error NotFoundDepartmentManagerById = new("Error.NotfoundDepartmentManager", "Not found departmentManager by id");
        public static readonly Error IncorrectPassword = new("Error.LoginError", "Tài khoản hoặc mật khẩu không chính xác xin vui lòng thử lại");
        public static readonly Error NotPermission = new("Error.User.NotPermission", "Bạn không có quyền thực hiện hành động này");
        //user canot update departement
        public static readonly Error CanNotUpdateDepartment = new("Error.CanNotUpdateDepartment", "Department can not change for account admin, manager, security");
        //user role does not match department
        public static readonly Error UserRoleNotMatchDepartment = new("Error.UserRoleNotMatchDepartment", "Bạn không có quyền thực hiện hành động này");
        public static readonly Error EmailResetPasswordNotValid = new("Error.EmailResetPasswordNotValid", "Email chưa được đăng kí trong hệ thống");
        public static readonly Error OTPNotEqual = new("Error.OTPNotEqual", "OTP Không chính xác hoặc hết hạn");
        public static readonly Error OTPExpired = new("Error.OTPExpired", "OTP Không chính xác hoặc hết hạn");
        #endregion


        #region Visit
        //Visit error
        public static readonly Error NotFoundVisit = new("fError.NotFoundVisit", "Không tìm thấy lịch hẹn");
        public static readonly Error UpdateTimeVisitError = new("Error.UpdateTimeVisitError", "Time Error");
        public static readonly Error NotFoundVisitCurrentDate = new("Error.NotFoundVisitCurrentDate", "There is no one visit in current day");
        public static readonly Error NotRoleNotPermission = new("Error.NotfoundVisit", "Not found this visit");
        public static readonly Error DuplicateVisitorDetail = new("Error.DuplicateVisitorDetail", "Khách này đã có trước đó");
        public static readonly Error VisitorIsBusy = new("Error.VisitorIsBusy", "Visitor busy at ...");
        public static readonly Error NoValidDateForVisit = new("Error.NoValidDateForVisit", "Số ngày tạo của chuyến thăm không đủ(Ít nhất có 1 ngày)");
        public static readonly Error NoScheduleAssignForThisStaff = new("Error.NoScheduleAssignForThisStaff", "No Assigned Schedule for staff");
        public static readonly Error ScheduleExpireAssignForThisStaff = new("Error.ScheduleExpireAssignForThisStaff", "Nhiệm vụ này đã hết hạn");
        public static readonly Error AppendTimeInvalid = new("Error.AppendTimeInvalid", "Expect End Time must grater than Start Time");
        public static readonly Error VisitCancel = new("Error.VisitCancel", "Chuyến thăm đã bị hủy");
        public static readonly Error VisitNotRegisCard = new("Error.VisitNotRegisCard", "Chuyến thăm chưa được đăng ký thẻ.");
        public static readonly Error VisitDailyRegisCardError = new("Error.VisitDailyRegisCardError", "Chuyến thăm trong ngày không dùng loại thẻ cho lịch trình.");
        public static readonly Error VisitScheduleRegisCardError = new("Error.VisitScheduleRegisCardError", "Chuyến thăm lịch trình không dùng loại thẻ trong ngày.");
        //Not found visit by credential card
        public static readonly Error NotFoundVisitByCredentialCard = new("Error.NotFoundVisitByCredentialCard", "Không tìm thấy chuyến thăm của thẻ CCCD đã quét.");
        //public static readonly Error VisitCancel = new("Error.NotFoundVisitByCredentialCard", "Không tìm thấy lịch thăm của thẻ CCCD đã quét.");
        #endregion

        #region VisitDetailError
        //Visit error
        public static readonly Error NotFoundVisitDetail = new("Error.VisitDetail", "Not found this VisitDetail");
        
        #endregion

        #region CardError
        //Card error
        public static readonly Error NotFoundCard = new("Error.NotfoundCard", "Không tìm được thẻ trong hệ thống.");
        public static readonly Error NotFoundCardByCredentialCard = new("Error.NotFoundCardByCredentialCard", "Không tìm được thẻ bằng CCCD/GPLX này.");
        public static readonly Error NotFoundCardByCardVerification = new("Error.NotfoundCard", "Không tìm được thẻ theo QR đã nhận.");
        public static readonly Error CardAcctive = new("Error.CardStatus", "Card is acctive cannot accept");
        public static readonly Error CardInActive = new("Error.CardStatus", "Card không còn hoạt đọng không thể sử dụng.");
        public static readonly Error CardLost = new("Error.CardStatus", "Card mất không thể sử dụng.");
        //DuplicateQRCard
        public static readonly Error DuplicateCard = new("Error.DuplicateCard", "Card đang được sử dụng cho 1 lịch hẹn, không sử dụng card cho 2 lịch hẹn.");
        public static readonly Error DuplicateVisitDetail = new("Error.DuplicateCard", "Lịch hẹn này đã được tạo card, không thể tạo 2 thẻ cho 1 lịch hẹn.");
        public static readonly Error CardSortTermError = new("Error.CardSortTermError", "");
        #endregion

        #region CardType
        //CardType not found
        public static readonly Error NotFoundCardType = new("Error.NotFoundCardType", "không có loại thẻ này trong hệ thống");
        #endregion

        #region VisitCardError
        //Can not found VisitCard
        public static readonly Error NotFoundVisitCard = new("Error.NotFound", "Thẻ chưa được đăng ký ra vào không thể checkin/checkout.");
        // Card expried 
        public static readonly Error CardExpried = new("Error.CardExpried", "Card is expried");
        // This type of card cannot be used for this visit
        public static readonly Error CardNotIssue = new("Error.CardNotIssue", "Thẻ này chưa được kích hoạt");
        public static readonly Error TypeVerifiError = new("Error.TypeVerifiError", "Cần input đúng loại thẻ (QRCardVerified) hoặc (CredentialCard)");

        #endregion

        # region VisitSesson
        //VisitSesson error 
        public static readonly Error NotFoundVisitSessonByQRId = new("Error.NotFound", "Not found this VisitorSession by QRId");
        public static readonly Error NotFoundVisitSesson = new("Error.NotFoundVisitSesson", "Not found this VisitorSession");
        public static readonly Error CardNotCheckIn = new("Error.CardNotCheckIn", "Card does not checked t");
        public static readonly Error FailCreateSession = new("Error.CanNotCreate", "Create session checkin fail");
        public static readonly Error ValidSession = new("Error.ValidSession", "Bạn đã checkin rồi, không thể checkin 2 lần liên liếp.");
        public static readonly Error CheckoutNotValid = new("Error.CheckoutNotValid", "Không thể checkout khi chưa checkin");
        public static readonly Error ValidCheckinSession = new("Error.ValidCheckinSession", "Khách này đã check-in, không thể check-in 2 lần liên tiếp.");
        public static readonly Error CheckInViolation = new("Error.CheckInViolation", "Khách này bị nhân viên đánh vi phạm.");
        public static readonly Error CheckoutNotvalidWithVisitActiveTemporary = new("Error.CheckoutNotvalidWithVisitActiveTemporary", "Khách thăm này chưa được nhân viên duyệt, cần duyệt chuyến thăm của khách này trước khi thực hiện CheckOut.");
        #endregion

        #region VehicleSession
        public static readonly Error ValidVehicleSession = new("Error.ValidVehicleSession", "Xe này đã checkin rồi, không thể checkin 2 lần liên liếp.");
        public static readonly Error ValidVehicleSessionCheckOut = new("Error.ValidVehicleSessionCheckOut", "Xe này đã chưa checkin, không thể checkout.");
        public static readonly Error ValidVehicleSessionNotCheckout = new("Error.ValidVehicleSessionNotCheckout", "Khách này đã checkin với xe, cần thực hiện thêm checkout cho xe.");
        public static readonly Error VehicleCheckoutDailyError = new("Error.VehicleCheckoutDailyError", "Lịch ra vào hằng ngày này có xe ra vào, cần phải thực hiện checkout cả xe ra vào.");

        #endregion

        #region Visitor
        //Visitor eror
        public static readonly Error NotFoundVisitor = new("Error.NotfoundVisitor", "Không tìm thấy khách này.");
        public static readonly Error NotFoundVisitorByCard = new("Error.NotfoundVisitor", "Khách này chưa được đăng ký trong hệ thống.");
        public static readonly Error CreateVisitor = new("Error.CreateVisitor", "Create error");
        public static readonly Error DuplicateCardNumber = new("Error.DuplicateCardNumber", "Số thẻ này đã được sử dụng");
        #endregion


        #region Schedule

        //Schedule Error
        public static readonly Error ScheduleCreateError = new("Error.Schedule", "Can not create schedule");
        public static readonly Error ScheduleUpdateError = new("Error.Schedule", "Can not update schedule");
        public static readonly Error ScheduleSaveError = new("Error.Schedule", "Can not save schedule");
        public static readonly Error NotFoundSchedule = new("Error.Schedule", "Không tìm thấy lịch trình");
        public static readonly Error NotFoundScheduleUser = new("Error.Schedule", "Không tìm thấy nhiệm vụ");
        public static readonly Error CanNotUpdateSchedule = new("Error.CanNotUpdateSchedule", "Không thể cập nhật vì lịch trình này đã được tạo chuyến thăm");
        public static readonly Error ScheduleValid = new("Error.Schedule", "DaysOfProcess is not valid for the selected Visit Type.");
        public static readonly Error ScheduleCannotUpdate = new("Error.Schedule", "Can not update Schedule daily");
        public static readonly Error ScheduleCannotAssign = new("Error.ScheduleCannotAssign", "Can not Assign Schedule daily");
        #endregion

        #region ScheduleUser Error
        //ScheduleUser not found
        public static readonly Error NotFoundScheduleUserList = new("Error.ScheduleUser", "Không tìm thấy nhiệm vụ này");
        //ScheduleUser reject error
        public static readonly Error ScheduleUserRejectError = new("Error.ScheduleUserReject", "Không thể từ chối nhiệm vụ này");
        //ScheduleUser aprove error
        public static readonly Error ScheduleUserAproveError = new("Error.ScheduleUserAprove", "Không thể xác nhận nhiệm vụ này");
        //ScheduleUser does not have visit 
        public static readonly Error ScheduleUserNotHaveVisit = new("Error.ScheduleUserNotHaveVisit", "Nhiệm vụ này chưa được tạo chuyến thăm");
        public static readonly Error ScheduleUserHaveVisit = new("Error.ScheduleUserNotHaveVisit", "Nhiệm vụ này đã được tạo chuyến thăm không thể hủy");
        #endregion

        #region ScheduleType
        //ScheduleType Error
        public static readonly Error ScheduleTypeCreateError = new("Error.ScheduleType", "Can not create schedule type");
        public static readonly Error ScheduleTypeUpdateError = new("Error.ScheduleType", "Can not update schedule type");
        public static readonly Error ScheduleTypeSaveError = new("Error.ScheduleType", "Can not save schedule type");
        //error when schedule type not found
        public static readonly Error NotFoundScheduleType = new("Error.ScheduleType", "Can not found Schedule Type");
        //error when schedule input is not valid
        public static readonly Error ScheduleTypeInputValid = new("Error.ScheduleType", "Wrong when input Schedule Type");
        #endregion

        #region CredentialCardType
        //CredentialCardType Error
        public static readonly Error CredentialCardTypeCreateError = new("Error.CredentialCardType", "Can not create Credential Card Type");
        public static readonly Error CredentialCardTypeUpdateError = new("Error.CredentialCardType", "Can not update Credential Card Type");
        //invalid input
        public static readonly Error CredentialCardTypeInputValid = new("Error.CredentialCardType", "Wrong when input Credential Card Type");
        #endregion

        //ProcessVisit Error
        public static readonly Error ProcessVisitCreateDateError = new("Error.ProcessVisitCreateDateError", @"Date format is ""Mon,Tue,Sun""");
    
        
        
        
        //Department Error
        public static readonly Error NotFoundDepartment = new("Error.NotfoundDepartment", "Not found this department");
        public static readonly Error NotFoundDepartmentByDepartmentManagerId = new("Error.NotfoundDepartment", "Not found department by department manager id");
        public static readonly Error NotFoundDepartmentById = new("Error.NotfoundDepartment", "Not found department by id");
        public static readonly Error CreateDepartment = new("Error.CreateDepartment", "Create department error");
        public static readonly Error UpdateDepartment = new("Error.UpdateDepartment", "Update department error");

        //Detection Error
        public static readonly Error DetectionError = new("Error.DetectionError", "Không tìm thấy giày hợp lệ");
        public static readonly Error NotShoe = new("Error.DetectionError", "Cần đưa đúng loại ảnh giày (type:Shoe).");
        public static readonly Error DetectionExeption = new("Error.DetectionExeption", "Lỗi trong quá trình detect giày.");
        //Not found shoe 
        public static readonly Error NotFoundShoeTypeImage = new("Error.NotFoundShoe", "Not found shoe type image");

        //CardType Repo
        public static readonly Error CardFoundError = new("Error.CardFoundError", "Card not found");

        //Checkin fail
        public static readonly Error CheckInFail = new("Error.CheckInFail", "Lỗi trong quá trình lưu trữ dữ liệu checkin.");
        public static Error ScheduleAndCardTypeMismatch(string scheduleType, string cardType)
        {
            return new Error("Error.ScheduleAndCardTypeMismatch", $"Schedule type '{scheduleType}' and card type '{cardType}' do not match.");
        }

        #region Image
        public static readonly Error NotFoundImage = new("Error.NotFoundImage", "Not found image");
        #endregion
    }

    public class Error<T> : Error
    {
        public Error(string code, string message, T data) : base(code, message)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
