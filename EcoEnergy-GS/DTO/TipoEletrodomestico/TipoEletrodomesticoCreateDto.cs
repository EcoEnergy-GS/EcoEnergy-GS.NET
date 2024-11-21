using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoEnergy_GS.DTO.TipoEletrodomestico
{
    public class TipoEletrodomesticoCreateDto
    {
        [Required]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "O dispositivo deve ter no máximo 50 caracteres.")]
        [Column("NOME_ELETRODOMESTICO")]
        public string nome_eletrodomestico { get; set; }

        [Required]
        [Column("QUANTIDADE")]
        public int quantidade { get; set; }
    }
}
