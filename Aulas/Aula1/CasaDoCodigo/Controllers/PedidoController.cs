using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
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
        private readonly IItemPedidoRepository itemPedidoRepository;

        public PedidoController(IProdutoRepository produtoRepository,
                                IPedidoRepository pedidoRepository,
                                IItemPedidoRepository itemPedidoRepository)
        {
            this.produtoRepository = produtoRepository;
            this.pedidoRepository = pedidoRepository;
            this.itemPedidoRepository = itemPedidoRepository;
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
            List<ItemPedido> itens = pedido.Itens;
            CarrinhoViewModel carrinhoViewModel = new CarrinhoViewModel(itens);
            return base.View(carrinhoViewModel);
        }

        //http://localhost:56317/pedido/cadastro
        public IActionResult Cadastro()
        {
            var pedido = pedidoRepository.GetPedido();

            if (pedido ==null)
            {
                return RedirectToAction("Carrossel");
            }
            
            return View(pedido.Cadastro);
        }
        //http://localhost:56317/pedido/resumo
        [HttpPost]
        public IActionResult Resumo( Cadastro cadastro)
        {
            Pedido pedido = pedidoRepository.GetPedido();
            return View(pedido);
        }

        [HttpPost]
        public UpdateQuantidadeResponse UpdateQuantidade([FromBody]ItemPedido itemPedido)
        {
          return   pedidoRepository.UpQuantidade(itemPedido);
        }
    }
}
