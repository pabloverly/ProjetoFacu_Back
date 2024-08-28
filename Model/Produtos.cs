using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTools.Model
{
    public class Produtos
    {
        public int cd_prouto { get; set; }           // Código do produto
        public string ds_produto { get; set; }       // Descrição do produto
        public float vl_produto { get; set; } = 0;   // Valor do produto
        public int sn_ativo { get; set; } = 1;       // Indica se o produto está ativo
    }
}