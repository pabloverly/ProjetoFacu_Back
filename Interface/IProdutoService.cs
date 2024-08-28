using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTools.Model
{
    public interface IProdutoService
    {
        IEnumerable<Produtos> GetAll();
        Produtos GetById(int id);
        void Add(Produtos produto);
        void Update(Produtos produto);
        void Delete(int id);
    }
}