using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace challenge_3_net.Models
{
    /// <summary>
    /// Entidade que representa uma operação realizada no sistema
    /// </summary>
    public class Operacao
    {
        /// <summary>
        /// Identificador único da operação
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// Tipo da operação realizada
        /// </summary>
        [Required]
        public TipoOperacao TipoOperacao { get; set; }

        /// <summary>
        /// Descrição detalhada da operação
        /// </summary>
        [MaxLength(1000)]
        public string? Descricao { get; set; }

        /// <summary>
        /// Data e hora da operação
        /// </summary>
        [Required]
        public DateTime DataOperacao { get; set; }

        /// <summary>
        /// ID da moto envolvida na operação
        /// </summary>
        [Required]
        public long MotoId { get; set; }

        /// <summary>
        /// ID do usuário que realizou a operação
        /// </summary>
        [Required]
        public long UsuarioId { get; set; }

        // Propriedades de navegação
        /// <summary>
        /// Moto envolvida na operação
        /// </summary>
        [ForeignKey("MotoId")]
        public virtual Moto Moto { get; set; } = null!;

        /// <summary>
        /// Usuário que realizou a operação
        /// </summary>
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; } = null!;
    }

    /// <summary>
    /// Enum que define os tipos de operações possíveis
    /// </summary>
    public enum TipoOperacao
    {
        /// <summary>
        /// Check-in da moto
        /// </summary>
        CHECK_IN = 0,

        /// <summary>
        /// Check-out da moto
        /// </summary>
        CHECK_OUT = 1
    }
}
