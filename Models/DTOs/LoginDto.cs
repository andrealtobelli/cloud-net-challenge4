using System.ComponentModel.DataAnnotations;

namespace challenge_3_net.Models.DTOs
{
    /// <summary>
    /// DTO para login de usuário
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Email do usuário
        /// </summary>
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Senha do usuário
        /// </summary>
        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(6, ErrorMessage = "Senha deve ter pelo menos 6 caracteres")]
        public string Senha { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO para resposta de login
    /// </summary>
    public class LoginResponseDto
    {
        /// <summary>
        /// Token JWT gerado
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// Data de expiração do token
        /// </summary>
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// Tipo do token
        /// </summary>
        public string TokenType { get; set; } = "Bearer";
    }
}
