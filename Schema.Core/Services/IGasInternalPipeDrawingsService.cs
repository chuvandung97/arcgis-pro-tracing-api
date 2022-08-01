using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Services
{
    public interface IGasInternalPipeDrawingsService
    {
        Task<HashSet<Dictionary<string, object>>> GetGasInternalPipeDrawingsAsync(string postalcode);
        Task<HashSet<Dictionary<string, object>>> GetGasInternalPipeDrawingsHistoryAsync();
        Task<HashSet<Dictionary<string, object>>> GetGasInternalPipeDrawingsByPostalCodeAsync(string PostalCode);
                
        Task<Dictionary<string, object>> UpdatePDFFileAsync(object JsonObj, string UserID);
        Task<Dictionary<string, object>> DeletePDFFileAsync(object JsonObj, string UserID);
        Task<Dictionary<string, object>> CreateGasInternalPipeDrawingsAsync(object JsonObj, string UserID);
        Task<Dictionary<string, object>> UpdateGasInternalPipeDrawingsAsync(object JsonObj, string UserID);
        Task<Dictionary<string, object>> DeleteGasInternalPipeDrawingsAsync(object JsonObj, string UserID);
    }
}
