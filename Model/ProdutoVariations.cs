using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTools.Model
{
    public class ProdutoVariations
    {
        public int Id { get; set; } // ID da variação do produto
        public int Product_id { get; set; } // ID do produto ao qual a variação pertence (chave estrangeira)
        public string Name { get; set; } // Nome da variação

        public Produtos Produto { get; set; }

    }
}