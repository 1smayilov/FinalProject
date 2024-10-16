using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    // Temel voidler ichin bashlangic
    public interface IResult
    {
        bool Success {  get; } // Sadəcə oxunabilər
        string Message { get; }
    }
}
