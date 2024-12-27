using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class GetVisitorRes
    {
        public int VisitorId { get; set; }
        public string VisitorName { get; private set; }
        public string CompanyName { get; private set; }
        public string PhoneNumber { get; private set; }
        public string CredentialsCard { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime UpdateDate { get; private set; }
        public string Status { get; private set; }
        public string? Email { get; private set; }
        public UserGetVisitorRes? CreateBy { get; private set; }
        public CredentialCardTypeRes CredentialCardType { get; private set; }
        public List<VisitorImageRes1> VisitorImage { get; private set; }

    }
    public class CredentialCardTypeRes
    {
        public int CredentialCardTypeId { get; private set; }
        public string CredentialCardTypeName { get; private set; }
    }
    public class UserGetVisitorRes
    {
        public int UserId { get; set; }
        public string UserName { get; private set; }
        public string FullName { get; private set; }
    }
}
