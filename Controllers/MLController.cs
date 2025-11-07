using Microsoft.AspNetCore.Mvc;
using challenge_3_net.Services.ML;
using Microsoft.AspNetCore.Authorization;

namespace challenge_3_net.Controllers
{
    /// <summary>
    /// Controller para funcionalidades de Machine Learning
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    // [Authorize] // Requer autenticação JWT
    [Produces("application/json")]
    public class MLController : ControllerBase
    {
        private readonly MotoAnalysisService _motoAnalysisService;
        private readonly ILogger<MLController> _logger;

        public MLController(MotoAnalysisService motoAnalysisService, ILogger<MLController> logger)
        {
            _motoAnalysisService = motoAnalysisService;
            _logger = logger;
        }

        /// <summary>
        /// Treina um modelo de Machine Learning para predição de status de motos
        /// </summary>
        /// <returns>Resultado do treinamento</returns>
        /// <response code="200">Treinamento realizado com sucesso</response>
        /// <response code="400">Dados insuficientes para treinamento</response>
        /// <response code="401">Usuário não autenticado</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpPost("train-model")]
        [ProducesResponseType(typeof(ModelTrainingResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    // [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> TrainModel()
        {
            try
            {
                _logger.LogInformation("Iniciando treinamento do modelo ML.NET");

                var result = await _motoAnalysisService.TrainStatusPredictionModelAsync();

                if (!result.Success)
                {
                    return BadRequest(new ErrorResponseDto
                    {
                        Message = result.Message,
                        Details = new[] { "Verifique se há dados suficientes no banco de dados" }
                    });
                }

                _logger.LogInformation("Modelo treinado com sucesso. Acurácia: {Accuracy}", result.Accuracy);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante treinamento do modelo");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto
                {
                    Message = "Erro interno do servidor",
                    Details = new[] { ex.Message }
                });
            }
        }

        /// <summary>
        /// Prediz o status de uma moto usando Machine Learning
        /// </summary>
        /// <param name="input">Dados de entrada para predição</param>
        /// <returns>Predição do status da moto</returns>
        /// <response code="200">Predição realizada com sucesso</response>
        /// <response code="400">Dados de entrada inválidos</response>
        /// <response code="401">Usuário não autenticado</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpPost("predict-status")]
        [ProducesResponseType(typeof(StatusPredictionResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PredictMotoStatus([FromBody] MotoPredictionInput input)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ErrorResponseDto
                    {
                        Message = "Dados de entrada inválidos",
                        Details = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                    });
                }

                _logger.LogInformation("Realizando predição de status para moto");

                var result = await _motoAnalysisService.PredictMotoStatusAsync(input);

                if (!result.Success)
                {
                    return BadRequest(new ErrorResponseDto
                    {
                        Message = result.Message,
                        Details = new[] { "Verifique se o modelo foi treinado corretamente" }
                    });
                }

                _logger.LogInformation("Predição realizada: {PredictedStatus}", result.PredictedStatus);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante predição de status");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto
                {
                    Message = "Erro interno do servidor",
                    Details = new[] { ex.Message }
                });
            }
        }

        /// <summary>
        /// Analisa padrões nas operações de motos usando Machine Learning
        /// </summary>
        /// <returns>Análise de padrões</returns>
        /// <response code="200">Análise realizada com sucesso</response>
        /// <response code="401">Usuário não autenticado</response>
        /// <response code="500">Erro interno do servidor</response>
        [HttpGet("analyze-patterns")]
        [ProducesResponseType(typeof(PatternAnalysisResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AnalyzePatterns()
        {
            try
            {
                _logger.LogInformation("Iniciando análise de padrões com ML.NET");

                var result = await _motoAnalysisService.AnalyzeOperationPatternsAsync();

                if (!result.Success)
                {
                    return BadRequest(new ErrorResponseDto
                    {
                        Message = result.Message,
                        Details = new[] { "Verifique se há operações no banco de dados" }
                    });
                }

                _logger.LogInformation("Análise de padrões concluída. Total de operações: {Total}", result.TotalOperacoes);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante análise de padrões");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto
                {
                    Message = "Erro interno do servidor",
                    Details = new[] { ex.Message }
                });
            }
        }

        /// <summary>
        /// Obtém informações sobre o modelo de Machine Learning
        /// </summary>
        /// <returns>Informações do modelo</returns>
        /// <response code="200">Informações obtidas com sucesso</response>
        /// <response code="401">Usuário não autenticado</response>
        [HttpGet("model-info")]
        [ProducesResponseType(typeof(ModelInfoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        public IActionResult GetModelInfo()
        {
            try
            {
                var modelInfo = new ModelInfoDto
                {
                    ModelName = "Moto Status Prediction Model",
                    Version = "1.0",
                    Algorithm = "SDCA Maximum Entropy",
                    Features = new List<string>
                    {
                        "Perfil do Usuário",
                        "Tipo de Operação",
                        "Dias desde Criação",
                        "Total de Operações"
                    },
                    TargetVariable = "Status da Moto",
                    PossibleStatuses = new List<string>
                    {
                        "PENDENTE",
                        "REPARO_SIMPLES",
                        "DANOS_ESTRUTURAIS",
                        "MOTOR_DEFEITUOSO",
                        "MANUTENCAO_AGENDADA",
                        "PRONTA",
                        "SEM_PLACA",
                        "ALUGADA",
                        "AGUARDANDO_ALUGUEL"
                    },
                    Description = "Modelo de Machine Learning para predição do status de motos baseado em características operacionais e históricas",
                    CreatedAt = DateTime.UtcNow,
                    Framework = "ML.NET 4.0.2"
                };

                return Ok(modelInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter informações do modelo");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto
                {
                    Message = "Erro interno do servidor",
                    Details = new[] { ex.Message }
                });
            }
        }
    }

    /// <summary>
    /// DTO para informações do modelo ML
    /// </summary>
    public class ModelInfoDto
    {
        /// <summary>
        /// Nome do modelo
        /// </summary>
        public string ModelName { get; set; } = string.Empty;

        /// <summary>
        /// Versão do modelo
        /// </summary>
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// Algoritmo utilizado
        /// </summary>
        public string Algorithm { get; set; } = string.Empty;

        /// <summary>
        /// Features utilizadas
        /// </summary>
        public List<string> Features { get; set; } = new();

        /// <summary>
        /// Variável alvo
        /// </summary>
        public string TargetVariable { get; set; } = string.Empty;

        /// <summary>
        /// Status possíveis
        /// </summary>
        public List<string> PossibleStatuses { get; set; } = new();

        /// <summary>
        /// Descrição do modelo
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Data de criação
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Framework utilizado
        /// </summary>
        public string Framework { get; set; } = string.Empty;
    }
}
