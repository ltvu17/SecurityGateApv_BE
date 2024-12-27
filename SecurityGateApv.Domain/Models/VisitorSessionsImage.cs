using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Models
{
    public class VisitorSessionsImage
    {
        public VisitorSessionsImage(string imageType, string imageURL, VisitorSession visitorSession)
        {
            ImageType = imageType;
            ImageURL = imageURL;
            VisitorSession = visitorSession;
        }

        private VisitorSessionsImage( string imageType, string imageURL, int visitorSessionId)
        {
            ImageType = imageType;
            ImageURL = imageURL;
            VisitorSessionId = visitorSessionId;
        }

        [Key]
        public int VisitorSessionsImageId { get; set; }
        public string ImageType { get; private set; }
        public string ImageURL { get; private set; }

        [ForeignKey("VisitorSession")]
        public int VisitorSessionId { get; private set; }
        public VisitorSession VisitorSession { get; private set; }

        public static Result<VisitorSessionsImage> Create(string imageType, string imageURL, int visitorSessionId)
        {
            var result = new VisitorSessionsImage(  imageType,  imageURL,  visitorSessionId);
            return result;
        }
    }
}
