using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EcoEnergy_GS.Models
{
    [Table("TIPO_ELETRODOMESTICO")]
    public class TipoEletrodomesticoModel
    {
        [Key]
        [Column("ID_ELETRODOMESTICO")]
        public int id_eletrodomestico { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "O dispositivo deve ter no máximo 50 caracteres.")]
        [Column("NOME_ELETRODOMESTICO")]
        public string nome_eletrodomestico { get; set; }

        [Required]
        [Column("QUANTIDADE")]
        public int quantidade { get; set; }

        [JsonIgnore]
        [InverseProperty("TipoEletrodomestico")]
        public ICollection<ResidenciaModel> Residencia { get; set; }
    }
}
