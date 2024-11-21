using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoEnergy_GS.DTO.Endereco
{
    public class EnderecoCreateDto
    {
        [Required]
        [Column("CEP")]
        public string cep { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Nome da rua deve ter no máximo 50 caracteres.")]
        [Column("RUA")]
        public string rua { get; set; }

        [Required]
        [Column("NUMERO")]
        public int numero { get; set; }

        [StringLength(50, MinimumLength = 8, ErrorMessage = "O complemento deve ter no máximo 50 caracteres.")]
        [Column("COMPLEMENTO")]
        public string complemento { get; set; }
    }
}
