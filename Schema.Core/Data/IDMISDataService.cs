using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema.Core.Data;
using Schema.Core.Models;
using Schema.Core.Services;

namespace Schema.Core.Data
{
    public interface IDMISDataService
    {
        Task<Dictionary<string, object>> CreateDMISPointAsync(List<DMISGasLeakItem> rowInfo, string JobID, string Username);
        Task<HashSet<Dictionary<string, object>>> GetDMISEditorsAsync();
        Task<HashSet<Dictionary<string, object>>> GetApprovalOfficersAsync(string Username);
        Task<HashSet<Dictionary<string, object>>> GetDMISPointsAsync(string Username);
        Task<HashSet<Dictionary<string, object>>> GetDMISpointsWithinExtentAsync(string Geometry, string Username);
        Task<HashSet<Dictionary<string, object>>> GetDMISPointDetailsAsync(string OID);
        Task<Dictionary<string, object>> SendToApproverAsync(Int64[] oidArr, string approverName);
        Task<HashSet<Dictionary<string, object>>> GetEmailIDAsync(string approverName, Int64 ObjectID);
        Task<Dictionary<string, object>> ApprovalProcessAsync(string objectID, int statusCode, string approverRemarks, string Username);
        Task<Dictionary<string, object>> UpdateDMISPointAsync(List<DMISGasLeakItem> rowInfo, int objectID, string Username);
        Task<Dictionary<string, object>> DeleteDMISPointAsync(string objectID);
        Task<HashSet<Dictionary<string, object>>> GetPointSnaptoGaslineAsync(double X, double Y, string Layer);
        Task<string> GetDMISJobID();
    }
}
