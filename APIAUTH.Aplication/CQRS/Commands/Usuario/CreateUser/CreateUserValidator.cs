﻿using FluentValidation;

namespace APIAUTH.Aplication.CQRS.Commands.Usuario.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Domicilio).SetValidator(new CreateDomicilioValidator());
        }
    }

    public class CreateDomicilioValidator : AbstractValidator<CreateDomicilioCommand>
    {
        public CreateDomicilioValidator()
        {
            RuleFor(x => x.Calle).NotEmpty();
            RuleFor(x => x.CodigoPostal).NotEmpty();
        }
    }
}
