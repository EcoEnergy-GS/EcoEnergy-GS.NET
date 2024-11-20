using EcoEnergy_GS.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcoEnergy_GS.DTO.TrocasRecompensas
{
    public class TrocasRecompensasEditDto
    {
        [Key]
        [Column("ID_TROCAS")]
        public int id_trocas { get; set; }

        [Column("DATA_TROCA")]
        public DateTime data_troca { get; set; }

        [Column("PONTOS_UTILIZADOS")]
        public int pontos_utilizados { get; set; }

        [ForeignKey("Usuario")]
        [Column("ID_USUARIOS")]
        public int id_usuarios { get; set; }

        [ForeignKey("Recompensas")]
        [Column("ID_RECOMPENSAS")]
        public int id_recompensas { get; set; }
    }
}
