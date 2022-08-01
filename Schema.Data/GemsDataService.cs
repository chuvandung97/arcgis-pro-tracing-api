using Schema.Core.Data;
using Schema.Core.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schema.Data
{
    public class GemsDataService : PgDbBase, IGemsDataService
    {
        public GemsDataService(IConfigService configService, ILoggingService loggingService)
            : base(configService, loggingService)
        { }
        public async Task<HashSet<Dictionary<string, object>>> AsBuiltDrawingsAsync(string Voltage, string Geometry, string SubtypeCD)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_as_built_drawings");
            var param1 = command.CreateParameter();
            param1.ParameterName = "pvoltage";
            param1.DbType = DbType.String;
            param1.Value = Voltage;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pxmin";
            param2.DbType = DbType.Double;
            param2.Value = Geometry.Split(',')[0];
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pymin";
            param3.DbType = DbType.Double;
            param3.Value = Geometry.Split(',')[1];
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "pxmax";
            param4.DbType = DbType.Double;
            param4.Value = Geometry.Split(',')[2];
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "pymax";
            param5.DbType = DbType.Double;
            param5.Value = Geometry.Split(',')[3];
            command.Parameters.Add(param5);

            var param6 = command.CreateParameter();
            param6.ParameterName = "psubtypecd";
            param6.DbType = DbType.String;
            param6.Value = SubtypeCD;
            command.Parameters.Add(param6);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetXSectionInfoAsync(string Geometry, string Query, int CriteriaID)
        {
            string voltage = "", subtype = "";
            for (int i = 0; i < Query.Split('|').Length; i++)
            {
                if (Query.Split('|')[i].Split(',')[0] != "")// if the value is null from the loop then prevent dummy commas
                    voltage += Query.Split('|')[i].Split(',')[0].Trim() + ",";
                if (Query.Split('|')[i].Split(',').Length >= 2 && Query.Split('|')[i].Split(',')[1] != "")// checking whether value have sub type or not
                    subtype += Query.Split('|')[i].Split(',')[1].Trim() + ",";
            }
            // removing comma appended at the end
            if (voltage != "")// check if voltage have value
                voltage = voltage.Substring(0, voltage.Length - 1);
            if (subtype != "")// check is sub type have vaule
                subtype = subtype.Substring(0, subtype.Length - 1);

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_gis_cross_section");
            var param1 = command.CreateParameter();
            param1.ParameterName = "pxmin";
            param1.DbType = DbType.Double;
            param1.Value = Geometry.Split(',')[0];
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pymin";
            param2.DbType = DbType.Double;
            param2.Value = Geometry.Split(',')[1];
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pxmax";
            param3.DbType = DbType.Double;
            param3.Value = Geometry.Split(',')[2];
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "pymax";
            param4.DbType = DbType.Double;
            param4.Value = Geometry.Split(',')[3];
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "pvoltage";
            param5.DbType = DbType.String;
            param5.Value = voltage;
            command.Parameters.Add(param5);

            var param6 = command.CreateParameter();
            param6.ParameterName = "psubtype";
            param6.DbType = DbType.String;
            param6.Value = subtype;
            command.Parameters.Add(param6);

            var param7 = command.CreateParameter();
            param7.ParameterName = "pcriteriaid";
            param7.DbType = DbType.Int16;
            param7.Value = CriteriaID;
            command.Parameters.Add(param7);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        /*public async Task<HashSet<Dictionary<string, object>>> GetGridGeometryAsync(string SegmentID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_grid_geometry");
            var param1 = command.CreateParameter();
            param1.ParameterName = "segmentid";
            param1.DbType = DbType.String;
            param1.Value = SegmentID;
            command.Parameters.Add(param1);
            command.CommandType = CommandType.StoredProcedure;

            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetSchematicSegmentGeomAsync(int SegNum, string SheetID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_schematic_segment_geometry");
            var param1 = command.CreateParameter();
            param1.ParameterName = "segnum";
            param1.DbType = DbType.Int32;
            param1.Value = SegNum;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "sheetid";
            param2.DbType = DbType.String;
            param2.Value = SheetID;
            command.Parameters.Add(param2);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }*/
        public async Task<HashSet<Dictionary<string, object>>> GetNMACSDetailsAsync(double X, double Y, string Voltage, string VoltageType, string Geometry, string MapSheetID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();

            if (Geometry != null && MapSheetID != null)
            {
                var coords = Geometry.Split(',');
                string wkt = @"POLYGON ((" + coords[0] + " " + coords[1] + "," + coords[2] + " " + coords[1] + "," + coords[2] + " " + coords[3] + "," + coords[0] + " " + coords[3] + "," + coords[0] + " " + coords[1] + "))";

                command = new Npgsql.NpgsqlCommand("swift.api_get_sld_nmacs_details");

                var param1 = command.CreateParameter();
                param1.ParameterName = "pgeometry";
                param1.DbType = DbType.String;
                param1.Value = wkt;
                command.Parameters.Add(param1);

                var param2 = command.CreateParameter();
                param2.ParameterName = "pmapsheetid";
                param2.DbType = DbType.String;
                param2.Value = MapSheetID.ToUpper();
                command.Parameters.Add(param2);
                command.CommandType = CommandType.StoredProcedure;
                result = await ReadDataAsync(command);
            }
            else if (X != 0.0 && Y != 0.0)
            {
                command = new Npgsql.NpgsqlCommand("swift.api_get_gis_nmacs_details");
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
                param3.ParameterName = "pvolt";
                param3.DbType = DbType.String;
                if (!string.IsNullOrEmpty(Voltage) && !string.IsNullOrWhiteSpace(Voltage))
                    param3.Value = Voltage;
                else
                    param3.Value = DBNull.Value;
                command.Parameters.Add(param3);

                var param4 = command.CreateParameter();
                param4.ParameterName = "ptype";
                param4.DbType = DbType.String;
                if (!string.IsNullOrEmpty(VoltageType) && !string.IsNullOrWhiteSpace(VoltageType))
                    param4.Value = VoltageType;
                else
                    param4.Value = DBNull.Value;
                command.Parameters.Add(param4);
                command.CommandType = CommandType.StoredProcedure;
                result = await ReadDataAsync(command);
            }
            return result;
        }
        public async Task<HashSet<Dictionary<string, object>>> GetCustomerCountAsync(string UserID, string Geometry = null, double X = 0.0, double Y = 0.0)
        {
            /*string wkt = string.Empty;
            if (Geometry != null)
            {
                var coords = Geometry.Split(',');
                wkt = @"POLYGON ((" + coords[0] + " " + coords[1] + "," + coords[2] + " " + coords[1] + "," + coords[2] + " " + coords[3] + "," + coords[0] + " " + coords[3] + "," + coords[0] + " " + coords[1] + "))";
            }*/

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_building_customer_count");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pgeometry";
            param1.DbType = DbType.String;
            if (!string.IsNullOrEmpty(Geometry) && !string.IsNullOrWhiteSpace(Geometry))
                param1.Value = Geometry;
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "px";
            param2.DbType = DbType.Double;
            if (X != 0.0)
                param2.Value = X;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "py";
            param3.DbType = DbType.Double;
            if (Y != 0.0)
                param3.Value = Y;
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "puserid";
            param4.DbType = DbType.String;
            if (!string.IsNullOrEmpty(UserID))
                param4.Value = UserID;
            else
                param4.Value = DBNull.Value;
            command.Parameters.Add(param4);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> Get66kvSupplyZoneLayersAsync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_66kv_supply_zone_details");
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> Get66KVCustomerCountAsync(double X, double Y)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_66kv_supply_zone_customer_count_old");

            var param2 = command.CreateParameter();
            param2.ParameterName = "px";
            param2.DbType = DbType.Double;
            if (X != 0.0)
                param2.Value = X;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "py";
            param3.DbType = DbType.Double;
            if (Y != 0.0)
                param3.Value = Y;
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        /*public async Task<HashSet<Dictionary<string, object>>> GetAMIOutageCustomerCountAsync(object JsonObj)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_ami_outage_customer_count");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pqueryjson";
            param1.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Json;
            param1.Value = JsonObj;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }*/
    }
}
