using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web
{
    public class Relatorio : IRelatorio
    {
        private readonly ICatalogo catalogo;

        public Relatorio(ICatalogo _catalogo)
        {
            catalogo = _catalogo;
        }

        public async Task Imprimir(HttpContext context)
        {
            foreach (var item in catalogo.GetLivros())
            {
                await context.Response.WriteAsync($"Codido do livro: {item.Codigo,-10},Nome Livro: {item.Nome,-40},valor: {item.Preco.ToString("C"),10}\r\n");
            }
        }

    }
}
