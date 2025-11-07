using Microsoft.AspNetCore.Mvc;
using challenge_3_net.Services.Auth;
using challenge_3_net.Models.DTOs;
using System.Security.Claims;

namespace challenge_3_net.Controllers
{
    /// <summary>
    /// Controller para autenticação e autorização
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(JwtService jwtService, ILogger<AuthController> logger)
        {
            _jwtService = jwtService;
            _logger = logger;
        }

        /// <summary>
        /// Autentica um usuário e retorna um token JWT
        /// </summary>
        /// <param name="loginDto">Dados de login do usuário</param>
        /// <returns>Token JWT se autenticação bem-sucedida</returns>
        /// <response code="200">Login realizado com sucesso</response>
        /// <response code="401">Credenciais inválidas</response>
        /// <response code="400">Dados de entrada inválidos</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
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

                _logger.LogInformation("Tentativa de login para email: {Email}", loginDto.Email);

                var token = await _jwtService.AuthenticateAsync(loginDto.Email, loginDto.Senha);

                if (token == null)
                {
                    _logger.LogWarning("Falha na autenticação para email: {Email}", loginDto.Email);
                    return Unauthorized(new ErrorResponseDto
                    {
                        Message = "Email ou senha incorretos",
                        Details = new[] { "Verifique suas credenciais e tente novamente" }
                    });
                }

                _logger.LogInformation("Login realizado com sucesso para email: {Email}", loginDto.Email);

                return Ok(new LoginResponseDto
                {
                    Token = token,
                    TokenType = "Bearer",
                    ExpiresIn = 3600, // 1 hora
                    Message = "Login realizado com sucesso"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante processo de login para email: {Email}", loginDto.Email);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto
                {
                    Message = "Erro interno do servidor",
                    Details = new[] { "Tente novamente mais tarde" }
                });
            }
        }

        /// <summary>
        /// Valida um token JWT e retorna informações do usuário
        /// </summary>
        /// <returns>Informações do usuário autenticado</returns>
        /// <response code="200">Token válido</response>
        /// <response code="401">Token inválido ou expirado</response>
        [HttpPost("validate")]
        [ProducesResponseType(typeof(UserInfoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ValidateToken()
        {
            try
            {
                var authHeader = Request.Headers["Authorization"].FirstOrDefault();
                if (authHeader == null || !authHeader.StartsWith("Bearer "))
                {
                    return Unauthorized(new ErrorResponseDto
                    {
                        Message = "Token de autorização não fornecido",
                        Details = new[] { "Inclua o header 'Authorization: Bearer {token}'" }
                    });
                }

                var token = authHeader.Substring("Bearer ".Length).Trim();
                var principal = _jwtService.ValidateToken(token);

                if (principal == null)
                {
                    return Unauthorized(new ErrorResponseDto
                    {
                        Message = "Token inválido ou expirado",
                        Details = new[] { "Faça login novamente para obter um novo token" }
                    });
                }

                var usuario = await _jwtService.GetCurrentUserAsync(principal);
                if (usuario == null)
                {
                    return Unauthorized(new ErrorResponseDto
                    {
                        Message = "Usuário não encontrado",
                        Details = new[] { "Token válido mas usuário não existe mais" }
                    });
                }

                return Ok(new UserInfoDto
                {
                    Id = usuario.Id,
                    NomeFilial = usuario.NomeFilial,
                    Email = usuario.Email,
                    Perfil = usuario.Perfil.ToString(),
                    Cnpj = usuario.Cnpj,
                    DataCriacao = usuario.DataCriacao,
                    Roles = principal.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value)
                        .ToList()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante validação de token");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto
                {
                    Message = "Erro interno do servidor",
                    Details = new[] { "Tente novamente mais tarde" }
                });
            }
        }

        /// <summary>
        /// Obtém informações do usuário atual
        /// </summary>
        /// <returns>Informações do usuário atual</returns>
        /// <response code="200">Informações obtidas com sucesso</response>
        /// <response code="401">Usuário não autenticado</response>
        [HttpGet("me")]
        [ProducesResponseType(typeof(UserInfoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCurrentUser()
        {
            try
            {
                var usuario = await _jwtService.GetCurrentUserAsync(User);
                if (usuario == null)
                {
                    return Unauthorized(new ErrorResponseDto
                    {
                        Message = "Usuário não autenticado",
                        Details = new[] { "Faça login para acessar este recurso" }
                    });
                }

                return Ok(new UserInfoDto
                {
                    Id = usuario.Id,
                    NomeFilial = usuario.NomeFilial,
                    Email = usuario.Email,
                    Perfil = usuario.Perfil.ToString(),
                    Cnpj = usuario.Cnpj,
                    DataCriacao = usuario.DataCriacao,
                    Roles = User.Claims
                        .Where(c => c.Type == ClaimTypes.Role)
                        .Select(c => c.Value)
                        .ToList()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter informações do usuário atual");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto
                {
                    Message = "Erro interno do servidor",
                    Details = new[] { "Tente novamente mais tarde" }
                });
            }
        }

        /// <summary>
        /// Verifica se o usuário atual tem permissões de administrador
        /// </summary>
        /// <returns>Status de permissão</returns>
        /// <response code="200">Verificação realizada</response>
        /// <response code="401">Usuário não autenticado</response>
        [HttpGet("check-admin")]
        [ProducesResponseType(typeof(PermissionCheckDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status401Unauthorized)]
        public IActionResult CheckAdminPermission()
        {
            try
            {
                var isAdmin = _jwtService.IsAdmin(User);
                var isManager = _jwtService.IsManagerOrAdmin(User);

                return Ok(new PermissionCheckDto
                {
                    IsAdmin = isAdmin,
                    IsManager = isManager,
                    IsOperador = User.IsInRole("OPERADOR"),
                    Message = isAdmin ? "Usuário tem permissões de administrador" : 
                              isManager ? "Usuário tem permissões de gerente" : 
                              "Usuário tem permissões de operador"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao verificar permissões do usuário");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto
                {
                    Message = "Erro interno do servidor",
                    Details = new[] { "Tente novamente mais tarde" }
                });
            }
        }
    }

    /// <summary>
    /// DTO para dados de login
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Email do usuário
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Senha do usuário
        /// </summary>
        public string Senha { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO para resposta de login
    /// </summary>
    public class LoginResponseDto
    {
        /// <summary>
        /// Token JWT
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Tipo do token
        /// </summary>
        public string TokenType { get; set; } = string.Empty;

        /// <summary>
        /// Tempo de expiração em segundos
        /// </summary>
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Mensagem de resposta
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO para informações do usuário
    /// </summary>
    public class UserInfoDto
    {
        /// <summary>
        /// ID do usuário
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Nome da filial
        /// </summary>
        public string NomeFilial { get; set; } = string.Empty;

        /// <summary>
        /// Email do usuário
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Perfil do usuário
        /// </summary>
        public string Perfil { get; set; } = string.Empty;

        /// <summary>
        /// CNPJ da filial
        /// </summary>
        public string Cnpj { get; set; } = string.Empty;

        /// <summary>
        /// Data de criação
        /// </summary>
        public DateTime DataCriacao { get; set; }

        /// <summary>
        /// Roles do usuário
        /// </summary>
        public List<string> Roles { get; set; } = new();
    }

    /// <summary>
    /// DTO para verificação de permissões
    /// </summary>
    public class PermissionCheckDto
    {
        /// <summary>
        /// Se é administrador
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Se é gerente ou administrador
        /// </summary>
        public bool IsManager { get; set; }

        /// <summary>
        /// Se é operador
        /// </summary>
        public bool IsOperador { get; set; }

        /// <summary>
        /// Mensagem descritiva
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO para resposta de erro
    /// </summary>
    public class ErrorResponseDto
    {
        /// <summary>
        /// Mensagem de erro
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Detalhes do erro
        /// </summary>
        public IEnumerable<string> Details { get; set; } = new List<string>();
    }
}
