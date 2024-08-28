using ApiTools.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTools.Model
{
    public class ProdutoRepository : IProdutoRepository
    {

        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Produtos> GetAll()
        {
            return _context.Produtos.ToList();
        }

        public Produtos GetById(int id)
        {
            return _context.Produtos.FirstOrDefault(p => p.Id == id);
        }
        public IEnumerable<dynamic> GetProdutosWithVariations()
        {
            var query = from p in _context.Produtos
                        join v in _context.ProdutoVariations
                        on p.Id equals v.Product_id into variations
                        select new
                        {
                            p.Id,
                            p.Image,
                            p.Name,
                            p.Description,
                            p.Price,
                            p.Quantity,
                            p.Amount,
                            Variation = variations.Select(v => new { v.Name }).ToList()
                        };

            return query.ToList();
        }

        public void Add(Produtos produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
        }

        public void Update(Produtos produto)
        {
            _context.Produtos.Update(produto);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.Id == id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                _context.SaveChanges();
            }
        }
    }
}