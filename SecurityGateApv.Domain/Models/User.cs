using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityGateApv.Domain.Shared;

namespace SecurityGateApv.Domain.Models
{
    public class User
    {
        public User()
        {
            
        }
        internal User(int userId, string userName, string password, string fullName, string email, string phoneNumber, string image, DateTime createdDate, DateTime updatedDate, string status, int roleId, int? departmentId)
        {
            UserId = userId;
            UserName = userName;
            Password = password;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Image = image;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
            Status = status;
            RoleId = roleId;
            DepartmentId = departmentId;
        }
        internal User( string userName, string password, string fullName, string email, string phoneNumber, string image, DateTime createdDate, DateTime updatedDate, string status, int roleId, int departmentId)
        {
            UserName = userName;
            Password = password;
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Image = image;
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
            Status = status;
            RoleId = roleId;
            DepartmentId = departmentId;
        }
        [Key]
        public int UserId { get; set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string? Image { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime UpdatedDate { get; private set; }
        public string? OTP {  get; private set; }
        public DateTime? OTPIssueTime { get; private set; }
        public string Status { get; private set; }

        [ForeignKey("Role")]
        public int RoleId { get; private set; }
        public Role Role { get; private set; }
        
        [ForeignKey("Department")]
        public int? DepartmentId { get; private set; }
        public Department? Department { get; private set; }

        [InverseProperty("Sender")]
        public ICollection<NotificationUsers> SentNotifications { get; private set; }

        [InverseProperty("Receiver")]
        public ICollection<NotificationUsers> ReceivedNotifications { get; private set; }

        [InverseProperty("CreateBy")]
        public ICollection<Visit> CreatedVisits { get; private set; }

        [InverseProperty("UpdateBy")]
        public ICollection<Visit> UpdatedVisits { get; private set; }
        [InverseProperty("ResponsiblePerson")]
        public ICollection<Visit> ResponsiblePerson { get; private set; }

        public ICollection<VisitorSession> SecurityInSessions { get; private set; } 
        public ICollection<VisitorSession> SecurityOutSessions { get; private set; }

        public ICollection<Schedule> Schedules { get; private set; } 
        public ICollection<ScheduleUser> ScheduleUserTo { get; private set; }
        //public Visitor Visitor { get; private set; }
        public ICollection<Visitor> Visitor { get; private set; }



        public static Result<User> Create(int userId, string userName, string password, string fullName, string email, string phoneNumber, string? image, DateTime createdDate, DateTime updatedDate, string status, int roleId, int? departmentId)
        {
            var result = new User(userId, userName,password,fullName,email,phoneNumber,image,createdDate,updatedDate,status,roleId, departmentId);
            return result;
        }
        public static Result<User> Create( string userName, string password, string fullName, string email, string phoneNumber, string? image, DateTime createdDate, DateTime updatedDate, string status, int roleId, int departmentId)
        {
            var result = new User(userName, password, fullName, email, phoneNumber, image, createdDate, updatedDate, status, roleId, departmentId);
            return result;
        }
        public Result<User> Update()
        {
            this.UpdatedDate = DateTime.Now;
            return this;
        }
        public Result<User> UpdatePassword(string password)
        {
            this.Password = password;
            return this;
        }
        public Result<User> Unactive()
        {
            if(this.Status == "Active")
            {
                this.Status = "Unactive";
            }
            else
            {
                this.Status = "Active";
            }
            return this;
        }
        public Result<User> SetOTP(string OTP)
        {
            this.OTP = OTP;
            this.OTPIssueTime = DateTime.Now;
            return this;
        }
        public Result<User> SetNewPassword(string password)
        {
            this.Password = password;
            return this;
        }
    }
}
