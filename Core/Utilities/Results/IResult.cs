using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //Soyutlama ve constructor yönetimi
    //Temel voidler için başlangıç
    public interface IResult
    {
        //Sadece okunabilir
        bool Success { get; }
        string Message { get; }


    }
}
