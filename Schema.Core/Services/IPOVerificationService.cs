using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Services
{
    public interface IPOVerificationService
    {
        Task<HashSet<Dictionary<string, object>>> GetPOJobInfoAsync(string ProjectOfficerID);
        Task<Dictionary<string, object>> UpdatePOJobInfoAsync(object JsonObj, string ProjectOfficerID);
        Task<Dictionary<string, object>> UpdatePOVImageOrPDFFileAsync(object JsonObj);        
        Task<HashSet<Dictionary<string, object>>> GetPOVGemsEnablePOButtonAsync(Int64 SessionID);
        Task<HashSet<Dictionary<string, object>>> GetPOVGemsEnableReconcileButtonAsync(Int64 SessionID);
        Task<HashSet<Dictionary<string, object>>> GetPOVGemsJobStatusAsync(Int64 SessionID);
        Task<Dictionary<string, object>> UpdatePOVGemsLandbaseStatusAsync(object JsonObj);       
        Task<Dictionary<string, object>> UpdatePOVGemsMEAReAssignAsync(object JsonObj);
        Task<Dictionary<string, object>> UpdatePOVGemsPostStatusAsync(object JsonObj);
        Task<Dictionary<string, object>> UpdatePOVGemsPDFNameAsync(object JsonObj);
        Task<Dictionary<string, object>> UpdatePOVGemsMEAIDAsync(object JsonObj);
        Task<Dictionary<string, object>> UpdatePOVGemsQAQCPctAsync(object JsonObj);
    }
}
