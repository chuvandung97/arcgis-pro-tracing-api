using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema.Core;
using Schema.Core.Models;

namespace Schema.Core.Services
{
    public interface IDMISService
    {
        Task<Dictionary<string, object>> CreateDMISPointAsync(object JsonObj, string Username);
        Task<HashSet<Dictionary<string, object>>> GetDMISEditorsAsync();
        Task<HashSet<Dictionary<string, object>>> GetApprovalOfficersAsync(string Username);
        Task<HashSet<Dictionary<string, object>>> GetDMISPointsAsync(string Username);
        Task<SchemaResult> GetDMISpointsWithinExtentAsync(string Geometry,string Username);
        Task<HashSet<Dictionary<string, object>>> GetDMISPointDetailsAsync(string OID);
        Task<Dictionary<string, object>> SendToApproverAsync(object JsonObj);
        Task<Dictionary<string, object>> ApprovalProcessAsync(object JsonObj, string Username, string ApproverName);
        Task<Dictionary<string, object>> UpdateDMISPointAsync(object JsonObj, string Username);
        Task<Dictionary<string, object>> DeleteDMISPointAsync(object JsonObj);
        Task<HashSet<Dictionary<string, object>>> GetPointSnaptoGaslineAsync(double X, double Y, string Layer);
    }
}
