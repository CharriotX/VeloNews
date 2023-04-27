using Data.Interface.DataModels.UserDataModels;
using Data.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetUserByNameAndPass(string name, string pass);
        bool IsUserExist(string userName);
        void UserRegistration(UserRegistrationData data);
        User GetUserByUserName(string userName);
        bool IsUserNameExist(string userName);
    }
}
