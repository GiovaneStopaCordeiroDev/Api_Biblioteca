using API_Biblioteca.Errors;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
namespace API_Biblioteca.Middleware
{
    public class ExceptionMiddleware
    {
        // O RequestDelegate é uma das partes do ciclo de vida da requisição que vai interferir no Middleware
        // Esse _next representa “o próximo passo da pipeline”
        private readonly RequestDelegate _next;

        // O ILogger vai fazer uma formatação no erro
        private readonly ILogger<ExceptionMiddleware> _logger;

        // Com essa variável conseguimos verificar se a aplicação está em produção ou desenvolvimento
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        // O InvokeAsync é o método que vai ser chamado quando a requisição passar por esse Middleware
        // O HTTPContext contém toda a requisição, como o caminho, os headers, o corpo, etc
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // a chamada do Next é essencial pois ela significa “continua a requisição”
                await _next(context);
            }
            catch(Exception ex)
            {
                //Aqui grava a mensagem, o StackTracer e a exception
                _logger.LogError(ex, ex.Message);

                // Define que o tipo de resposta será em json
                context.Response.ContentType = "application/json";

                //Define o status code da resposta como 500, que é o código de erro interno do servidor
                context.Response.StatusCode = ex switch
                {
                    NotFoundException => 404,
                    BadRequestException => 400,
                    _ => 500
                };

                // IsDevelopment é um método que verifica se a aplicação está em desenvolvimento ou produção, para mostrar o erro de forma mais detalhada ou não
                // Se estiver em desenvolvimento, mostra a mensagem de erro e o stack trace
                // Se estiver em produção, mostra apenas a mensagem de erro e uma mensagem genérica de “Internal Server Error”
                var response = _env.IsDevelopment() ?
                    new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()) :
                    new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");


                // transforma StatusCode em statusCode(json)
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                // transforma o Objeto C# em json
                var json = JsonSerializer.Serialize(response, options);

                //Envia o json pro cliente
                await context.Response.WriteAsync(json);
            }
        }

    }
}
