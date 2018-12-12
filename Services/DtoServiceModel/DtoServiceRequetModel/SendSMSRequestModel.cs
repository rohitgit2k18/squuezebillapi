using System;
using System.Collections.Generic;
using System.Text;

namespace Services.DtoServiceModel.DtoServiceRequetModel
{
   public class SendSMSRequestModel
    {
        public string MessageBody { get; set; }
        public string ToMobileNum { get; set; }
        public string CustomerName { get; set; }
    }
}
