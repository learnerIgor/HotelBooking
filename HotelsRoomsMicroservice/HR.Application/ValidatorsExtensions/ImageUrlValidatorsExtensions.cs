using FluentValidation;

namespace HR.Application.ValidatorsExtensions
{
    public static class ImageUrlValidatorsExtensions
    {
        public static IRuleBuilderOptions<T, string> IsValidImageUrl<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(url => Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)).WithErrorCode("Not a url");
        }
    }
}
