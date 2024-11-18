using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoEnergy_GS.Models
{
    [Table("CONSUMO_ENERGIA")]
    public class ConsumoEnergiaModel
    {
        [Key]
        [Column("ID_CONSUMO")]
        public int id_consumo { get; set; }

        [Column("DATA_CONSUMO")]
        public DateTime data_consumo { get; set; }

        [Column("CONSUMO")]
        public int consumo { get; set; }

        [ForeignKey("Residencia")]
        [Column("ID_RESIDENCIA")]
        public int id_residencia { get; set; }
        public ResidenciaModel Residencia { get; set; }
    }
}
