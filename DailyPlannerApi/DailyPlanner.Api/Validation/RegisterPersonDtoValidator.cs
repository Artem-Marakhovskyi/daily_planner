using DailyPlanner.Api.ViewDtos;
using FluentValidation;

namespace DailyPlanner.Api.Validation
{
    public class RegisterPersonDtoValidator : AbstractValidator<PersonRegisterDto>
    {
        public RegisterPersonDtoValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(p => p.FirstName).MinimumLength(3);

            RuleFor(p => p.LastName).MinimumLength(3);

            RuleFor(p => p.Password)
                .Must(password => password.Length > 4);

            RuleFor(p => p.ConfirmPassword)
                .Equal(p => p.Password);
        }
    }
}
