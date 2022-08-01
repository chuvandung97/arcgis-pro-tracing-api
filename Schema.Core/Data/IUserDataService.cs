using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Data
{
    public interface IUserDataService
    {
        Task<HashSet<Dictionary<string, object>>> CheckUserStatus(string UserID = null);
        HashSet<Dictionary<string, object>> GetADGroupsForMethodNames(string MethodName);
        Task<HashSet<Dictionary<string, object>>> GetUserDetailsAsync(string[] adGroupVal);
        Task<HashSet<Dictionary<string, object>>> GetLastLoginDateAsync(string Username);
        Task<HashSet<Dictionary<string, object>>> GetUserUnsuccessfulCountAsync(string Username, string LoginQueryDate);
        Task<HashSet<Dictionary<string, object>>> GetUserTypeAsync(string Username, bool StatsFlag = false);
        Task<HashSet<Dictionary<string, object>>> GetUrlsAsync();
        HashSet<Dictionary<string, object>> CheckAPIUserStatus(string UserID);
    }
}
