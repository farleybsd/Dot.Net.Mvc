using CasaDoCodigo.Models;
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
        private readonly IPedidoRepository pedidoRepository;

        public PedidoController(IProdutoRepository produtoRepository,
                                IPedidoRepository pedidoRepository)
        {
            this.produtoRepository = produtoRepository;
            this.pedidoRepository = pedidoRepository;
        }

        //http://localhost:56317/pedido/carrossel
        public IActionResult Carrossel()
        {
            return View(produtoRepository.GetProdutos());
        }

        //http://localhost:56317/pedido/carrinho
        public IActionResult Carrinho(string codigo)
        {
            if (!string.IsNullOrEmpty(codigo))
            {
                pedidoRepository.AddItem(codigo);
            }
            Pedido pedido = pedidoRepository.GetPedido();
            return View(pedido.Itens);
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
