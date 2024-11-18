﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcoEnergy_GS.Models
{
    [Table("RESIDENCIA")]
    public class ResidenciaModel
    {
        [Key]
        [Column("ID_RESIDENCIA")]
        public int id_residencia { get; set; }

        [StringLength(50, MinimumLength = 8, ErrorMessage = "O dispositivo deve ter no máximo 50 caracteres.")]
        [Required]
        [Column("DISPOSITIVO_MONITORAMENTO")]
        public string dispotivico_monitoramento { get; set; }

        [Required]
        [Column("QUANTIDADE_PESSOAS")]
        public int quantidade_pessoas { get; set; }

        [Required]
        [Column("MEDIA_CONSUMO")]
        public Double media_consumo { get; set; }

        [ForeignKey("Usuario")]
        [Column("ID_USUARIOS")]
        public int id_usuarios { get; set; }
        public UsuarioModel Usuario { get; set; }

        [ForeignKey("TipoEletrodomestico")]
        [Column("ID_ELETRODOMESTICO")]
        public int id_eletrodomestico { get; set; }
        public TipoEletrodomesticoModel TipoEletrodomestico { get; set; }

        [ForeignKey("Endereco")]
        [Column("ID_ENDERECO")]
        public int id_endereco { get; set; }
        public EnderecoModel Endereco { get; set; }

        [JsonIgnore]
        [InverseProperty("Residencia")]
        public ICollection<ConsumoEnergiaModel> ConsumoEnergia { get; set; }
    }
}
