using Questao5.Domain.Exceptions;

namespace Questao5.Middlewares;

public class BusinessValidationExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (BusinessRuleValidationException e)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { e.Code, e.Message });
        }
    }
}