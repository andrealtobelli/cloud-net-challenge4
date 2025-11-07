using Microsoft.ML;
using Microsoft.ML.Data;
using challenge_3_net.Data;
using Microsoft.EntityFrameworkCore;

namespace challenge_3_net.Services.ML
{
    /// <summary>
    /// Serviço de análise preditiva de motos usando ML.NET
    /// </summary>
    public class MotoAnalysisService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MotoAnalysisService> _logger;
        private readonly MLContext _mlContext;
        private ITransformer? _model;

        public MotoAnalysisService(ApplicationDbContext context, ILogger<MotoAnalysisService> logger)
        {
            _context = context;
            _logger = logger;
            _mlContext = new MLContext(seed: 1);
        }

        /// <summary>
        /// Treina um modelo para prever o status de uma moto baseado em características
        /// </summary>
        /// <returns>Resultado do treinamento</returns>
        public async Task<ModelTrainingResult> TrainStatusPredictionModelAsync()
        {
            try
            {
                _logger.LogInformation("Iniciando treinamento do modelo de predição de status");

                // Buscar dados históricos
                var trainingData = await GetTrainingDataAsync();
                
                if (trainingData.Count < 10)
                {
                    return new ModelTrainingResult
                    {
                        Success = false,
                        Message = "Dados insuficientes para treinamento (mínimo 10 registros)",
                        Accuracy = 0,
                        RecordsUsed = trainingData.Count
                    };
                }

                // Converter para IDataView
                var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);

                // Definir pipeline de treinamento
                var pipeline = _mlContext.Transforms.Categorical.OneHotEncoding("PerfilEncoded", "Perfil")
                    .Append(_mlContext.Transforms.Categorical.OneHotEncoding("TipoOperacaoEncoded", "TipoOperacao"))
                    .Append(_mlContext.Transforms.Concatenate("Features", "PerfilEncoded", "TipoOperacaoEncoded", "DiasDesdeCriacao", "TotalOperacoes"))
                    .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Status", "Features"))
                    .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel", "PredictedLabel"));

                // Treinar modelo
                _model = pipeline.Fit(dataView);

                // Avaliar modelo
                var predictions = _model.Transform(dataView);
                var metrics = _mlContext.MulticlassClassification.Evaluate(predictions);

                _logger.LogInformation("Modelo treinado com sucesso. Acurácia: {Accuracy}", metrics.MacroAccuracy);

                return new ModelTrainingResult
                {
                    Success = true,
                    Message = "Modelo treinado com sucesso",
                    Accuracy = metrics.MacroAccuracy,
                    RecordsUsed = trainingData.Count,
                    Metrics = new ModelMetrics
                    {
                        MacroAccuracy = metrics.MacroAccuracy,
                        MicroAccuracy = metrics.MicroAccuracy,
                        LogLoss = metrics.LogLoss,
                        LogLossReduction = metrics.LogLossReduction
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante treinamento do modelo");
                return new ModelTrainingResult
                {
                    Success = false,
                    Message = $"Erro durante treinamento: {ex.Message}",
                    Accuracy = 0,
                    RecordsUsed = 0
                };
            }
        }

        /// <summary>
        /// Prediz o status de uma moto baseado em características
        /// </summary>
        /// <param name="input">Dados de entrada para predição</param>
        /// <returns>Predição do status</returns>
        public async Task<StatusPredictionResult> PredictMotoStatusAsync(MotoPredictionInput input)
        {
            try
            {
                if (_model == null)
                {
                    _logger.LogWarning("Modelo não treinado. Treinando modelo automaticamente...");
                    var trainingResult = await TrainStatusPredictionModelAsync();
                    if (!trainingResult.Success)
                    {
                        return new StatusPredictionResult
                        {
                            Success = false,
                            Message = "Não foi possível treinar o modelo para predição",
                            PredictedStatus = "UNKNOWN"
                        };
                    }
                }

                // Criar dados de entrada
                var inputData = new MotoTrainingData
                {
                    Perfil = input.Perfil,
                    TipoOperacao = input.TipoOperacao,
                    DiasDesdeCriacao = input.DiasDesdeCriacao,
                    TotalOperacoes = input.TotalOperacoes,
                    Status = "UNKNOWN" // Será predito
                };

                // Fazer predição
                var predictionEngine = _mlContext.Model.CreatePredictionEngine<MotoTrainingData, MotoPredictionOutput>(_model);
                var prediction = predictionEngine.Predict(inputData);

                _logger.LogInformation("Predição realizada: {PredictedStatus}", prediction.PredictedStatus);

                return new StatusPredictionResult
                {
                    Success = true,
                    Message = "Predição realizada com sucesso",
                    PredictedStatus = prediction.PredictedStatus,
                    Confidence = prediction.Score?.Max() ?? 0,
                    AllScores = prediction.Score?.ToList() ?? new List<float>()
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante predição de status");
                return new StatusPredictionResult
                {
                    Success = false,
                    Message = $"Erro durante predição: {ex.Message}",
                    PredictedStatus = "UNKNOWN"
                };
            }
        }

        /// <summary>
        /// Analisa padrões nas operações de motos
        /// </summary>
        /// <returns>Análise de padrões</returns>
        public async Task<PatternAnalysisResult> AnalyzeOperationPatternsAsync()
        {
            try
            {
                _logger.LogInformation("Iniciando análise de padrões de operações");

                var operacoes = await _context.Operacoes
                    .Include(o => o.Moto)
                    .Include(o => o.Usuario)
                    .ToListAsync();

                if (!operacoes.Any())
                {
                    return new PatternAnalysisResult
                    {
                        Success = false,
                        Message = "Nenhuma operação encontrada para análise"
                    };
                }

                // Análise de frequência por tipo de operação
                var operacaoFrequencia = operacoes
                    .GroupBy(o => o.TipoOperacao)
                    .Select(g => new { Tipo = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .ToList();

                // Análise por horário
                var operacaoPorHora = operacoes
                    .GroupBy(o => o.DataOperacao.Hour)
                    .Select(g => new { Hora = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .ToList();

                // Análise por usuário
                var operacaoPorUsuario = operacoes
                    .GroupBy(o => o.UsuarioId)
                    .Select(g => new { UsuarioId = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .Take(5)
                    .ToList();

                // Análise de motos mais ativas
                var motosMaisAtivas = operacoes
                    .GroupBy(o => o.MotoId)
                    .Select(g => new { MotoId = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .Take(5)
                    .ToList();

                return new PatternAnalysisResult
                {
                    Success = true,
                    Message = "Análise de padrões concluída com sucesso",
                    TotalOperacoes = operacoes.Count,
                    OperacaoFrequencia = operacaoFrequencia.Cast<dynamic>().ToList(),
                    OperacaoPorHora = operacaoPorHora.Cast<dynamic>().ToList(),
                    OperacaoPorUsuario = operacaoPorUsuario.Cast<dynamic>().ToList(),
                    MotosMaisAtivas = motosMaisAtivas.Cast<dynamic>().ToList(),
                    PeriodoAnalise = new
                    {
                        Inicio = operacoes.Min(o => o.DataOperacao),
                        Fim = operacoes.Max(o => o.DataOperacao)
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante análise de padrões");
                return new PatternAnalysisResult
                {
                    Success = false,
                    Message = $"Erro durante análise: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Obtém dados de treinamento do banco de dados
        /// </summary>
        private async Task<List<MotoTrainingData>> GetTrainingDataAsync()
        {
            var dados = await _context.StatusMotos
                .Include(s => s.Moto)
                .Include(s => s.Usuario)
                .Select(s => new MotoTrainingData
                {
                    Perfil = s.Usuario.Perfil.ToString(),
                    TipoOperacao = "CHECK_IN", // Simplificado para exemplo
                    DiasDesdeCriacao = (int)(DateTime.UtcNow - s.Moto.DataCriacao).TotalDays,
                    TotalOperacoes = _context.Operacoes.Count(o => o.MotoId == s.MotoId),
                    Status = s.Status.ToString()
                })
                .ToListAsync();

            return dados;
        }
    }

    /// <summary>
    /// Dados de treinamento para ML.NET
    /// </summary>
    public class MotoTrainingData
    {
        [LoadColumn(0)]
        public string Perfil { get; set; } = string.Empty;

        [LoadColumn(1)]
        public string TipoOperacao { get; set; } = string.Empty;

        [LoadColumn(2)]
        public int DiasDesdeCriacao { get; set; }

        [LoadColumn(3)]
        public int TotalOperacoes { get; set; }

        [LoadColumn(4)]
        public string Status { get; set; } = string.Empty;
    }

    /// <summary>
    /// Output de predição ML.NET
    /// </summary>
    public class MotoPredictionOutput
    {
        [ColumnName("PredictedLabel")]
        public string PredictedStatus { get; set; } = string.Empty;

        [ColumnName("Score")]
        public float[]? Score { get; set; }
    }

    /// <summary>
    /// Input para predição
    /// </summary>
    public class MotoPredictionInput
    {
        public string Perfil { get; set; } = string.Empty;
        public string TipoOperacao { get; set; } = string.Empty;
        public int DiasDesdeCriacao { get; set; }
        public int TotalOperacoes { get; set; }
    }

    /// <summary>
    /// Resultado do treinamento do modelo
    /// </summary>
    public class ModelTrainingResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public double Accuracy { get; set; }
        public int RecordsUsed { get; set; }
        public ModelMetrics? Metrics { get; set; }
    }

    /// <summary>
    /// Métricas do modelo
    /// </summary>
    public class ModelMetrics
    {
        public double MacroAccuracy { get; set; }
        public double MicroAccuracy { get; set; }
        public double LogLoss { get; set; }
        public double LogLossReduction { get; set; }
    }

    /// <summary>
    /// Resultado de predição de status
    /// </summary>
    public class StatusPredictionResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string PredictedStatus { get; set; } = string.Empty;
        public float Confidence { get; set; }
        public List<float> AllScores { get; set; } = new();
    }

    /// <summary>
    /// Resultado de análise de padrões
    /// </summary>
    public class PatternAnalysisResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int TotalOperacoes { get; set; }
        public List<dynamic> OperacaoFrequencia { get; set; } = new();
        public List<dynamic> OperacaoPorHora { get; set; } = new();
        public List<dynamic> OperacaoPorUsuario { get; set; } = new();
        public List<dynamic> MotosMaisAtivas { get; set; } = new();
        public dynamic? PeriodoAnalise { get; set; }
    }
}
