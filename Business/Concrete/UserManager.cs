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

        public async Task<List<OperationClaim>> GetClaimsAsync(User user) // Verilmiş istifadəçi üçün rolunu (OperationClaim) əldə etmək. 
        {
            return await _userDal.GetClaimsAsync(user); 
        }

        public async Task AddAsync(User user) //  Yeni bir istifadəçi əlavə etmək.
        {
            await _userDal.AddAsync(user);
        }

        public async Task<User> GetByMailAsync(string email) // Verilmiş e-poçt ünvanına görə istifadəçini əldə etmək.
        {
            return await _userDal.GetAsync(u => u.Email == email);
        }
    }
}
