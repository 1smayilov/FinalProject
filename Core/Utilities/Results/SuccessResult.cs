using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        // base(true, message) ifadesi ile Result sınıfının bool success,
        // string message parametrelerini alan constructor'ını çağırır.
        public SuccessResult(string message) : base(true,message) // base-dəki 2 parametrli constructora göndəririk
        {

        }

        public SuccessResult() : base(true) // base dəki bir parametre olan constructora yollayır
        {

        }
    }
}
