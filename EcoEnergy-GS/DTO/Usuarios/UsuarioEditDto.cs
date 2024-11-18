using System.Globalization;

namespace EcoEnergy_GS.DTO.Usuarios
{
    public class UsuarioEditDto
    {
        private string _nome;

        public int id_usuarios { get; set; }

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
