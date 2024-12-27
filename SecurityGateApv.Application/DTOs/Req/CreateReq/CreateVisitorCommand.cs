using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req.CreateReq
{
    public class CreateVisitorCommand
    {
        [Required(ErrorMessage = "Yêu cầu nhập tên khách")]
        public string VisitorName { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập tên công ty")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập số điện thoại")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập Số thẻ định dạng")]
        public string CredentialsCard { get; set; }
        public int CredentialCardTypeId { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập email")]
        public string Email { get; set; }
        [JsonIgnore]
        [Required(ErrorMessage = "Yêu cầu ảnh đầu vào")]
        public IFormFile? VisitorCredentialFrontImageFromRequest { get; set; }
        [JsonIgnore]
        [Required(ErrorMessage = "Yêu cầu ảnh đầu vào")]
        public IFormFile? VisitorCredentialBackImageFromRequest { get; set; }
        [JsonIgnore]
        [Required(ErrorMessage = "Yêu cầu ảnh đầu vào")]
        public IFormFile? VisitorCredentialBlurImageFromRequest { get; set; }
    }
}
