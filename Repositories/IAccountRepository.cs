using Microsoft.AspNetCore.Identity;
using seminar.Models;

namespace seminar.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel model);
        Task<string> SignInAsync(SignInModel model);

    }
}
