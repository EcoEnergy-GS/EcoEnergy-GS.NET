using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoEnergy_GS.Models
{
    [Table("Endereco")]
    public class EnderecoModel
    {
        [Key]
        public int Id_endereco { get; set; }

        [Required]
        public string Cep { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Nome da rua deve ter no máximo 50 caracteres.")]
        public string Rua { get; set; }

        [Required]
        public int Numero { get; set; }

        [StringLength(50, MinimumLength = 8, ErrorMessage = "O complemento deve ter no máximo 50 caracteres.")]
        public string Complemento { get; set; }

        [InverseProperty("Endereco")]
        public ICollection<ResidenciaModel> Residencia { get; set; }
    }
}
