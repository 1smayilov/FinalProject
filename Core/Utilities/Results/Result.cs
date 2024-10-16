using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        // kosntraktır yaradırıq ki o biri tərəfdə bu dəyərlərdən istifadə edək

        // this(success) ifadesi, Result sınıfının birinci constructor'ı çalıştırıldığında,
        // ikinci constructor'ı da çağırır ve success parametresini ona iletir.Bu sayede,
        // success değeri iki defa set edilmek zorunda kalınmaz, kod tekrarı önlenir ve daha temiz bir yapı oluşturulur.

        public Result(bool success, string message) : this(success)        
        {
             Message = message;
             
        } // get readonly-dir get ler constructor da set oluna bilər
        public Result(bool success) 
        {
            Success = success;
        } // Ancaq success görmək üçün bu konstraktırdan istifadə edəciyik

        public bool Success { get; } 

        public string Message { get; }
    }
}
