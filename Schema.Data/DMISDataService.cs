using Schema.Core.Data;
using Schema.Core.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema.Core.Models;
using System.Collections;

namespace Schema.Data
{
    public class DMISDataService : PgDbBase, IDMISDataService
    {
        public DMISDataService(IConfigService configService, ILoggingService loggingService)
          : base(configService, loggingService)
        { }
        public async Task<Dictionary<string, object>> CreateDMISPointAsync(List<DMISGasLeakItem> rowInfo, string JobID, string Username)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_create_dmis_leak_point");

            var param0 = command.CreateParameter();
            param0.ParameterName = "pjobid";
            param0.DbType = DbType.String;
            param0.Value = JobID;
            command.Parameters.Add(param0);

            var param1 = command.CreateParameter();
            param1.ParameterName = "pdmisno";
            param1.DbType = DbType.String;
            param1.Value = rowInfo[0].dmisno;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pactiontaken";
            param2.DbType = DbType.String;
            param2.Value = rowInfo[0].actiontaken;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pactiontakendesc";
            param3.DbType = DbType.String;
            param3.Value = rowInfo[0].actiontakendesc.Replace("'", "''");
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "pbuildingname";
            param4.DbType = DbType.String;
            param4.Value = rowInfo[0].buildingname.Replace("'", "''");
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "pcause";
            param5.DbType = DbType.String;
            param5.Value = rowInfo[0].causedesc.Replace("'", "''");
            command.Parameters.Add(param5);

            var param6 = command.CreateParameter();
            param6.ParameterName = "pcomcause";
            param6.DbType = DbType.String;
            param6.Value = rowInfo[0].causeoffeedback;
            command.Parameters.Add(param6);

            var param7 = command.CreateParameter();
            param7.ParameterName = "pleakclass";
            param7.DbType = DbType.String;
            param7.Value = rowInfo[0].leakclassification;
            command.Parameters.Add(param7);

            var param8 = command.CreateParameter();
            param8.ParameterName = "pcomclass";
            param8.DbType = DbType.String;
            param8.Value = rowInfo[0].comclassification;
            command.Parameters.Add(param8);

            var param9 = command.CreateParameter();
            param9.ParameterName = "pdatecomp";
            param9.DbType = DbType.DateTime;
            string strDateComp = rowInfo[0].datecompleted.ToString();
            if (!string.IsNullOrEmpty(strDateComp) && !string.IsNullOrWhiteSpace(strDateComp))
            {
                DateTime datecomp = DateTime.Parse(strDateComp, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                param9.Value = datecomp;
            }
            else
                param9.Value = DBNull.Value;
            command.Parameters.Add(param9);

            var param10 = command.CreateParameter();
            param10.ParameterName = "ptimeatt";
            param10.DbType = DbType.DateTime;
            string strTimeAtt = rowInfo[0].timeatt.ToString();
            if (!string.IsNullOrEmpty(strTimeAtt) && !string.IsNullOrWhiteSpace(strTimeAtt))
            {
                DateTime timeAtt = DateTime.Parse(strTimeAtt, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                param10.Value = timeAtt;
            }
            else
                param10.Value = DBNull.Value;
            command.Parameters.Add(param10);

            var param11 = command.CreateParameter();
            param11.ParameterName = "ptimerec";
            param11.DbType = DbType.DateTime;
            string strTimeRec = rowInfo[0].timerec.ToString();
            if (!string.IsNullOrEmpty(strTimeRec) && !string.IsNullOrWhiteSpace(strTimeRec))
            {
                DateTime timerec = DateTime.Parse(strTimeRec, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                param11.Value = timerec;
            }
            else
                param11.Value = DBNull.Value;
            command.Parameters.Add(param11);

            var param12 = command.CreateParameter();
            param12.ParameterName = "ptimerest";
            param12.DbType = DbType.DateTime;
            string strTimeRest = rowInfo[0].timerest.ToString();
            if (!string.IsNullOrEmpty(strTimeRest) && !string.IsNullOrWhiteSpace(strTimeRest))
            {
                DateTime timerest = DateTime.Parse(strTimeRest, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                param12.Value = timerest;
            }
            else
                param12.Value = DBNull.Value;
            command.Parameters.Add(param12);

            var param13 = command.CreateParameter();
            param13.ParameterName = "pdmisarea";
            param13.DbType = DbType.String;
            param13.Value = rowInfo[0].area;
            command.Parameters.Add(param13);

            var param14 = command.CreateParameter();
            param14.ParameterName = "pgangatt";
            param14.DbType = DbType.String;
            param14.Value = rowInfo[0].gangatt.Replace("'", "''");
            command.Parameters.Add(param14);

            var param15 = command.CreateParameter();
            param15.ParameterName = "pgasleakdetail";
            param15.DbType = DbType.String;
            param15.Value = rowInfo[0].gasleakdetails;
            command.Parameters.Add(param15);

            var param16 = command.CreateParameter();
            param16.ParameterName = "phseno";
            param16.DbType = DbType.String;
            param16.Value = rowInfo[0].hseno.Replace("'", "''");
            command.Parameters.Add(param16);

            var param18 = command.CreateParameter();
            param18.ParameterName = "plocdesc";
            param18.DbType = DbType.String;
            param18.Value = rowInfo[0].locdesc.Replace("'", "''");
            command.Parameters.Add(param18);

            var param19 = command.CreateParameter();
            param19.ParameterName = "plocleak";
            param19.DbType = DbType.String;
            param19.Value = rowInfo[0].locleak;
            command.Parameters.Add(param19);

            var param20 = command.CreateParameter();
            param20.ParameterName = "pmaterial";
            param20.DbType = DbType.String;
            param20.Value = rowInfo[0].material;
            command.Parameters.Add(param20);

            var param21 = command.CreateParameter();
            param21.ParameterName = "pfollowuprepair";
            param21.DbType = DbType.String;
            param21.Value = rowInfo[0].followuprepair;
            command.Parameters.Add(param21);

            var param22 = command.CreateParameter();
            param22.ParameterName = "pnatureofcom";
            param22.DbType = DbType.String;
            param22.Value = rowInfo[0].natureofcom;
            command.Parameters.Add(param22);

            var param23 = command.CreateParameter();
            param23.ParameterName = "pnoofcaller";
            param23.DbType = DbType.Int16;
            if (rowInfo[0].noofcaller != null)
                param23.Value = rowInfo[0].noofcaller;
            else
                param23.Value = DBNull.Value;
            command.Parameters.Add(param23);

            var param24 = command.CreateParameter();
            param24.ParameterName = "pnumofcustaffect";
            param24.DbType = DbType.Int16;
            if (rowInfo[0].noofcustaffected != null)
                param24.Value = rowInfo[0].noofcustaffected;
            else
                param24.Value = DBNull.Value;
            command.Parameters.Add(param24);

            var param25 = command.CreateParameter();
            param25.ParameterName = "ppipesize";
            param25.DbType = DbType.Int16;
            if (rowInfo[0].pipesize != null)
                param25.Value = rowInfo[0].pipesize;
            else
                param25.Value = DBNull.Value;
            command.Parameters.Add(param25);

            var param26 = command.CreateParameter();
            param26.ParameterName = "ppipetype";
            param26.DbType = DbType.String;
            param26.Value = rowInfo[0].pipetype;
            command.Parameters.Add(param26);

            var param27 = command.CreateParameter();
            param27.ParameterName = "ppressureregime";
            param27.DbType = DbType.String;
            param27.Value = rowInfo[0].pressureregime;
            command.Parameters.Add(param27);

            var param28 = command.CreateParameter();
            param28.ParameterName = "premarks";
            param28.DbType = DbType.String;
            param28.Value = rowInfo[0].remarks.Replace("'", "''");
            command.Parameters.Add(param28);

            var param29 = command.CreateParameter();
            param29.ParameterName = "prootcause";
            param29.DbType = DbType.String;
            param29.Value = rowInfo[0].rootcause;
            command.Parameters.Add(param29);

            var param30 = command.CreateParameter();
            param30.ParameterName = "psourcetype";
            param30.DbType = DbType.String;
            param30.Value = rowInfo[0].sourcetype;
            command.Parameters.Add(param30);

            var param31 = command.CreateParameter();
            param31.ParameterName = "pstreetname";
            param31.DbType = DbType.String;
            param31.Value = rowInfo[0].streetname.Replace("'", "''");
            command.Parameters.Add(param31);

            var param32 = command.CreateParameter();
            param32.ParameterName = "pinstallyear";
            param32.DbType = DbType.Int16;
            if (rowInfo[0].installyear != null)
                param32.Value = rowInfo[0].installyear;
            else
                param32.Value = DBNull.Value;
            command.Parameters.Add(param32);

            var param33 = command.CreateParameter();
            param33.ParameterName = "pcreationuser";
            param33.DbType = DbType.String;
            param33.Value = Username.ToUpper();
            command.Parameters.Add(param33);

            var param34 = command.CreateParameter();
            param34.ParameterName = "pstatus";
            param34.DbType = DbType.Int16;
            param34.Value = 0;
            command.Parameters.Add(param34);

            var param35 = command.CreateParameter();
            param35.ParameterName = "pisdeleted";
            param35.DbType = DbType.Int16;
            param35.Value = 0;
            command.Parameters.Add(param35);

            var param38 = command.CreateParameter();
            param38.ParameterName = "pglobalid";
            param38.DbType = DbType.String;
            param38.Value = "{" + Guid.NewGuid().ToString().ToUpper() + "}";
            command.Parameters.Add(param38);

            var param39 = command.CreateParameter();
            param39.ParameterName = "pshape";
            param39.DbType = DbType.String;
            param39.Value = rowInfo[0].geometry;
            command.Parameters.Add(param39);

            var param40 = command.CreateParameter();
            param40.ParameterName = "ptransmainguid";
            param40.DbType = DbType.String;
            if (rowInfo[0].transmainguid != null)
                param40.Value = rowInfo[0].transmainguid;
            else
                param40.Value = DBNull.Value;
            command.Parameters.Add(param40);

            var param41 = command.CreateParameter();
            param41.ParameterName = "pdistmainguid";
            param41.DbType = DbType.String;
            if (rowInfo[0].distmainguid != null)
                param41.Value = rowInfo[0].distmainguid;
            else
                param41.Value = DBNull.Value;
            command.Parameters.Add(param41);

            var param42 = command.CreateParameter();
            param42.ParameterName = "rowcount";
            param42.DbType = DbType.Int16;
            param42.Direction = ParameterDirection.Output;
            command.Parameters.Add(param42);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateDataAsync(command);
            result.Add("JobID", command.Parameters["pjobid"].Value);
            return result;
        }
        public async Task<HashSet<Dictionary<string, object>>> GetApprovalOfficersAsync(string Username)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_dmis_approval_officers");

            var param1 = command.CreateParameter();
            param1.ParameterName = "puserid";
            param1.DbType = DbType.String;
            param1.Value = Username.ToUpper();
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);

            /*Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select UPPER(loginname) as userid, displayname, email from sde.sp_gems_activedirectoryinfo where UPPER(memberof) LIKE '%DMIS APPROVER%' and upper(loginname) !='" + Username.ToUpper() + "' order by displayname";*/
        }
        public async Task<HashSet<Dictionary<string, object>>> GetDMISEditorsAsync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_dmis_editors");
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);

            /*Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select UPPER(loginname) as userid, displayname, email from sde.sp_gems_activedirectoryinfo where UPPER(memberof) LIKE '%DMIS WRITER%' order by displayname";*/

        }
        public async Task<HashSet<Dictionary<string, object>>> GetDMISPointsAsync(string Username)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_dmis_points");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pusername";
            param1.DbType = DbType.String;
            param1.Value = Username.ToUpper();
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetDMISpointsWithinExtentAsync(string Geometry, string Username)
        {
            var coords = Geometry.Split(',');
            string wkt = @"POLYGON ((" + coords[0] + " " + coords[1] + "," + coords[2] + " " + coords[1] + "," + coords[2] + " " + coords[3] + "," + coords[0] + " " + coords[3] + "," + coords[0] + " " + coords[1] + "))";

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_dmis_points_within_geometry");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pgeometry";
            param1.DbType = DbType.String;
            param1.Value = wkt;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pusername";
            param2.DbType = DbType.String;
            param2.Value = Username.ToUpper();
            command.Parameters.Add(param2);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);

        }
        public async Task<HashSet<Dictionary<string, object>>> GetDMISPointDetailsAsync(string OID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_dmis_point_details");

            var param1 = command.CreateParameter();
            param1.ParameterName = "poid";
            param1.DbType = DbType.String;
            param1.Value = OID;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<Dictionary<string, object>> SendToApproverAsync(Int64[] oidArr, string approverName)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_dmis_send_to_approver");

            var param1 = command.CreateParameter();
            param1.ParameterName = "papprovername";
            param1.DbType = DbType.String;
            param1.Value = approverName.ToUpper();
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "passigndate";
            param2.DbType = DbType.DateTime;
            param2.Value = DateTime.Now;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "poid";
            param3.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Bigint;
            param3.Value = oidArr;
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
        public async Task<Dictionary<string, object>> ApprovalProcessAsync(string objectID, int statusCode, string approverRemarks, string Username)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_dmis_approval_process");
            var param1 = command.CreateParameter();
            param1.ParameterName = "pstatus";
            param1.DbType = DbType.Int16;
            param1.Value = statusCode;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "papproverremarks";
            param2.DbType = DbType.String;
            param2.Value = approverRemarks;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "poid";
            param3.DbType = DbType.Int64;
            param3.Value = Convert.ToInt64(objectID);
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "pusername";
            param4.DbType = DbType.String;
            param4.Value = Username.ToUpper();
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "rowcount";
            param5.DbType = DbType.Int16;
            param5.Direction = ParameterDirection.Output;
            command.Parameters.Add(param5);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateDataAsync(command);
            return result;
        }
        public async Task<HashSet<Dictionary<string, object>>> GetEmailIDAsync(string ApproverName, Int64 ObjectID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_dmis_emailids");

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
                param1.ParameterName = "poid";
                param1.DbType = DbType.Int64;
                param1.Value = ObjectID;
                command.Parameters.Add(param1);
            }

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);

            /*string creationUser = string.Empty;
            if (string.IsNullOrEmpty(ApproverName))
            {
                Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
                command.CommandText = "select creationuser from swift.dmisgasleak where objectid =" + ObjectID;
                HashSet<Dictionary<string, object>> value = await ReadDataAsync(command);
                foreach (Dictionary<string, object> item in (IEnumerable)value)
                {
                    if (item["creationuser"] != null)
                        creationUser = item["creationuser"].ToString();
                }
                ApproverName = creationUser;
            }          
            Npgsql.NpgsqlCommand command1 = new Npgsql.NpgsqlCommand();
            command1.CommandText = "select email from sde.sp_gems_activedirectoryinfo where upper(loginname) ='" + ApproverName.ToUpper() + "'";
            return await ReadDataAsync(command1);*/
        }
        public async Task<Dictionary<string, object>> UpdateDMISPointAsync(List<DMISGasLeakItem> rowInfo, int objectID, string Username)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_dmis_leak_point");

            var param0 = command.CreateParameter();
            param0.ParameterName = "pjobid";
            param0.DbType = DbType.String;
            param0.Value = rowInfo[0].jobid;
            command.Parameters.Add(param0);

            var param1 = command.CreateParameter();
            param1.ParameterName = "pdmisno";
            param1.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].dmisno))
                param1.Value = rowInfo[0].dmisno;
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pactiontaken";
            param2.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].actiontaken))
                param2.Value = rowInfo[0].actiontaken;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pactiontakendesc";
            param3.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].actiontakendesc))
                param3.Value = rowInfo[0].actiontakendesc.Replace("'", "''");
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "pbuildingname";
            param4.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].buildingname))
                param4.Value = rowInfo[0].buildingname.Replace("'", "''");
            else
                param4.Value = DBNull.Value;
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "pcause";
            param5.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].causedesc))
                param5.Value = rowInfo[0].causedesc.Replace("'", "''");
            else
                param5.Value = DBNull.Value;
            command.Parameters.Add(param5);

            var param6 = command.CreateParameter();
            param6.ParameterName = "pcomcause";
            param6.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].causeoffeedback))
                param6.Value = rowInfo[0].causeoffeedback;
            else
                param6.Value = DBNull.Value;
            command.Parameters.Add(param6);

            var param7 = command.CreateParameter();
            param7.ParameterName = "pleakclass";
            param7.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].leakclassification))
                param7.Value = rowInfo[0].leakclassification;
            else
                param7.Value = DBNull.Value;
            command.Parameters.Add(param7);

            var param8 = command.CreateParameter();
            param8.ParameterName = "pcomclass";
            param8.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].comclassification))
                param8.Value = rowInfo[0].comclassification;
            else
                param8.Value = DBNull.Value;
            command.Parameters.Add(param8);

            var param9 = command.CreateParameter();
            param9.ParameterName = "pdatecomp";
            param9.DbType = DbType.DateTime;
            string strDateComp = rowInfo[0].datecompleted.ToString();
            if (!string.IsNullOrEmpty(strDateComp) && !string.IsNullOrWhiteSpace(strDateComp))
            {
                DateTime datecomp = DateTime.Parse(strDateComp, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                param9.Value = datecomp;
            }
            else
                param9.Value = DBNull.Value;
            command.Parameters.Add(param9);

            var param10 = command.CreateParameter();
            param10.ParameterName = "ptimeatt";
            param10.DbType = DbType.DateTime;
            string strTimeAtt = rowInfo[0].timeatt.ToString();
            if (!string.IsNullOrEmpty(strTimeAtt) && !string.IsNullOrWhiteSpace(strTimeAtt))
            {
                DateTime timeAtt = DateTime.Parse(strTimeAtt, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                param10.Value = timeAtt;
            }
            else
                param10.Value = DBNull.Value;
            command.Parameters.Add(param10);

            var param11 = command.CreateParameter();
            param11.ParameterName = "ptimerec";
            param11.DbType = DbType.DateTime;
            string strTimeRec = rowInfo[0].timerec.ToString();
            if (!string.IsNullOrEmpty(strTimeRec) && !string.IsNullOrWhiteSpace(strTimeRec))
            {
                DateTime timerec = DateTime.Parse(strTimeRec, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                param11.Value = timerec;
            }
            else
                param11.Value = DBNull.Value;
            command.Parameters.Add(param11);

            var param12 = command.CreateParameter();
            param12.ParameterName = "ptimerest";
            param12.DbType = DbType.DateTime;
            string strTimeRest = rowInfo[0].timerest.ToString();
            if (!string.IsNullOrEmpty(strTimeRest) && !string.IsNullOrWhiteSpace(strTimeRest))
            {
                DateTime timerest = DateTime.Parse(strTimeRest, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                param12.Value = timerest;
            }
            else
                param12.Value = DBNull.Value;
            command.Parameters.Add(param12);

            var param13 = command.CreateParameter();
            param13.ParameterName = "pdmisarea";
            param13.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].area))
                param13.Value = rowInfo[0].area;
            else
                param13.Value = DBNull.Value;
            command.Parameters.Add(param13);

            var param14 = command.CreateParameter();
            param14.ParameterName = "pgangatt";
            param14.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].gangatt))
                param14.Value = rowInfo[0].gangatt.Replace("'", "''");
            else
                param14.Value = DBNull.Value;
            command.Parameters.Add(param14);

            var param15 = command.CreateParameter();
            param15.ParameterName = "pgasleakdetail";
            param15.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].gasleakdetails))
                param15.Value = rowInfo[0].gasleakdetails.Replace("'", "''");
            else
                param15.Value = DBNull.Value;
            command.Parameters.Add(param15);

            var param16 = command.CreateParameter();
            param16.ParameterName = "phseno";
            param16.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].hseno))
                param16.Value = rowInfo[0].hseno.Replace("'", "''");
            else
                param16.Value = DBNull.Value;
            command.Parameters.Add(param16);

            var param18 = command.CreateParameter();
            param18.ParameterName = "plocdesc";
            param18.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].locdesc))
                param18.Value = rowInfo[0].locdesc.Replace("'", "''");
            else
                param18.Value = DBNull.Value;
            command.Parameters.Add(param18);

            var param19 = command.CreateParameter();
            param19.ParameterName = "plocleak";
            param19.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].locleak))
                param19.Value = rowInfo[0].locleak.Replace("'", "''");
            else
                param19.Value = DBNull.Value;
            param19.Value = rowInfo[0].locleak;
            command.Parameters.Add(param19);

            var param20 = command.CreateParameter();
            param20.ParameterName = "pmaterial";
            param20.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].material))
                param20.Value = rowInfo[0].material;
            else
                param20.Value = DBNull.Value;
            command.Parameters.Add(param20);

            var param21 = command.CreateParameter();
            param21.ParameterName = "pfollowuprepair";
            param21.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].followuprepair))
                param21.Value = rowInfo[0].followuprepair;
            else
                param21.Value = DBNull.Value;
            command.Parameters.Add(param21);

            var param22 = command.CreateParameter();
            param22.ParameterName = "pnatureofcom";
            param22.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].natureofcom))
                param22.Value = rowInfo[0].natureofcom;
            else
                param22.Value = DBNull.Value;
            command.Parameters.Add(param22);

            var param23 = command.CreateParameter();
            param23.ParameterName = "pnoofcaller";
            param23.DbType = DbType.Int16;
            if (rowInfo[0].noofcaller != null)
                param23.Value = rowInfo[0].noofcaller;
            else
                param23.Value = DBNull.Value;
            command.Parameters.Add(param23);

            var param24 = command.CreateParameter();
            param24.ParameterName = "pnumofcustaffect";
            param24.DbType = DbType.Int16;
            if (rowInfo[0].noofcustaffected != null)
                param24.Value = rowInfo[0].noofcustaffected;
            else
                param24.Value = DBNull.Value;
            command.Parameters.Add(param24);

            var param25 = command.CreateParameter();
            param25.ParameterName = "ppipesize";
            param25.DbType = DbType.Int16;
            if (rowInfo[0].pipesize != null)
                param25.Value = rowInfo[0].pipesize;
            else
                param25.Value = DBNull.Value;
            command.Parameters.Add(param25);

            var param26 = command.CreateParameter();
            param26.ParameterName = "ppipetype";
            param26.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].pipetype))
                param26.Value = rowInfo[0].pipetype;
            else
                param26.Value = DBNull.Value;
            command.Parameters.Add(param26);

            var param27 = command.CreateParameter();
            param27.ParameterName = "ppressureregime";
            param27.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].pressureregime))
                param27.Value = rowInfo[0].pressureregime;
            else
                param27.Value = DBNull.Value;
            command.Parameters.Add(param27);

            var param28 = command.CreateParameter();
            param28.ParameterName = "premarks";
            param28.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].remarks))
                param28.Value = rowInfo[0].remarks;
            else
                param28.Value = DBNull.Value;
            param28.Value = rowInfo[0].remarks.Replace("'", "''");
            command.Parameters.Add(param28);

            var param29 = command.CreateParameter();
            param29.ParameterName = "prootcause";
            param29.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].rootcause))
                param29.Value = rowInfo[0].rootcause;
            else
                param29.Value = DBNull.Value;
            command.Parameters.Add(param29);

            var param30 = command.CreateParameter();
            param30.ParameterName = "psourcetype";
            param30.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].sourcetype))
                param30.Value = rowInfo[0].sourcetype;
            else
                param30.Value = DBNull.Value;
            command.Parameters.Add(param30);

            var param31 = command.CreateParameter();
            param31.ParameterName = "pstreetname";
            param31.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].streetname))
                param31.Value = rowInfo[0].streetname.Replace("'", "''");
            else
                param31.Value = DBNull.Value;
            command.Parameters.Add(param31);

            var param32 = command.CreateParameter();
            param32.ParameterName = "pinstallyear";
            param32.DbType = DbType.Int16;
            if (rowInfo[0].installyear != null)
                param32.Value = rowInfo[0].installyear;
            else
                param32.Value = DBNull.Value;
            command.Parameters.Add(param32);

            var param34 = command.CreateParameter();
            param34.ParameterName = "pstatus";
            param34.DbType = DbType.Int16;
            param34.Value = rowInfo[0].status;
            command.Parameters.Add(param34);

            var param35 = command.CreateParameter();
            param35.ParameterName = "pisdeleted";
            param35.DbType = DbType.Int16;
            param35.Value = 0;
            command.Parameters.Add(param35);

            //var param37 = command.CreateParameter();
            //param37.ParameterName = "pismodified";
            //param37.DbType = DbType.String;
            //param37.Value = "U";
            //command.Parameters.Add(param37);

            var param38 = command.CreateParameter();
            param38.ParameterName = "pglobalid";
            param38.DbType = DbType.String;
            param38.Value = "{" + Guid.NewGuid().ToString().ToUpper() + "}";
            command.Parameters.Add(param38);

            var param39 = command.CreateParameter();
            param39.ParameterName = "pshape";
            param39.DbType = DbType.String;
            param39.Value = rowInfo[0].geometry;
            command.Parameters.Add(param39);

            var param40 = command.CreateParameter();
            param40.ParameterName = "ptransmainguid";
            param40.DbType = DbType.String;
            if (rowInfo[0].transmainguid != null)
                param40.Value = rowInfo[0].transmainguid;
            else
                param40.Value = DBNull.Value;
            command.Parameters.Add(param40);

            var param41 = command.CreateParameter();
            param41.ParameterName = "pdistmainguid";
            param41.DbType = DbType.String;
            if (rowInfo[0].distmainguid != null)
                param41.Value = rowInfo[0].distmainguid;
            else
                param41.Value = DBNull.Value;
            command.Parameters.Add(param41);

            var param42 = command.CreateParameter();
            param42.ParameterName = "pdatemodified";
            param42.DbType = DbType.DateTime;
            param42.Value = DateTime.Now;
            command.Parameters.Add(param42);

            var param43 = command.CreateParameter();
            param43.ParameterName = "pusername";
            param43.DbType = DbType.String;
            param43.Value = Username.ToUpper();
            command.Parameters.Add(param43);

            var param44 = command.CreateParameter();
            param44.ParameterName = "poid";
            param44.DbType = DbType.Int64;
            param44.Value = rowInfo[0].objectid;
            command.Parameters.Add(param44);

            var param45 = command.CreateParameter();
            param45.ParameterName = "rowcount";
            param45.DbType = DbType.Int16;
            param45.Direction = ParameterDirection.Output;
            command.Parameters.Add(param45);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateDataAsync(command);
            return result;
        }
        public async Task<Dictionary<string, object>> DeleteDMISPointAsync(string objectID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_delete_dmis_leak_point");
            var param1 = command.CreateParameter();
            param1.ParameterName = "poid";
            param1.DbType = DbType.Int64;
            param1.Value = Convert.ToInt64(objectID);
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "rowcount";
            param2.DbType = DbType.Int16;
            param2.Direction = ParameterDirection.Output;
            command.Parameters.Add(param2);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateDataAsync(command);
            return result;
        }
        public async Task<HashSet<Dictionary<string, object>>> GetPointSnaptoGaslineAsync(double X, double Y, string Layer)
        {
            //string result = string.Empty;
            //string wkt = @"'POINT (" + X + " " + Y + ")'";

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_point_snap_to_gasline");

            var param1 = command.CreateParameter();
            param1.ParameterName = "px";
            param1.DbType = DbType.Double;
            param1.Value = X;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "py";
            param2.DbType = DbType.Double;
            param2.Value = Y;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "player";
            param3.DbType = DbType.String;
            param3.Value = Layer;
            command.Parameters.Add(param3);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<string> GetDMISJobID()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_create_dmis_jobid_maxseqno");
            command.CommandType = CommandType.StoredProcedure;

            //changed int to Int64 on 11th Oct 2018
            Int64 maxSeqNo = await ScalarDataAsync(command);
            string jobID = "GC1" + maxSeqNo.ToString().PadLeft(7, '0');
            return jobID;
        }
    }
}
