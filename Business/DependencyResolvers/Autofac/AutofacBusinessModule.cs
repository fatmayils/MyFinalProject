using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;
//autofac bize aop desteği de sağlıyor
namespace Business.DependencyResolvers.Autofac
{
    //web api startup daki şeyleri yapmamak için
    public class AutofacBusinessModule:Module//artık autofac modülüsün
    {
        //load:yükleme,uygulama yayınlandığında ayağa kaltığında çalışacak
        protected override void Load(ContainerBuilder builder)
        {
            //addsingleton a karşılık geliyor,sondaki tek bir instance üretiyo
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
        }
    }
}
