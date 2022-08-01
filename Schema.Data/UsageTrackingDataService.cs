using Schema.Core.Data;
using Schema.Core.Models;
using Schema.Core.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Schema.Data
{
    public class UsageTrackingDataService : PgDbBase, IUsageTrackingDataService
    {
        public UsageTrackingDataService(IConfigService configService, ILoggingService loggingService)
            : base(configService, loggingService)
        { }
        //added by Sandip on 9th April 2019 for RFC0018439 -- This api returns the list of users reporting under the logged in manager 
        //and their thresold limit to access the Schema application.
        //added by Sandip on 8th Jan 2020 for RFC0023594 -- API modified to allow ReportOfficerID as an input parameter
        public async Task<HashSet<Dictionary<string, object>>> GetUsageTrackingUserThresholdAync(string ReportOfficerID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_stats_userthreshold");

            var param1 = command.CreateParameter();
            param1.ParameterName = "proid";
            param1.DbType = DbType.String;
            param1.Value = ReportOfficerID.ToUpper();
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 17th April 2019 for RFC0018439 -- This api returns the section wise thresold limit for accessing the Schema application.
        //added by Sandip on 8th Jan 2020 for RFC0023594 -- API modified to allow ReportOfficerID as an input parameter
        public async Task<HashSet<Dictionary<string, object>>> GetUsageTrackingSectionThresholdAync(string ReportOfficerID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_stats_sectionthreshold");

            var param1 = command.CreateParameter();
            param1.ParameterName = "proid";
            param1.DbType = DbType.String;
            param1.Value = ReportOfficerID.ToUpper();
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 22nd April 2019 for RFC0018439 -- This api returns the list of those users only who have accessed Schema application for logged in manager
        //added by Sandip on 19th Aug 2019 for RFC0020665 -- Added FromDate and ToDate parameters
        public async Task<HashSet<Dictionary<string, object>>> GetUsageTrackingSchemaUserAync(string ReportOfficerID, string FromDate, string ToDate)
        {
            DateTime fromDate = Convert.ToDateTime(FromDate);
            DateTime toDate = Convert.ToDateTime(ToDate);

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            if (fromDate == toDate)
            {
                command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_stats_schemauser");

                var param1 = command.CreateParameter();
                param1.ParameterName = "proid";
                param1.DbType = DbType.String;
                param1.Value = ReportOfficerID.ToUpper();
                command.Parameters.Add(param1);

                var param2 = command.CreateParameter();
                param2.ParameterName = "pinputdate";
                param2.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date;
                param2.Value = FromDate;
                command.Parameters.Add(param2);
            }
            else
            {
                command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_stats_schemauser_fromto");

                var param1 = command.CreateParameter();
                param1.ParameterName = "proid";
                param1.DbType = DbType.String;
                param1.Value = ReportOfficerID.ToUpper();
                command.Parameters.Add(param1);

                var param2 = command.CreateParameter();
                param2.ParameterName = "pfromdate";
                param2.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date;
                param2.Value = FromDate;
                command.Parameters.Add(param2);

                var param3 = command.CreateParameter();
                param3.ParameterName = "ptodate";
                param3.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date;
                param3.Value = ToDate;
                command.Parameters.Add(param3);
            }
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 14th May 2019 for RFC0018439 -- This api returns Total Accessible, Unaccessible, Blocked and Unblocked users count for logged in manager
        //added by Sandip on 19th Aug 2019 for RFC0020665 -- Added FromDate and ToDate parameters
        public async Task<HashSet<Dictionary<string, object>>> GetUsageTrackingSummaryAsync(string ReportOfficerID, string FromDate, string ToDate)
        {
            DateTime fromDate = Convert.ToDateTime(FromDate);
            DateTime toDate = Convert.ToDateTime(ToDate);

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            if (fromDate == toDate)
            {
                command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_stats_sumary");

                var param1 = command.CreateParameter();
                param1.ParameterName = "proid";
                param1.DbType = DbType.String;
                param1.Value = ReportOfficerID.ToUpper();
                command.Parameters.Add(param1);

                var param2 = command.CreateParameter();
                param2.ParameterName = "pinputdate";
                param2.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date;
                param2.Value = FromDate;
                command.Parameters.Add(param2);
            }
            else
            {
                command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_stats_sumary_fromto");

                var param1 = command.CreateParameter();
                param1.ParameterName = "proid";
                param1.DbType = DbType.String;
                param1.Value = ReportOfficerID.ToUpper();
                command.Parameters.Add(param1);

                var param2 = command.CreateParameter();
                param2.ParameterName = "pfromdate";
                param2.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date;
                param2.Value = FromDate;
                command.Parameters.Add(param2);

                var param3 = command.CreateParameter();
                param3.ParameterName = "ptodate";
                param3.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date;
                param3.Value = ToDate;
                command.Parameters.Add(param3);
            }
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 15th May 2019 for RFC0018439 -- This api returns map sheet information accessed by user in both GIS and SLD
        //added by Sandip on 19th Aug 2019 for RFC0020665 -- Added FromDate and ToDate parameters
        public async Task<HashSet<Dictionary<string, object>>> GetUsageTrackingMapSheetsAsync(string UserID, string FromDate, string ToDate)
        {
            DateTime fromDate = Convert.ToDateTime(FromDate);
            DateTime toDate = Convert.ToDateTime(ToDate);

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            if (fromDate == toDate)
            {
                command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_stats_mapsheets");

                var param1 = command.CreateParameter();
                param1.ParameterName = "puserid";
                param1.DbType = DbType.String;
                param1.Value = UserID.ToUpper();
                command.Parameters.Add(param1);

                var param2 = command.CreateParameter();
                param2.ParameterName = "pinputdate";
                param2.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date;
                param2.Value = FromDate;
                command.Parameters.Add(param2);
            }
            else
            {
                command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_stats_mapsheets_fromto");

                var param1 = command.CreateParameter();
                param1.ParameterName = "puserid";
                param1.DbType = DbType.String;
                param1.Value = UserID.ToUpper();
                command.Parameters.Add(param1);

                var param2 = command.CreateParameter();
                param2.ParameterName = "pfromdate";
                param2.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date;
                param2.Value = FromDate;
                command.Parameters.Add(param2);

                var param3 = command.CreateParameter();
                param3.ParameterName = "ptodate";
                param3.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date;
                param3.Value = ToDate;
                command.Parameters.Add(param3);
            }
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 7th May 2019 for RFC0018439 -- This api returns the list of Dormant Users who have not accessed the Schema application more than 90 days.
        public async Task<HashSet<Dictionary<string, object>>> GetUsageTrackingDormantUserAync(string ReportOfficerID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_stats_dormantuser");

            var param1 = command.CreateParameter();
            param1.ParameterName = "proid";
            param1.DbType = DbType.String;
            param1.Value = ReportOfficerID.ToUpper();
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 3rd May 2019 for RFC0018439 -- Unblocks blocked Schema users. 
        public async Task<Dictionary<string, object>> UpdateUsageTrackingUnBlockUserAsync(List<UCONListItem> rowInfo, string ReportOfficerID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_ucon_blocked_user");

            var param1 = command.CreateParameter();
            param1.ParameterName = "puserid";
            param1.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].userid))
                param1.Value = rowInfo[0].userid.ToUpper();
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "punblocked_by";
            param2.DbType = DbType.String;
            if (!string.IsNullOrEmpty(ReportOfficerID))
                param2.Value = ReportOfficerID.ToUpper();
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "rowcount";
            param3.DbType = DbType.Int16;
            param3.Direction = ParameterDirection.Output;
            command.Parameters.Add(param3);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateDataAsync(command);
            return result;
        }
        //added by Sandip on 5th May 2019 for RFC0018439 -- Updates new user thresold value
        public async Task<Dictionary<string, object>> UpdateUsageTrackingUserThresholdAsync(List<UCONListItem> rowInfo, string ReportOfficerID, int isAdmin)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_ucon_stats_userthreshold");

            var param1 = command.CreateParameter();
            param1.ParameterName = "proid";
            param1.DbType = DbType.String;
            if (!string.IsNullOrEmpty(ReportOfficerID))
                param1.Value = ReportOfficerID.ToUpper();
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "puserid";
            param2.DbType = DbType.String;
            param2.Value = rowInfo[0].userid.ToUpper();
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pworkingzone";
            param3.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].workingzone))
                param3.Value = rowInfo[0].workingzone;
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "pmainzonegisthreshold";
            param4.DbType = DbType.Int32;
            if (rowInfo[0].mainzonegisthreshold != null)
                param4.Value = rowInfo[0].mainzonegisthreshold;
            else
                param4.Value = DBNull.Value;
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "potherzonegisthreshold";
            param5.DbType = DbType.Int32;
            if (rowInfo[0].otherzonegisthreshold != null)
                param5.Value = rowInfo[0].otherzonegisthreshold;
            else
                param5.Value = DBNull.Value;
            command.Parameters.Add(param5);

            var param6 = command.CreateParameter();
            param6.ParameterName = "pmainzonesldthreshold";
            param6.DbType = DbType.Int32;
            if (rowInfo[0].mainzonesldthreshold != null)
                param6.Value = rowInfo[0].mainzonesldthreshold;
            else
                param6.Value = DBNull.Value;
            command.Parameters.Add(param6);

            var param7 = command.CreateParameter();
            param7.ParameterName = "potherzonesldthreshold";
            param7.DbType = DbType.Int32;
            if (rowInfo[0].otherzonesldthreshold != null)
                param7.Value = rowInfo[0].otherzonesldthreshold;
            else
                param7.Value = DBNull.Value;
            command.Parameters.Add(param7);

            var param8 = command.CreateParameter();
            param8.ParameterName = "pofficehourfrom";
            param8.DbType = DbType.Int32;
            if (rowInfo[0].officehourfrom != null)
                param8.Value = rowInfo[0].officehourfrom;
            else
                param8.Value = DBNull.Value;
            command.Parameters.Add(param8);

            var param9 = command.CreateParameter();
            param9.ParameterName = "pofficehourto";
            param9.DbType = DbType.Int32;
            if (rowInfo[0].officehourto != null)
                param9.Value = rowInfo[0].officehourto;
            else
                param9.Value = DBNull.Value;
            command.Parameters.Add(param9);

            var param10 = command.CreateParameter();
            param10.ParameterName = "padminflag";
            param10.DbType = DbType.Int32;
            param10.Value = isAdmin;
            command.Parameters.Add(param10);

            var param11 = command.CreateParameter();
            param11.ParameterName = "rowcount";
            param11.DbType = DbType.Int16;
            param11.Direction = ParameterDirection.Output;
            command.Parameters.Add(param11);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateDataAsync(command);
            return result;
        }
        //added by Sandip on 17th May 2019 for RFC0018439 -- Updates new section thresold value
        public async Task<Dictionary<string, object>> UpdateUsageTrackingSectionThresholdAsync(List<UCONListItem> rowInfo, string ReportOfficerID, int isAdmin)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_ucon_stats_sectionthreshold");

            var param1 = command.CreateParameter();
            param1.ParameterName = "proid";
            param1.DbType = DbType.String;
            if (!string.IsNullOrEmpty(ReportOfficerID))
                param1.Value = ReportOfficerID.ToUpper();
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pdsid";
            param2.DbType = DbType.Int64;
            if (rowInfo[0].dsid != null)
                param2.Value = Convert.ToInt32(rowInfo[0].dsid);
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pworkingzone";
            param3.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].workingzone))
                param3.Value = rowInfo[0].workingzone;
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "pmainzonegisthreshold";
            param4.DbType = DbType.Int32;
            if (rowInfo[0].mainzonegisthreshold != null)
                param4.Value = rowInfo[0].mainzonegisthreshold;
            else
                param4.Value = DBNull.Value;
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "potherzonegisthreshold";
            param5.DbType = DbType.Int32;
            if (rowInfo[0].otherzonegisthreshold != null)
                param5.Value = rowInfo[0].otherzonegisthreshold;
            else
                param5.Value = DBNull.Value;
            command.Parameters.Add(param5);

            var param6 = command.CreateParameter();
            param6.ParameterName = "pmainzonesldthreshold";
            param6.DbType = DbType.Int32;
            if (rowInfo[0].mainzonesldthreshold != null)
                param6.Value = rowInfo[0].mainzonesldthreshold;
            else
                param6.Value = DBNull.Value;
            command.Parameters.Add(param6);

            var param7 = command.CreateParameter();
            param7.ParameterName = "potherzonesldthreshold";
            param7.DbType = DbType.Int32;
            if (rowInfo[0].otherzonesldthreshold != null)
                param7.Value = rowInfo[0].otherzonesldthreshold;
            else
                param7.Value = DBNull.Value;
            command.Parameters.Add(param7);

            var param8 = command.CreateParameter();
            param8.ParameterName = "pofficehourfrom";
            param8.DbType = DbType.Int32;
            if (rowInfo[0].officehourfrom != null)
                param8.Value = rowInfo[0].officehourfrom;
            else
                param8.Value = DBNull.Value;
            command.Parameters.Add(param8);

            var param9 = command.CreateParameter();
            param9.ParameterName = "pofficehourto";
            param9.DbType = DbType.Int32;
            if (rowInfo[0].officehourto != null)
                param9.Value = rowInfo[0].officehourto;
            else
                param9.Value = DBNull.Value;
            command.Parameters.Add(param9);

            var param10 = command.CreateParameter();
            param10.ParameterName = "padminflag";
            param10.DbType = DbType.Int32;
            param10.Value = isAdmin;
            command.Parameters.Add(param10);

            var param11 = command.CreateParameter();
            param11.ParameterName = "rowcount";
            param11.DbType = DbType.Int16;
            param11.Direction = ParameterDirection.Output;
            command.Parameters.Add(param11);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateDataAsync(command);
            return result;
        }
        //added by Sandip on 22th May 2019 for RFC0018439 -- Deletes the updated user threshold value and resets it back to original thresold limit.
        public async Task<Dictionary<string, object>> DeleteUsageTrackingUserThresholdAsync(List<UCONListItem> rowInfo, string ReportOfficerID, int isAdmin)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_delete_ucon_stats_userthreshold");

            var param1 = command.CreateParameter();
            param1.ParameterName = "proid";
            param1.DbType = DbType.String;
            if (!string.IsNullOrEmpty(ReportOfficerID))
                param1.Value = ReportOfficerID.ToUpper();
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "puserid";
            param2.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].userid))
                param2.Value = rowInfo[0].userid.ToUpper();
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "padminflag";
            param3.DbType = DbType.Int32;
            param3.Value = isAdmin;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "rowcount";
            param4.DbType = DbType.Int16;
            param4.Direction = ParameterDirection.Output;
            command.Parameters.Add(param4);            

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateDataAsync(command);
            return result;
        }
        //added by Sandip on 24th May 2019 for RFC0018439 -- Get complete list of Schema batch jobs and their status
        public async Task<HashSet<Dictionary<string, object>>> GetUsageTrackingJobInfoAync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_jobinfo");
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 25th May 2019 for RFC0018439 -- Resets the 'PROCESSING' or 'FAIL' batch job status to 'PENDING'
        public async Task<Dictionary<string, object>> UpdateUsageTrackingJobInfoAsync()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_ucon_jobinfo_status");

            var param1 = command.CreateParameter();
            param1.ParameterName = "rowcount";
            param1.DbType = DbType.Int16;
            param1.Direction = ParameterDirection.Output;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateAdminDataAsync(command);
            return result;
        }
        public async Task<HashSet<Dictionary<string, object>>> GetUserEmailIDAsync(string UserID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_stats_useremail");

            var param1 = command.CreateParameter();
            param1.ParameterName = "puserid";
            param1.DbType = DbType.String;
            param1.Value = UserID.ToUpper().ToString();
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetEmailIDAsync(string ReportOfficerID, string DirectFlag, bool NextLevelFlag)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_stats_userhead");

            var param1 = command.CreateParameter();
            param1.ParameterName = "puserid";
            param1.DbType = DbType.String;
            param1.Value = ReportOfficerID.ToUpper();
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pdirectflag";
            param2.DbType = DbType.Int16;
            param2.Value = Convert.ToInt16(DirectFlag);
            command.Parameters.Add(param2);

            /*var param3 = command.CreateParameter();
            param3.ParameterName = "pnextlevelflag";
            param3.DbType = DbType.Boolean;
            param3.Value = NextLevelFlag;
            command.Parameters.Add(param3);*/

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 19th Aug 2019 for RFC0020665 -- This api returns list of usernames on click of each type in summary of Schema Stats.
        public async Task<HashSet<Dictionary<string, object>>> GetUsageTrackingListOfUsernameAsync(string ReportOfficerID, Int16 Type, string FromDate, string ToDate, Int16 DirectFlag = 0)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_listusername_fromto");

            var param1 = command.CreateParameter();
            param1.ParameterName = "proid";
            param1.DbType = DbType.String;
            param1.Value = ReportOfficerID.ToUpper();
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "ptype";
            param2.DbType = DbType.Int16;
            param2.Value = Type;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pfromdate";
            param3.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date;
            param3.Value = FromDate;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "ptodate";
            param4.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date;
            param4.Value = ToDate;
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "pdirectflag";
            param5.DbType = DbType.Int16;
            if (DirectFlag == 1)
                param5.Value = DirectFlag;
            else
                param5.Value = DBNull.Value;
            command.Parameters.Add(param5);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }

        public async Task<HashSet<Dictionary<string, object>>> GetUsageTrackingBlockedUserHistoryAsync(string ReportOfficerID, Int16 Type, Int16 DirectFlag = 0)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_stats_blockeduserhistory");

            var param1 = command.CreateParameter();
            param1.ParameterName = "proid";
            param1.DbType = DbType.String;
            param1.Value = ReportOfficerID.ToUpper();
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "ptype";
            param2.DbType = DbType.Int16;
            param2.Value = Type;
            command.Parameters.Add(param2);

            if (DirectFlag == 0)
            { }
            else
            {
                var param3 = command.CreateParameter();
                param3.ParameterName = "pdirectflag";
                param3.DbType = DbType.Int16;
                param3.Value = DirectFlag;
                command.Parameters.Add(param3);
            }
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public static string GetClientIp(HttpRequest request)
        {
            if (request == null)
            {
                return "NA";
            }

            string remoteAddr = request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrWhiteSpace(remoteAddr))
            {
                remoteAddr = request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
                // the HTTP_X_FORWARDED_FOR may contain an array of IP, this can happen if you connect through a proxy.
                string[] ipRange = remoteAddr.Split(',');

                remoteAddr = ipRange[ipRange.Length - 1];
            }
            return remoteAddr;
        }
    }
}
