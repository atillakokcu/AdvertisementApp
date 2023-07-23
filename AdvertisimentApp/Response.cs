using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisimentApp
{
    public class Response :IResponse   // data taşımadan sadece mesaj tutan bir sınıf
    {
        public Response(ResponseType responseType)
        {
                ResponseType = responseType;
        }

        public Response(ResponseType responseType, string message)
        {
            ResponseType = responseType;
            Message = message;
        }

        public string Message { get; set; }

        public ResponseType ResponseType { get; set; }
    }

    public enum ResponseType { Success, ValidationError, NotFount }



}
