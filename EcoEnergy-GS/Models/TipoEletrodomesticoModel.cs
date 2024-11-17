using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoEnergy_GS.Models
{
    [Table("Tipo_Eletrodomestico")]
    public class TipoEletrodomesticoModel
    {
        [Key]
        public int Id_eletrodomestico { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "O dispositivo deve ter no máximo 50 caracteres.")]
        public string Nome { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [InverseProperty("TipoEletrodomestico")]
        public ICollection<ResidenciaModel> Residencia { get; set; }
    }
}
