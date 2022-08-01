using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Schema.Web.Models
{
    public class SLDMaxMinTransformerCapacityReadingModel
    {
        public SelectList Voltages { get; set; }
        public SelectList ReportTypes { get; set; }
        public SelectList SearchCriteria { get; set; }
        public SelectListItem SelectedVoltage { get; set; }
        public SelectListItem SelectedReportType { get; set; }
        public SelectListItem SelectedSearchCriteria { get; set; }
        public string SearchedSubstation { get; set; }
        public SLDMaxMinTransformerCapacityReadingModel()
        {
            Voltages = new SelectList(
              new List<string> { "Please Select", "6.6 kV", "22 kV" }
              );
            ReportTypes = new SelectList(
             //new List<string> { "Please Select", "Trace Results", "Ambiguous Trace Results", "Third Injection Results" }
             new List<string> { "Please Select", "Trace Results", "Ambiguous Trace Results" }
             );
            SearchCriteria = new SelectList(
                new List<string> { "Please Select", "Source Substation", "Target Substation" }
             );
            SelectedVoltage = Voltages.FirstOrDefault();
            SelectedReportType = ReportTypes.FirstOrDefault();
            SelectedSearchCriteria = SearchCriteria.FirstOrDefault();
        }
        public HashSet<Dictionary<string, object>> Result { get; set; }
        public string substationName { get; set; }
        public string Message { get; set; }


    }
}