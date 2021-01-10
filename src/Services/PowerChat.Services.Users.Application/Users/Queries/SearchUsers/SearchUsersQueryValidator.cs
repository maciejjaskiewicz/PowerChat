using FluentValidation;

namespace PowerChat.Services.Users.Application.Users.Queries.SearchUsers
{
    public class SearchUsersQueryValidator : AbstractValidator<SearchUsersQuery>
    {
        public SearchUsersQueryValidator()
        {
            RuleFor(x => x.SearchStr)
                .NotEmpty()
                .MinimumLength(2);
        }
    }
}
