using EbisconDemo.Services.Exceptions;

namespace EbisconDemo.Api.Middlewares
{
    public class ServerErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public ServerErrorMiddleware(RequestDelegate request)
        {
            _next = request;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch(NotFoundException)
            {
                context.Response.StatusCode = 403;
            }
            catch (UserAlreadyExistException)
            {
                context.Response.StatusCode = 409;
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
                // TODO: change request body. MAYBE
            }            
        }
    }
}
