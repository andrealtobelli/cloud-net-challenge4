using Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using challenge_3_net.Services.Auth;
using challenge_3_net.Models;
using challenge_3_net.Data;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace challenge_3_net.Tests.Unit
{
    /// <summary>
    /// Testes unitários para JwtService
    /// </summary>
    public class JwtServiceTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<ILogger<JwtService>> _mockLogger;
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;

        public JwtServiceTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockLogger = new Mock<ILogger<JwtService>>();

            // Configurar mock do IConfiguration com seção JwtSettings
            var jwtSettingsSection = new Mock<IConfigurationSection>();
            jwtSettingsSection.Setup(x => x["SecretKey"])
                .Returns("TestSecretKeyForUnitTesting123456789");
            jwtSettingsSection.Setup(x => x["Issuer"])
                .Returns("TestIssuer");
            jwtSettingsSection.Setup(x => x["Audience"])
                .Returns("TestAudience");
            jwtSettingsSection.Setup(x => x["ExpiryMinutes"])
                .Returns("60");

            _mockConfiguration.Setup(x => x.GetSection("JwtSettings"))
                .Returns(jwtSettingsSection.Object);

            // Configurar DbContext em memória
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            _jwtService = new JwtService(_mockConfiguration.Object, _context, _mockLogger.Object);
        }

        [Fact]
        public void GenerateToken_WithValidUsuario_ShouldReturnValidToken()
        {
            // Arrange
            var usuario = new Usuario
            {
                Id = 1,
                NomeFilial = "Teste Filial",
                Email = "teste@teste.com",
                Perfil = PerfilUsuario.ADMIN,
                Cnpj = "12345678000199",
                SenhaHash = BCrypt.Net.BCrypt.HashPassword("123456")
            };

            // Act
            var token = _jwtService.GenerateToken(usuario);

            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
            Assert.Contains(".", token); // JWT deve ter pontos separadores
        }

        [Fact]
        public void GenerateToken_WithDifferentPerfis_ShouldGenerateDifferentTokens()
        {
            // Arrange
            var adminUsuario = new Usuario
            {
                Id = 1,
                NomeFilial = "Admin Filial",
                Email = "admin@teste.com",
                Perfil = PerfilUsuario.ADMIN,
                Cnpj = "11111111000111",
                SenhaHash = BCrypt.Net.BCrypt.HashPassword("123456")
            };

            var operadorUsuario = new Usuario
            {
                Id = 2,
                NomeFilial = "Operador Filial",
                Email = "operador@teste.com",
                Perfil = PerfilUsuario.OPERADOR,
                Cnpj = "22222222000222",
                SenhaHash = BCrypt.Net.BCrypt.HashPassword("123456")
            };

            // Act
            var adminToken = _jwtService.GenerateToken(adminUsuario);
            var operadorToken = _jwtService.GenerateToken(operadorUsuario);

            // Assert
            Assert.NotEqual(adminToken, operadorToken);
        }

        [Fact]
        public void ValidateToken_WithValidToken_ShouldReturnClaimsPrincipal()
        {
            // Arrange
            var usuario = new Usuario
            {
                Id = 1,
                NomeFilial = "Teste Filial",
                Email = "teste@teste.com",
                Perfil = PerfilUsuario.ADMIN,
                Cnpj = "12345678000199",
                SenhaHash = BCrypt.Net.BCrypt.HashPassword("123456")
            };

            var token = _jwtService.GenerateToken(usuario);

            // Act
            var principal = _jwtService.ValidateToken(token);

            // Assert
            Assert.NotNull(principal);
            Assert.NotNull(principal.Identity);
            Assert.True(principal.Identity.IsAuthenticated);
            Assert.Equal("1", principal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
            Assert.Equal("Teste Filial", principal.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value);
            Assert.Equal("teste@teste.com", principal.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value);
        }

        [Fact]
        public void ValidateToken_WithInvalidToken_ShouldReturnNull()
        {
            // Arrange
            var invalidToken = "invalid.token.here";

            // Act
            var principal = _jwtService.ValidateToken(invalidToken);

            // Assert
            Assert.Null(principal);
        }

        [Fact]
        public void ValidateToken_WithExpiredToken_ShouldReturnNull()
        {
            // Arrange
            var expiredToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyLCJleHAiOjF9.invalid";

            // Act
            var principal = _jwtService.ValidateToken(expiredToken);

            // Assert
            Assert.Null(principal);
        }

        [Fact]
        public void HasRole_WithAdminUser_ShouldReturnTrueForAdminRole()
        {
            // Arrange
            var usuario = new Usuario
            {
                Id = 1,
                NomeFilial = "Admin Filial",
                Email = "admin@teste.com",
                Perfil = PerfilUsuario.ADMIN,
                Cnpj = "12345678000199",
                SenhaHash = BCrypt.Net.BCrypt.HashPassword("123456")
            };

            var token = _jwtService.GenerateToken(usuario);
            var principal = _jwtService.ValidateToken(token);

            // Act
            Assert.NotNull(principal);
            var hasAdminRole = _jwtService.HasRole(principal, "ADMIN");
            var isAdmin = _jwtService.IsAdmin(principal);
            var isManagerOrAdmin = _jwtService.IsManagerOrAdmin(principal);

            // Assert
            Assert.True(hasAdminRole);
            Assert.True(isAdmin);
            Assert.True(isManagerOrAdmin);
        }

        [Fact]
        public void HasRole_WithOperadorUser_ShouldReturnFalseForAdminRole()
        {
            // Arrange
            var usuario = new Usuario
            {
                Id = 1,
                NomeFilial = "Operador Filial",
                Email = "operador@teste.com",
                Perfil = PerfilUsuario.OPERADOR,
                Cnpj = "12345678000199",
                SenhaHash = BCrypt.Net.BCrypt.HashPassword("123456")
            };

            var token = _jwtService.GenerateToken(usuario);
            var principal = _jwtService.ValidateToken(token);

            // Act
            Assert.NotNull(principal);
            var hasAdminRole = _jwtService.HasRole(principal, "ADMIN");
            var isAdmin = _jwtService.IsAdmin(principal);
            var isManagerOrAdmin = _jwtService.IsManagerOrAdmin(principal);

            // Assert
            Assert.False(hasAdminRole);
            Assert.False(isAdmin);
            Assert.False(isManagerOrAdmin);
        }

        [Fact]
        public void HasRole_WithGerenteUser_ShouldReturnTrueForManagerRole()
        {
            // Arrange
            var usuario = new Usuario
            {
                Id = 1,
                NomeFilial = "Gerente Filial",
                Email = "gerente@teste.com",
                Perfil = PerfilUsuario.GERENTE,
                Cnpj = "12345678000199",
                SenhaHash = BCrypt.Net.BCrypt.HashPassword("123456")
            };

            var token = _jwtService.GenerateToken(usuario);
            var principal = _jwtService.ValidateToken(token);

            // Act
            Assert.NotNull(principal);
            var hasManagerRole = _jwtService.HasRole(principal, "GERENTE");
            var isAdmin = _jwtService.IsAdmin(principal);
            var isManagerOrAdmin = _jwtService.IsManagerOrAdmin(principal);

            // Assert
            Assert.True(hasManagerRole);
            Assert.False(isAdmin);
            Assert.True(isManagerOrAdmin);
        }

        private void Dispose()
        {
            _context.Dispose();
        }
    }
}
