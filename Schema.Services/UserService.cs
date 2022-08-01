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

namespace Schema.Services
{
    public class UserService : IUserService
    {
        ILoggingService _loggingService;
        IUserDataService _userDataService;

        Dictionary<string, object> errorLogInfo;
        public UserService(ILoggingService LoggingService, IUserDataService UserDataService)
        {
            _loggingService = LoggingService;
            _userDataService = UserDataService;
        }
        public ICustomAuthorizeService CustomAuthorizeService
        {
            get
            {
                return DependencyResolver.Current.GetService<ICustomAuthorizeService>();
            }
        }
        //sandip - need to remove 
        /*public async Task<SchemaResult> GetUserDetailsAsync(string userData, string userName)
        {
            SchemaResult result = new SchemaResult();
            HashSet<Dictionary<string, object>> rolesResult = new HashSet<Dictionary<string, object>>();
            Dictionary<string, object> finalResults = new Dictionary<string, object>();
            try
            {
                List<string> claimResults = new List<string>();
                finalResults.Add("ClaimResults", "SLD_LocateSheet,SLD_OWTS_ViewAddModReading,GIS_CableXSectionOrientation,SLD_Rpt_CableTransformerRing,OP_VTG_230kV,GIS_BldgCustCnt,GIS_CableXSectionLV,SLD_ZoomSeg5,GIS_ReportsLVE,GIS_TraceOGBSingleLeg,SatelliteImage,GIS_DMIS_Delete,SLD_Propagate(Length),SearchDMIS,GISMap,GAS_OPERATIONZONE,SLD_ZoomSeg3,GIS_CableXSectionDistPipe,SLD_TraceSingle,SLD_Rpt_MaxLoadReading,GIS_DMIS_Approve,GIS_Propagate,SLD_Rpt_OWTSIR,GAS_DISTRIBUTION,SLD_Propagate,Identifier,GIS_CableXSectionLVCCS,SLD_ZoomSeg4,Search,OP_VTG_LOW,TOC,GIS_66KVCustCount,GIS_Rpt_Electricity,SLD_ZoomSeg2,SLD_TraceBldgCustCnt,GIS_CableXSectionTransPipeXSectionJacket,SplitMap,DMISGasLeak,GIS_CableXSection,GIS_TraceOGBBoth,GIS_TracePlanning,GIS_TraceLT,GIS_CableXSectionTransPipe,GIS_TraceBldgCustCnt,SLDMap,SLD_ZoomSeg6,GIS_TracePlanningDownStream,GIS_BldgCustCntMultiple,GIS_Rpt_Landbase,OP_VTG_66kV,OP_VTG_22kV,GIS_TraceHTTrans,GIS_HybridMap,GIS_CableXSectionDistCSC,GIS_CableXSectionTransPipeJacketXSection,GIS_CableXSectionTransCSC,GIS_DMIS_SendForApproal,GIS_Elect_MapService,GAS_LANDBASE,GIS_DMIS_Update,QAQC_Dashboard,GIS_Reports,SUPPLYZONE,GIS_CableXSectionDistCCS,GIS_TraceOGBDownStream,SLD_Rpt_SubStationList,GIS_CableXSectionDist,OP_VTG_ABANDON,SLDMapService,SLD_ScadaReading,GIS_TraceOGB,SLD_ZoomSeg1,OP_VTG_6_6kV,GIS_CableXSectionTransCCS,GIS_TracePlanningUpStream,GAS_NETWORK_OVERVIEW,GAS_ABANDON,GIS_AsBuiltDwg,GIS_TraceOGBUpStream,GIS_CableXSectionTrans,GAS_TRANSMISSION,OP_VTG_400kV,SLD_JumpToDest,SLD_ScadaReadingPMMDay,SLD_ScadaReadingPMMMonth,GIS_DMIS_Create,GIS_TracePlanningClearBarriers,SLD_NMACS,Measure,GIS_TracePlanningSetBarriers,GIS_CableXSectionLVCSC,GIS_Rpt_Gas,SLDPrint,GIS_TraceHTDist,GAS_WORK_REQUEST,SLD_OWTS_PendingTask,GISPrint,GIS_CableXSectionDistPipeJacketXSection,SLD_Rpt_TotalNetworkTransformer,SLD_ScadaReadingDayNight,GAS_MISCELLANEOUS,SLD_Reports,SLD_OWTSIR,SLD_TraceXVoltage,GIS_CableXSectionDistPipeXSectionJacket,GIS_DMIS_View,GIS_DMIS_Move,ELEC_PIPES,SLD_TraceXVoltageBarrier,ELEC_WIP,GAS_DIMENSIONING,GAS_WIP,GIS_NMACS,SLD_Rpt_MaxMinTransformerCapacity,GIS_BldgCustCntSingle,SLD_TraceMulti");
                finalResults.Add("Username", "nitinbaban");
                result.Result = finalResults;
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
        }*/
        public string GetADGroupsForMethodNames(string MethodName)
        {
            string result = string.Empty;
            HashSet<Dictionary<string, object>> adRolesResult = new HashSet<Dictionary<string, object>>();
            try
            {
                adRolesResult = _userDataService.GetADGroupsForMethodNames(MethodName);
                string OID = string.Empty;
                if (adRolesResult.Count > 0)
                {
                    foreach (Dictionary<string, object> item in (IEnumerable)adRolesResult)
                    {
                        if (item["key"] != null)
                            result = result + "," + item["key"].ToString();
                    }
                    result = result.Substring(1, result.Length - 1);
                }
            }
            catch (Exception ex)
            {
                //errorLogInfo = new Dictionary<string, object>();
                //if (ex.Message.Length > 2000)
                //    errorLogInfo = await _customAuthorizeDataService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                //else
                //    errorLogInfo = await _customAuthorizeDataService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        public async Task<SchemaResult> GetUserDetailsAsync(string userData, string userName)
        {
            SchemaResult result = new SchemaResult();
            HashSet<Dictionary<string, object>> rolesResult = new HashSet<Dictionary<string, object>>();
            HashSet<Dictionary<string, object>> lastLoginDate = new HashSet<Dictionary<string, object>>();
            HashSet<Dictionary<string, object>> unSuccessfulCount = new HashSet<Dictionary<string, object>>();
            HashSet<Dictionary<string, object>> userType = new HashSet<Dictionary<string, object>>();
            Dictionary<string, object> finalResults = new Dictionary<string, object>();
            string cat2bUrlKeys = string.Empty;
            string loginDate = string.Empty;
            string loginQueryDate = string.Empty;
            string count = string.Empty;
            string userTypeVal = string.Empty;
            string securityClearance = string.Empty;
            bool adGrpsassigned = false;
            try
            {
                List<string> claimResults = new List<string>();

                //added by Sandip on 18/04/2021 for RFC0032836, Implementation of MHA security clearance
                if (userData.ToUpper().Contains("GIS VIEWER") && userData.ToUpper().Contains("GEMS_VIEWER_DISTRIBUTION"))
                    adGrpsassigned = true;

                string[] adGrpArr = new string[userData.Split(',').Length];
                for (int i = 0; i < adGrpArr.Length; i++)
                {
                    if (userData.Split(',')[i].ToString().ToUpper().Contains("GEMS_VIEWER_DISTRIBUTION") && adGrpsassigned == true)
                        adGrpsassigned = false;
                    else
                        adGrpArr[i] = userData.Split(',')[i].ToString();
                }
                //WriteErrorLog(adGrpArr.ToString());
                rolesResult = await _userDataService.GetUserDetailsAsync(adGrpArr);

                string roles = string.Empty;
                foreach (Dictionary<string, object> item in (IEnumerable)rolesResult)
                {
                    if (item["key"] != null)
                        roles = roles + "," + item["key"].ToString();
                }
                if (!string.IsNullOrEmpty(roles))
                    claimResults.Add(roles.Remove(0, 1));

                try
                {
                    //To get user last sucessful login date
                    try
                    {
                        lastLoginDate = await _userDataService.GetLastLoginDateAsync(userName);
                        foreach (Dictionary<string, object> item in (IEnumerable)lastLoginDate)
                        {
                            if (item["userlastlogindate"] != null)
                                loginDate = item["userlastlogindate"].ToString();
                        }
                        //WriteErrorLog("Last Login Date:- " + loginDate.ToString());
                    }
                    catch (Exception ex)
                    {
                    }

                    //To get user total number of unsuccessful login count since his/her last successful login
                    try
                    {
                        if (lastLoginDate.Count != 0)
                        {

                            unSuccessfulCount = await _userDataService.GetUserUnsuccessfulCountAsync(userName, loginDate);
                            foreach (Dictionary<string, object> item in (IEnumerable)unSuccessfulCount)
                            {
                                if (item["count"] != null)
                                    count = item["count"].ToString();
                            }
                            //WriteErrorLog("Unsuccessful Count:- " + unSuccessfulCount.ToString());
                        }
                        else
                            count = "";
                    }
                    catch (Exception ex1)
                    {
                    }
                    //added by Sandip on 17/04/2021 for RFC0032836, Implemetation of MHA security clearance

                    //To get the user type
                    userType = await _userDataService.GetUserTypeAsync(userName);
                    foreach (Dictionary<string, object> item in (IEnumerable)userType)
                    {
                        if (item["security"] != null)
                            securityClearance = item["security"].ToString();
                    }

                    if (securityClearance != "NA")
                        userTypeVal = securityClearance;
                    else
                        userTypeVal = "";

                    //if (userData.ToUpper().Contains("AG_VPN_CITRIX"))
                        //userTypeVal = "CAT2B";

                    //commented by Sandip on 16/04/2021 for RFC0032836, Implemetation of MHA security clearance
                    /*
                     //if userTypeVal is CAT2, then remove CAT2B url keys from claim results
                     if (securityClearance.ToUpper() == "CAT2")
                    {
                        userTypeVal = "CAT2";
                        cat2bUrlKeys = ConfigurationManager.AppSettings["RemoveCAT2BKeysFromClaimResults"];
                        string[] cat2bUrlKeysList = cat2bUrlKeys.Split(',');

                        for (int i = cat2bUrlKeysList.Count() - 1; i >= 0; i--)
                        {
                            claimResults[0] = claimResults[0].Replace(cat2bUrlKeysList[i].ToString() + ",", string.Empty);
                            claimResults[0] = claimResults[0].Replace(cat2bUrlKeysList[i].ToString(), string.Empty);
                        }
                    }
                    else if (securityClearance.ToUpper() == "CAT2B" || (securityClearance.ToUpper() == "CAT1(S)"))
                    {
                        userTypeVal = securityClearance.ToUpper();
                        cat2bUrlKeys = ConfigurationManager.AppSettings["RemoveCAT2KeysFromClaimResults"];
                        string[] cat2bUrlKeysList = cat2bUrlKeys.Split(',');

                        for (int i = cat2bUrlKeysList.Count() - 1; i >= 0; i--)
                        {
                            claimResults[0] = claimResults[0].Replace(cat2bUrlKeysList[i].ToString() + ",", string.Empty);
                            claimResults[0] = claimResults[0].Replace(cat2bUrlKeysList[i].ToString(), string.Empty);
                        }
                    }
                    else
                    {
                        userTypeVal = "";
                    }*/
                }
                catch (Exception ex2)
                {
                }
                //WriteErrorLog("ClaimResults:- " + claimResults.ToString());
                //WriteErrorLog("Username:- " + userName.ToString());
                //WriteErrorLog("UserType:- " + userTypeVal.ToString());
                //WriteErrorLog("Security Clearance:- " + securityClearance.ToString());

                finalResults.Add("ClaimResults", claimResults);
                finalResults.Add("Username", userName);
                finalResults.Add("LastLoginTime", loginDate);
                finalResults.Add("UnsuccessfulLogin", count);
                finalResults.Add("UserType", userTypeVal);

                result.Result = finalResults;
            }
            catch (Exception ex)
            {
                //WriteErrorLog("Exception :-" + ex.Message.ToString());
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

        public async Task<HashSet<Dictionary<string, object>>> GetUrlsAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _userDataService.GetUrlsAsync();
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
        //added by Sandip on 8th April 2019 for RFC0018439 -- To get user type for the logged in user. By default StatsFlag is false but when accessed through 
        //Schema Stats application the StatsFlag is set to true.
        public async Task<HashSet<Dictionary<string, object>>> GetUserTypeAsync(string Username, bool StatsFlag = false)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _userDataService.GetUserTypeAsync(Username, StatsFlag);
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
        //added by Sandip on 4th April 2019 for RFC0018439 -- To check whether the logged in user account is blocked or not.
        //It also checks whether the user has access to specific map service or not.
        public async Task<SchemaResult> CheckUserStatus(string UserID, string MapServiceURL = null)
        {
            //string result = string.Empty;
            SchemaResult result = new SchemaResult();
            HashSet<Dictionary<string, object>> results = new HashSet<Dictionary<string, object>>();
            HashSet<Dictionary<string, object>> userType = new HashSet<Dictionary<string, object>>();
            string securityClearance = string.Empty;
            string mapServiceUsage = ConfigurationManager.AppSettings["RestrictMapServiceforCAT2Users"];
            string conditionToEnableUsageTracking = ConfigurationManager.AppSettings["UCONStatsConditionToEnableUsageTracking"];
            string blockedStatus = string.Empty;
            try
            {
                results = await _userDataService.CheckUserStatus(UserID);
                //Condition to check if user is blocked or not
                if (conditionToEnableUsageTracking == "true")
                {
                    foreach (Dictionary<string, object> item in (IEnumerable)results)
                    {
                        if (item["blocked_by"] != null)
                            blockedStatus = item["blocked_by"].ToString();
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(blockedStatus))
                {
                    result.Message = blockedStatus;
                }
                else
                {
                    //commented by Sandip on 16/04/2021 for RFC0032836, Implemetation of MHA security clearance

                    /* userType = await _userDataService.GetUserTypeAsync(UserID);
                    foreach (Dictionary<string, object> item in (IEnumerable)userType)
                    {
                        if (item["security"] != null)
                            securityClearance = item["security"].ToString();
                    }
                    //WriteErrorLog("SecurityClearance:-" + securityClearance);

                    //if securityClearance is CAT2, then check the map service url if it contains any map service that needs to be accessed by CAT2
                    if (!string.IsNullOrEmpty(MapServiceURL))
                    {
                        string[] compareArr = mapServiceUsage.Split(',');
                        for (int i = 0; i <= compareArr.Length - 1; i++)
                        {
                            if (securityClearance.ToUpper() == "CAT2" && MapServiceURL.ToUpper().Contains(compareArr[i].ToUpper()))
                            {
                                result.Message = "Authorization has been denied for this request.";
                                //WriteErrorLog("Message:-" + result.Message);
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(result.Message))
                        {
                            if (MapServiceURL.ToUpper().Contains("SUBSTATION"))
                            {
                                userType = await _userDataService.GetUserTypeAsync(UserID);
                                result.Result = userType;
                            }
                        }
                    }*/
                    result.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
            }
            return result;
        }
        //added by Sandip on 20th May 2019 for RFC0018439 -- To check whether the logged in user account is blocked or not. This condition is checked for each API request.
        public string CheckAPIUserStatus(string UserID)
        {
            string result = string.Empty;
            HashSet<Dictionary<string, object>> userStatusResults = new HashSet<Dictionary<string, object>>();
            try
            {
                userStatusResults = _userDataService.CheckAPIUserStatus(UserID);
                foreach (Dictionary<string, object> item in (IEnumerable)userStatusResults)
                {
                    if (item["blocked_by"] != null)
                        result = item["blocked_by"].ToString();
                    break;
                }
            }
            catch (Exception ex)
            {
                //errorLogInfo = new Dictionary<string, object>();
                //if (ex.Message.Length > 2000)
                //    errorLogInfo = await _customAuthorizeDataService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                //else
                //    errorLogInfo = await _customAuthorizeDataService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        /*public void WriteErrorLog(string message)
        {
            string _filePath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
            string content = DateTime.Now + Environment.NewLine + message + Environment.NewLine;
            System.IO.File.AppendAllText(_filePath, content);
        }*/
    }
}
