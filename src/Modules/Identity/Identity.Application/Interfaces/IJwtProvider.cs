using Identity.Domain.Entities;

namespace Identity.Application.Interfaces
{
    public interface IJwtProvider
    {
        Task<string> Generate(ApplicationUser user);
    }
}
