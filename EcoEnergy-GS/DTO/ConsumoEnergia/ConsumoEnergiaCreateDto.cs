using System.ComponentModel.DataAnnotations.Schema;

namespace EcoEnergy_GS.DTO.ConsumoEnergia
{
    public class ConsumoEnergiaCreateDto
    {
        [Column("DATA_CONSUMO")]
        public DateTime data_consumo { get; set; }

        [Column("CONSUMO")]
        public int consumo { get; set; }

        [ForeignKey("Residencia")]
        [Column("ID_RESIDENCIA")]
        public int id_residencia { get; set; }
    }
}
