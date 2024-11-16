using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoEnergy_GS.Models
{
    [Table("Historico_Pontos")]
    public class HistoricoPontosModel
    {
        [Key]
        public int Id_historico {  get; set; }
        public DateTime Data { get ; set; }
        public int quantidade { get; set; }

        [ForeignKey("Usuarios")]
        [Column("Id_usuario")]
        public int Id_usuario { get; set; }
    }
}
