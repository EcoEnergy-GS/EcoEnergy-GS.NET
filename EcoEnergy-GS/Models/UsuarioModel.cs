using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.Json.Serialization;

namespace EcoEnergy_GS.Models
{
    [Table("USUARIOS")]
    public class UsuarioModel
    {
        private string _nome;

        [Key]
        [Column("ID_USUARIOS")]
        public int id_usuarios { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "O nome pode ter no máximo 50 caracteres.")]
        [Column("NOME")]
        public string nome
        {
            get => _nome;
            set => _nome = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value?.ToLower());
        }

        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 20 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, uma letra minúscula, um número e um caractere especial.")]
        //[JsonIgnore]
        [Column("SENHA")]
        public string senha { get; set; }

        [Column("TELEFONE")]
        public string telefone { get; set; }

        [Required]
        [Column("PONTOS")]
        public int pontos { get; set; }

        [InverseProperty("Usuario")]
        public ICollection<HistoricoPontosModel> HistoricoPontos { get; set; }

        [InverseProperty("Usuario")]
        public ICollection<ResidenciaModel> Residencia { get; set; }

        [InverseProperty("Usuario")]
        public ICollection<TrocasRecompensasModel> Trocas { get; set; }
    }
}