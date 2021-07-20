using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfoService.Rating.App.Middleware.ExceptionHandling.CustomExceptions
{
    public class CustomException : Exception
    {
        public CustomException() : base()
        {

        }
        public CustomException(string message) : base(message)
        {

        }
        public CustomException(string message, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {

        }
    }
}
