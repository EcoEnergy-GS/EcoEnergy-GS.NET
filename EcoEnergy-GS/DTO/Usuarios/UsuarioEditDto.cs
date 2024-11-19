using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace EcoEnergy_GS.DTO.Usuarios
{
    public class UsuarioEditDto
    {
        private string _nome;

        [Key]
        public int id_usuarios { get; set; }

        [Required]
        public string nome
        {
            get => _nome;
            set => _nome = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value?.ToLower());
        }

        [Required]
        public string senha { get; set; }

        public string telefone { get; set; }

        [Required]
        public int pontos { get; set; }
    }
}
