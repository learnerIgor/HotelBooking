using System.Linq.Expressions;
using Users.Domain;

namespace Users.Application.Handlers.Queries
{
    internal static class ListUserWhere
    {
        public static Expression<Func<ApplicationUser, bool>> Where(ListUserFilter filter)
        {
            var freeText = filter.FreeText?.Trim();
            return user => (freeText == null || user.Login.Contains(freeText)) && user.IsActive;
        }
    }
}