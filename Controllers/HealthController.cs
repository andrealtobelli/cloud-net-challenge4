using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;

namespace challenge_3_net.Controllers
{
    /// <summary>
    /// Controller para Health Checks da API
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class HealthController : ControllerBase
    {
        private readonly HealthCheckService _healthCheckService;
        private readonly ILogger<HealthController> _logger;

        public HealthController(HealthCheckService healthCheckService, ILogger<HealthController> logger)
        {
            _healthCheckService = healthCheckService;
            _logger = logger;
        }

        /// <summary>
        /// Verifica o status geral da aplicação
        /// </summary>
        /// <returns>Status da aplicação</returns>
        [HttpGet]
        [ProducesResponseType(typeof(HealthStatusDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HealthStatusDto), StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetHealth()
        {
            try
            {
                var healthReport = await _healthCheckService.CheckHealthAsync();
                
                var response = new HealthStatusDto
                {
                    Status = healthReport.Status.ToString(),
                    TotalDuration = healthReport.TotalDuration,
                    Timestamp = DateTime.UtcNow,
                    Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown",
                    Version = GetType().Assembly.GetName().Version?.ToString() ?? "Unknown",
                    Checks = healthReport.Entries.Select(entry => new HealthCheckDto
                    {
                        Name = entry.Key,
                        Status = entry.Value.Status.ToString(),
                        Duration = entry.Value.Duration,
                        Description = entry.Value.Description,
                        Data = entry.Value.Data,
                        Exception = entry.Value.Exception?.Message
                    }).ToList()
                };

                var statusCode = healthReport.Status == HealthStatus.Healthy ? 
                    StatusCodes.Status200OK : 
                    StatusCodes.Status503ServiceUnavailable;

                return StatusCode(statusCode, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar health checks");
                
                var errorResponse = new HealthStatusDto
                {
                    Status = "Unhealthy",
                    TotalDuration = TimeSpan.Zero,
                    Timestamp = DateTime.UtcNow,
                    Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown",
                    Version = GetType().Assembly.GetName().Version?.ToString() ?? "Unknown",
                    Checks = new List<HealthCheckDto>
                    {
                        new HealthCheckDto
                        {
                            Name = "HealthCheckService",
                            Status = "Unhealthy",
                            Duration = TimeSpan.Zero,
                            Description = "Erro interno no serviço de health checks",
                            Exception = ex.Message
                        }
                    }
                };

                return StatusCode(StatusCodes.Status503ServiceUnavailable, errorResponse);
            }
        }

        /// <summary>
        /// Verifica o status do banco de dados
        /// </summary>
        /// <returns>Status do banco de dados</returns>
        [HttpGet("database")]
        [ProducesResponseType(typeof(HealthStatusDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HealthStatusDto), StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetDatabaseHealth()
        {
            try
            {
                var healthReport = await _healthCheckService.CheckHealthAsync(check => 
                    check.Tags.Contains("database"));

                var response = new HealthStatusDto
                {
                    Status = healthReport.Status.ToString(),
                    TotalDuration = healthReport.TotalDuration,
                    Timestamp = DateTime.UtcNow,
                    Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown",
                    Version = GetType().Assembly.GetName().Version?.ToString() ?? "Unknown",
                    Checks = healthReport.Entries
                        .Where(entry => entry.Value.Tags.Contains("database"))
                        .Select(entry => new HealthCheckDto
                        {
                            Name = entry.Key,
                            Status = entry.Value.Status.ToString(),
                            Duration = entry.Value.Duration,
                            Description = entry.Value.Description,
                            Data = entry.Value.Data,
                            Exception = entry.Value.Exception?.Message
                        }).ToList()
                };

                var statusCode = healthReport.Status == HealthStatus.Healthy ? 
                    StatusCodes.Status200OK : 
                    StatusCodes.Status503ServiceUnavailable;

                return StatusCode(statusCode, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar health check do banco de dados");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { 
                    Status = "Unhealthy", 
                    Error = ex.Message 
                });
            }
        }

        /// <summary>
        /// Verifica o status de memória da aplicação
        /// </summary>
        /// <returns>Status da memória</returns>
        [HttpGet("memory")]
        [ProducesResponseType(typeof(HealthStatusDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HealthStatusDto), StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetMemoryHealth()
        {
            try
            {
                var healthReport = await _healthCheckService.CheckHealthAsync(check => 
                    check.Tags.Contains("memory"));

                var response = new HealthStatusDto
                {
                    Status = healthReport.Status.ToString(),
                    TotalDuration = healthReport.TotalDuration,
                    Timestamp = DateTime.UtcNow,
                    Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown",
                    Version = GetType().Assembly.GetName().Version?.ToString() ?? "Unknown",
                    Checks = healthReport.Entries
                        .Where(entry => entry.Value.Tags.Contains("memory"))
                        .Select(entry => new HealthCheckDto
                        {
                            Name = entry.Key,
                            Status = entry.Value.Status.ToString(),
                            Duration = entry.Value.Duration,
                            Description = entry.Value.Description,
                            Data = entry.Value.Data,
                            Exception = entry.Value.Exception?.Message
                        }).ToList()
                };

                var statusCode = healthReport.Status == HealthStatus.Healthy ? 
                    StatusCodes.Status200OK : 
                    StatusCodes.Status503ServiceUnavailable;

                return StatusCode(statusCode, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar health check de memória");
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { 
                    Status = "Unhealthy", 
                    Error = ex.Message 
                });
            }
        }
    }

    /// <summary>
    /// DTO para resposta de health check
    /// </summary>
    public class HealthStatusDto
    {
        /// <summary>
        /// Status geral da aplicação
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Duração total da verificação
        /// </summary>
        public TimeSpan TotalDuration { get; set; }

        /// <summary>
        /// Timestamp da verificação
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Ambiente da aplicação
        /// </summary>
        public string Environment { get; set; } = string.Empty;

        /// <summary>
        /// Versão da aplicação
        /// </summary>
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// Lista de verificações individuais
        /// </summary>
        public List<HealthCheckDto> Checks { get; set; } = new();
    }

    /// <summary>
    /// DTO para verificação individual de health check
    /// </summary>
    public class HealthCheckDto
    {
        /// <summary>
        /// Nome da verificação
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Status da verificação
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Duração da verificação
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Descrição da verificação
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Dados adicionais da verificação
        /// </summary>
        public IReadOnlyDictionary<string, object>? Data { get; set; }

        /// <summary>
        /// Exceção ocorrida (se houver)
        /// </summary>
        public string? Exception { get; set; }
    }
}
