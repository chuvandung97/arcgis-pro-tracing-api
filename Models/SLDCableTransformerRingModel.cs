using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Schema.Web.Models
{
    public class SLDCableTransformerRingModel
    {
        public SelectList Voltages { get; set; }
        public SelectList ReportTypes { get; set; }
        public SelectList SearchCriteria { get; set; }
        public SelectListItem SelectedVoltage { get; set; }
        public SelectListItem SelectedReportType { get; set; }
        public SelectListItem SelectedSearchCriteria { get; set; }
        public SLDCableTransformerRingModel()
        {
            Voltages = new SelectList(
               new List<string> { "Please Select", "6.6 kV", "22 kV" }
               );
            ReportTypes = new SelectList(
               new List<string> { "Please Select", "Cable", "Transformer" }
               );
            SearchCriteria = new SelectList(
                new List<string> { "Please Select", "Substation", "66/22kV Source Substation" }
             );
            SelectedVoltage = Voltages.FirstOrDefault();
            SelectedReportType = ReportTypes.FirstOrDefault();
            SelectedSearchCriteria = SearchCriteria.FirstOrDefault();
        }
        public HashSet<Dictionary<string, object>> Result { get; set; }
        public string Message { get; set; }
        public string substationName { get; set; }

        //Cable Ring
        [Display(Name = "Source Zone")]
        public string sourceZone { get; set; }

        [Display(Name = "First Leg Substation")]
        public string firstLegSubstation { get; set; }

        [Display(Name = "Target Zone")]
        public string targetZone { get; set; }

        [Display(Name = "Last Leg Substation")]
        public string lastLegSubstation { get; set; }

        [Display(Name = "Sum of Max. Load")]
        public string sumfmaxload { get; set; }

        [Display(Name = "Sum of All Time High")]
        public string sumofalltimehigh { get; set; }

        [Display(Name = "Date")]
        public string date { get; set; }

        [Display(Name = "U Factor")]
        public string ufactor { get; set; }

        [Display(Name = "Downstream TF Max Load")]
        public int downstreamtfmaxload { get; set; }

        [Display(Name = "Downstream CUS Max. Load")]
        public int downstreamcusmaxload { get; set; }

        [Display(Name = "Source Station")]
        public int sourcestation { get; set; }

        [Display(Name = "VOLT. Level")]
        public int voltlevel { get; set; }

        [Display(Name = "Max Dayload & Yr")]
        public string maxdayload { get; set; }

        [Display(Name = "Max Nightload")]
        public double maxnightload { get; set; }

        // Transformer Ring
        [Display(Name = "Station Name")]
        public string stationname { get; set; }

        [Display(Name = "TF No.")]
        public string tfno { get; set; }

        [Display(Name = "All Time High Load")]
        public string alltimehighload { get; set; }

        [Display(Name = "Max Load")]
        public string maxload { get; set; }

        [Display(Name = "Min Load")]
        public string minload { get; set; }

        [Display(Name = "TF Capacity")]
        public string tfcapacity { get; set; }

        [Display(Name = "UF")]
        public string uf { get; set; }

        [Display(Name = "Zone")]
        public string zone { get; set; }


    }
}
