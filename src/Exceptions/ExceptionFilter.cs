using Microsoft.AspNetCore.Mvc.Filters;

namespace MisticFy.API.src.Exceptions
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
