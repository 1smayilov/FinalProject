using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims); // Istifadəçi və yetkilər
    }

    // Mən bir user bilgiləri və rol göndərəcəm o da bu bilgilərə görə Token yaradacaq

    // Adam sistemə login parolla giriş eliyəcək, gəldi Api yə, əgər doğrudursa burada CreateToken işləyəcək 
    // User üçün database ə gedəcək bu istifadəçinin claimlərini tapacaq, orada bir JWT (Token) yaradacaq,
    // və bunları istifadəçiyə qaytaracaq
    
}
