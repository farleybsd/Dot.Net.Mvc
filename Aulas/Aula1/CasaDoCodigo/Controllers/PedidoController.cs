using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IProdutoRepository produtoRepository;

        public PedidoController(IProdutoRepository produtoRepository)
        {
            this.produtoRepository = produtoRepository;
        }

        //http://localhost:56317/pedido/carrossel
        public IActionResult Carrossel()
        {
            return View(produtoRepository.GetProdutos());
        }

        //http://localhost:56317/pedido/carrinho
        public IActionResult Carrinho()
        {
            return View();
        }

        //http://localhost:56317/pedido/cadastro
        public IActionResult Cadastro()
        {
            return View();
        }
        //http://localhost:56317/pedido/resumo
        public IActionResult Resumo()
        {
            return View();
        }
    }
}
