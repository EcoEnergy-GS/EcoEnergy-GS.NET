using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoEnergy_GS.Models
{
    [Table("Historico_Pontos")]
    public class HistoricoPontosModel
    {
        [Key]
        public int Id_historico { get; set; }
        public DateTime Data { get; set; }
        public int Quantidade { get; set; }

        [ForeignKey("Usuario")]
        [Column("Id_usuario")]
        public int Id_usuario { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
