using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcoEnergy_GS.Models
{
    [Table("Residencia")]
    public class ResidenciaModel
    {
        [Key]
        public int Id_residencia { get; set; }

        [StringLength(50, MinimumLength = 8, ErrorMessage = "O dispositivo deve ter no máximo 50 caracteres.")]
        [Required]
        public string Dispotivico_monitoramento { get; set; }

        [Required]
        public int Quantidade_pessoas { get; set; }

        [Required]
        public Double Media_consumo { get; set; }

        [ForeignKey("Usuario")]
        [Column("Id_usuario")]
        public int Id_usuario { get; set; }
        public UsuarioModel Usuario { get; set; }

        [ForeignKey("TipoEletrodomestico")]
        [Column("Id_eletrodomestico")]
        public int Id_eletrodomestico { get; set; }
        public TipoEletrodomesticoModel TipoEletrodomestico { get; set; }

        [ForeignKey("Endereco")]
        [Column("Id_endereco")]
        public int Id_endereco { get; set; }
        public EnderecoModel Endereco { get; set; }

        [JsonIgnore]
        [InverseProperty("Residencia")]
        public ICollection<ConsumoEnergiaModel> ConsumoEnergia { get; set; }
    }
}
