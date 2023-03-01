using FluentValidation;
using Meetup.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meetup.BusinessLogic.Accounts.Validations
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            var msg = "Error in field {PropertyName}: value {PropertyValue}";
            RuleFor(c => c.Name)
            .Must(c => c.All(Char.IsLetter)).WithMessage(msg);

            RuleFor(c => c.Email)
                .NotNull().WithMessage(msg)
                .EmailAddress();


            RuleFor(c => c.Password)
               .NotNull().WithMessage(msg)
               .MinimumLength(6).WithMessage("The length must be greater than {MinLength}. Current length {TotalLength}")
               .MaximumLength(15).WithMessage("The length must be less than {MaxLength}. Current length {TotalLength}");

            RuleFor(c => c.PhoneNumber)
                .Must(IsPhoneValid).WithMessage(msg)
                .Length(13).WithMessage("The length must be between {MinLength} and {MaxLength}. Current length: {TotalLength}");
        }

        private bool IsPhoneValid(string phone)
        {
            return !(!phone.StartsWith("+375")
                || !phone.Substring(1).All(c => Char.IsDigit(c)));
        }
      
     }
    }
