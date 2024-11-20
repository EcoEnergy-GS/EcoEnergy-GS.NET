﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcoEnergy_GS.DTO.Recompensas
{
    public class RecompensasCreateDto
    {

        [StringLength(100, MinimumLength = 8, ErrorMessage = "A descrição deve ter no máximo 100 caracteres.")]
        [Column("DESCRICAO")]
        public string descricao { get; set; }

        [Column("PONTOS_NECESSARIOS")]
        public int pontos_necessarios { get; set; }
    }
}