using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.Json.Serialization;

namespace EcoEnergy_GS.Models
{
    [Table("Usuario")]
    public class UsuarioModel
    {
        private string _nome;

        [Key]
        public int Id_usuario { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "O nome pode ter no máximo 50 caracteres.")]
        public string Nome
        {
            get => _nome;
            set => _nome = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value?.ToLower());
        }

        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 20 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caractere especial.")]
        [JsonIgnore]
        public string Senha { get; set; }

        public string Telefone { get; set; }

        [Required]
        public int Pontos { get; set; }

        [InverseProperty("Usuario")]
        public ICollection<HistoricoPontosModel> HistoricoPontos { get; set; }

        [InverseProperty("Usuario")]
        public ICollection<ResidenciaModel> Residencia { get; set; }

        [InverseProperty("Usuario")]
        public ICollection<TrocasRecompensasModel> Trocas { get; set; }
    }
}