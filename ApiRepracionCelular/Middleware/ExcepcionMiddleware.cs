namespace ApiRepracionCelular.Middleware
{
    public class ExcepcionMiddleware
    {
        private readonly RequestDelegate next;

        public ExcepcionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new { Mensaje = e.Message});
            }
        }
    }
}
