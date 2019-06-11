using FluentValidation;
using Htp.ITnews.Domain.Contracts.ViewModels;

namespace Htp.ITnews.Domain.Contracts.Validators
{
    public class NewsViewModelValidator : AbstractValidator<NewsViewModel>
    {
        public NewsViewModelValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("The 'Title' is required")
                .MinimumLength(10)
                .MaximumLength(255);

            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("The 'Content' is required")
                .MinimumLength(5)
                .MaximumLength(1024);

            RuleFor(x => x.Description)
                .NotEmpty();
        }
    }
}
