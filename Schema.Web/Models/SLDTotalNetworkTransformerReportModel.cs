using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Schema.Web.Models
{
    public class SLDTotalNetworkTransformerReportModel
    {
        public SelectList MVARatings { get; set; }
        public SelectListItem SelectedMVARating { get; set; }
        public SLDTotalNetworkTransformerReportModel()
        {
            MVARatings = new SelectList(
              new List<string> { "Please Select", "ALL", "66/22kV (75 MVA T/F)", "66/22kV (31.25 MVA T/F)",  "22/6.6kV (10 MVA T/F)" }
              );

            SelectedMVARating = MVARatings.FirstOrDefault();
        }
        public HashSet<Dictionary<string, object>> Result { get; set; }
        public string Message { get; set; }

        [Display(Name = "Source Zone")]
        public string sourcezone { get; set; }

        [Display(Name = "Source Substation")]
        public string sourcesubstation { get; set; }

        [Display(Name = "Transformer Capacity MVA")]
        public int transformercapacitymva { get; set; }

        [Display(Name = "Transformer NO")]
        public string transformerno { get; set; }

        [Display(Name = "Prev MTH Max Load Reading")]
        public string prevmthmaxloadreading { get; set; }

        [Display(Name = "All Time Max Load Reading")]
        public string alltimemaxloadreading { get; set; }

        [Display(Name = "Dayload Max Load Reading")]
        public string dayloadmaxloadreading { get; set; }

        [Display(Name = "Nightload Max Load Reading")]
        public string nightload_maxloadreading { get; set; }

        [Display(Name = "Date")]
        public string mthYear { get; set; }
    }
}