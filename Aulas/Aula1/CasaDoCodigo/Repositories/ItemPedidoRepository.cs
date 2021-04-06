using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface IItemPedidoRepository
    {
        ItemPedido GetItemPedido(int iTemPedidoId);
        void RemoveItemPedido(int iTemPedidoId);
    }
    public class ItemPedidoRepository : BaseRepository<ItemPedido>, IItemPedidoRepository
    {
        public ItemPedidoRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public ItemPedido GetItemPedido(int iTemPedidoId)
        {
                 return      dbSets
                                  .Where(ip => ip.Id == iTemPedidoId)
                                  .FirstOrDefault();
        }

        public void RemoveItemPedido(int iTemPedidoId)
        {
            dbSets.Remove(GetItemPedido(iTemPedidoId));
           
        }
    }
}
