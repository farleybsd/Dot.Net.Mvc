using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Injencao de depedencia de uma Interface para uma classe concreta
            //services.AddTransient<ICatalogo,Catalogo>(); // servico temporario
            // Injencao de depedencia de uma Interface para uma classe concreta
            // services.AddTransient<IRelatorio,Relatorio>(); // servico temporario

            // Injencao de depedencia de uma Interface para uma classe concreta
            //services.AddScoped<ICatalogo, Catalogo>(); // evitar que cada requisao gere um novo objeto
            //services.AddScoped<IRelatorio, Relatorio>();//evitar que cada requisao gere um novo objeto

            // Injencao de depedencia de uma Interface para uma classe concreta
            var catalogo = new Catalogo();
            services.AddSingleton<ICatalogo>(catalogo); // instancia unica ao longo de toda aplicacao
            services.AddSingleton<IRelatorio>(new Relatorio(catalogo));//instancia unica ao longo de toda aplicacao
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ICatalogo catalogo = serviceProvider.GetService<ICatalogo>();
            IRelatorio relatorio = serviceProvider.GetService<IRelatorio>();

            app.Run(async (context) =>
            {
               await relatorio.Imprimir(context);
            });

            
        }
    }
}
