using CoreAuth.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Policy;

namespace CoreAuth.Repository.Implementation
{
    public class Im_Dapper(IConfiguration con, IHttpContextAccessor httpContextAccessor) : IDapper
    {
        public string Dappercon()
        {
            var a= con.GetConnectionString("DefaultConnection");         
            return a;
        }

        public string GetLoggedUserName()
        {
            var claimsUser = httpContextAccessor.HttpContext?.User;
            string fullName = (claimsUser.Identity?.Name)??"UnAuthorized";
            return fullName;
        }
    }
}
