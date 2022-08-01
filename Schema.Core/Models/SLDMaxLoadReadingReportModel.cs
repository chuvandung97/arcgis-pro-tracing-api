using System.ComponentModel.DataAnnotations;

namespace Schema.Core.Models
{
    public class SLDMaxLoadReadingReportModel
    {
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

        [Display(Name = "MonthYear")]
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
