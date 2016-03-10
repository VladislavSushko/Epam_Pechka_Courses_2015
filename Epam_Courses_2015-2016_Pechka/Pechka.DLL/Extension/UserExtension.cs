using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pechka.DLL.Models;
using Pechka.DLL.ModelsForWEBUI;
using Pechka.DLL.ModelsForWEBUI.DTO;
using Pechka.DLL.ModelsForWEBUI.DTO.Admin;

namespace Pechka.DLL.Extends
{
    static class UserExtends
    {
       
        public static User ToUser(this RegistrationModel model)
        {
            User simpleUser=new User();
            simpleUser.Email = model.Email;
            simpleUser.FirstName = model.UserFirstName;
            simpleUser.LastName = model.UserLastName;
            simpleUser.RoleId = 1;
            simpleUser.ConfirmedEmail = false;
            simpleUser.InBlackList = false;
            simpleUser.Password = model.Password;
            simpleUser.ImgType = model.ImgType;
            simpleUser.ImgData = model.ImgData;
            simpleUser.ScoreId = null;
            return simpleUser;

        }

        public static UserForSettingDTO ToUserForSetting(this User model)
        {
            var castedUser=new UserForSettingDTO();
            castedUser.Email = model.Email;
            castedUser.FirstName = model.FirstName;
            castedUser.LastName = model.LastName;
            castedUser.Password = string.Empty;
            castedUser.NewPassword = string.Empty;
            castedUser.ImgType = model.ImgType;
            return castedUser;
        }

        public static User ToUser(this UserForSettingDTO model,User user)
        { 
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Password = model.NewPassword;
            if (model.ImgData != null && model.ImgType != null)
            {
                user.ImgData = model.ImgData;
                user.ImgType = model.ImgType;
            }
            return user;

        }

        public static UserDto ToUserDTO(this User model)
        {
            var castedUser=new UserDto();
            castedUser.Id = model.Id;
            castedUser.FirstName = model.FirstName;
            castedUser.LastName = model.LastName;
            castedUser.Email = model.Email;
            castedUser.ConfirmedEmail = model.ConfirmedEmail ? 1 : 0;
            castedUser.InBlackList = model.InBlackList ? 1 : 0;
            castedUser.RoleId = model.RoleId;
            castedUser.Money = model.LastScore.Money;
            return castedUser;
        }
    }
}
