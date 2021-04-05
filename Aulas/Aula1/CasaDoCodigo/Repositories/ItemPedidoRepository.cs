using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface IItemPedidoRepository
    {
        void UpQuantidade(ItemPedido itemPedido);
    }
    public class ItemPedidoRepository : BaseRepository<ItemPedido>, IItemPedidoRepository
    {
        public ItemPedidoRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public void UpQuantidade(ItemPedido itemPedido)
        {
          var itemPedidoDb=  dbSets
                                .Where(ip => ip.Id == itemPedido.Id)
                                .FirstOrDefault();
            if (itemPedidoDb != null)
            {
                itemPedidoDb.AtualizaQuantidade(itemPedido.Quantidade);
                contexto.SaveChanges();
            }
        }
    }
}
