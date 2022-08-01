using Schema.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Data
{
    public interface IGasInternalPipeDrawingsDataService
    {
        Task<HashSet<Dictionary<string, object>>> GetGasInternalPipeDrawingsAsync(string PostalCode);
        Task<HashSet<Dictionary<string, object>>> GetGasInternalPipeDrawingsHistoryAsync();
        Task<HashSet<Dictionary<string, object>>> GetGasInternalPipeDrawingsByPostalCodeAsync(string PostalCode);
        Task<Dictionary<string, object>> UpdatePDFFileAsync(List<GasInternalPipeDrawingsItems> rowInfo, string UserID);
        Task<Dictionary<string, object>> DeletePDFFileAsync(List<GasInternalPipeDrawingsItems> rowInfo, string UserID);
        Task<Dictionary<string, object>> CreateGasInternalPipeDrawingsAsync(List<GasInternalPipeDrawingsItems> rowInfo, string UserID);
        Task<Dictionary<string, object>> UpdateGasInternalPipeDrawingsAsync(List<GasInternalPipeDrawingsItems> rowInfo, string UserID);
        Task<Dictionary<string, object>> DeleteGasInternalPipeDrawingsAsync(List<GasInternalPipeDrawingsItems> rowInfo, string UserID);
    }
}
