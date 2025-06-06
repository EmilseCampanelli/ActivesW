﻿using APIAUTH.Aplication.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAUTH.Aplication.Valitations
{
    public class UserValidator : AbstractValidator<AccountDto>
    {
        public UserValidator()
        {
            RuleFor(c => c.Email).NotEmpty();
            RuleFor(c => c.Password).MinimumLength(6).NotEmpty();
        }

    }
}
