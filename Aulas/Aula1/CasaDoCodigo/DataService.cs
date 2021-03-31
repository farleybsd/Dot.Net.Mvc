using CasaDoCodigo.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CasaDoCodigo
{
    class DataService : IDataService
    {
        private readonly ApplicationContext contexto;

        public DataService(ApplicationContext contexto)
        {
            this.contexto = contexto;
        }

        public void InicializaDb()
        {
            contexto.Database.EnsureCreated(); //Database.Migrate();

           var json = File.ReadAllText("livros.json");
           var livros = JsonConvert.DeserializeObject<List<Livro>>(json);

            foreach (var livro in livros)
            {
                //Preenchendo Modelo para insert
                contexto.Set<Produto>().Add(new Produto(livro.Codigo,livro.Nome,livro.Preco));
            }

            // realizando insert
            contexto.SaveChanges();

        }
    }
    class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
    }
}

