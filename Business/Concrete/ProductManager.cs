using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Business;
//web api ios,flutter,angular,androit,react vb için kodu anlaması için kullanılır
//Restful denilen ve json formatta çalışan bir standart kullancaz.
namespace Business.Concrete
{
    public class ProductManager : IProductService
    { 
        IProductDal _productDal;
        ICategoryService _categoryService;
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }
        [ValidationAspect(typeof(ProductValidator))]
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
            //ValidationTool.Validate(new ProductValidator(), product);//merkezi yapıya aspect e aldık bunu
            //yukardakini core da yaptık ama 1 satır da olsa kötü duruyo,atribute olarak versek daha iyi
            //cross cutting concerns
            //loglama,cache,transaction,authorization,validation
            //core katmanında validate yap,bu da bunlara dahil
           IResult result= BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId),
             CheckIfProductNameExists(product.ProductName),
             CheckIfCategoryLimitExceted());
            if(result!=null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);             
        }
        private IResult CheckIfProductNameExists(string name)
        {
            if (_productDal.GetAll(p => p.ProductName == name).Any())
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);

            }
            return new SuccessResult();
        }
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            if (_productDal.GetAll(p => p.CategoryId ==categoryId).Count >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);

            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceted()
        {
            if (_categoryService.GetAll().Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);

            }
            return new SuccessResult();
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
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
