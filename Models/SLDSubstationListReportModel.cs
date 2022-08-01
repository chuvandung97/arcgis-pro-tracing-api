using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Schema.Web.Models
{
    public class SLDSubstationListReportModel
    {
        public SelectList Zones { get; set; }
        public SelectList Voltages { get; set; }
        public SelectList Spacing { get; set; }
        public SelectListItem SelectedZone { get; set; }
        public SelectListItem SelectedVoltage { get; set; }
        public SelectListItem SelectedSpacing { get; set; }
        public SLDSubstationListReportModel()
        {
            Zones = new SelectList(
              new List<string> { "Please Select", "ALL", "CENTRAL", "EAST", "WEST", "NORTH", "SOUTH" }
              );
            Voltages = new SelectList(
               new List<string> { "Please Select", "22/6.6kV", "6.6 kV", "22 kV" }
               );
            Spacing = new SelectList(
               new List<string> { "No Spacing", "Single Spacing" }
               );

            SelectedZone = Zones.FirstOrDefault();
            SelectedVoltage = Voltages.FirstOrDefault();
            SelectedSpacing = Spacing.FirstOrDefault();
        }
        public HashSet<Dictionary<string, object>> Result { get; set; }
        public string Message { get; set; }

        [Display(Name = "SNO")]
        public string sno { get; set; }

        [Display(Name = "MRC")]
        public string mrc { get; set; }

        [Display(Name = "Sheet")]
        public string sheet { get; set; }

        [Display(Name = "Segment")]
        public int segment { get; set; }

        [Display(Name = "Substation Name")]
        public string substationname { get; set; }

        [Display(Name = "Location Description")]
        public string locationdescription { get; set; }

        [Display(Name = "Gear")]
        public string gear { get; set; }

        [Display(Name = "Zone")]
        public string zone { get; set; }

        [Display(Name = "Votage")]
        public string voltage { get; set; }
    }
}