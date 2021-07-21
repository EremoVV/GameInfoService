using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameInfoService.Rating.App.Middleware.ExceptionHandling.CustomExceptions
{
    public class InternalException : Exception
    {
        public InternalException() : base()
        {

        }

        public InternalException(string message) : base(message)
        {

        }
    }
}
