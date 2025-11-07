using Microsoft.Extensions.Diagnostics.HealthChecks;
using challenge_3_net.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace challenge_3_net.Services.HealthChecks
{
    /// <summary>
    /// Health check customizado para verificar a conectividade do banco de dados (SQL Server)
    /// </summary>
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DatabaseHealthCheck> _logger;

        public DatabaseHealthCheck(ApplicationDbContext context, ILogger<DatabaseHealthCheck> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Iniciando verificação de saúde do banco de dados");

                // Verificar se consegue conectar ao banco
                var canConnect = await _context.Database.CanConnectAsync(cancellationToken);
                
                if (!canConnect)
                {
                    _logger.LogWarning("Não foi possível conectar ao banco de dados");
                    return HealthCheckResult.Unhealthy("Não foi possível conectar ao banco de dados");
                }

                // Verificar se consegue executar uma query simples
                var startTime = DateTime.UtcNow;
                var count = await _context.Usuarios.CountAsync(cancellationToken);
                var duration = DateTime.UtcNow - startTime;

                _logger.LogInformation("Health check do banco de dados concluído com sucesso. Duração: {Duration}ms", duration.TotalMilliseconds);

                var data = new Dictionary<string, object>
                {
                    { "total_usuarios", count },
                    { "connection_time_ms", duration.TotalMilliseconds },
                    { "database_provider", "SQL Server" },
                    { "timestamp", DateTime.UtcNow }
                };

                return HealthCheckResult.Healthy("Banco de dados está funcionando corretamente", data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante verificação de saúde do banco de dados");
                
                var data = new Dictionary<string, object>
                {
                    { "error_message", ex.Message },
                    { "error_type", ex.GetType().Name },
                    { "timestamp", DateTime.UtcNow }
                };

                return HealthCheckResult.Unhealthy("Erro ao verificar banco de dados", ex, data);
            }
        }
    }
}
