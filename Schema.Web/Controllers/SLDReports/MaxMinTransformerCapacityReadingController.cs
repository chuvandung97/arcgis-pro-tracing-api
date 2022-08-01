using Schema.Core.Services;
using Schema.Web.AuthorizeUser;
using Schema.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Schema.Web.Controllers.SLDReports
{
    public class MaxMinTransformerCapacityReadingController : Controller
    {
        // GET: MaxMinTransformerCapacityReading
        ISearchService _searchService;
        ILoggingService _loggingService;
        ISLDReportService _sldReportService;

        HashSet<Dictionary<string, object>> result;
        List<SLDMaxMinTransformerCapacityReadingModel> substationList;
        public ActionResult Index()
        {
            return View();
        }
        public MaxMinTransformerCapacityReadingController(ISLDReportService SLDReportService, ISearchService SearchService, ILoggingService loggingService)
        {
            _sldReportService = SLDReportService;
            _searchService = SearchService;
            _loggingService = loggingService;
        }

        public async Task<ActionResult> MaxMinTransformerCapacityReadingReport()
        {
            SLDMaxMinTransformerCapacityReadingModel model = new SLDMaxMinTransformerCapacityReadingModel();
            return View(model);
        }
        [CustomAuthorize]
        [HttpPost]
        public async Task<ActionResult> MaxMinTransformerCapacityReadingReport(string searchTerm, string SelectedVoltage, string SelectedReportType, string SelectedSearchCriteria)
        {
            SLDMaxMinTransformerCapacityReadingModel model = new SLDMaxMinTransformerCapacityReadingModel();
            model.SelectedVoltage = model.Voltages.Where(z => z.Text == SelectedVoltage).FirstOrDefault();
            model.SelectedReportType = model.ReportTypes.Where(z => z.Text == SelectedReportType).FirstOrDefault();
            model.SelectedSearchCriteria = model.SearchCriteria.Where(z => z.Text == SelectedSearchCriteria).FirstOrDefault();

            int voltage = 0;
            if (model.SelectedVoltage.Text == "6.6 kV")
                voltage = 2;
            else if (model.SelectedVoltage.Text == "22 kV")
                voltage = 4;

            if ((string.IsNullOrEmpty(searchTerm)) || (SelectedVoltage == "Please Select") || (SelectedReportType == "Please Select") || (SelectedSearchCriteria == "Please Select"))
            {
                model.Result = null;
                model.Message = "Select mandatory fields to continue...";
            }
            else
            {
                model.Result = await _sldReportService.MaxMinTransformerCapacityReadingAsync(searchTerm, voltage, SelectedReportType, SelectedSearchCriteria);
                if (model.Result.Count == 0)
                {
                    model.Result = null;
                    model.Message = "No results found";
                }
            }
           
            return View(model);

        }

        private List<SLDMaxMinTransformerCapacityReadingModel> ConvertToModel(HashSet<Dictionary<string, object>> hsCollection)
        {
            List<SLDMaxMinTransformerCapacityReadingModel> maxLoadReadingReport = new List<SLDMaxMinTransformerCapacityReadingModel>();
            try
            {
                if (hsCollection.Count > 0)
                {
                    foreach (Dictionary<string, object> item in (IEnumerable)hsCollection)
                    {
                        SLDMaxMinTransformerCapacityReadingModel reportModel = new SLDMaxMinTransformerCapacityReadingModel();
                        if (item["substation_name"] != null)
                            reportModel.substationName = item["substation_name"].ToString();

                        maxLoadReadingReport.Add(reportModel);
                    }

                }
            }
            catch (Exception ex)
            {
                _loggingService.Error(ex);
            }
            return maxLoadReadingReport;
        }

        public async Task<JsonResult> GetSubstation(string term)
        {
            //List<string> substations;
            SLDMaxMinTransformerCapacityReadingModel model = new SLDMaxMinTransformerCapacityReadingModel();
            model.Result = await _searchService.GetSearchResultsAsync("7", "", term);

            substationList = new List<SLDMaxMinTransformerCapacityReadingModel>();
            substationList = ConvertToModel(model.Result);
            List<string> substations = substationList.Select(x => x.substationName.ToString()).ToList();

            //substations = substationList.Where(x => x.substationName.StartsWith(term))
            //    .Select(y => y.substationName).ToList();

            return Json(substations, JsonRequestBehavior.AllowGet);
        }
    }
}