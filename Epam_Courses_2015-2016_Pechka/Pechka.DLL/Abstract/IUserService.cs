﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pechka.DLL.ModelsForWEBUI;
using Pechka.DLL.ModelsForWEBUI.DTO;

namespace Pechka.DLL.Abstract
{
   public interface IUserService
    {
        IEnumerable<User> Users { get; }
       bool ValidateUser(string email, string password);
       bool SaveNewUser(RegistrationModel user);
       User GetUser(string email);
       bool ConfirmEmail(string id, string email);
       UserForSettingDTO GetUserForsetting(string email);
       bool SaveNewSettings(UserForSettingDTO model, string email);
       bool SetUserLastScore(int userId, int scoreId);
       User GetUserById(int id);
    }
}
