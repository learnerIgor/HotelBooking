using FluentValidation;

namespace Users.Application.Handlers.Queries.GetUserByLogin
{
    public class GetUserByLoginQueryValidator : AbstractValidator<GetUserByLoginQuery>
    {
        public GetUserByLoginQueryValidator() 
        {
            RuleFor(e => e).IsValidListUserFilter();
        }
    }
}
