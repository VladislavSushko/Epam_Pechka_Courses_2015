﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pechka.DLL.ModelsForWEBUI;

namespace Pechka.DLL.Abstract
{
    public interface IEmailService
    {
        bool ConfirmEmailSend(User model, string body);
    }
}
