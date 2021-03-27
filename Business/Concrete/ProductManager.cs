using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
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
        public IResult Add(Product product)
        {
            //business codes
            //validation
            //context ilgili bi thread ı anlatır
            //aşağıdaki şeyler tek satır olsa da kodu kötüleştirir.Bunun için de bir yapı kuracaz.
            //loglama
            //cacheremove
            //performence
            //transaction
            //yetkilendirme
            ValidationTool.Validate(new ProductValidator(), product);
            _productDal.Add(product);
        
                       
            return new SuccessResult(Messages.ProductAdded);
        }
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
