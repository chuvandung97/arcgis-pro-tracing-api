using Schema.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Data
{
    public interface IPOVerificationDataService
    {
        Task<HashSet<Dictionary<string, object>>> GetPOJobInfoAsync(string ProjectOfficerID);
        Task<Dictionary<string, object>> UpdatePOJobInfoAsync(List<POVerficationListItem> rowInfo);
        Task<HashSet<Dictionary<string, object>>> GetPOVGemsEnablePOButtonAsync(Int64 SessionID);
        Task<HashSet<Dictionary<string, object>>> GetPOVGemsEnableReconcileButtonAsync(Int64 SessionID);
        Task<HashSet<Dictionary<string, object>>> GetPOVGemsJobStatusAsync(Int64 SessionID);
        Task<Dictionary<string, object>> UpdatePOVGemsLandbaseStatusAsync(List<POVerficationListItem> rowInfo);
        Task<Dictionary<string, object>> UpdatePOVGemsMEAReAssignAsync(List<POVerficationListItem> rowInfo);
        Task<Dictionary<string, object>> UpdatePOVGemsPostStatusAsync(List<POVerficationListItem> rowInfo);
        Task<Dictionary<string, object>> UpdatePOVGemsPDFNameAsync(List<POVerficationListItem> rowInfo);
        Task<HashSet<Dictionary<string, object>>> GetEmailsIDsOfPOVOfficersAsync(Int64 SessionID);
        Task<Dictionary<string, object>> UpdatePOVGemsMEAIDAsync(List<POVerficationListItem> rowInfo);
        Task<Dictionary<string, object>> UpdatePOVGemsQAQCPctAsync(List<POVerficationListItem> rowInfo);
    }
}
