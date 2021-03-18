using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    //generic constraint
    //class:referans tip demektir
    //IEntity:ama herhangi bir referans tip vermemek için  IEntity veya bunun türevlerini verdik
    //new() new lenebilir olmalı demek
    //artık sistemimiz veri tabanı nesneleriyle çalışan bir repository oldu
    public interface IEntityRepository<T>where T:class,IEntity,new()
    {
        List<T> GetAll(Expression<Func<T,bool>> filter=null);//ürünleri ...ya göre filtrele yapmak için kullanıyoruz.
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
       // List<T> GetAllByCategory(int categoryId);
       //Delegeler sayesinde bu koda ihtiyacımız yok
    }
}
