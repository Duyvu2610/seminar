using Microsoft.AspNetCore.Authorization;

namespace seminar.Policy
{
    // .net7
    /*
    public class MinimumAgeAuthorizeAttribute : AuthorizeAttribute

    {
        const string POLICY_PREFIX = "MinimumAge";

        public MinimumAgeAuthorizeAttribute(int age) => Age = age;

        // Get or set the Age property by manipulating the underlying Policy property
        public int Age
        {
            get
            {
                if (int.TryParse(Policy!.Substring(POLICY_PREFIX.Length), out var age))
                {
                    return age;
                }
                return default(int);
            }
            set
            {
                Policy = $"{POLICY_PREFIX}{value}";
            }
        }
    }
    */
    // .net8
    public class MinimumAgeAuthorizeAttribute : AuthorizeAttribute, IAuthorizationRequirement, IAuthorizationRequirementData
    {
        public MinimumAgeAuthorizeAttribute(int age) => Age = age;
        public int Age { get; }

        public IEnumerable<IAuthorizationRequirement> GetRequirements()
        {
            yield return this;
        }
    }
}
