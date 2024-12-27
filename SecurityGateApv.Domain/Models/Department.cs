using SecurityGateApv.Domain.Enums;
using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Models
{
    public class Department
    {
        public Department()
        {
            
        }

        public Department(int departmentId, string departmentName, string description, DateTime createDate, DateTime updateDate, int acceptLevel, string status)
        {
            DepartmentId = departmentId;
            DepartmentName = departmentName;
            Description = description;
            CreateDate = createDate;
            UpdateDate = updateDate;
            AcceptLevel = acceptLevel;
            Status = status;
        }

        internal Department(string departmentName, string description, 
            DateTime createDate, DateTime updatedDate, int acceptLevel, string status)
        {
            DepartmentName = departmentName;
            Description = description;
            CreateDate = createDate;
            UpdatedDate = updatedDate;
            AcceptLevel = acceptLevel;
            Status = status;
        }

        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int AcceptLevel { get; set; }
        public string Status { get; set; }
        public ICollection<User> User { get; set; }
        public DateTime UpdateDate { get; }

        public static Result<Department> Create(string departmentName, string description,
             int acceptLevel)
        {
            var department = new Department(departmentName, description,
                DateTime.Now, DateTime.Now, acceptLevel, DepartmentStatusEnum.Active.ToString());
            return department;
        }
        public static Result<Department> Create(int departmentId, string departmentName,DateTime createDate, DateTime updateDate, string description,
             int acceptLevel, string status)
        {
            var department = new Department(departmentId, departmentName, description,
                createDate, updateDate, acceptLevel, status);
            return department;
        }
        public Result<Department> Update() {
            this.UpdatedDate = DateTime.Now;
            return this;      
        }
        public  Result<Department> Delete() {
            this.UpdatedDate = DateTime.Now;
            this.Status = DepartmentStatusEnum.Inactive.ToString();
            return this;      
        }

    }
}
