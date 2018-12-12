using System;
using System.Collections.Generic;
using System.Text;

namespace SqueezeBill.Data.DtoModel.DtoResponseModel
{
   public class ForgetPasswordResponseModel
    {
        public ForgetPasswordResponseModel()
        {
            Response = new Forgetpassword();
        }
        public Forgetpassword Response { get; set; }
    }

    public class Forgetpassword
    {
        public string Message { get; set; }
        public Int32 StatusCode { get; set; }
    }
}
