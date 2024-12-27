using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Models
{
    public class VisitorImage
    {
        public VisitorImage()
        {
            
        }
        public VisitorImage(string imageType, string imageURL, Visitor visitor)
        {
            ImageType = imageType;
            ImageURL = imageURL;
            Visitor = visitor;
        }
        public int VisitorImageId { get; set; }
        public string ImageType { get; private set; }
        public string ImageURL { get; private set; }
        [ForeignKey("Visitor")]
        public int VisitorId { get; set; }
        public Visitor Visitor { get; set; }
        public static Result<VisitorImage> Create(string imageType, string imageURL, Visitor visitor)
        {
            return new VisitorImage(imageType, imageURL, visitor);
        }
        public Result<VisitorImage> DecryptResponseImage(string Image)
        {
            this.ImageURL = Image;  
            return this;
        }
    }
}
