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
    public class SupplyZoneDataService : PgDbBase, ISupplyZoneDataService
    {
        public SupplyZoneDataService(IConfigService configService, ILoggingService loggingService)
            : base(configService, loggingService)
        { }
        public async Task<HashSet<Dictionary<string, object>>> BindEventDataAsync(string EventID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();

            command = new Npgsql.NpgsqlCommand("swift.api_get_supplyzone_event_data");
            var param1 = command.CreateParameter();
            param1.ParameterName = "peventid";
            param1.DbType = DbType.Int64;
            if (EventID.ToUpper() == "CREATE" || string.IsNullOrEmpty(EventID))
                param1.Value = DBNull.Value;
            else
                param1.Value = Convert.ToInt64(EventID);
            command.Parameters.Add(param1);
            command.CommandType = CommandType.StoredProcedure;

            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetEffectedBoundaryAsync(string EventID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_supplyzone_effected_boundary");
            var param1 = command.CreateParameter();
            param1.ParameterName = "peventid";
            param1.DbType = DbType.Int64;
            param1.Value = Convert.ToInt64(EventID);
            command.Parameters.Add(param1);
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> RetrieveSummaryDataAsync(string EventID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_supplyzone_summary_incident");
            var param1 = command.CreateParameter();
            param1.ParameterName = "peventid";
            param1.DbType = DbType.Int64;
            param1.Value = Convert.ToInt64(EventID);
            command.Parameters.Add(param1);
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> RetrieveEventAllAsync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_supplyzone_all_events_info");
            var param1 = command.CreateParameter();
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> RetrieveEventTotalCountAsync(string EventID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_supplyzone_total_count_incident");
            var param1 = command.CreateParameter();
            param1.ParameterName = "peventid";
            param1.DbType = DbType.Int64;
            param1.Value = Convert.ToInt64(EventID);
            command.Parameters.Add(param1);
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> RetrieveEventTotalMRCCountAsync(string MRC)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_supplyzone_total_mrc_count_incident");
            var param1 = command.CreateParameter();
            param1.ParameterName = "pmrc";
            param1.DbType = DbType.String;
            param1.Value = MRC;
            command.Parameters.Add(param1);
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> RetrieveEventBuildingCountAsync(string EventID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_supplyzone_bldg_count_incident");
            var param1 = command.CreateParameter();
            param1.ParameterName = "peventid";
            param1.DbType = DbType.Int64;
            param1.Value = Convert.ToInt64(EventID);
            command.Parameters.Add(param1);
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> RetrieveEventStreetCountAsync(string EventID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_supplyzone_street_count_incident");
            var param1 = command.CreateParameter();
            param1.ParameterName = "peventid";
            param1.DbType = DbType.Int64;
            param1.Value = Convert.ToInt64(EventID);
            command.Parameters.Add(param1);
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> RetrieveEventBuildingCountMRCAsync(string MRC)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_supplyzone_bldg_count_mrc_incident");
            var param1 = command.CreateParameter();
            param1.ParameterName = "pmrc";
            param1.DbType = DbType.String;
            param1.Value = MRC;
            command.Parameters.Add(param1);
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> RetrieveEventStreetCountMRCAsync(string MRC)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_supplyzone_street_count_mrc_incident");
            var param1 = command.CreateParameter();
            param1.ParameterName = "pmrc";
            param1.DbType = DbType.String;
            param1.Value = MRC;
            command.Parameters.Add(param1);
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> RetrieveEventStreetCountUsingStreetCodeAsync(string StreetCode, string EventID, string MRC)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_supplyzone_street_count_using_streetcode");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pstreetcode";
            param1.DbType = DbType.String;
            if (!string.IsNullOrEmpty(StreetCode))
                param1.Value = StreetCode;
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "peventid";
            param2.DbType = DbType.Int64;
            if (!string.IsNullOrEmpty(EventID))
                param2.Value = Convert.ToInt64(EventID);
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pmrc";
            param3.DbType = DbType.String;
            if (!string.IsNullOrEmpty(MRC))
                param3.Value = MRC;
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        /*public async Task<HashSet<Dictionary<string, object>>> LayerMappingAsync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select * from (" +
                                    "select(a.zonename) as Tablename, trim(b.name):: character varying as Layername, a.mrc from swift.sz_zone_building a" +
                                    " Left Join swift.sz_source_station b " +
                                    "on a.mrc = b.mrc  group by a.zonename,b.name,a.mrc) tb1" +
                                    " where layername is not null order by Tablename";
            command.CommandType = CommandType.Text;
            return await ReadDataAsync(command);
        }*/
        public async Task<Int64> GenerateEventID()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_supplyzone_generate_event_id");
            command.CommandType = CommandType.StoredProcedure;

            Int64 eventID = await ScalarDataAsync(command);
            return eventID;
        }
        /*public async Task<HashSet<Dictionary<string, object>>> CheckEventName(string EventName)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_supplyzone_check_event_name");

            var param1 = command.CreateParameter();
            param1.ParameterName = "peventname";
            param1.DbType = DbType.String;
            if (!string.IsNullOrEmpty(EventName))
                param1.Value = EventName.ToUpper();
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }*/
        public async Task<Dictionary<string, object>> CreateEventAsync(List<SupplyZoneItem> rowInfo, string EmpID, Int64 EventID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_create_supplyzone_event_incident");

            var param0 = command.CreateParameter();
            param0.ParameterName = "peventid";
            param0.DbType = DbType.Int64;
            param0.Value = EventID;
            command.Parameters.Add(param0);

            var param1 = command.CreateParameter();
            param1.ParameterName = "peventname";
            param1.DbType = DbType.String;
            param1.Value = rowInfo[0].eventname;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "peventdate";
            param2.DbType = DbType.DateTime;
            string strEventDate = rowInfo[0].eventdate.ToString();
            if (!string.IsNullOrEmpty(strEventDate) && !string.IsNullOrWhiteSpace(strEventDate))
            {
                DateTime eventDate = DateTime.Parse(strEventDate, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                param2.Value = eventDate;
            }
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pempid";
            param3.DbType = DbType.String;
            if (!string.IsNullOrEmpty(EmpID))
                param3.Value = EmpID.Replace("'", "''");
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "pisnew";
            param4.DbType = DbType.Int16;
            param4.Value = 1;
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
        public async Task<Dictionary<string, object>> CreateEventDetailsAsync(SupplyZoneItem rowInfo, string EmpID, Int64 EventID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_create_supplyzone_event_detail_incident");

            var param0 = command.CreateParameter();
            param0.ParameterName = "peventid";
            param0.DbType = DbType.Int64;
            param0.Value = EventID;
            command.Parameters.Add(param0);

            var param1 = command.CreateParameter();
            param1.ParameterName = "pmrc";
            param1.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo.mrc))
                param1.Value = rowInfo.mrc;
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "ppoutageflag";
            param2.DbType = DbType.Int16;
            param2.Value = Convert.ToInt16(rowInfo.poutageflag);
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "poutageflag";
            param3.DbType = DbType.Int16;
            param3.Value = Convert.ToInt16(rowInfo.outageflag);
            command.Parameters.Add(param3);

            var param6 = command.CreateParameter();
            param6.ParameterName = "psupplystation";
            param6.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo.supplystation))
                param6.Value = rowInfo.supplystation.Replace("'", "''");
            else
                param6.Value = DBNull.Value;
            command.Parameters.Add(param6);

            var param7 = command.CreateParameter();
            param7.ParameterName = "pdtepoutage";
            param7.DbType = DbType.DateTime;
            if (Convert.ToInt16(rowInfo.poutageflag) == 1)
                param7.Value = DateTime.Now;
            else
                param7.Value = DBNull.Value;
            command.Parameters.Add(param7);

            var param8 = command.CreateParameter();
            param8.ParameterName = "pdteoutage";
            param8.DbType = DbType.DateTime;
            if (Convert.ToInt16(rowInfo.outageflag) == 1)
                param8.Value = DateTime.Now;
            else
                param8.Value = DBNull.Value;
            command.Parameters.Add(param8);

            var param9 = command.CreateParameter();
            param9.ParameterName = "pempid";
            param9.DbType = DbType.String;
            if (!string.IsNullOrEmpty(EmpID))
                param9.Value = EmpID.Replace("'", "''");
            else
                param9.Value = DBNull.Value;
            command.Parameters.Add(param9);

            var param10 = command.CreateParameter();
            param10.ParameterName = "pisnew";
            param10.DbType = DbType.Int16;
            param10.Value = 1;
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
        public async Task<Dictionary<string, object>> UpdateEventDetailsAsync(SupplyZoneItem rowInfo, string EmpID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_supplyzone_event_detail_incident");

            var param0 = command.CreateParameter();
            param0.ParameterName = "peventid";
            param0.DbType = DbType.Int64;
            param0.Value = Convert.ToInt64(rowInfo.eventid);
            command.Parameters.Add(param0);

            var param1 = command.CreateParameter();
            param1.ParameterName = "pmrc";
            param1.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo.mrc))
                param1.Value = rowInfo.mrc;
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "ppoutageflag";
            param2.DbType = DbType.Int16;
            param2.Value = Convert.ToInt16(rowInfo.poutageflag);
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "poutageflag";
            param3.DbType = DbType.Int16;
            param3.Value = Convert.ToInt16(rowInfo.outageflag);
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "prestoreflag";
            param4.DbType = DbType.Int16;
            param4.Value = Convert.ToInt16(rowInfo.restoreflag);
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "pdterestore";
            param5.DbType = DbType.DateTime;
            string strRestoreDate = rowInfo.dterestore.ToString();
            if (Convert.ToInt16(rowInfo.restoreflag) == 1)
            {
                if (!string.IsNullOrEmpty(strRestoreDate) && !string.IsNullOrWhiteSpace(strRestoreDate))
                {
                    DateTime restoreDate = DateTime.Parse(strRestoreDate, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                    param5.Value = restoreDate;
                }
                else
                    param5.Value = DBNull.Value;
            }
            else
                param5.Value = DBNull.Value;
            command.Parameters.Add(param5);

            var param7 = command.CreateParameter();
            param7.ParameterName = "pdtepoutage";
            param7.DbType = DbType.DateTime;
            if (Convert.ToInt16(rowInfo.poutageflag) == 1)
                param7.Value = DateTime.Now;
            else
                param7.Value = DBNull.Value;
            command.Parameters.Add(param7);

            var param8 = command.CreateParameter();
            param8.ParameterName = "pdteoutage";
            param8.DbType = DbType.DateTime;
            if (Convert.ToInt16(rowInfo.outageflag) == 1)
                param8.Value = DateTime.Now;
            else
                param8.Value = DBNull.Value;
            command.Parameters.Add(param8);

            var param9 = command.CreateParameter();
            param9.ParameterName = "psupplystation";
            param9.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo.supplystation))
                param9.Value = rowInfo.supplystation.Replace("'", "''");
            else
                param9.Value = DBNull.Value;
            command.Parameters.Add(param9);

            var param10 = command.CreateParameter();
            param10.ParameterName = "pempid";
            param10.DbType = DbType.String;
            if (!string.IsNullOrEmpty(EmpID))
                param10.Value = EmpID.Replace("'", "''");
            else
                param10.Value = DBNull.Value;
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
        /*public async Task<HashSet<Dictionary<string, object>>> BindUserDetailsAsync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select * from swift.sz_usergroup";
            command.CommandType = CommandType.Text;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetUserGroupAsync(string UserID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select * from swift.sz_usergroup";
            command.CommandType = CommandType.Text;
            return await ReadDataAsync(command);
        }
        public async Task<Dictionary<string, object>> AddUserAsync(List<SupplyZoneItem> rowInfo)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_add_supplyzone_user");

            var param0 = command.CreateParameter();
            param0.ParameterName = "pusername";
            param0.DbType = DbType.String;
            param0.Value = rowInfo[0].username.ToLower();
            command.Parameters.Add(param0);

            var param1 = command.CreateParameter();
            param1.ParameterName = "prole";
            param1.DbType = DbType.String;
            param1.Value = rowInfo[0].role.ToLower();
            command.Parameters.Add(param1);


            var param3 = command.CreateParameter();
            param3.ParameterName = "puserid";
            param3.DbType = DbType.String;
            param3.Value = rowInfo[0].userid.ToLower();
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "rowcount";
            param4.DbType = DbType.Int16;
            param4.Direction = ParameterDirection.Output;
            command.Parameters.Add(param4);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateAdminDataAsync(command);
            return result;
        }
        public async Task<Dictionary<string, object>> DeleteUserAsync(List<SupplyZoneItem> rowInfo)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_delete_supplyzone_user");

            var param1 = command.CreateParameter();
            param1.ParameterName = "puserid";
            param1.DbType = DbType.String;
            param1.Value = rowInfo[0].userid.ToLower();
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "rowcount";
            param2.DbType = DbType.Int16;
            param2.Direction = ParameterDirection.Output;
            command.Parameters.Add(param2);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateAdminDataAsync(command);
            return result;
        }
        public async Task<Dictionary<string, object>> UpdateUserRoleAsync(List<SupplyZoneItem> rowInfo)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_supplyzone_user_role");

            var param1 = command.CreateParameter();
            param1.ParameterName = "puserid";
            param1.DbType = DbType.String;
            param1.Value = rowInfo[0].userid.ToLower();
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "prole";
            param2.DbType = DbType.String;
            param2.Value = rowInfo[0].role.ToLower();
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "rowcount";
            param3.DbType = DbType.Int16;
            param3.Direction = ParameterDirection.Output;
            command.Parameters.Add(param3);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateAdminDataAsync(command);
            return result;
        }*/
    }
}
