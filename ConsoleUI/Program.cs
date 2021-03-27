using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        //SOLİD O harfi
        //Open closed principle
        //yaptığın yazılıma yeni bir özellik ekliyorsan mevcuttaki koduna dokunmuyorsun

        static void Main(string[] args)
        {
             ProductManager productManager = new ProductManager(new EfProductDal());
            /*   foreach (var product in productManager.GetAll().Data)
              {
                  Console.WriteLine(product.ProductName);
              }
              Console.WriteLine("***********************************************");
              foreach (var product in productManager.GetProductDetailDtos().Data)
              {
                  Console.WriteLine(product.ProductName+"   /  "+product.CategoryName);
              }
              Console.WriteLine("***********************************************");
              foreach (var product in productManager.GetAllByCategoryId(2).Data)
              {
                  Console.WriteLine(product.ProductName);
              }
              Console.WriteLine("************************************************");
              foreach (var product in productManager.GetByUnitPrice(40, 100).Data)
              {
                  Console.WriteLine(product.ProductName);
              }
              Console.WriteLine("*****************************************************");
              //IOC ile bi şii yapçaz,newlememek için
              CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
              foreach (var category in categoryManager.GetAll())
              {
                  Console.WriteLine(category.CategoryName);
              }
              */

            var result = productManager.GetProductDetailDtos();
            if(result.Success==true)
            {
                foreach (var item in result.Data)
                {
                    Console.WriteLine(item.ProductName+"/"+item.CategoryName);
                }
               
            }
            else
            {
                Console.WriteLine(  result.Message);
            }
        }
    }
}
