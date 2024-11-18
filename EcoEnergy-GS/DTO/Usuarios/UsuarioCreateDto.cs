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

        public string senha { get; set; }

        public string telefone { get; set; }

        public int pontos { get; set; }
    }
}
