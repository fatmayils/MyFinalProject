using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
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
            //sadece yukardakini yazarsak çalışmaz program,program cs e git tanıt kendini
            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();
            //en en en son,aspect ten sonra bunu yazman lazım,intercepter ler aşağıdaki kodla devreye giriyor
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();//çalışan uygulama içinde
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()//implemente edilmiş interface leri bul
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()//onlara bu şeyi çağır ****
                }).SingleInstance();
        }
    }
}
//registerşerden sonra ilk yıldız çalışçak,intercepter var mı diye bakacak