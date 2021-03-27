using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //AOP
            //Autofac,Ninject,CastleWindsor,StructureMap,LightInject,DryInject--->IoC Container
            //AOP
            //Postsharp:�cretli ama �cretsiz olarak yap�lan baz� �eyleri var,autofack otomatik yap�land�rma yap�yo
            //startapta yapt���m�z �eyi backendde yap�yo
            //ba�ka bir api eklersek patlar�z
            //bu y�zden outofack yap�land�rmas� kullancaz...
            services.AddControllers();
            //services.AddSingleton<IProductService,ProductManager>();//bana arka planda bir referans olu�tur
            //services.AddSingleton<IProductDal, EfProductDal>();//bana arka planda bir referans olu�tur
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
