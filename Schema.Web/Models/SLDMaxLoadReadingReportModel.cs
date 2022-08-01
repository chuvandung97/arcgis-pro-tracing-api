using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Schema.Web.Models
{
    public class SLDMaxLoadReadingReportModel
    {
        public SelectList Voltages { get; set; }
        public SelectList ReportTypes { get; set; }
        public SelectListItem SelectedVoltage { get; set; }
        public SelectListItem SelectedReportType { get; set; }
        public SLDMaxLoadReadingReportModel()
        {
            Voltages = new SelectList(
              new List<string> { "Please Select", "6.6 kV Circuit Network Tracing", "22 kV Circuit Network Tracing" }
              );
            ReportTypes = new SelectList(
               new List<string> { "Please Select", "Trace Results", "Ambiguous Trace Results", "With Alternative Source" }
               );
            SelectedVoltage = Voltages.FirstOrDefault();
            SelectedReportType = ReportTypes.FirstOrDefault();
        }       
        public HashSet<Dictionary<string, object>> Result { get; set; }
        public string Message { get; set; }
       
        [Display(Name = "Source Zone")]
        public string sourceZone { get; set; }

        [Display(Name = "First Leg Substation")]
        public string firstLegSubstation { get; set; }

        [Display(Name = "Target Zone")]
        public string targetZone { get; set; }

        [Display(Name = "Last Leg Substation")]
        public string lastLegSubstation { get; set; }

        [Display(Name = "Previous Month Max Reading")]
        public int prevmthMaxLoadReading { get; set; }

        [Display(Name = "All Time Max Reading")]
        public int alltimeMaxLoadReading { get; set; }

        [Display(Name = "Dayload Max Reading")]
        public int dayloadMaxLoadReading { get; set; }

        [Display(Name = "Nightload Max Reading")]
        public int nightloadMaxLoadReading { get; set; }   

        [Display(Name = "Date")]
        public string mthYear { get; set; }

        [Display(Name = "Total Network Capacity")]
        public double totalNetworkCapacity { get; set; }

        [Display(Name = "cuCab185sq")]
        public string cuCab185sq { get; set; }
        
        [Display(Name = "Votage")]
        public string voltage { get; set; }

        [Display(Name = "Sheet No")]
        public int sheetNo { get; set; }

        [Display(Name = "Segment No")]
        public int segmentNo { get; set; }

        [Display(Name = "Panel Section")]
        public string panelSection { get; set; }

        [Display(Name = "Panel No")]
        public int panelNo { get; set; }
    }
}
