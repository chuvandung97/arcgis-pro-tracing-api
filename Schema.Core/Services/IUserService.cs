using Schema.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Services
{
    public interface IUserService
    {
        Task<SchemaResult> CheckUserStatus(string UserID, string MapServiceURL = null);
        string GetADGroupsForMethodNames(string MethodName);
        Task<SchemaResult> GetUserDetailsAsync(string userData, string userName);
        Task<HashSet<Dictionary<string, object>>> GetUrlsAsync();
        Task<HashSet<Dictionary<string, object>>> GetUserTypeAsync(string Username, bool StatsFlag = false);
        string CheckAPIUserStatus(string UserID);
    }
}
