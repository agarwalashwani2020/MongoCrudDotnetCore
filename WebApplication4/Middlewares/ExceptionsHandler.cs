using System;

namespace WebApplication4.Middlewares
{
    public abstract class ExceptionsHandler:Exception
    {
        public ExceptionsHandler(string message):base(message)
        {

        }
    }

    public class NotFoundException : ExceptionsHandler
    {
        public NotFoundException(string message):base(message)
        {

        }
    }

    public class InternalServerException : ExceptionsHandler
    {
        public InternalServerException(string message) : base(message)
        {

        }
    }
}
