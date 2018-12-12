using Services.DtoServiceModel.DtoServiceRequetModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
   public class SMSService
    {
        public string SendSMS(SendSMSRequestModel SSRM)
        {
            string Result = "";
            try
            {             
                Result = "Ok";
            }
            catch (Exception ex)
            {
                Result = Convert.ToString(ex);
            }
            return Result;
        }
    }
}
