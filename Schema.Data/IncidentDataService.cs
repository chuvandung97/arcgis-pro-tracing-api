using Schema.Core.Data;
using Schema.Core.Models;
using Schema.Core.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Data
{
    public class IncidentDataService : PgDbBase, IIncidentDataService
    {
        public IncidentDataService(IConfigService configService, ILoggingService loggingService)
          : base(configService, loggingService)
        { }
        //added by Sandip on 05th Feb 2020 for RFC0024551 -- This api returns the incident information.
        public async Task<HashSet<Dictionary<string, object>>> GetHTOutageIncidentAsync(string RequestType, string IncidentID, string FromDate = null, string ToDate = null)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_imm_incident");

            var param1 = command.CreateParameter();
            param1.ParameterName = "prequesttype";
            param1.DbType = DbType.String;
            param1.Value = RequestType.ToUpper();
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pincidentid";
            param2.DbType = DbType.Int64;
            if (!string.IsNullOrEmpty(IncidentID))
                param2.Value = Convert.ToInt64(IncidentID);
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pfromdate";
            param3.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date;
            if (!string.IsNullOrEmpty(FromDate))
                param3.Value = FromDate;
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "ptodate";
            param4.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Date;
            if (!string.IsNullOrEmpty(ToDate))
                param4.Value = ToDate;
            else
                param4.Value = DBNull.Value;
            command.Parameters.Add(param4);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 11th Feb 2020 for RFC0024551 -- This api returns the information details of the particular incident.
        public async Task<HashSet<Dictionary<string, object>>> GetHTOutageIncidentDetailAsync(Int64 IncidentID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_imm_incident_dtl");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pincidentid";
            param1.DbType = DbType.Int64;
            param1.Value = IncidentID;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 14th Feb 2020 for RFC0024551 -- This api inserts newly created incident details in database.
        public async Task<Dictionary<string, object>> CreateHTOutageIncidentAsync(List<IncidentManagementItems> rowInfo, string UserID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_insert_imm_incident");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pincidentname";
            param1.DbType = DbType.String;
            param1.Value = rowInfo[0].incidentname;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pincidentno";
            param2.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].incidentno))
                param2.Value = rowInfo[0].incidentno;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pincidentdesc";
            param3.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].remarks))
                param3.Value = rowInfo[0].remarks;
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "pincidentdate";
            param4.DbType = DbType.DateTime;
            string strIncidentDate = rowInfo[0].incidentdate.ToString();
            if (!string.IsNullOrEmpty(strIncidentDate) && !string.IsNullOrWhiteSpace(strIncidentDate))
            {
                DateTime dateincident = DateTime.Parse(strIncidentDate, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                param4.Value = dateincident;
            }
            else
                param4.Value = DBNull.Value;
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "pestaffectedcustcount";
            param5.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
            param5.Value = rowInfo[0].estaffectedcustomers;
            command.Parameters.Add(param5);

            var param6 = command.CreateParameter();
            param6.ParameterName = "pestrestoreduration";
            param6.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
            param6.Value = rowInfo[0].estrestoreduration;
            command.Parameters.Add(param6);

            var param7 = command.CreateParameter();
            param7.ParameterName = "puserid";
            param7.DbType = DbType.String;
            param7.Value = UserID.ToUpper();
            command.Parameters.Add(param7);

            var param8 = command.CreateParameter();
            param8.ParameterName = "rowcount";
            param8.DbType = DbType.Int64;
            param8.Direction = ParameterDirection.Output;
            command.Parameters.Add(param8);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateDataAsync(command);
            result.Add("incidentid", command.Parameters["rowcount"].Value);
            return result;
        }
        //added by Sandip on 18th Feb 2020 for RFC0024551 -- This api inserts manually searched records based on 'Postal Code/Block Number/By selecting building on map' by users
        public async Task<Dictionary<string, object>> CreateHTOutageIncidentByUserAsync(List<IncidentManagementItems> rowInfo, string UserID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_insert_imm_incident_dtl");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pincidentid";
            param1.DbType = DbType.Int64;
            param1.Value = rowInfo[0].incidentid;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pflag";
            param2.DbType = DbType.Int16;
            param2.Value = rowInfo[0].manualflag;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pinput";
            param3.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].manualinput))
                param3.Value = rowInfo[0].manualinput;
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "puserid";
            param4.DbType = DbType.String;
            param4.Value = UserID.ToString();
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "rowcount";
            param5.DbType = DbType.Int16;
            param5.Direction = ParameterDirection.Output;
            command.Parameters.Add(param5);

            command.CommandType = CommandType.StoredProcedure;
            return await UpdateDataAsync(command);
        }
        //added by Sandip on 20th Feb 2020 for RFC0024551 -- This api allows user to update the incident details.
        public async Task<Dictionary<string, object>> UpdateHTOutageIncidentAsync(List<IncidentManagementItems> rowInfo, string UserID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_imm_incident");

            var param0 = command.CreateParameter();
            param0.ParameterName = "pincidentid";
            param0.DbType = DbType.Int64;
            param0.Value = rowInfo[0].incidentid;
            command.Parameters.Add(param0);

            var param1 = command.CreateParameter();
            param1.ParameterName = "pincidentname";
            param1.DbType = DbType.String;
            if (string.IsNullOrEmpty(rowInfo[0].publishflag))
                param1.Value = rowInfo[0].incidentname;
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pincidentno";
            param2.DbType = DbType.String;
            if (string.IsNullOrEmpty(rowInfo[0].publishflag))
                param2.Value = rowInfo[0].incidentno;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pincidentdesc";
            param3.DbType = DbType.String;
            if (string.IsNullOrEmpty(rowInfo[0].publishflag))
                param3.Value = rowInfo[0].remarks;
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "pincidentdate";
            param4.DbType = DbType.DateTime;
            if (string.IsNullOrEmpty(rowInfo[0].publishflag))
            {
                string strIncidentDate = rowInfo[0].incidentdate.ToString();
                if (!string.IsNullOrEmpty(strIncidentDate) && !string.IsNullOrWhiteSpace(strIncidentDate))
                {
                    DateTime dateincident = DateTime.Parse(strIncidentDate, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                    param4.Value = dateincident;
                }
            }
            else
                param4.Value = DBNull.Value;
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "pestaffectedcustcount";
            param5.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
            if (string.IsNullOrEmpty(rowInfo[0].publishflag))
                param5.Value = rowInfo[0].estaffectedcustomers;
            else
                param5.Value = DBNull.Value;
            command.Parameters.Add(param5);

            var param6 = command.CreateParameter();
            param6.ParameterName = "pestrestoreduration";
            param6.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
            if (string.IsNullOrEmpty(rowInfo[0].publishflag))
                param6.Value = rowInfo[0].estrestoreduration;
            else
                param6.Value = DBNull.Value;
            command.Parameters.Add(param6);

            var param7 = command.CreateParameter();
            param7.ParameterName = "pestrestoredate";
            param7.DbType = DbType.DateTime;
            if (string.IsNullOrEmpty(rowInfo[0].publishflag))
            {
                string strFullRestorationDate = rowInfo[0].fullrestorationdate.ToString();
                if (!string.IsNullOrEmpty(strFullRestorationDate))
                {
                    if (!string.IsNullOrEmpty(strFullRestorationDate) && !string.IsNullOrWhiteSpace(strFullRestorationDate))
                    {
                        DateTime daterestore = DateTime.Parse(strFullRestorationDate, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                        param7.Value = daterestore;
                    }
                }
                else
                    param7.Value = DBNull.Value;
            }
            else
                param7.Value = DBNull.Value;
            command.Parameters.Add(param7);

            var param8 = command.CreateParameter();
            param8.ParameterName = "pispublish";
            param8.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
            if (string.IsNullOrEmpty(rowInfo[0].publishflag))
                param8.Value = DBNull.Value;
            else
                param8.Value = Convert.ToInt16(rowInfo[0].publishflag);
            command.Parameters.Add(param8);

            var param9 = command.CreateParameter();
            param9.ParameterName = "puserid";
            param9.DbType = DbType.String;
            param9.Value = UserID.ToString();
            command.Parameters.Add(param9);

            var param10 = command.CreateParameter();
            param10.ParameterName = "rowcount";
            param10.DbType = DbType.Int16;
            param10.Direction = ParameterDirection.Output;
            command.Parameters.Add(param10);

            command.CommandType = CommandType.StoredProcedure;
            return await UpdateDataAsync(command);
        }
        //added by Sandip on 21th Feb 2020 for RFC0024551 -- This api allows to update the customer status of the incident.
        public async Task<Dictionary<string, object>> UpdateHTOutageAffectedCustomerStatusAsync(List<IncidentManagementItems> rowInfo, string UserID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_imm_incident_dtl");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pincidentdtlids";
            param1.DbType = DbType.String;
            param1.Value = rowInfo[0].incidentdtlsids;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pstatus";
            param2.DbType = DbType.Int16;
            param2.Value = rowInfo[0].status;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "prestoredate";
            param3.DbType = DbType.DateTime;
            string strRestoreDate = rowInfo[0].restoredate.ToString();
            if (!string.IsNullOrEmpty(strRestoreDate) && !string.IsNullOrWhiteSpace(strRestoreDate))
            {
                DateTime daterestore = DateTime.Parse(strRestoreDate, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                param3.Value = daterestore;
            }
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "puserid";
            param4.DbType = DbType.String;
            param4.Value = UserID.ToString();
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "rowcount";
            param5.DbType = DbType.Int16;
            param5.Direction = ParameterDirection.Output;
            command.Parameters.Add(param5);

            command.CommandType = CommandType.StoredProcedure;
            return await UpdateDataAsync(command);
        }
        //added by Sandip on 24th Feb 2020 for RFC0024551 -- This api allows to delete indiviual customer of the particular incident.
        public async Task<Dictionary<string, object>> DeleteHTOutageIncidentByUserAsync(List<IncidentManagementItems> rowInfo, string UserID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_delete_imm_incident_dtl");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pincidentdtlids";
            param1.DbType = DbType.String;
            param1.Value = rowInfo[0].incidentdtlsids;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "puserid";
            param2.DbType = DbType.String;
            param2.Value = UserID;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "rowcount";
            param3.DbType = DbType.Int16;
            param3.Direction = ParameterDirection.Output;
            command.Parameters.Add(param3);

            command.CommandType = CommandType.StoredProcedure;
            return await UpdateDataAsync(command);
        }
        //added by Sandip on 25th Feb 2020 for RFC0024551 -- This api is consumed by admin portal to delete any previously created incident.
        public async Task<Dictionary<string, object>> DeleteHTOutageIncidentAsync(List<IncidentManagementItems> rowInfo, string UserID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_delete_imm_incident");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pincidentid";
            param1.DbType = DbType.Int64;
            param1.Value = rowInfo[0].incidentid;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "puserid";
            param2.DbType = DbType.String;
            param2.Value = UserID.ToUpper().ToString();
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "rowcount";
            param3.DbType = DbType.Int16;
            param3.Direction = ParameterDirection.Output;
            command.Parameters.Add(param3);

            command.CommandType = CommandType.StoredProcedure;
            return await UpdateDataAsync(command);
        }
        //added by Sandip on 27th Feb 2020 for RFC0024551 -- This api is used to generate incident boundary in Schema. 
        public async Task<Dictionary<string, object>> GenerateIncidentBoundaryAsync(List<IncidentManagementItems> rowInfo, string UserID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_generate_imm_boundary");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pincidentid";
            param1.DbType = DbType.Int64;
            param1.Value = rowInfo[0].incidentid;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "puserid";
            param2.DbType = DbType.String;
            param2.Value = UserID.ToUpper().ToString();
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "rowcount";
            param3.DbType = DbType.Int16;
            param3.Direction = ParameterDirection.Output;
            command.Parameters.Add(param3);

            command.CommandType = CommandType.StoredProcedure;
            return await UpdateDataAsync(command);
        }
        //added by Sandip on 28th Feb 2020 for RFC0024551 -- This api returns the transformer list for the searched substation based on MRC.
        public async Task<HashSet<Dictionary<string, object>>> GetIncidentTransformersAsync(string MRC)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_imm_transformers");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pmrc";
            param1.DbType = DbType.String;
            if (!string.IsNullOrEmpty(MRC))
                param1.Value = MRC.ToUpper();
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 01st March 2020 for RFC0024551 -- This api perform trace based on feeder/ transformer and generates affected customers and returns network data.
        public async Task<HashSet<Dictionary<string, object>>> GetIncidentCustomerTraceAsync(Int64 IncidentID, string UserID, string TRFIDs = null, string FID = null, string EID = null, string DirectionFlag = null, int[] SLDBarriers = null, int[] GISBarriers = null, string OverwriteFlag = null)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_imm_customer_trace");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pincidentid";
            param1.DbType = DbType.Int64;
            param1.Value = IncidentID;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "puserid";
            param2.DbType = DbType.String;
            param2.Value = UserID;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "ptrfs";
            param3.DbType = DbType.String;
            if (!string.IsNullOrEmpty(TRFIDs))
                param3.Value = TRFIDs;
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "pfid";
            param4.DbType = DbType.Int64;
            if (!string.IsNullOrEmpty(FID))
                param4.Value = Convert.ToInt64(FID);
            else
                param4.Value = DBNull.Value;
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "peid";
            param5.DbType = DbType.Int64;
            if (!string.IsNullOrEmpty(EID))
                param5.Value = Convert.ToInt64(EID);
            else
                param5.Value = DBNull.Value;
            command.Parameters.Add(param5);

            var param6 = command.CreateParameter();
            param6.ParameterName = "pdirection";
            param6.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
            if (!string.IsNullOrEmpty(DirectionFlag))
                param6.Value = Convert.ToInt16(DirectionFlag);
            else
                param6.Value = DBNull.Value;
            command.Parameters.Add(param6);

            var param7 = command.CreateParameter();
            param7.ParameterName = "psldbarriers";
            param7.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer;
            if (SLDBarriers != null && SLDBarriers.Length > 0)
                param7.Value = SLDBarriers;
            else
                param7.Value = DBNull.Value;
            command.Parameters.Add(param7);

            var param8 = command.CreateParameter();
            param8.ParameterName = "pgisbarriers";
            param8.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer;
            if (GISBarriers != null && GISBarriers.Length > 0)
                param8.Value = GISBarriers;
            else
                param8.Value = DBNull.Value;
            command.Parameters.Add(param8);

            var param9 = command.CreateParameter();
            param9.ParameterName = "poverwrite";
            param9.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
            if (!string.IsNullOrEmpty(OverwriteFlag))
                param9.Value = Convert.ToInt16(OverwriteFlag);
            else
                param9.Value = DBNull.Value;
            command.Parameters.Add(param9);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 08th May 2020 for RFC0021956 -- This api returns the transformer details within a specified extent.
        public async Task<HashSet<Dictionary<string, object>>> GetIncidentTransformersOnMapSelectAsync(string Geometry)
        {
            var coords = Geometry.Split(',');
            string wkt = @"POLYGON ((" + coords[0] + " " + coords[1] + "," + coords[2] + " " + coords[1] + "," + coords[2] + " " + coords[3] + "," + coords[0] + " " + coords[3] + "," + coords[0] + " " + coords[1] + "))";

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_imm_select_transformers");
            var param1 = command.CreateParameter();
            param1.ParameterName = "pgeometry";
            param1.DbType = DbType.String;
            param1.Value = wkt;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 27th May 2020 for RFC00XXXXX -- This api returns the building summary details of the particular incident.
        public async Task<HashSet<Dictionary<string, object>>> GetHTOutageBuildingSummaryAsync(Int64 IncidentID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_imm_bldg_count");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pincidentid";
            param1.DbType = DbType.Int64;
            param1.Value = IncidentID;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 27th May 2020 for RFC00XXXX -- This api inserts user entered feedback from the intranet application into database table.
        public async Task<Dictionary<string, object>> InsertFeedbackAsync(List<IncidentManagementItems> rowInfo, string UserID, string UserName, string EmailID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_insert_imm_feedback");

            var param0 = command.CreateParameter();
            param0.ParameterName = "puserid";
            param0.DbType = DbType.String;
            param0.Value = UserID.ToUpper();
            command.Parameters.Add(param0);

            var param1 = command.CreateParameter();
            param1.ParameterName = "pusername";
            param1.DbType = DbType.String;
            param1.Value = UserName.ToUpper();
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pemail";
            param2.DbType = DbType.String;
            param2.Value = EmailID.ToUpper();
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pfeedback";
            param3.DbType = DbType.String;
            param3.Value = Convert.ToString(rowInfo[0].feedback);
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "rowcount";
            param4.DbType = DbType.Int16;
            param4.Direction = ParameterDirection.Output;
            command.Parameters.Add(param4);

            command.CommandType = CommandType.StoredProcedure;
            return await UpdateDataAsync(command);
        }
    }
}
