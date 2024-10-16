using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        // Console.WriteLine(result.Data.ProductName); // "Laptop"

        // T data olmaginin sebebi - bu axı void deyil return eləməlidir nəyisə
        public DataResult(T data, bool success, string message) : base(success,message) // base-dəki constr. success ilə, message-ni yolla 
        {
            Data = data;
        }

        public DataResult(T data, bool success) : base(success) // base-dəki constraktıra  sucess - i yolla
        {
            Data = data;
        }
        public T Data { get; }
    }
}
