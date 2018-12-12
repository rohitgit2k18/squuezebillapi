using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DtoServiceModel.DtoServiceRequetModel
{
    public class SendEmailRequestModel
    {
        public string ToEmailId { get; set; }
        public string CustomerName { get; set; }
        public string Subject { get; set; }
        public string MessageBody { get; set; }
        public string FileAttachmentURL { get; set; }
    }
}
