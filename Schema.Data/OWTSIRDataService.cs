using Schema.Core.Data;
using Schema.Core.Models;
using Schema.Core.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Schema.Data
{
    public class OWTSIRDataService : PgDbBase, IOWTSIRDataService
    {
        public OWTSIRDataService(IConfigService configService, ILoggingService loggingService)
          : base(configService, loggingService)
        { }
        public async Task<HashSet<Dictionary<string, object>>> GetCompletedTasksAync(string Officer, string FromDate, string ToDate)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_completed_task_owts_ir_readings");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pofficer";
            param1.DbType = DbType.String;
            param1.Value = Officer.ToUpper();
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pfromdate";
            param2.DbType = DbType.DateTime;
            if (!string.IsNullOrEmpty(FromDate) && !string.IsNullOrWhiteSpace(FromDate))
            {
                DateTime dtFromDate = DateTime.Parse(FromDate, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                param2.Value = dtFromDate;
            }
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "ptodate";
            param3.DbType = DbType.DateTime;
            if (!string.IsNullOrEmpty(ToDate) && !string.IsNullOrWhiteSpace(ToDate))
            {
                DateTime dtToDate = DateTime.Parse(ToDate, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                param3.Value = dtToDate;
            }
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetPendingItemsAync(string Username)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_pending_owts_ir_readings");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pusername";
            param1.DbType = DbType.String;
            param1.Value = Username.ToUpper();
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetReadingDetailsAsync(string ID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_owts_ir_details");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pid";
            param1.DbType = DbType.String;
            param1.Value = ID;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetPrevReadingsAsync(string SrcStationName, string SrcStationBoardNo, string SrcStationPanelNo, string TgtStationName, string TgtStationBoardNo, string TgtStationPanelNo)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_owts_ir_previous_readings");

            var param1 = command.CreateParameter();
            param1.ParameterName = "psrcstationname";
            param1.DbType = DbType.String;
            if (!string.IsNullOrEmpty(SrcStationName))
                param1.Value = SrcStationName;
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "psrcstationboardno";
            param2.DbType = DbType.Int16;
            if (SrcStationBoardNo != null)
                param2.Value = SrcStationBoardNo;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "psrcstationpanelno";
            param3.DbType = DbType.Int16;
            if (SrcStationPanelNo != null)
                param3.Value = SrcStationPanelNo;
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "ptgtstationname";
            param4.DbType = DbType.String;
            if (!string.IsNullOrEmpty(TgtStationName))
                param4.Value = TgtStationName;
            else
                param4.Value = DBNull.Value;
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "ptgtstationboardno";
            param5.DbType = DbType.Int16;
            if (TgtStationBoardNo != null)
                param5.Value = TgtStationBoardNo;
            else
                param5.Value = DBNull.Value;
            command.Parameters.Add(param5);

            var param6 = command.CreateParameter();
            param6.ParameterName = "ptgtstationpanelno";
            param6.DbType = DbType.Int16;
            if (TgtStationPanelNo != null)
                param6.Value = TgtStationPanelNo;
            else
                param6.Value = DBNull.Value;
            command.Parameters.Add(param6);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetReadingWithinExtentAsync(string Geometry)
        {
            var coords = Geometry.Split(',');
            string wkt = @"POLYGON ((" + coords[0] + " " + coords[1] + "," + coords[2] + " " + coords[1] + "," + coords[2] + " " + coords[3] + "," + coords[0] + " " + coords[3] + "," + coords[0] + " " + coords[1] + "))";

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_owts_ir_readings_within_geometry");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pgeometry";
            param1.DbType = DbType.String;
            param1.Value = wkt;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetOWTSIREditors()
        {

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_owts_ir_editors");
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);

            /*Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select UPPER(loginname) as userid, displayname, email from sde.sp_gems_activedirectoryinfo where UPPER(memberof) LIKE '%ELECTRICITY OWTSIR WRITER%' order by displayname";*/
        }
        public async Task<HashSet<Dictionary<string, object>>> GetApprovalOfficersAsync(string Username)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_owts_ir_approval_officers");

            var param1 = command.CreateParameter();
            param1.ParameterName = "puserid";
            param1.DbType = DbType.String;
            param1.Value = Username.ToUpper();
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);

            /*Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select UPPER(loginname) as userid, displayname, email from sde.sp_gems_activedirectoryinfo where UPPER(memberof) LIKE '%ELECTRICITY OWTSIR APPROVER%' and upper(loginname) !='" + Username.ToUpper() + "' order by displayname";*/
           
        }
        public async Task<Dictionary<string, object>> ApprovalProcessAsync(string ID, int statusCode, string approverRemarks)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_owts_ir_approval_process");
            var param1 = command.CreateParameter();
            param1.ParameterName = "pstatus";
            param1.DbType = DbType.Int16;
            param1.Value = statusCode;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pofficerremarks";
            param2.DbType = DbType.String;
            param2.Value = approverRemarks;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pid";
            param3.DbType = DbType.Int64;
            param3.Value = Convert.ToInt64(ID);
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
        public async Task<Dictionary<string, object>> AddReadingAsync(List<OWTSIRItem> rowInfo, string Username)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_add_owts_ir_readings");

            var param0 = command.CreateParameter();
            param0.ParameterName = "preadingdate";
            param0.DbType = DbType.DateTime;
            if (rowInfo[0].readingdate != null)
            {
                string strReadingDate = rowInfo[0].readingdate.ToString();
                if (!string.IsNullOrEmpty(strReadingDate) && !string.IsNullOrWhiteSpace(strReadingDate))
                {
                    DateTime readingDate = DateTime.Parse(strReadingDate, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                    param0.Value = readingDate;
                }
            }
            else
                param0.Value = DBNull.Value;
            command.Parameters.Add(param0);

            var param1 = command.CreateParameter();
            param1.ParameterName = "pofficer";
            param1.DbType = DbType.String;
            param1.Value = rowInfo[0].officer;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "passigndate";
            param2.DbType = DbType.DateTime;
            param2.Value = DateTime.Now;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pmaxpd";
            param3.DbType = DbType.String;
            if (rowInfo[0].maxpd != null)
                param3.Value = rowInfo[0].maxpd.Replace("'", "''");
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "pcomments";
            param4.DbType = DbType.String;
            if (rowInfo[0].comments != null)
                param4.Value = rowInfo[0].comments.Replace("'", "''");
            else
                param4.Value = DBNull.Value;
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "pirafterb";
            param5.DbType = DbType.Int64;
            if (rowInfo[0].irafterb != null)
                param5.Value = rowInfo[0].irafterb;
            else
                param5.Value = DBNull.Value;
            command.Parameters.Add(param5);

            var param6 = command.CreateParameter();
            param6.ParameterName = "piraftery";
            param6.DbType = DbType.Int64;
            if (rowInfo[0].iraftery != null)
                param6.Value = rowInfo[0].iraftery;
            else
                param6.Value = DBNull.Value;
            command.Parameters.Add(param6);

            var param7 = command.CreateParameter();
            param7.ParameterName = "pirafterr";
            param7.DbType = DbType.Int64;
            if (rowInfo[0].irafterr != null)
                param7.Value = rowInfo[0].irafterr;
            else
                param7.Value = DBNull.Value;
            command.Parameters.Add(param7);

            var param8 = command.CreateParameter();
            param8.ParameterName = "pirbeforeb";
            param8.DbType = DbType.Int64;
            if (rowInfo[0].irbeforeb != null)
                param8.Value = rowInfo[0].irbeforeb;
            else
                param8.Value = DBNull.Value;
            command.Parameters.Add(param8);

            var param9 = command.CreateParameter();
            param9.ParameterName = "pirbeforey";
            param9.DbType = DbType.Int64;
            if (rowInfo[0].irbeforey != null)
                param9.Value = rowInfo[0].irbeforey;
            else
                param9.Value = DBNull.Value;
            command.Parameters.Add(param9);

            var param10 = command.CreateParameter();
            param10.ParameterName = "pirbeforer";
            param10.DbType = DbType.Int64;
            if (rowInfo[0].irbeforer != null)
                param10.Value = rowInfo[0].irbeforer;
            else
                param10.Value = DBNull.Value;
            command.Parameters.Add(param10);

            var param11 = command.CreateParameter();
            param11.ParameterName = "pcreateuser";
            param11.DbType = DbType.String;
            param11.Value = Username.ToUpper();
            command.Parameters.Add(param11);

            var param12 = command.CreateParameter();
            param12.ParameterName = "pfeederno";
            param12.DbType = DbType.Int16;
            if (rowInfo[0].feederno != null)
                param12.Value = rowInfo[0].feederno;
            else
                param12.Value = DBNull.Value;
            command.Parameters.Add(param12);

            var param13 = command.CreateParameter();
            param13.ParameterName = "pjobscope";
            param13.DbType = DbType.String;
            if (rowInfo[0].jobscope != null)
                param13.Value = rowInfo[0].jobscope.Replace("'", "''");
            else
                param13.Value = DBNull.Value;
            command.Parameters.Add(param13);

            var param14 = command.CreateParameter();
            param14.ParameterName = "pstatuscode";
            param14.DbType = DbType.Int16;
            param14.Value = 0;
            command.Parameters.Add(param14);

            var param15 = command.CreateParameter();
            param15.ParameterName = "pflag";
            param15.DbType = DbType.Int16;
            param15.Value = rowInfo[0].owtsirflag;
            command.Parameters.Add(param15);

            var param23 = command.CreateParameter();
            param23.ParameterName = "pdistance";
            param23.DbType = DbType.Double;
            if (rowInfo[0].distance != null)
                param23.Value = rowInfo[0].distance;
            else
                param23.Value = DBNull.Value;
            command.Parameters.Add(param23);

            var param16 = command.CreateParameter();
            param16.ParameterName = "pselectedinfoid";
            param16.DbType = DbType.Int64;
            if (rowInfo[0].selectedinfoid != null)
                param16.Value = rowInfo[0].selectedinfoid;
            else
                param16.Value = DBNull.Value;
            command.Parameters.Add(param16);

            var param17 = command.CreateParameter();
            param17.ParameterName = "pselectedfcid";
            param17.DbType = DbType.Int16;
            if (rowInfo[0].selectedfcid != null)
                param17.Value = rowInfo[0].selectedfcid;
            else
                param17.Value = DBNull.Value;
            command.Parameters.Add(param17);

            var param18 = command.CreateParameter();
            param18.ParameterName = "pvoltage";
            param18.DbType = DbType.Int16;
            if (rowInfo[0].voltage != null)
                param18.Value = rowInfo[0].voltage;
            else
                param18.Value = DBNull.Value;
            command.Parameters.Add(param18);

            var param19 = command.CreateParameter();
            param19.ParameterName = "psrcstationname";
            param19.DbType = DbType.String;
            if (rowInfo[0].srcstationname != null)
                param19.Value = rowInfo[0].srcstationname;
            else
                param19.Value = DBNull.Value;
            command.Parameters.Add(param19);

            var param20 = command.CreateParameter();
            param20.ParameterName = "psrcstationboardno";
            param20.DbType = DbType.Int16;
            if (rowInfo[0].srcstationboardno != null)
                param20.Value = rowInfo[0].srcstationboardno;
            else
                param20.Value = DBNull.Value;
            command.Parameters.Add(param20);

            var param21 = command.CreateParameter();
            param21.ParameterName = "psrcstationpanelno";
            param21.DbType = DbType.Int16;
            if (rowInfo[0].srcstationpanelno != null)
                param21.Value = rowInfo[0].srcstationpanelno;
            else
                param21.Value = DBNull.Value;
            command.Parameters.Add(param21);

            var param22 = command.CreateParameter();
            param22.ParameterName = "ptgtstationname";
            param22.DbType = DbType.String;
            if (rowInfo[0].tgtstationname != null)
                param22.Value = rowInfo[0].tgtstationname;
            else
                param22.Value = DBNull.Value;
            command.Parameters.Add(param22);

            var param25 = command.CreateParameter();
            param25.ParameterName = "ptgtstationboardno";
            param25.DbType = DbType.Int16;
            if (rowInfo[0].tgtstationboardno != null)
                param25.Value = rowInfo[0].tgtstationboardno;
            else
                param25.Value = DBNull.Value;
            command.Parameters.Add(param25);

            var param26 = command.CreateParameter();
            param26.ParameterName = "ptgtstationpanelno";
            param26.DbType = DbType.Int16;
            if (rowInfo[0].tgtstationpanelno != null)
                param26.Value = rowInfo[0].tgtstationpanelno;
            else
                param26.Value = DBNull.Value;
            command.Parameters.Add(param26);

            var param24 = command.CreateParameter();
            param24.ParameterName = "rowcount";
            param24.DbType = DbType.Int16;
            param24.Direction = ParameterDirection.Output;
            command.Parameters.Add(param24);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateDataAsync(command);
            return result;
        }
        public async Task<Dictionary<string, object>> UpdateReadingAsync(List<OWTSIRItem> rowInfo, string Username)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            //before updating main table take a backup in transaction table
            Npgsql.NpgsqlCommand command1 = new Npgsql.NpgsqlCommand("swift.api_update_owts_ir_readings_transaction");
            var param = command1.CreateParameter();
            param.ParameterName = "pid";
            param.DbType = DbType.Int64;
            param.Value = rowInfo[0].id;
            command1.Parameters.Add(param);

            var paramx = command1.CreateParameter();
            paramx.ParameterName = "rowcount";
            paramx.DbType = DbType.Int16;
            paramx.Direction = ParameterDirection.Output;
            command1.Parameters.Add(paramx);

            command1.CommandType = CommandType.StoredProcedure;
            result = await UpdateDataAsync(command1);
            if (result.ContainsValue("Success"))
            { }

            //update main table
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_owts_ir_readings");

            var param0 = command.CreateParameter();
            param0.ParameterName = "preadingdate";
            param0.DbType = DbType.DateTime;
            if (rowInfo[0].readingdate != null)
            {
                string strReadingDate = rowInfo[0].readingdate.ToString();
                if (!string.IsNullOrEmpty(strReadingDate) && !string.IsNullOrWhiteSpace(strReadingDate))
                {
                    DateTime readingDate = DateTime.Parse(strReadingDate, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                    param0.Value = readingDate;
                }
            }
            else
                param0.Value = DBNull.Value;
            command.Parameters.Add(param0);

            var param1 = command.CreateParameter();
            param1.ParameterName = "pofficer";
            param1.DbType = DbType.String;
            param1.Value = rowInfo[0].officer;
            command.Parameters.Add(param1);

            var param14 = command.CreateParameter();
            param14.ParameterName = "pofficerremarks";
            param14.DbType = DbType.String;
            param14.Value = DBNull.Value;
            command.Parameters.Add(param14);

            var param15 = command.CreateParameter();
            param15.ParameterName = "passigndate";
            param15.DbType = DbType.DateTime;
            param15.Value = DateTime.Now;
            command.Parameters.Add(param15);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pmaxpd";
            param3.DbType = DbType.String;
            param3.Value = rowInfo[0].maxpd.Replace("'", "''");
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "pcomments";
            param4.DbType = DbType.String;
            param4.Value = rowInfo[0].comments.Replace("'", "''");
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "pirafterb";
            param5.DbType = DbType.Int64;
            if (rowInfo[0].irafterb != null)
                param5.Value = rowInfo[0].irafterb;
            else
                param5.Value = DBNull.Value;
            command.Parameters.Add(param5);

            var param6 = command.CreateParameter();
            param6.ParameterName = "piraftery";
            param6.DbType = DbType.Int64;
            if (rowInfo[0].iraftery != null)
                param6.Value = rowInfo[0].iraftery;
            else
                param6.Value = DBNull.Value;
            command.Parameters.Add(param6);

            var param7 = command.CreateParameter();
            param7.ParameterName = "pirafterr";
            param7.DbType = DbType.Int64;
            if (rowInfo[0].irafterr != null)
                param7.Value = rowInfo[0].irafterr;
            else
                param7.Value = DBNull.Value;
            command.Parameters.Add(param7);

            var param8 = command.CreateParameter();
            param8.ParameterName = "pirbeforeb";
            param8.DbType = DbType.Int64;
            if (rowInfo[0].irbeforeb != null)
                param8.Value = rowInfo[0].irbeforeb;
            else
                param8.Value = DBNull.Value;
            command.Parameters.Add(param8);

            var param9 = command.CreateParameter();
            param9.ParameterName = "pirbeforey";
            param9.DbType = DbType.Int64;
            if (rowInfo[0].irbeforey != null)
                param9.Value = rowInfo[0].irbeforey;
            else
                param9.Value = DBNull.Value;
            command.Parameters.Add(param9);

            var param10 = command.CreateParameter();
            param10.ParameterName = "pirbeforer";
            param10.DbType = DbType.Int64;
            if (rowInfo[0].irbeforer != null)
                param10.Value = rowInfo[0].irbeforer;
            else
                param10.Value = DBNull.Value;
            command.Parameters.Add(param10);

            var param11 = command.CreateParameter();
            param11.ParameterName = "pcreateuser";
            param11.DbType = DbType.String;
            param11.Value = Username.ToUpper();
            command.Parameters.Add(param11);

            var param12 = command.CreateParameter();
            param12.ParameterName = "plastuser";
            param12.DbType = DbType.String;
            param12.Value = Username.ToUpper();
            command.Parameters.Add(param12);

            var param13 = command.CreateParameter();
            param13.ParameterName = "pdatemodified";
            param13.DbType = DbType.DateTime;
            param13.Value = DateTime.Now;
            command.Parameters.Add(param13);

            var param16 = command.CreateParameter();
            param16.ParameterName = "pfeederno";
            param16.DbType = DbType.Int16;
            if (rowInfo[0].feederno != null)
                param16.Value = rowInfo[0].feederno;
            else
                param16.Value = DBNull.Value;
            command.Parameters.Add(param16);

            var param17 = command.CreateParameter();
            param17.ParameterName = "pjobscope";
            param17.DbType = DbType.String;
            param17.Value = rowInfo[0].jobscope.Replace("'", "''");
            command.Parameters.Add(param17);

            var param18 = command.CreateParameter();
            param18.ParameterName = "pstatuscode";
            param18.DbType = DbType.Int16;
            param18.Value = 0;
            command.Parameters.Add(param18);

            var param19 = command.CreateParameter();
            param19.ParameterName = "pflag";
            param19.DbType = DbType.Int16;
            param19.Value = rowInfo[0].owtsirflag;
            command.Parameters.Add(param19);

            var param20 = command.CreateParameter();
            param20.ParameterName = "pdistance";
            param20.DbType = DbType.Double;
            if (rowInfo[0].distance != null)
                param20.Value = rowInfo[0].distance;
            else
                param20.Value = DBNull.Value;
            command.Parameters.Add(param20);

            var param21 = command.CreateParameter();
            param21.ParameterName = "pselectedinfoid";
            param21.DbType = DbType.Int64;
            if (rowInfo[0].selectedinfoid != null)
                param21.Value = rowInfo[0].selectedinfoid;
            else
                param21.Value = DBNull.Value;
            command.Parameters.Add(param21);

            var param22 = command.CreateParameter();
            param22.ParameterName = "pselectedfcid";
            param22.DbType = DbType.Int16;
            if (rowInfo[0].selectedfcid != null)
                param22.Value = rowInfo[0].selectedfcid;
            else
                param22.Value = DBNull.Value;
            command.Parameters.Add(param22);

            var param23 = command.CreateParameter();
            param23.ParameterName = "pvoltage";
            param23.DbType = DbType.Int16;
            if (rowInfo[0].voltage != null)
                param23.Value = rowInfo[0].voltage;
            else
                param23.Value = DBNull.Value;
            command.Parameters.Add(param23);

            var param24 = command.CreateParameter();
            param24.ParameterName = "psrcstationname";
            param24.DbType = DbType.String;
            if (rowInfo[0].srcstationname != null)
                param24.Value = rowInfo[0].srcstationname;
            else
                param24.Value = DBNull.Value;
            command.Parameters.Add(param24);

            var param25 = command.CreateParameter();
            param25.ParameterName = "psrcstationboardno";
            param25.DbType = DbType.Int16;
            if (rowInfo[0].srcstationboardno != null)
                param25.Value = rowInfo[0].srcstationboardno;
            else
                param25.Value = DBNull.Value;
            command.Parameters.Add(param25);

            var param26 = command.CreateParameter();
            param26.ParameterName = "psrcstationpanelno";
            param26.DbType = DbType.Int16;
            if (rowInfo[0].srcstationpanelno != null)
                param26.Value = rowInfo[0].srcstationpanelno;
            else
                param26.Value = DBNull.Value;
            command.Parameters.Add(param26);

            var param27 = command.CreateParameter();
            param27.ParameterName = "ptgtstationname";
            param27.DbType = DbType.String;
            if (rowInfo[0].tgtstationname != null)
                param27.Value = rowInfo[0].tgtstationname;
            else
                param27.Value = DBNull.Value;
            command.Parameters.Add(param27);

            var param28 = command.CreateParameter();
            param28.ParameterName = "ptgtstationboardno";
            param28.DbType = DbType.Int16;
            if (rowInfo[0].tgtstationboardno != null)
                param28.Value = rowInfo[0].tgtstationboardno;
            else
                param28.Value = DBNull.Value;
            command.Parameters.Add(param28);

            var param29 = command.CreateParameter();
            param29.ParameterName = "ptgtstationpanelno";
            param29.DbType = DbType.Int16;
            if (rowInfo[0].tgtstationpanelno != null)
                param29.Value = rowInfo[0].tgtstationpanelno;
            else
                param29.Value = DBNull.Value;
            command.Parameters.Add(param29);

            var param30 = command.CreateParameter();
            param30.ParameterName = "pid";
            param30.DbType = DbType.Int64;
            param30.Value = rowInfo[0].id;
            command.Parameters.Add(param30);

            var param31 = command.CreateParameter();
            param31.ParameterName = "rowcount";
            param31.DbType = DbType.Int16;
            param31.Direction = ParameterDirection.Output;
            command.Parameters.Add(param31);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateDataAsync(command);
            return result;
        }
        public async Task<HashSet<Dictionary<string, object>>> GetEmailIDAsync(string ApproverName, Int64 ID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_owts_ir_emailids");

            if (!string.IsNullOrEmpty(ApproverName))
            {
                var param1 = command.CreateParameter();
                param1.ParameterName = "papprovername";
                param1.DbType = DbType.String;
                param1.Value = ApproverName.ToString();
                command.Parameters.Add(param1);
            }
            else
            {
                var param1 = command.CreateParameter();
                param1.ParameterName = "pid";
                param1.DbType = DbType.Int64;
                param1.Value = ID;
                command.Parameters.Add(param1);
            }
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);

            /*string creationUser = string.Empty;

            if (string.IsNullOrEmpty(ApproverName))
            {
                Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
                command.CommandText = "select creationuser from swift.owts_ir_readings where ID = " + ID;
                HashSet<Dictionary<string, object>> value = await ReadDataAsync(command);
                foreach (Dictionary<string, object> item in (IEnumerable)value)
                {
                    if (item["creationuser"] != null)
                        creationUser = item["creationuser"].ToString();
                }
                ApproverName = creationUser;
            }

            Npgsql.NpgsqlCommand command1 = new Npgsql.NpgsqlCommand();
            command1.CommandText = "select email from sde.sp_gems_activedirectoryinfo where upper(loginname) ='" + ApproverName.ToUpper() + "'";*/           
        }
    }
}
