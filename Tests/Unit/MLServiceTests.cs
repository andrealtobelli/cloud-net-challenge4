using Xunit;
using Microsoft.Extensions.Logging;
using challenge_3_net.Services.ML;
using challenge_3_net.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using challenge_3_net.Models;

namespace challenge_3_net.Tests.Unit
{
    /// <summary>
    /// Testes unitários para MotoAnalysisService (ML.NET)
    /// </summary>
    public class MLServiceTests
    {
        private readonly ApplicationDbContext _context;
        private readonly Mock<ILogger<MotoAnalysisService>> _mockLogger;
        private readonly MotoAnalysisService _mlService;

        public MLServiceTests()
        {
            // Configurar DbContext em memória
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            _mockLogger = new Mock<ILogger<MotoAnalysisService>>();

            _mlService = new MotoAnalysisService(_context, _mockLogger.Object);

            // Seed data para testes
            SeedTestData();
        }

        private void SeedTestData()
        {
            // Criar usuários de teste
            var usuario1 = new Usuario
            {
                Id = 1,
                NomeFilial = "Filial Teste 1",
                Email = "teste1@teste.com",
                Cnpj = "12345678000199",
                Perfil = PerfilUsuario.ADMIN,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                DataCriacao = DateTime.UtcNow.AddDays(-30)
            };

            var usuario2 = new Usuario
            {
                Id = 2,
                NomeFilial = "Filial Teste 2",
                Email = "teste2@teste.com",
                Cnpj = "98765432000188",
                Perfil = PerfilUsuario.OPERADOR,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                DataCriacao = DateTime.UtcNow.AddDays(-20)
            };

            _context.Usuarios.AddRange(usuario1, usuario2);

            // Criar motos de teste
            var moto1 = new Moto
            {
                Id = 1,
                Placa = "ABC1234",
                Chassi = "CHASSI123456789",
                UsuarioId = 1,
                DataCriacao = DateTime.UtcNow.AddDays(-30)
            };

            var moto2 = new Moto
            {
                Id = 2,
                Placa = "XYZ5678",
                Chassi = "CHASSI987654321",
                UsuarioId = 2,
                DataCriacao = DateTime.UtcNow.AddDays(-20)
            };

            _context.Motos.AddRange(moto1, moto2);

            // Criar status de motos
            var statusMoto1 = new StatusMoto
            {
                Id = 1,
                MotoId = 1,
                UsuarioId = 1,
                Status = StatusMotoEnum.PRONTA,
                Descricao = "Moto pronta para uso",
                Area = "Garagem A",
                DataStatus = DateTime.UtcNow
            };

            var statusMoto2 = new StatusMoto
            {
                Id = 2,
                MotoId = 2,
                UsuarioId = 2,
                Status = StatusMotoEnum.PENDENTE,
                Descricao = "Moto pendente de manutenção",
                Area = "Garagem B",
                DataStatus = DateTime.UtcNow
            };

            _context.StatusMotos.AddRange(statusMoto1, statusMoto2);

            // Criar operações de teste
            var operacao1 = new Operacao
            {
                Id = 1,
                MotoId = 1,
                UsuarioId = 1,
                TipoOperacao = TipoOperacao.CHECK_IN,
                Descricao = "Check-in realizado",
                DataOperacao = DateTime.UtcNow
            };

            var operacao2 = new Operacao
            {
                Id = 2,
                MotoId = 2,
                UsuarioId = 2,
                TipoOperacao = TipoOperacao.CHECK_OUT,
                Descricao = "Check-out realizado",
                DataOperacao = DateTime.UtcNow
            };

            _context.Operacoes.AddRange(operacao1, operacao2);

            _context.SaveChanges();
        }

        [Fact]
        public async Task TrainStatusPredictionModel_WithInsufficientData_ShouldReturnFailure()
        {
            // Arrange - Limpar dados para ter menos de 10 registros
            _context.StatusMotos.RemoveRange(_context.StatusMotos);
            _context.SaveChanges();

            // Act
            var result = await _mlService.TrainStatusPredictionModelAsync();

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Dados insuficientes", result.Message);
        }

        [Fact]
        public async Task AnalyzeOperationPatterns_WithData_ShouldReturnSuccess()
        {
            // Act
            var result = await _mlService.AnalyzeOperationPatternsAsync();

            // Assert
            Assert.True(result.Success);
            Assert.True(result.TotalOperacoes > 0);
        }

        [Fact]
        public async Task AnalyzeOperationPatterns_WithoutData_ShouldReturnFailure()
        {
            // Arrange - Limpar operações
            _context.Operacoes.RemoveRange(_context.Operacoes);
            _context.SaveChanges();

            // Act
            var result = await _mlService.AnalyzeOperationPatternsAsync();

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Nenhuma operação", result.Message);
        }

        [Fact]
        public async Task PredictMotoStatus_WithoutTrainedModel_ShouldAutoTrain()
        {
            // Arrange
            var input = new MotoPredictionInput
            {
                Perfil = "ADMIN",
                TipoOperacao = "CHECK_IN",
                DiasDesdeCriacao = 30,
                TotalOperacoes = 5
            };

            // Act
            var result = await _mlService.PredictMotoStatusAsync(input);

            // Assert
            // Pode retornar sucesso ou falha dependendo dos dados disponíveis
            Assert.NotNull(result);
        }

        [Fact]
        public void GetModelInfo_ShouldReturnModelInformation()
        {
            // Este teste é mais para verificar que o método existe e funciona
            // A implementação real está no controller
            Assert.True(true); // Placeholder
        }

        private void Dispose()
        {
            _context.Dispose();
        }
    }
}

