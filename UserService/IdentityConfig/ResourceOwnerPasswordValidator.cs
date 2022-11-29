using IdentityServer4.Models;
using IdentityServer4.Validation;
using UserService.Repository;

namespace UserService.IdentityConfig
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _authRepository;

        public ResourceOwnerPasswordValidator(IUserRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var userName = context.UserName;
            var password = context.Password;

            var user = _authRepository.GetUserByEmail(userName).Result;
            bool isEqual = BCrypt.Net.BCrypt.Verify(password, user.Password);

            // Succes login
            if (user != null && isEqual)
            {
                context.Result = new GrantValidationResult(subject: user.Id.ToString(), authenticationMethod: "password");
                return Task.FromResult(context.Result);
            }

            // Fail login
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Wrong username or password");
            return Task.FromResult(context.Result);
        }
    }
}
