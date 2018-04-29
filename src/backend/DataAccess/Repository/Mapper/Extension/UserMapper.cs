using Domain.Core;
using DTO.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper.Extension
{
    public static class UserMapper
    {
        public static LoginResult LoginMap(this User user)
        {
            if (user == null)
                return null;

            var item = new LoginResult();
            item.Id = user.Id;
            item.CompanyId = user.CompanyId;
            item.AttachmentId = user.AttachmentId;
            item.Email = user.Email;
            item.FirstName = user.FirstName;
            item.LastName = user.LastName;
            item.Title = user.Title;
            item.TimeZone = user.TimeZone;

            return item;
        }

        public static UserInfo UserInfoMap(this User user)
        {
            if (user == null)
                return null;

            var item = new UserInfo();
            item.Id = user.Id;
            item.Status = user.Status;
            item.Email = user.Email;

            return item;
        }
    }
}
