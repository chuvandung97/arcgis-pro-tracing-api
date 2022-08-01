using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema.Core.Models;

namespace Schema.Core.Services
{
    public interface IOWTSIRService
    {
        Task<HashSet<Dictionary<string, object>>> GetCompletedTasksAync(string Officer, string FromDate, string ToDate);
        Task<HashSet<Dictionary<string, object>>> GetPendingItemsAync(string Username);
        Task<HashSet<Dictionary<string, object>>> GetReadingDetailsAsync(string ID);
        Task<HashSet<Dictionary<string, object>>> GetPrevReadingsAsync(string SrcStationName, string SrcStationBoardNo, string SrcStationPanelNo, string TgtStationName, string TgtStationBoardNo, string TgtStationPanelNo);
        Task<HashSet<Dictionary<string, object>>> GetReadingWithinExtentAsync(string Geometry);
        Task<HashSet<Dictionary<string, object>>> GetOWTSIREditors();
        Task<HashSet<Dictionary<string, object>>> GetApprovalOfficersAsync(string Username);
        Task<Dictionary<string, object>> ApprovalProcessAsync(object JsonObj, string approverName);
        Task<Dictionary<string, object>> AddReadingAsync(object JsonObj, string Username);
        Task<Dictionary<string, object>> UpdateReadingAsync(object JsonObj, string Username);
    }
}
