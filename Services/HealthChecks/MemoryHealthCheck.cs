using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace challenge_3_net.Services.HealthChecks
{
    /// <summary>
    /// Health check customizado para verificar o uso de memória da aplicação
    /// </summary>
    public class MemoryHealthCheck : IHealthCheck
    {
        private readonly ILogger<MemoryHealthCheck> _logger;
        private readonly long _thresholdBytes;

        public MemoryHealthCheck(ILogger<MemoryHealthCheck> logger, IConfiguration configuration)
        {
            _logger = logger;
            // Limite padrão de 1GB, pode ser configurado via appsettings
            _thresholdBytes = configuration.GetValue<long>("HealthChecks:MemoryThresholdBytes", 1024 * 1024 * 1024);
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Iniciando verificação de saúde da memória");

                var process = System.Diagnostics.Process.GetCurrentProcess();
                var workingSet = process.WorkingSet64;
                var privateMemory = process.PrivateMemorySize64;
                var virtualMemory = process.VirtualMemorySize64;

                var isHealthy = workingSet < _thresholdBytes;
                var status = isHealthy ? "Healthy" : "Degraded";

                _logger.LogInformation("Verificação de memória concluída. Status: {Status}, Working Set: {WorkingSet}MB, Limite: {Threshold}MB", 
                    status, workingSet / 1024 / 1024, _thresholdBytes / 1024 / 1024);

                var data = new Dictionary<string, object>
                {
                    { "working_set_bytes", workingSet },
                    { "working_set_mb", Math.Round(workingSet / 1024.0 / 1024.0, 2) },
                    { "private_memory_bytes", privateMemory },
                    { "private_memory_mb", Math.Round(privateMemory / 1024.0 / 1024.0, 2) },
                    { "virtual_memory_bytes", virtualMemory },
                    { "virtual_memory_mb", Math.Round(virtualMemory / 1024.0 / 1024.0, 2) },
                    { "threshold_bytes", _thresholdBytes },
                    { "threshold_mb", Math.Round(_thresholdBytes / 1024.0 / 1024.0, 2) },
                    { "is_healthy", isHealthy },
                    { "timestamp", DateTime.UtcNow },
                    { "process_id", process.Id },
                    { "process_name", process.ProcessName }
                };

                if (isHealthy)
                {
                    return Task.FromResult(HealthCheckResult.Healthy("Uso de memória está dentro dos limites aceitáveis", data));
                }
                else
                {
                    return Task.FromResult(HealthCheckResult.Degraded("Uso de memória está próximo ou acima do limite"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante verificação de saúde da memória");
                
                var data = new Dictionary<string, object>
                {
                    { "error_message", ex.Message },
                    { "error_type", ex.GetType().Name },
                    { "timestamp", DateTime.UtcNow }
                };

                return Task.FromResult(HealthCheckResult.Unhealthy("Erro ao verificar uso de memória", ex));
            }
        }
    }
}
