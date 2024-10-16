using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal) 
        {
            _userDal = userDal;
        }

        public List<OperationClaim> GetClaims(User user) // Verilmiş istifadəçi üçün rolunu (OperationClaim) əldə etmək. 
        {
            return _userDal.GetClaims(user); 
        }

        public void Add(User user) //  Yeni bir istifadəçi əlavə etmək.
        {
            _userDal.Add(user);
        }

        public User GetByMail(string email) // Verilmiş e-poçt ünvanına görə istifadəçini əldə etmək.
        {
            return _userDal.Get(u => u.Email == email);
        }
    }
}
