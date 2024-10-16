using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        // Console.WriteLine(result.Data.ProductName); // "Laptop"
        public SuccessDataResult(T data, string message) : base(data, true, message) // DataResult daki 2 parametrli constructora gedir
        {

        }

        public SuccessDataResult(T data) : base(data,true) // DataResult daki 1 parametrli constructora gedir
        {
            
        }

        public SuccessDataResult(string message) : base(default,true,message) // DataResult daki 2 parametrli constructora gedir
        {

        }

        public SuccessDataResult() : base(default,true) // DataResult daki 1 parametrli constructora gedir
        {
            
        }


    }
}
