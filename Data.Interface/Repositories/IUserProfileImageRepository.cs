using Data.Interface.DataModels;
using Data.Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface.Repositories
{
    public interface IUserProfileImageRepository : IBaseRepository<UserProfileImage>
    {
        void SaveDefaultUserProfileImage(User user);
        void EditProfileImage(ProfileImageData data);
    }
}
