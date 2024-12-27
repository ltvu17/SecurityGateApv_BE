using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace SecurityGateApv.Application.DTOs.Req
{
    public  class VisitorSessionCheckOutCommand
    {
        [JsonIgnore]
        public DateTime CheckoutTime { get; set; }
        public int SecurityOutId { get; set; }
        public int GateOutId { get; set; }
        [JsonIgnore]
        public string Status { get; set; } = string.Empty;
        public List<VisitorSessionsImageCheckoutCommand> Images { get; set; }
        //public ICollection<VehicleSessionImage> Images { get; set; }
        public VehicleSessionComand? VehicleSession { get; set; }
    }
    public class VisitorSessionsImageCheckoutCommand
    {
        public string ImageType { get; set; }
        public string ImageURL { get; set; }
        ////[JsonIgnore]
        //public IFormFile Image { get; set; }
    }
}
