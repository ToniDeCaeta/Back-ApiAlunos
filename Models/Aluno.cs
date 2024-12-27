using System;
using System.ComponentModel.DataAnnotations;

namespace AlunosApi.Models
{
    public class Aluno
    {
        [Key]
        public int Id { get; set; }


        [Required]// Equivalente a um NotNull
        [StringLength(80, ErrorMessage = "Tamanho Maior que o permitido")]
        public string Nome { get; set; }


        [Required]
        [StringLength(100)]
        [EmailAddress] //Verifica o formato do Email
        public string Email { get; set; }


        
        [Required]
        public int Idade { get; set; }
    }
}
