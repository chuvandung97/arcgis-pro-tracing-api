using Schema.Core.Data;
using Schema.Core.Models;
using Schema.Core.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Web.Mvc;
using Schema.Core.Utilities;
using Newtonsoft.Json;
using Schema.Core.Extensions;

namespace Schema.Services
{
    public class IncidentService : IIncidentService
    {
        ILoggingService _loggingService;
        IIncidentDataService _incidentDataService;
        IGemsDataService _gemsDataService;

        Dictionary<string, object> errorLogInfo;
        CommonUtilities _commonUtilities = new CommonUtilities();
        public IncidentService(ILoggingService LoggingService, IIncidentDataService IncidentDataService, IGemsDataService GemsDataService)
        {
            _loggingService = LoggingService;
            _incidentDataService = IncidentDataService;
            _gemsDataService = GemsDataService;
        }
        public ICustomAuthorizeService CustomAuthorizeService
        {
            get
            {
                return DependencyResolver.Current.GetService<ICustomAuthorizeService>();
            }
        }
        //added by Sandip on 05th Feb 2020 for RFC0024551 -- This api returns the incident information.
        public async Task<HashSet<Dictionary<string, object>>> GetHTOutageIncidentAsync(string RequestType, string IncidentID, string FromDate = null, string ToDate = null)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _incidentDataService.GetHTOutageIncidentAsync(RequestType, IncidentID, FromDate, ToDate);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        //added by Sandip on 11th Feb 2020 for RFC0024551 -- This api returns the information details of the particular incident.
        public async Task<SchemaResult> GetHTOutageIncidentDetailAsync(Int64 IncidentID)
        {
            HashSet<Dictionary<string, object>> bldgSummaryResult = new HashSet<Dictionary<string, object>>();
            HashSet<Dictionary<string, object>> incidentResult = new HashSet<Dictionary<string, object>>();
            HashSet<Dictionary<string, object>> summaryResult = new HashSet<Dictionary<string, object>>();
            Dictionary<string, object> finalResults = new Dictionary<string, object>();
            SchemaResult results = new SchemaResult();
            try
            {
                summaryResult = await _incidentDataService.GetHTOutageIncidentAsync("SUMMARY", IncidentID.ToString());
                bldgSummaryResult = await _incidentDataService.GetHTOutageBuildingSummaryAsync(IncidentID);
                incidentResult = await _incidentDataService.GetHTOutageIncidentDetailAsync(IncidentID);

                finalResults.Add("Summary", summaryResult);
                finalResults.Add("BuildingSummary", bldgSummaryResult);
                finalResults.Add("IncidentDetails", incidentResult);

                results.Result = finalResults;
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return results;
        }
        //added by Sandip on 14th Feb 2020 for RFC0024551 -- This api inserts newly created incident details in database.
        public async Task<Dictionary<string, object>> CreateHTOutageIncidentAsync(object JsonObj, string UserID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "insertIncident");
                var rowInfo = JsonConvert.DeserializeObject<List<IncidentManagementItems>>(jsonVal[0]);

                result = await _incidentDataService.CreateHTOutageIncidentAsync(rowInfo, UserID);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        //added by Sandip on 18th Feb 2020 for RFC0024551 -- This api inserts manually searched records based on 'Postal Code/Block Number/By selecting building on map' by users
        public async Task<SchemaResult> CreateHTOutageIncidentByUserAsync(object JsonObj, string UserID)
        {
            string postalCode = string.Empty;
            SchemaResult finalResults = new SchemaResult();
            Dictionary<string, object> insertResults = new Dictionary<string, object>();
            HashSet<Dictionary<string, object>> incidentResults = new HashSet<Dictionary<string, object>>();
            HashSet<Dictionary<string, object>> buildingResults = new HashSet<Dictionary<string, object>>();
            try
            {

                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "insertIncidentDetailByUser");
                var rowInfo = JsonConvert.DeserializeObject<List<IncidentManagementItems>>(jsonVal[0]);

                incidentResults = await _incidentDataService.GetHTOutageIncidentDetailAsync(rowInfo[0].incidentid);

                if (rowInfo[0].manualflag == 0)
                {
                    foreach (Dictionary<string, object> item in (IEnumerable)incidentResults)
                    {
                        if (rowInfo[0].manualinput == item["postalcode"].ToString())
                        {
                            finalResults.Message = "Postal Code already exists in the below customer list. Can't be duplicated!";
                            finalResults.Result = insertResults;
                            return finalResults;
                        }
                    }
                }
                else if (rowInfo[0].manualflag == 1)
                {
                    foreach (Dictionary<string, object> item in (IEnumerable)incidentResults)
                    {
                        if (rowInfo[0].manualinput == item["address"].ToString())
                        {
                            finalResults.Message = "Address already exists in the below customer list. Can't be duplicated!";
                            finalResults.Result = insertResults;
                            return finalResults;
                        }
                    }
                }
                else if (rowInfo[0].manualflag == 2)
                {
                    buildingResults = await _gemsDataService.GetCustomerCountAsync(null, null, Convert.ToDouble(rowInfo[0].manualinput.Split(' ')[0]), Convert.ToDouble(rowInfo[0].manualinput.Split(' ')[1]));
                    if (buildingResults.Count == 0)
                    {
                        finalResults.Message = "No building found! Click on the building.";
                        finalResults.Result = insertResults;
                        return finalResults;
                    }
                    foreach (Dictionary<string, object> item in (IEnumerable)buildingResults)
                    {
                        postalCode = item["postalcode"].ToString();
                        break;
                    }
                    foreach (Dictionary<string, object> item in (IEnumerable)incidentResults)
                    {
                        if (postalCode == item["postalcode"].ToString())
                        {
                            finalResults.Message = "Selected building already exists in the below customer list. Can't be duplicated!";
                            finalResults.Result = insertResults;
                            return finalResults;
                        }
                    }
                }
                insertResults = await _incidentDataService.CreateHTOutageIncidentByUserAsync(rowInfo, UserID);

                if (insertResults.Values.Contains("No records"))
                {
                    if (rowInfo[0].manualflag == 0)
                        finalResults.Message = "Not a valid Postal Code!";
                    else if (rowInfo[0].manualflag == 1 || rowInfo[0].manualflag == 2)
                        finalResults.Message = "Not a valid Address!";
                }
                finalResults.Result = insertResults;
                return finalResults;
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return finalResults;
        }
        //added by Sandip on 20th Feb 2020 for RFC0024551 -- This api allows user to update the incident details.
        public async Task<Dictionary<string, object>> UpdateHTOutageIncidentAsync(object JsonObj, string UserID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "updateIncident");
                var rowInfo = JsonConvert.DeserializeObject<List<IncidentManagementItems>>(jsonVal[0]);

                result = await _incidentDataService.UpdateHTOutageIncidentAsync(rowInfo, UserID);

            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        //added by Sandip on 21th Feb 2020 for RFC0024551 -- This api allows to update the customer status of the incident.
        public async Task<Dictionary<string, object>> UpdateHTOutageAffectedCustomerStatusAsync(object JsonObj, string UserID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "updateIncidentCustomerStatus");
                var rowInfo = JsonConvert.DeserializeObject<List<IncidentManagementItems>>(jsonVal[0]);

                result = await _incidentDataService.UpdateHTOutageAffectedCustomerStatusAsync(rowInfo, UserID);

            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        //added by Sandip on 24th Feb 2020 for RFC0024551 -- This api allows to delete indiviual customer of the particular incident.
        public async Task<Dictionary<string, object>> DeleteHTOutageIncidentByUserAsync(object JsonObj, string UserID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "deleteIncidentDetailByUser");
                var rowInfo = JsonConvert.DeserializeObject<List<IncidentManagementItems>>(jsonVal[0]);

                result = await _incidentDataService.DeleteHTOutageIncidentByUserAsync(rowInfo, UserID);

            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        //added by Sandip on 25th Feb 2020 for RFC0024551 -- This api is consumed by admin portal to delete any previously created incident.
        public async Task<Dictionary<string, object>> DeleteHTOutageIncidentAsync(object JsonObj, string UserID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "deleteIncident");
                var rowInfo = JsonConvert.DeserializeObject<List<IncidentManagementItems>>(jsonVal[0]);

                result = await _incidentDataService.DeleteHTOutageIncidentAsync(rowInfo, UserID);

            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        //added by Sandip on 27th Feb 2020 for RFC0024551 -- This api is used to generate incident boundary in Schema. 
        public async Task<Dictionary<string, object>> GenerateIncidentBoundaryAsync(object JsonObj, string UserID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "generateBoundary");
                var rowInfo = JsonConvert.DeserializeObject<List<IncidentManagementItems>>(jsonVal[0]);

                result = await _incidentDataService.GenerateIncidentBoundaryAsync(rowInfo, UserID);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        //added by Sandip on 28th Feb 2020 for RFC0024551 -- This api returns the transformer list for the searched substation based on MRC.
        public async Task<HashSet<Dictionary<string, object>>> GetIncidentTransformersAsync(string MRC)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _incidentDataService.GetIncidentTransformersAsync(MRC);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        //added by Sandip on 01st March 2020 for RFC0024551 -- This api perform trace based on feeder/ transformer and generates affected customers and returns network data.
        public async Task<SchemaResult> GetIncidentCustomerTraceAsync(Int64 IncidentID, string UserID, string TRFIDs = null, string FID = null, string EID = null, string DirectionFlag = null, string SLDBarriers = null, string GISBarriers = null, string OverwriteFlag = null)
        {
            SchemaResult result = new SchemaResult();
            Dictionary<string, object> feeder = new Dictionary<string, object>();
            HashSet<Dictionary<string, object>> traceResults = new HashSet<Dictionary<string, object>>();
            try
            {
                HashSet<int> sldBarriersList = new HashSet<int>();
                if (!string.IsNullOrEmpty(SLDBarriers))
                {
                    foreach (var barrier in SLDBarriers.Split(','))
                    {
                        sldBarriersList.Add(Convert.ToInt32(barrier));
                    }
                }
                HashSet<int> gisBarriersList = new HashSet<int>();
                if (!string.IsNullOrEmpty(GISBarriers))
                {
                    foreach (var barrier in GISBarriers.Split(','))
                    {
                        gisBarriersList.Add(Convert.ToInt32(barrier));
                    }
                }
                if (string.IsNullOrEmpty(DirectionFlag))
                    DirectionFlag = "1";

                var traceData = await _incidentDataService.GetIncidentCustomerTraceAsync(IncidentID, UserID, TRFIDs, FID, EID, DirectionFlag, sldBarriersList.Count > 0 ? sldBarriersList.ToArray() : null, gisBarriersList.Count > 0 ? gisBarriersList.ToArray() : null, OverwriteFlag);

                if (traceData != null && traceData.Count == 1)
                {
                    feeder.Add("TraceItems", traceData.First().GetValue("graphics"));
                    feeder.Add("Substation", traceData.First().GetValue("subs"));
                    feeder.Add("Transformer", traceData.First().GetValue("trfs"));
                    feeder.Add("Address", traceData.First().GetValue("adrs"));

                    Int64 EidVal = 0;

                    foreach (Dictionary<string, object> item in (IEnumerable)traceData)
                    {
                        if (item["eid"] != null)
                            EidVal = Convert.ToInt64(item["eid"]);
                    }

                    var reportData = traceData.First().GetValue<Dictionary<string, object>[]>("report");
                    if (reportData != null)
                    {
                        var substationData = new HashSet<XVoltageReportTreeItem>(reportData.Select(s => new XVoltageReportTreeItem(s)).ToArray());
                        XVoltageHierarchyHelper helper = new XVoltageHierarchyHelper(substationData);
                        feeder.Add("TraceReport", helper.GetHierarchy(60, Convert.ToInt64(EidVal), Convert.ToInt16(DirectionFlag)));
                        traceResults.Add(feeder);
                    }
                }
                result.Result = traceResults;
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        //added by Sandip on 08th May 2020 for RFC0021956 -- This api returns the transformer details within a specified extent.
        public async Task<HashSet<Dictionary<string, object>>> GetIncidentTransformersOnMapSelectAsync(string Geometry)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _incidentDataService.GetIncidentTransformersOnMapSelectAsync(Geometry);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        //added by Sandip on 27th May 2020 for RFC00XXXX -- This api inserts user entered feedback from the intranet application into database table.
        public async Task<Dictionary<string, object>> InsertFeedbackAsync(object JsonObj, string UserID, string UserName, string EmailID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            string fromAddress = string.Empty;
            string toAddress = string.Empty;
            string subject = string.Empty;
            string body = string.Empty;
            string response = string.Empty;
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "insertFeedback");
                var rowInfo = JsonConvert.DeserializeObject<List<IncidentManagementItems>>(jsonVal[0]);

                result = await _incidentDataService.InsertFeedbackAsync(rowInfo, UserID, UserName, EmailID);

                fromAddress = System.Configuration.ConfigurationManager.AppSettings["EmailFromAddress"];

                if (result.Values.Contains("Success"))
                {
                    toAddress = "schema.support@cyient.com,ITD_MAPS_Group@spgroup.com.sg";
                    subject = "IMS feedback submitted";
                    body = "Dear Team, @@'" + rowInfo[0].feedback + "' has been submitted by " + UserName.ToUpper() + ". @@Please review. " +
                           "@@Thank You,";
                    response = await _commonUtilities.SendEmailMethod(fromAddress, toAddress, subject, body);
                }
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
    }
}
