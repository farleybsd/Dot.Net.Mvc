using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Produto>().HasKey(p => p.Id);

            //Relacionamento de um para muitos
            modelBuilder.Entity<Pedido>().HasKey(p => p.Id);
            modelBuilder.Entity<Pedido>().HasMany(p => p.Itens) // relacionamento de ida
                                         .WithOne(p => p.Pedido); // relacionamento de volta com um pedido
            //Relacionamento de um para Um
            modelBuilder.Entity<Pedido>().HasOne(p=> p.Cadastro) // relacionamento de ida
                                         .WithOne(p => p.Pedido).IsRequired(); // relacionamento de volta com um pedido
            //Relacionamento ItemPedido
            modelBuilder.Entity<ItemPedido>().HasKey(p => p.Id);
            modelBuilder.Entity<ItemPedido>().HasOne(p => p.Pedido); // relacionamento de ida
            modelBuilder.Entity<ItemPedido>().HasOne(p => p.Produto);

            //Relacionamento Cadastro
            modelBuilder.Entity<Cadastro>().HasKey(p => p.Id);
            modelBuilder.Entity<Cadastro>().HasOne(p => p.Pedido);
        }
    }
}
