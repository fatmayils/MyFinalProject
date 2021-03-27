using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
//web api ios,flutter,angular,androit,react vb için kodu anlaması için kullanılır
//Restful denilen ve json formatta çalışan bir standart kullancaz.
namespace Business.Concrete
{
    public class ProductManager : IProductService
    { 
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        /*  public void Add(Product product)
          {
              //business codes
              //ekleme şartları diyebiliriz 
              _productDal.Add(product);
          }
        */
       //IproductService de void değiştiği için aşağıdaki gibi oldu
        public IResult Add(Product product)
        {
            if(product.ProductName.Length<2)
            {
                //magic strings
                //return new ErrorResult("ürün ismi en az 2 karakter olmalıdır");
                return new ErrorResult(Messages.ProductNameInvalid);
            }
            _productDal.Add(product);
            //return kısmını yazmassak add den hata yiyoruz:d
            //return new Result() tu ama constructor ekleyerek aşağıdaki gibi paremetre yazabiliyoruz.
            //return new Result(true, "Ürün eklendi");
            //mesaj göndermek istemeyebilir vs vs 
            return new SuccessResult(Messages.ProductAdded);
        }

        /* public List<Product> GetAll()
         {
             //iş kodları
             //yetkisi var mı?
             return _productDal.GetAll();
         }
        */
         public IDataResult<List<Product>> GetAll()
       {
           //iş kodları
           //yetkisi var mı?
           if(DateTime.Now.Hour==222)
            {
                return new ErrorDataResult<List<Product>>(Messages.ProductMaintenanceTime);
            }
           return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductListed);
       }
     
        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id),"kategoriler listelendi");
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice <= max && p.UnitPrice >= min),"");
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetailDtos()
        {
            if (DateTime.Now.Hour == 3)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.ProductMaintenanceTime);
            }
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails(),"");
        }
    }
}
