using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoEnergy_GS.Models
{
    [Table("TROCAS_RECOMPENSAS")]
    public class TrocasRecompensasModel
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
        public UsuarioModel Usuario { get; set; }

        [ForeignKey("Recompensas")]
        [Column("ID_RECOMPENSAS")]
        public int id_recompensas { get; set; }
        public RecompensasModel Recompensas { get; set; }
    }
}
