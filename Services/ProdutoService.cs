using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiTools.Context;
using ApiTools.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApiTools.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        public IEnumerable<Produtos> GetAll()
        {
            return _produtoRepository.GetAll();
        }

        public Produtos GetById(int id)
        {
            return _produtoRepository.GetById(id);
        }

        public void Add(Produtos produto)
        {
            _produtoRepository.Add(produto);
        }

        public void Update(Produtos produto)
        {
            _produtoRepository.Update(produto);
        }

        public void Delete(int id)
        {
            _produtoRepository.Delete(id);
        }
    }
}