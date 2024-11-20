using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json.Serialization;

namespace EcoEnergy_GS.DTO.Usuarios
{
    public class UsuarioCreateDto
    {
        private string _nome;

        public string nome
        {
            get => _nome;
            set => _nome = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value?.ToLower());
        }

        [StringLength(20, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 20 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caractere especial.")]
        public string senha { get; set; }

        public string telefone { get; set; }

        public int pontos { get; set; }
    }
}
