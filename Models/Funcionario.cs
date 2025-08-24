using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace sistema_de_rh_API.Models
{
    public class Funcionario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Nome cannot be longer than 100 characters.")]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(200, ErrorMessage = "Endereco cannot be longer than 200 characters.")]
        public string Endereco { get; set; } = string.Empty;

        [Required]
        [StringLength(20, ErrorMessage = "Ramal cannot be longer than 20 characters.")]
        public string Ramal { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string EmailProfissional { get; set; } = string.Empty;

        [Required]
        [StringLength(50, ErrorMessage = "Departamento cannot be longer than 50 characters.")]
        public string Departamento { get; set; } = string.Empty;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Salario must be a positive value.")]
        [Precision(18, 2)]
        public decimal Salario { get; set; }

        [Required]
        public DateTime DataAdmissao { get; set; }
    }
}