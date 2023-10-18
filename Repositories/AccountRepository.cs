using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using seminar.Data;
using seminar.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace seminar.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration configuration;
        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }

        public async Task<string> SignInAsync(SignInModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (!result.Succeeded)
            {
                return string.Empty;
            }
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                // Tại đây, bạn có thể truy cập thông tin về người dùng bằng cách sử dụng biến "user"
                // Ví dụ: var username = user.UserName;
                // Ví dụ: var dateOfBirth = user.DateOfBirth;

                // Tiếp tục tạo token và trả về
                var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, model.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToString())
        };

                var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: configuration["JWT:ValidIssuer"],
                    audience: configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(20),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature)
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return string.Empty;
        }

        public async Task<IdentityResult> SignUpAsync(SignUpModel model)
        {
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                DateOfBirth = model.DateOfBirth
            };
       

            return await userManager.CreateAsync(user, model.Password);
        }
    }
}
