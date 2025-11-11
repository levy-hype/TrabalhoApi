using System.ComponentModel.DataAnnotations;

namespace Trabalhoestoque.Models
{
    public class Produto
    {
        [Key] // Indica que é a Chave Primária
        public int Id { get; set; }

        [Required] // Campo obrigatório
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public decimal Preco
        {
            get; set;
        }
    }
}
