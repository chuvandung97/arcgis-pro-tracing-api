using Schema.Core.Data;
using Schema.Core.Models;
using Schema.Core.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Schema.Data
{
    public class SearchDataService : PgDbBase, ISearchDataService
    {
        public SearchDataService(IConfigService configService, ILoggingService loggingService)
            : base(configService, loggingService)
        { }
        public async Task<HashSet<Dictionary<string, object>>> SearchSchemaDetailsAsync(string SearchType, string Text, string UserType = null)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_search_schema_details");
            var parameter = command.CreateParameter();
            parameter.ParameterName = "psearchindex";
            parameter.DbType = DbType.Int16;
            parameter.Value = Convert.ToInt16(SearchType);
            command.Parameters.Add(parameter);

            var parameter1 = command.CreateParameter();
            parameter1.ParameterName = "psearchtxt";
            parameter1.DbType = DbType.String;
            parameter1.Value = Text.ToUpper();
            command.Parameters.Add(parameter1);

            if (!string.IsNullOrEmpty(UserType))
            {
                var parameter2 = command.CreateParameter();
                parameter2.ParameterName = "pflag";
                parameter2.DbType = DbType.Boolean;                
                if (UserType == "CAT2")
                    parameter2.Value = true;
                else
                    parameter2.Value = false;
                command.Parameters.Add(parameter2);              
            }
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        /*public async Task<HashSet<Dictionary<string, object>>> SearchAddressPointAsync(string Text)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select * FROM swift.vw_gis_address_point where upper(concataddr) like @val order by concataddr";
            command.CommandType = System.Data.CommandType.Text;
            var param1 = new Npgsql.NpgsqlParameter("@val", NpgsqlTypes.NpgsqlDbType.Text);
            param1.Value = string.Format("%{0}%", Text.ToUpper());
            command.Parameters.Add(param1);
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> SearchPostalCodeAsync(string Text)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select * FROM swift.vw_gis_address_point where upper(post_code) like @val order by post_code";
            command.CommandType = System.Data.CommandType.Text;
            var param1 = new Npgsql.NpgsqlParameter("@val", NpgsqlTypes.NpgsqlDbType.Text);
            param1.Value = string.Format("{0}%", Text.ToUpper());
            command.Parameters.Add(param1);
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> SearchBuildingAsync(string Text)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select * from swift.vw_gis_building where upper(name) like @val order by name";
            command.CommandType = System.Data.CommandType.Text;
            var param1 = new Npgsql.NpgsqlParameter("@val", NpgsqlTypes.NpgsqlDbType.Text);
            param1.Value = string.Format("%{0}%", Text.ToUpper());
            command.Parameters.Add(param1);
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> SearchMapSheetsAsync(string Text)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select * from swift.vw_gis_mapsheets where upper(si_name) like @val order by si_name";
            command.CommandType = System.Data.CommandType.Text;
            var param1 = new Npgsql.NpgsqlParameter("@val", NpgsqlTypes.NpgsqlDbType.Text);
            param1.Value = string.Format("{0}%", Text.ToUpper());
            command.Parameters.Add(param1);
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> SearchRoadNamesShortAsync(string Text)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select * from swift.vw_gis_road where upper(st_name) like @val order by st_name";
            command.CommandType = System.Data.CommandType.Text;
            var param1 = new Npgsql.NpgsqlParameter("@val", NpgsqlTypes.NpgsqlDbType.Text);
            param1.Value = string.Format("{0}%", Text.ToUpper());
            command.Parameters.Add(param1);
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> SearchRoadNamesLongAsync(string Text)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select * from swift.vw_gis_road where upper(first_pwdst_name) like @val order by first_pwdst_name";
            command.CommandType = System.Data.CommandType.Text;
            var param1 = new Npgsql.NpgsqlParameter("@val", NpgsqlTypes.NpgsqlDbType.Text);
            param1.Value = string.Format("{0}%", Text.ToUpper());
            command.Parameters.Add(param1);
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> SearchMukimLotsAsync(string Text)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select * from swift.vw_gis_slamukimlot where upper(mklot) like @val order by mklot";
            command.CommandType = System.Data.CommandType.Text;
            var param1 = new Npgsql.NpgsqlParameter("@val", NpgsqlTypes.NpgsqlDbType.Text);
            param1.Value = string.Format("{0}%", Text.ToUpper());
            command.Parameters.Add(param1);
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> SearchSubstationAsync(string Text, string UserType)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select * from swift.vw_gis_substation where upper(substation_name) like @val order by substation_name";
            command.CommandType = System.Data.CommandType.Text;
            var param1 = new Npgsql.NpgsqlParameter("@val", NpgsqlTypes.NpgsqlDbType.Text);
            param1.Value = string.Format("{0}%", Text.ToUpper());
            command.Parameters.Add(param1);
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> SearchOverGroundBoxAsync(string Text)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select * from swift.vw_gis_overgroundbox where upper(mrc) like @val order by mrc";
            command.CommandType = System.Data.CommandType.Text;
            var param1 = new Npgsql.NpgsqlParameter("@val", NpgsqlTypes.NpgsqlDbType.Text);
            param1.Value = string.Format("{0}%", Text.ToUpper());
            command.Parameters.Add(param1);
            return await ReadDataAsync(command);
        }*/
        public async Task<HashSet<Dictionary<string, object>>> SearchDMISJobIDAsync(string Username, string Text)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_search_dmis_point");
            var parameter = command.CreateParameter();
            parameter.ParameterName = "username";
            parameter.DbType = DbType.AnsiString;
            parameter.Value = Username.ToUpper();
            command.Parameters.Add(parameter);

            var parameter1 = command.CreateParameter();
            parameter1.ParameterName = "searchtxt";
            parameter1.DbType = DbType.AnsiString;
            parameter1.Value = Text.ToUpper();
            command.Parameters.Add(parameter1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetRoadJunctionsAsync(string RoadName)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_road_junctions");
            var parameter = command.CreateParameter();
            parameter.ParameterName = "streetname";
            parameter.DbType = DbType.AnsiString;
            parameter.Value = RoadName.ToUpper();
            command.Parameters.Add(parameter);
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetRoadInfoAsync(string RoadName)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_road_info");
            var parameter = command.CreateParameter();
            parameter.ParameterName = "streetname";
            parameter.DbType = DbType.AnsiString;
            parameter.Value = RoadName.ToUpper();
            command.Parameters.Add(parameter);
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 1st April 2019 for RFC0018439 -- To track and log exact GIS searched location in database 
        public async Task<Dictionary<string, object>> InsertSearchedLocationInDBAsync(List<UsageTrackingItem> rowinfo)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            #region get values from http context
            var httpContext = HttpContext.Current;
            string url = string.Empty;
            string parameters = string.Empty;
            string referrer = string.Empty;
            string functionName = string.Empty;
            string clientIP = string.Empty;
            string partURL = string.Empty;

            string username = httpContext.User.Identity.Name;
            string browser = HttpContext.Current.Request.Browser.Browser;
            string useragent = HttpContext.Current.Request.UserAgent;

            if (username.Contains('\\'))
                username = username.Split('\\')[1].ToString();

            if (HttpContext.Current.Request.Url != null)
                url = HttpContext.Current.Request.Url.ToString();

            clientIP = GetClientIp(HttpContext.Current.Request);

            if (HttpContext.Current.Request.UrlReferrer != null)
                referrer = HttpContext.Current.Request.UrlReferrer.ToString();

            parameters = "SearchIndex=" + rowinfo[0].searchIndex + "&SearchLocation='" + rowinfo[0].searchLocation + "'&XCoord=" + rowinfo[0].xCoord + "&YCoord=" + rowinfo[0].yCoord + "";

            //Get the function name
            string part = string.Empty;
            if (url.Contains("?"))
                partURL = url.Substring(0, url.IndexOf('?'));
            else
                partURL = url;
            string[] splitString = partURL.Split('/');
            functionName = splitString[splitString.Length - 1].Trim();
            #endregion

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_insert_schema_app_requestlog"); //api_insert_usage_tracking_schema_searched_location;

            var param1 = command.CreateParameter();
            param1.ParameterName = "pusername";
            param1.DbType = DbType.String;
            if (!string.IsNullOrEmpty(username))
                param1.Value = username.ToUpper();
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "purl";
            param2.DbType = DbType.String;
            if (!string.IsNullOrEmpty(url))
                param2.Value = url;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pparam";
            param3.DbType = DbType.String;
            if (!string.IsNullOrEmpty(url))
                param3.Value = parameters;
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "pfunctionname";
            param4.DbType = DbType.String;
            if (!string.IsNullOrEmpty(functionName))
                param4.Value = functionName;
            else
                param4.Value = DBNull.Value;
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "pclientip";
            param5.DbType = DbType.String;
            if (!string.IsNullOrEmpty(clientIP))
                param5.Value = clientIP;
            else
                param5.Value = DBNull.Value;
            command.Parameters.Add(param5);

            var param6 = command.CreateParameter();
            param6.ParameterName = "preferrer";
            param6.DbType = DbType.String;
            if (!string.IsNullOrEmpty(referrer))
                param6.Value = referrer;
            else
                param6.Value = DBNull.Value;
            command.Parameters.Add(param6);

            var param7 = command.CreateParameter();
            param7.ParameterName = "pbrowser";
            param7.DbType = DbType.String;
            if (!string.IsNullOrEmpty(browser))
                param7.Value = browser;
            else
                param7.Value = DBNull.Value;
            command.Parameters.Add(param7);

            var param8 = command.CreateParameter();
            param8.ParameterName = "puseragent";
            param8.DbType = DbType.String;
            if (!string.IsNullOrEmpty(useragent))
                param8.Value = useragent;
            else
                param8.Value = DBNull.Value;
            command.Parameters.Add(param8);

            var param9 = command.CreateParameter();
            param9.ParameterName = "ptype";
            param9.DbType = DbType.String;
            param9.Value = "CUSTOM SERVICE";
            command.Parameters.Add(param9);

            var param10 = command.CreateParameter();
            param10.ParameterName = "presponsetime";
            param10.DbType = DbType.Double;
            param10.Value = 0;
            command.Parameters.Add(param10);

            var param11 = command.CreateParameter();
            param11.ParameterName = "rowcount";
            param11.DbType = DbType.Int16;
            param11.Direction = ParameterDirection.Output;
            command.Parameters.Add(param11);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateWebLogDataAsync(command);
            return result;
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
