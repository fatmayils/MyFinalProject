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
            //Postsharp:ücretli ama ücretsiz olarak yapýlan bazý þeyleri var,autofack otomatik yapýlandýrma yapýyo
            //startapta yaptýðýmýz þeyi backendde yapýyo
            //baþka bir api eklersek patlarýz
            //bu yüzden outofac yapýlandýrmasý kullancaz...
            services.AddControllers();
            //services.AddSingleton<IProductService,ProductManager>();//bana arka planda bir referans oluþtur
            //services.AddSingleton<IProductDal, EfProductDal>();//bana arka planda bir referans oluþtur
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
