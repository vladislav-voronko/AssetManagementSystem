using Identity.Domain.Entities;

namespace Identity.Application.Interfaces
{
    public interface IJwtProvider
    {
        string Generate(ApplicationUser user);
    }
}
