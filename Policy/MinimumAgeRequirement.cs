using Microsoft.AspNetCore.Authorization;

namespace seminar.Policy
{
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public int Age { get; private set; }
        public MinimumAgeRequirement(int age) { Age = age; }
    }
}
