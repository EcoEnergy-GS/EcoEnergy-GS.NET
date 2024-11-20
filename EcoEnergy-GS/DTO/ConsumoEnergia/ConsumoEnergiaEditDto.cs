﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcoEnergy_GS.DTO.ConsumoEnergia
{
    public class ConsumoEnergiaEditDto
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
    }
}