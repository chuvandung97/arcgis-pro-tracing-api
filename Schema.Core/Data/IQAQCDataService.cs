using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema.Core.Models;

namespace Schema.Core.Data
{
    public interface IQAQCDataService
    {
        Task<HashSet<Dictionary<string, object>>> GetAllUsersAsync();
        Task<HashSet<Dictionary<string, object>>> GetAllErrorCategoriesAsync();
        Task<HashSet<Dictionary<string, object>>> GetLast3MonthErrorsAsync();
        Task<HashSet<Dictionary<string, object>>> GetLast3MonthErrCatgAsync();
        Task<HashSet<Dictionary<string, object>>> GetLast3MonthTotalErrCatgAsync();
        Task<HashSet<Dictionary<string, object>>> GetTop10UserQAQCErrorsAsync(int Year, string Month);
        Task<HashSet<Dictionary<string, object>>> GetAllErrorsListAsync(int Year, string Month, string ErrCatg, string Username);
    }
}
