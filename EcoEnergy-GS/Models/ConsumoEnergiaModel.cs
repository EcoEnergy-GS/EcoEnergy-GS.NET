using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoEnergy_GS.Models
{
    [Table("Consumo_Energia")]
    public class ConsumoEnergiaModel
    {
        [Key]
        public int Id_consumo { get; set; }
        public DateTime Data {  get; set; }
        public int Consumo { get; set; }

        [ForeignKey("Residencia")]
        [Column("Id_residencia")]
        public int Id_residencia { get; set; }
    }
}
