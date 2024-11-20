using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoEnergy_GS.Models
{
    [Table("HISTORICO_PONTOS")]
    public class HistoricoPontosModel
    {
        [Key]
        [Column("ID_HISTORICO")]
        public int id_historico { get; set; }
        [Column("DATA_HISTORICO")]
        public DateTime data_historico { get; set; }
        [Column("QUANTIDADE")]
        public int quantidade { get; set; }

        [ForeignKey("Usuario")]
        [Column("ID_USUARIOS")]
        public int id_usuarios { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
