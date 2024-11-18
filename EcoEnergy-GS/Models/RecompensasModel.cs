using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoEnergy_GS.Models
{
    [Table("RECOMPENSAS")]
    public class RecompensasModel
    {
        [Key]
        [Column("ID_RECOMPENSAS")]
        public int id_recompensas { get; set; }

        [StringLength(100, MinimumLength = 8, ErrorMessage = "A descrição deve ter no máximo 100 caracteres.")]
        [Column("DESCRICAO")]
        public string descricao { get; set; }

        [Column("PONTOS_NECESSARIOS")]
        public int pontos_necessarios { get; set; }

        [InverseProperty("Recompensas")]
        public ICollection<TrocasRecompensasModel> Trocas { get; set; }
    }
}
