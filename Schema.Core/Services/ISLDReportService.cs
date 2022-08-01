using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Core.Services
{
   public interface ISLDReportService
    {
        Task<HashSet<Dictionary<string, object>>> SubstationListAsync(string Zoneval, string Voltage);
        Task<HashSet<Dictionary<string, object>>> MaxLoadReadingAsync(int Voltage, string TabName);
        Task<HashSet<Dictionary<string, object>>> TotalNetworkTransformerAsync(string mvaRating);
        Task<HashSet<Dictionary<string, object>>> MaxMinTransformerCapacityReadingAsync(string searchTerm, int Voltage, string ReportType, string SearchCriteria);
        Task<HashSet<Dictionary<string, object>>> CableTransformerRingAsync(string searchTerm, int Voltage, string ReportType, string SearchCriteria);
    }
}
