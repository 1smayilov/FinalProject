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
        public static async Task<IResult> Run(params Task<IResult>[] logics) // İstədiyimiz qədər IResult verə bilirik bunları IResult arrayində tutur
        {
            foreach (var logic in logics)
            {
                var result = await logic;
                if (!result.Success)
                {
                    return result;  // Kurala uymayanı döndürüyoruz ( Göndəririk əsas Metoda )
                }
            }
            return null;
        }
    }
}
