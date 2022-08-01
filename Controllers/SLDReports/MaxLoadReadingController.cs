using Schema.Core.Models;
using Schema.Core.Services;
using Schema.Web.AuthorizeUser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Schema.Web.Controllers
{
    public class MaxLoadReadingController : Controller
    {
        // GET: MaxLoadReading
        // GET: SubstationList
        ISLDReportService _sldReportService;
        ILoggingService _loggingService;
        HashSet<Dictionary<string, object>> result;

        public MaxLoadReadingController(ISLDReportService SLDReportService, ILoggingService loggingService)
        {
            _sldReportService = SLDReportService;
            _loggingService = loggingService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> MaxLoadReadingReport()
        {
            Models.SLDMaxLoadReadingReportModel model = new Models.SLDMaxLoadReadingReportModel();
            return View(model);
        }
        [CustomAuthorize]
        [HttpPost]
        public async Task<ActionResult> MaxLoadReadingReport(string SelectedVoltage, string SelectedReportType)
        {
            Models.SLDMaxLoadReadingReportModel model = new Models.SLDMaxLoadReadingReportModel();
            model.SelectedVoltage = model.Voltages.Where(z => z.Text == SelectedVoltage).FirstOrDefault();
            model.SelectedReportType = model.ReportTypes.Where(v => v.Text == SelectedReportType).FirstOrDefault();
            int voltage = 0;
            if (model.SelectedVoltage.Text == "6.6 kV Circuit Network Tracing")
                voltage = 2;
            else if (model.SelectedVoltage.Text == "22 kV Circuit Network Tracing")
                voltage = 4;

            if ((SelectedVoltage == "Please Select") || (SelectedReportType =="Please Select") )
            {
                model.Result = null;
                model.Message = "Select mandatory fields to continue...";
            }
            else
            {
                model.Result = await _sldReportService.MaxLoadReadingAsync(voltage, model.SelectedReportType.Text);
                if (model.Result.Count == 0)
                {
                    model.Result = null;
                    model.Message = "No results found";
                }
            }
            
            return View(model);          
        }        
    }
}