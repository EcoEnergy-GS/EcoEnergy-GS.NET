using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcoEnergy_GS.DTO.TipoEletrodomestico
{
    public class TipoEletrodomesticoEditDto
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
    }
}
