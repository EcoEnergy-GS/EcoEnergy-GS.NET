using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoEnergy_GS.Models
{
    [Table("Recompensas")]
    public class RecompensasModel
    {
        [Key]
        public int Id_recompensas { get; set; }

        [StringLength(100, MinimumLength = 8, ErrorMessage = "A descrição deve ter no máximo 100 caracteres.")]
        public string Descricao { get; set; }
        public int Pontos_necessarios { get; set; }

        [InverseProperty("Recompensas")]
        public ICollection<TrocasRecompensasModel> Trocas { get; set; }
    }
}
