using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TaskManager.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = new ObjectResult(new
        {
            Message = "Erro no servidor",
            Error = context.Exception.Message
        })
        {
            StatusCode = 500
        };

        context.ExceptionHandled = true;
    }
}