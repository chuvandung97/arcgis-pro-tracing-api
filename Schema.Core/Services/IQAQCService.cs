using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema.Core.Models;

namespace Schema.Core.Services
{
    public interface IQAQCService
    {
        Task<HashSet<Dictionary<string, object>>> GetAllUsersAsync();
        Task<HashSet<Dictionary<string, object>>> GetAllErrorCategoriesAsync();
        Task<SchemaResult> GetLast3MonthErrorsAsync();
        Task<HashSet<Dictionary<string, object>>> GetTop10UserQAQCErrorsAsync(int Year, string Month);
        Task<HashSet<Dictionary<string, object>>> GetAllErrorsListAsync(int Year, string Month, string ErrCatg, string Username);
    }
}
