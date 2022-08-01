using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Schema.Services;
using Schema.Core.Models;
using Schema.Core.Services;
using System.Collections;
using Schema.Web.Models;
using Schema.Web.AuthorizeUser;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text.html.simpleparser;

namespace Schema.Web.Controllers
{
    public class SubstationListController : Controller
    {
        // GET: SubstationList
        ISLDReportService _sldReportService;
        ILoggingService _loggingService;
        HashSet<Dictionary<string, object>> result;
        private static string zoneVal;
        private static string VoltageVal;

        public SubstationListController(ISLDReportService SLDReportService, ILoggingService loggingService)
        {
            _sldReportService = SLDReportService;
            _loggingService = loggingService;
        }
        public ActionResult Index()
        {
            zoneVal = string.Empty;
            VoltageVal = string.Empty;
            return View();
        }
        public async Task<ActionResult> SubstationListReport()
        {
            string token = string.Empty;
            if (Request.Headers["MobToken"] != null)
                token = Request.Headers["MobToken"].ToString();

            //WriteErrorLog("MobToken: " + token);

                Models.SLDSubstationListReportModel model = new Models.SLDSubstationListReportModel();
            return View(model);
        }
        [CustomAuthorize]
        [HttpPost]
        public async Task<ActionResult> SubstationListReport(string SelectedZone, string SelectedVoltage, string SelectedSpacing)
        {
            Models.SLDSubstationListReportModel model = new Models.SLDSubstationListReportModel();
            model.SelectedZone = model.Zones.Where(z => z.Text == SelectedZone).FirstOrDefault();
            model.SelectedVoltage = model.Voltages.Where(v => v.Text == SelectedVoltage).FirstOrDefault();
            model.SelectedSpacing = model.Spacing.Where(x => x.Text == SelectedSpacing).FirstOrDefault();
            zoneVal = model.SelectedZone.Text;

            string voltage = string.Empty;
            if (model.SelectedVoltage.Text == "6.6 kV")
                voltage = "2";
            else if (model.SelectedVoltage.Text == "22 kV")
                voltage = "4";
            else
                voltage = model.SelectedVoltage.Text;

            if ((SelectedZone == "Please Select") || (SelectedVoltage == "Please Select"))
            {
                model.Result = null;
                model.Message = "Select mandatory fields to continue...";
            }
            else
            {
                model.Result = await _sldReportService.SubstationListAsync(model.SelectedZone.Text, voltage);
                if (model.Result.Count == 0)
                {
                    model.Result = null;
                    model.Message = "No results found";
                }
            }
            return View(model);
        }
        //sandip
        [CustomAuthorize]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> ExportReport(string GridHtml)
        {
            //return Redirect("http://www.google.com");
            string test = zoneVal;

            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                //String htmlText = html.ToString();
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);

                string path = Path.Combine(Server.MapPath("~/SLDReports"), "mypdf1.pdf");
                PdfWriter.GetInstance(pdfDoc, new FileStream(path, FileMode.Create));

                pdfDoc.Open();
                iTextSharp.text.html.simpleparser.HTMLWorker hw = new iTextSharp.text.html.simpleparser.HTMLWorker(pdfDoc);
                hw.Parse(new StringReader(GridHtml));
                pdfDoc.Close();

                /*StringReader sr = new StringReader(GridHtml);                
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);              
                pdfDoc.Close();*/

                return File(stream.ToArray(), "application/pdf", "Grid.pdf");
            }
        }
        /*public void WriteErrorLog(string message)
        {
            string _filePath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
            string content = DateTime.Now + " " + " " + message + Environment.NewLine;
            System.IO.File.AppendAllText(_filePath, content);
        }*/
    }
}