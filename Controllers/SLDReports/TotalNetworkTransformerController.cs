using Schema.Core.Services;
using Schema.Web.AuthorizeUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Schema.Web.Controllers.SLDReports
{
    public class TotalNetworkTransformerController : Controller
    {
        // GET: TotalNetworkTransformer
        ISLDReportService _sldReportService;
        ILoggingService _loggingService;
        HashSet<Dictionary<string, object>> result;

        public TotalNetworkTransformerController(ISLDReportService SLDReportService, ILoggingService loggingService)
        {
            _sldReportService = SLDReportService;
            _loggingService = loggingService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> TotalNetworkTransformerReport()
        {
            Models.SLDTotalNetworkTransformerReportModel model = new Models.SLDTotalNetworkTransformerReportModel();
            return View(model);
        }
        [CustomAuthorize]
        [HttpPost]
        public async Task<ActionResult> TotalNetworkTransformerReport(string SelectedMVARating)
        {
            Models.SLDTotalNetworkTransformerReportModel model = new Models.SLDTotalNetworkTransformerReportModel();
            model.SelectedMVARating = model.MVARatings.Where(z => z.Text == SelectedMVARating).FirstOrDefault();
            string mvaRating = string.Empty;
            if (model.SelectedMVARating.Text == "66/22kV (75 MVA T/F)")
                mvaRating = "75";
            else if (model.SelectedMVARating.Text == "66/22kV (31.25 MVA T/F)")
                mvaRating = "31.25";
            else if (model.SelectedMVARating.Text == "22/6.6kV (10 MVA T/F)")
                mvaRating = "10";
            else
                mvaRating = "ALL";

            if ((SelectedMVARating == "Please Select"))
            {
                model.Result = null;
                model.Message = "Select mandatory fields to continue...";
            }
            else
            {
                model.Result = await _sldReportService.TotalNetworkTransformerAsync(mvaRating);
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