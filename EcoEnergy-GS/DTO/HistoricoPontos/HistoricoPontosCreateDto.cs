using EcoEnergy_GS.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcoEnergy_GS.DTO.HistoricoPontos
{
    public class HistoricoPontosCreateDto
    {
        [Column("DATA_HISTORICO")]
        public DateTime data_historico { get; set; }
        [Column("QUANTIDADE")]
        public int quantidade { get; set; }

        [ForeignKey("Usuario")]
        [Column("ID_USUARIOS")]
        public int id_usuarios { get; set; }
    }
}
