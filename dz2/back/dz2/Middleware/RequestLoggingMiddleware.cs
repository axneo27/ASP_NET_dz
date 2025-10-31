using System.Text;
using System.Text.Json;

namespace spr421_spotify_clone.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        private readonly string _logFilePath;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger, IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _logFilePath = Path.Combine(environment.ContentRootPath, "last_request.json");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var requestInfo = await CaptureRequestAsync(context);
            
            var originalBodyStream = context.Response.Body;
            
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            var responseInfo = await CaptureResponseAsync(context, responseBody);

            var logEntry = new
            {
                Timestamp = DateTime.UtcNow,
                Request = requestInfo,
                Response = responseInfo
            };

            await WriteToFileAsync(logEntry);

            await responseBody.CopyToAsync(originalBodyStream);
        }

        private async Task<object> CaptureRequestAsync(HttpContext context)
        {
            var request = context.Request;
            
            request.EnableBuffering();
            
            string body = string.Empty;
            if (request.ContentLength.HasValue && request.ContentLength > 0)
            {
                using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
                body = await reader.ReadToEndAsync();
                request.Body.Position = 0;
            }

            return new
            {
                Method = request.Method,
                Scheme = request.Scheme,
                Host = request.Host.Value,
                Path = request.Path.Value,
                QueryString = request.QueryString.Value,
                Headers = request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
                Body = body,
                ContentType = request.ContentType,
                ContentLength = request.ContentLength,
                ClientIP = context.Connection.RemoteIpAddress?.ToString()
            };
        }

        private async Task<object> CaptureResponseAsync(HttpContext context, MemoryStream responseBody)
        {
            var response = context.Response;
            
            responseBody.Seek(0, SeekOrigin.Begin);
            string body = await new StreamReader(responseBody).ReadToEndAsync();
            responseBody.Seek(0, SeekOrigin.Begin);

            return new
            {
                StatusCode = response.StatusCode,
                Headers = response.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()),
                Body = body,
                ContentType = response.ContentType,
                ContentLength = response.ContentLength ?? responseBody.Length
            };
        }

        private async Task WriteToFileAsync(object logEntry)
        {
            try
            {
                var json = JsonSerializer.Serialize(logEntry, new JsonSerializerOptions 
                { 
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
                
                await File.WriteAllTextAsync(_logFilePath, json, Encoding.UTF8);
                
                _logger.LogDebug("Дані записано у файл: {FilePath}", _logFilePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Помилка запису даних у файл: {FilePath}", _logFilePath);
            }
        }
    }
}