using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics) // İstədiyimiz qədər IResult verə bilirik bunları IResult arrayində tutur
        {
            foreach (var logic in logics)
            {
                if (!logic.Success)
                {
                    return logic;  // Kurala uymayanı döndürüyoruz
                }
            }
            return null;
        }
    }
}
