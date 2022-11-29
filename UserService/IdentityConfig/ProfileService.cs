using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Security.Claims;
using UserService.Repository;

namespace UserService.IdentityConfig
{
    public class ProfileService : IProfileService
    {
        private readonly IUserRepository _authRepository;
        public ProfileService(IUserRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {

                var subject = context.Subject.Claims.ToList().Find(s => s.Type == "sub").Value;
                var userId = Int32.Parse(subject);

                var user = await _authRepository.GetUserById(userId);

                if (subject == null)
                {
                    return;
                }
                var claims = new List<Claim>
                {
                new Claim("RoleId", user.RoleId.ToString()),
                new Claim("Email", user.Email),
                };

                context.IssuedClaims = claims;
            }
            catch (Exception e)
            {
                return;
            }
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.FromResult(0);
        }
    }
}
