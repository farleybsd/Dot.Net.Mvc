using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface IPedidoRepository
    {
        Pedido GetPedido();
        void AddItem(string codigo);
        UpdateQuantidadeResponse UpQuantidade(ItemPedido itemPedido);
    }
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IItemPedidoRepository itemPedidoRepository;
        public PedidoRepository(ApplicationContext contexto,
                                IHttpContextAccessor contextAccessor,
                                IItemPedidoRepository itemPedidoRepository) : base(contexto)
        {
            this.contextAccessor = contextAccessor;
            this.itemPedidoRepository = itemPedidoRepository;
        }

        public void AddItem(string codigo)
        {
            var produto = contexto.Set<Produto>()
                           .Where(p => p.Codigo == codigo)
                           .FirstOrDefault();

            if (produto ==null)
            {
                throw new ArgumentException("Produto Não Encontrado");
            }

            var pedido = GetPedido();

            var itemPedido = contexto.Set<ItemPedido>()
                                     .Where(i => i.Produto.Codigo == codigo
                                            && i.Pedido.Id == pedido.Id)
                                     .SingleOrDefault();

            if (itemPedido == null)
            {
                itemPedido = new ItemPedido(pedido,produto,1,produto.Preco);
                contexto.Set<ItemPedido>()
                        .Add(itemPedido);
                contexto.SaveChanges();
            }
        }

        public Pedido GetPedido()
        {
            var pedidoId = GetPedidoId();
            var pedido = dbSets
                .Include(p=> p.Itens)
                .ThenInclude(i => i.Produto)
                .Where(p => p.Id == pedidoId)
                .SingleOrDefault();

            if (pedido == null)
            {
                pedido = new Pedido();
                dbSets.Add(pedido);
                contexto.SaveChanges();
                SetPeidoId(pedido.Id); 
            }

            return pedido;
        }

        private int? GetPedidoId()
        {
           return  contextAccessor.HttpContext.Session.GetInt32("pedidoId");
        }
        private void SetPeidoId(int pedidoId)
        {
            contextAccessor.HttpContext.Session.SetInt32("pedidoId", pedidoId);
        }

        public UpdateQuantidadeResponse UpQuantidade(ItemPedido itemPedido)
        {
            var itemPedidoDb = itemPedidoRepository.GetItemPedido(itemPedido.Id);
            if (itemPedidoDb != null)
            {
                itemPedidoDb.AtualizaQuantidade(itemPedido.Quantidade);

                if (itemPedido.Quantidade == 0)
                {
                    itemPedidoRepository.RemoveItemPedido(itemPedido.Id);
                }

                contexto.SaveChanges();

                var carrinhoViewModel = new CarrinhoViewModel(GetPedido().Itens);

                return new UpdateQuantidadeResponse(itemPedidoDb,carrinhoViewModel);
            }

            throw new ArgumentException("Item Pedido Nao Encontrado");
        }
    }
}
