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
    public class SLDDataService : PgDbBase, ISLDDataService
    {
        public SLDDataService(IConfigService configService, ILoggingService loggingService)
            : base(configService, loggingService)
        { }
        public async Task<HashSet<Dictionary<string, object>>> GetSubstationListAsync(int Segment = 0, string SubstationName = null, string SheetID = null, string Voltage = null)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_sld_substation_list");
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);

            /*Npgsql.NpgsqlParameter param1 = new Npgsql.NpgsqlParameter("@val", NpgsqlTypes.NpgsqlDbType.Integer);
            Npgsql.NpgsqlParameter param2 = new Npgsql.NpgsqlParameter("@val", NpgsqlTypes.NpgsqlDbType.Text);
            Npgsql.NpgsqlParameter param3 = new Npgsql.NpgsqlParameter("@val", NpgsqlTypes.NpgsqlDbType.Text);
            Npgsql.NpgsqlParameter param4 = new Npgsql.NpgsqlParameter("@val", NpgsqlTypes.NpgsqlDbType.Text);

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "SELECT SubstationName, SheetID, Segment, Geometry FROM swift.vw_sld_substationlist";
            command.CommandType = System.Data.CommandType.Text;
            if (Segment != 0)
            {
                param1 = new Npgsql.NpgsqlParameter("@val", NpgsqlTypes.NpgsqlDbType.Integer);
                param1.Value = Segment;
                command.Parameters.Add(param1);
            }
            if (SubstationName != null)
            {
                param2 = new Npgsql.NpgsqlParameter("@val", NpgsqlTypes.NpgsqlDbType.Text);
                param2.Value = string.Format("{0}%", SubstationName.ToUpper());
                command.Parameters.Add(param2);
            }
            if (SheetID != null)
            {
                param3 = new Npgsql.NpgsqlParameter("@val", NpgsqlTypes.NpgsqlDbType.Text);
                param3.Value = string.Format("{0}%", SheetID.ToUpper());
                command.Parameters.Add(param3);
            }
            if (Voltage != null)
            {
                param4 = new Npgsql.NpgsqlParameter("@val", NpgsqlTypes.NpgsqlDbType.Text);
                param4.Value = string.Format("{0}%", Voltage.ToUpper());
                command.Parameters.Add(param4);
            }
            return await ReadDataAsync(command);*/
        }
        public async Task<HashSet<Dictionary<string, object>>> PropagateSLDToGISAsync(string Voltage, string Geometry, string MapSheetID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_propagate_sld_to_gis");
            var param1 = command.CreateParameter();
            param1.ParameterName = "voltage";
            param1.DbType = DbType.String;
            param1.Value = Voltage;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "xmin";
            param2.DbType = DbType.Double;
            param2.Value = Geometry.Split(',')[0];
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "ymin";
            param3.DbType = DbType.Double;
            param3.Value = Geometry.Split(',')[1];
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "xmax";
            param4.DbType = DbType.Double;
            param4.Value = Geometry.Split(',')[2];
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "ymax";
            param5.DbType = DbType.Double;
            param5.Value = Geometry.Split(',')[3];
            command.Parameters.Add(param5);

            var param6 = command.CreateParameter();
            param6.ParameterName = "mapsheetid";
            param6.DbType = DbType.String;
            param6.Value = MapSheetID;
            command.Parameters.Add(param6);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> PropagateGISToSLDAsync(string Voltage, string Geometry)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_propagate_gis_to_sld");
            var param1 = command.CreateParameter();
            param1.ParameterName = "voltage";
            param1.DbType = DbType.String;
            param1.Value = Voltage;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "xmin";
            param2.DbType = DbType.Double;
            param2.Value = Geometry.Split(',')[0];
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "ymin";
            param3.DbType = DbType.Double;
            param3.Value = Geometry.Split(',')[1];
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "xmax";
            param4.DbType = DbType.Double;
            param4.Value = Geometry.Split(',')[2];
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "ymax";
            param5.DbType = DbType.Double;
            param5.Value = Geometry.Split(',')[3];
            command.Parameters.Add(param5);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        /*public async Task<HashSet<Dictionary<string, object>>> SheetIDsListingAsync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_sld_sheetid_list");
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> JumpToDestinationAsync(string Geometry)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_sld_jump_to_destination");
            var param1 = command.CreateParameter();
            param1.ParameterName = "xmin";
            param1.DbType = DbType.Double;
            param1.Value = Geometry.Split(',')[0];
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "ymin";
            param2.DbType = DbType.Double;
            param2.Value = Geometry.Split(',')[1];
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "xmax";
            param3.DbType = DbType.Double;
            param3.Value = Geometry.Split(',')[2];
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "ymax";
            param4.DbType = DbType.Double;
            param4.Value = Geometry.Split(',')[3];
            command.Parameters.Add(param4);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }*/
        public async Task<HashSet<Dictionary<string, object>>> GetCableLengthAsync(string Voltage, string Geometry, string MapSheetID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.propagate_sld_gis_cable");
            var param1 = command.CreateParameter();
            param1.ParameterName = "voltage";
            param1.DbType = DbType.String;
            param1.Value = Voltage;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "xmin";
            param2.DbType = DbType.Double;
            param2.Value = Geometry.Split(',')[0];
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "ymin";
            param3.DbType = DbType.Double;
            param3.Value = Geometry.Split(',')[1];
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "xmax";
            param4.DbType = DbType.Double;
            param4.Value = Geometry.Split(',')[2];
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "ymax";
            param5.DbType = DbType.Double;
            param5.Value = Geometry.Split(',')[3];
            command.Parameters.Add(param5);

            var param6 = command.CreateParameter();
            param6.ParameterName = "mapsheetid";
            param6.DbType = DbType.String;
            param6.Value = MapSheetID;
            command.Parameters.Add(param6);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 2nd April 2019 for RFC0018439 -- To track and log searched SLD mapsheet/substation in database 
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
