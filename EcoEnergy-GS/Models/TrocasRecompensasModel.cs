using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoEnergy_GS.Models
{
    [Table("Trocas_recompensas")]
    public class TrocasRecompensasModel
    {
        [Key]
        public int Id_trocas { get; set; }
        public DateTime DataTroca { get; set; }
        public int Pontos_utilizados { get; set; }

        [ForeignKey("Usuarios")]
        [Column("Id_usuario")]
        public int Id_usuario { get; set; }

        [ForeignKey("Recompensas")]
        [Column("Id_recompensas")]
        public int Id_recompensas { get; set; }
    }
}
