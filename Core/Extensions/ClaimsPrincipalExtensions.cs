using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ClaimsPrincipalExtensions // Claim tiplərdəki rolları məcbur bu formada filter edirik ki rahat istifadə edək
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList(); // Sual işarəsi - yoxdursa null qaytarsın
            return result;
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal) // Bunlar mənə rolları qaytarır (admin, user)
        {
            return claimsPrincipal?.Claims(ClaimTypes.Role);
        }
    }
}