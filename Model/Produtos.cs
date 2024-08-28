using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTools.Model
{
    public class Produtos
    {
        public int Id { get; set; } // Identificador único do produto
        public string Image { get; set; } // URL da imagem do produto
        public string Name { get; set; } // Nome do produto
        public string Description { get; set; } // Descrição do produto
        public decimal Price { get; set; } // Preço do produto
        public int Quantity { get; set; } // Quantidade em estoque
        public decimal Amount { get; set; } // Quantidade total (ou outro valor associado)

        public ICollection<ProdutoVariations> Variations { get; set; } // Navegação para variações



    }
}