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
    public class TraceDataService : PgDbBase, ITraceDataService
    {
        public TraceDataService(IConfigService configService, ILoggingService loggingService)
          : base(configService, loggingService)
        { }

        public async Task<HashSet<Dictionary<string, object>>> PlanningSelectCablesAsync(string Geometry, int Voltage, int[] Subtypecd)
        {
            var coords = Geometry.Split(',');
            string wkt = @"POLYGON ((" + coords[0] + " " + coords[1] + "," + coords[2] + " " + coords[1] + "," + coords[2] + " " + coords[3] + "," + coords[0] + " " + coords[3] + "," + coords[0] + " " + coords[1] + "))";

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_cables_by_geometry");
            var param1 = command.CreateParameter();
            param1.ParameterName = "geometry";
            param1.DbType = DbType.String;
            param1.Value = wkt;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "voltage";
            param2.DbType = DbType.Int16;
            param2.Value = Voltage;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "subtypecd";
            param3.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer;
            param3.Value = Subtypecd;
            command.Parameters.Add(param3);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }

        public async Task<HashSet<Dictionary<string, object>>> CablesByGeometryAsync(string Geometry, int Voltage, int[] Subtypecd)
        {
            var coords = Geometry.Split(',');
            string wkt = @"POLYGON ((" + coords[0] + " " + coords[1] + "," + coords[2] + " " + coords[1] + "," + coords[2] + " " + coords[3] + "," + coords[0] + " " + coords[3] + "," + coords[0] + " " + coords[1] + "))";

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_cables_by_geometry_voltage_subtypes");
            var param1 = command.CreateParameter();
            param1.ParameterName = "geometry";
            param1.DbType = DbType.String;
            param1.Value = wkt;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "voltage";
            param2.DbType = DbType.Int16;
            param2.Value = Voltage;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "subtypecd";
            param3.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer;
            param3.Value = Subtypecd;
            command.Parameters.Add(param3);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }

        public async Task<HashSet<Dictionary<string, object>>> CablesByGlobalIdsAsync(string[] GlobalIds)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_cables_by_globalids");
            var param1 = command.CreateParameter();
            param1.ParameterName = "pglobalids";
            param1.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Text;
            param1.Value = GlobalIds;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }

        public async Task<HashSet<Dictionary<string, object>>> CablesTraceAsync(long[] CableIds)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_trace_cables");
            var param1 = command.CreateParameter();
            param1.ParameterName = "peids";
            param1.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Bigint;
            param1.Value = CableIds;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> ContextMenuCableTraceAsync(long[] CableIds)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_trace_cables");
            var param1 = command.CreateParameter();
            param1.ParameterName = "peids";
            param1.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Bigint;
            param1.Value = CableIds;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }

        //public async Task<int> CablesCountAsync(string Geometry, int Voltage, int[] Subtypecd)
        //{
        //    var coords = Geometry.Split(',');
        //    string wkt = @"POLYGON ((" + coords[0] + " " + coords[1] + "," + coords[2] + " " + coords[1] + "," + coords[2] + " " + coords[3] + "," + coords[0] + " " + coords[3] + "," + coords[0] + " " + coords[1] + "))";

        //    Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_cables_count_by_geometry");
        //    var param1 = command.CreateParameter();
        //    param1.ParameterName = "geometry";
        //    param1.DbType = DbType.String;
        //    param1.Value = wkt;
        //    command.Parameters.Add(param1);

        //    var param2 = command.CreateParameter();
        //    param2.ParameterName = "voltage";
        //    param2.DbType = DbType.Int16;
        //    param2.Value = Voltage;
        //    command.Parameters.Add(param2);

        //    var param3 = command.CreateParameter();
        //    param3.ParameterName = "subtypecd";
        //    param3.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer;
        //    param3.Value = Subtypecd;
        //    command.Parameters.Add(param3);

        //    command.CommandType = CommandType.StoredProcedure;
        //    int cableCount = await ScalarDataAsync(command);

        //    return cableCount;
        //}
        public async Task<HashSet<Dictionary<string, object>>> PlanningSelectBarriersAsync(string Geometry)
        {
            var coords = Geometry.Split(',');
            string wkt = @"POLYGON ((" + coords[0] + " " + coords[1] + "," + coords[2] + " " + coords[1] + "," + coords[2] + " " + coords[3] + "," + coords[0] + " " + coords[3] + "," + coords[0] + " " + coords[1] + "))";

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_barriers_by_geometry");
            var param1 = command.CreateParameter();
            param1.ParameterName = "pgeometry";
            param1.DbType = DbType.String;
            param1.Value = wkt;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //public async Task<HashSet<Dictionary<string, object>>> SubstationsAsync(int Eid, int Fid)
        //{
        //    Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_substation_to_substation_trace");
        //    var param1 = command.CreateParameter();
        //    param1.ParameterName = "peid";
        //    param1.DbType = DbType.Int64;
        //    param1.Value = Eid;
        //    command.Parameters.Add(param1);

        //    var param2 = command.CreateParameter();
        //    param2.ParameterName = "pfid";
        //    param2.DbType = DbType.Int16;
        //    param2.Value = Fid;
        //    command.Parameters.Add(param2);

        //    command.CommandType = CommandType.StoredProcedure;
        //    return await ReadDataAsync(command);
        //}
        //public async Task<HashSet<Dictionary<string, object>>> DistributionTraceAsync(Int32 eid, Int16 fid)
        //{
        //    HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();

        //    Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_substation_to_substation_distribution_trace");
        //    var param1 = command.CreateParameter();
        //    param1.ParameterName = "peid";
        //    param1.DbType = DbType.Int32;
        //    param1.Value = eid;
        //    command.Parameters.Add(param1);

        //    var param2 = command.CreateParameter();
        //    param2.ParameterName = "pfid";
        //    param2.DbType = DbType.Int16;
        //    param2.Value = fid;
        //    command.Parameters.Add(param2);

        //    command.CommandType = CommandType.StoredProcedure;
        //    result = await ReadDataAsync(command);
        //    return result;
        //}

        //public async Task<HashSet<Dictionary<string, object>>> DistributionTraceAsync(string GlobalId)
        //{
        //    HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();

        //    Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_substation_to_substation_distribution_desk_trace");
        //    var param1 = command.CreateParameter();
        //    param1.ParameterName = "pglobalid";
        //    param1.DbType = DbType.String;
        //    param1.Value = GlobalId;
        //    command.Parameters.Add(param1);

        //    command.CommandType = CommandType.StoredProcedure;
        //    result = await ReadDataAsync(command);
        //    return result;
        //}

        //public async Task<HashSet<Dictionary<string, object>>> TransmissionTraceAsync(Int32 eid)
        //{
        //    HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();

        //    Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_substation_to_substation_transmission_trace");
        //    var param1 = command.CreateParameter();
        //    param1.ParameterName = "peid";
        //    param1.DbType = DbType.Int32;
        //    param1.Value = eid;
        //    command.Parameters.Add(param1);

        //    command.CommandType = CommandType.StoredProcedure;
        //    result = await ReadDataAsync(command);
        //    return result;
        //}
        //public HashSet<Dictionary<string, object>> GetTransmissionCablesByGeometry(string Geometry, int Voltage, int[] Subtypecd)
        //{
        //    HashSet<Dictionary<string, object>> selectedcableResult = new HashSet<Dictionary<string, object>>();
        //    var coords = Geometry.Split(',');
        //    string wkt = @"POLYGON ((" + coords[0] + " " + coords[1] + "," + coords[2] + " " + coords[1] + "," + coords[2] + " " + coords[3] + "," + coords[0] + " " + coords[3] + "," + coords[0] + " " + coords[1] + "))";

        //    Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_transmission_cables_by_geometry");
        //    var param1 = command.CreateParameter();
        //    param1.ParameterName = "geometry";
        //    param1.DbType = DbType.String;
        //    param1.Value = wkt;
        //    command.Parameters.Add(param1);

        //    var param2 = command.CreateParameter();
        //    param2.ParameterName = "voltage";
        //    param2.DbType = DbType.Int16;
        //    param2.Value = Voltage;
        //    command.Parameters.Add(param2);

        //    var param3 = command.CreateParameter();
        //    param3.ParameterName = "subtypecd";
        //    param3.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer;
        //    param3.Value = Subtypecd;
        //    command.Parameters.Add(param3);

        //    command.CommandType = CommandType.StoredProcedure;
        //    selectedcableResult = ReadData(command);
        //    return selectedcableResult;
        //}
        //public async Task<int> GetTransmissionCablesCountByGeometry(string Geometry, int Voltage, int[] Subtypecd)
        //{
        //    HashSet<Dictionary<string, object>> selectedcableResult = new HashSet<Dictionary<string, object>>();
        //    var coords = Geometry.Split(',');
        //    string wkt = @"POLYGON ((" + coords[0] + " " + coords[1] + "," + coords[2] + " " + coords[1] + "," + coords[2] + " " + coords[3] + "," + coords[0] + " " + coords[3] + "," + coords[0] + " " + coords[1] + "))";

        //    Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_transmission_cables_count_by_geometry");
        //    var param1 = command.CreateParameter();
        //    param1.ParameterName = "pgeometry";
        //    param1.DbType = DbType.String;
        //    param1.Value = wkt;
        //    command.Parameters.Add(param1);

        //    var param2 = command.CreateParameter();
        //    param2.ParameterName = "pvoltage";
        //    param2.DbType = DbType.Int16;
        //    param2.Value = Voltage;
        //    command.Parameters.Add(param2);

        //    var param3 = command.CreateParameter();
        //    param3.ParameterName = "psubtypecd";
        //    param3.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer;
        //    param3.Value = Subtypecd;
        //    command.Parameters.Add(param3);

        //    command.CommandType = CommandType.StoredProcedure;
        //    int cableCount = await ScalarDataAsync(command);
        //    return cableCount;
        //}
        public async Task<HashSet<Dictionary<string, object>>> DownstreamTraceAsync(int EdgeId, int FeederId, int VoltageDepth, int[] Barriers)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_trace_downstream_voltage_barriers");
            command.CommandType = CommandType.StoredProcedure;

            var param1 = command.CreateParameter();
            param1.ParameterName = "peid";
            param1.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bigint;
            param1.Value = EdgeId;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pfid";
            param2.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
            param2.Value = FeederId;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pvoltage";
            param3.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
            param3.Value = VoltageDepth;
            command.Parameters.Add(param3);

            if (Barriers != null && Barriers.Length > 0)
            {
                var param4 = command.CreateParameter();
                param4.ParameterName = "pbarriers";
                param4.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer;
                param4.Value = Barriers;
                command.Parameters.Add(param4);
            }
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> DownstreamTraceReportAsync(int EdgeId, int FeederId, int VoltageDepth, int[] Barriers)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_trace_downstream_substation_voltage_barriers");
            command.CommandType = CommandType.StoredProcedure;

            var param1 = command.CreateParameter();
            param1.ParameterName = "peid";
            param1.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bigint;
            param1.Value = EdgeId;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pfid";
            param2.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
            param2.Value = FeederId;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pvoltage";
            param3.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
            param3.Value = VoltageDepth;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "pbarriers";
            param4.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer;
            param4.Value = Barriers;
            command.Parameters.Add(param4);

            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> UpstreamTraceAsync(int EdgeId, int FeederId)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_trace_upstream");
            command.CommandType = CommandType.StoredProcedure;

            var param1 = command.CreateParameter();
            param1.ParameterName = "peid";
            param1.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bigint;
            param1.Value = EdgeId;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pfid";
            param2.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
            param2.Value = FeederId;
            command.Parameters.Add(param2);

            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> UpstreamTraceReportAsync(int EdgeId, int FeederId)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_trace_upstream_substation");
            command.CommandType = CommandType.StoredProcedure;

            var param1 = command.CreateParameter();
            param1.ParameterName = "peid";
            param1.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bigint;
            param1.Value = EdgeId;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pfid";
            param2.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
            param2.Value = FeederId;
            command.Parameters.Add(param2);

            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> SLDRingTraceAsync(string Geometry)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            var coords = Geometry.Split(',');
            string wkt = @"POLYGON ((" + coords[0] + " " + coords[1] + "," + coords[2] + " " + coords[1] + "," + coords[2] + " " + coords[3] + "," + coords[0] + " " + coords[3] + "," + coords[0] + " " + coords[1] + "))";

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_sld_single_feeder");
            var param1 = command.CreateParameter();
            param1.ParameterName = "geometry";
            param1.DbType = DbType.String;
            param1.Value = wkt;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            result = await ReadDataAsync(command);
            return result;
        }

        //public async Task<HashSet<Dictionary<string, object>>> SLDRingTraceDesktopAsync(string[] GlobalIds)
        //{
        //    HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();

        //    Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_sld_single_feeder_desktop");
        //    var param1 = command.CreateParameter();
        //    param1.ParameterName = "pglobalids";
        //    param1.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Text;
        //    param1.Value = GlobalIds;
        //    command.Parameters.Add(param1);

        //    command.CommandType = CommandType.StoredProcedure;
        //    result = await ReadDataAsync(command);
        //    return result;
        //}
        public async Task<HashSet<Dictionary<string, object>>> SLDRingItemsAsync(int feederID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_sld_ring_trace");
            var param1 = command.CreateParameter();
            param1.ParameterName = "pfid";
            param1.DbType = DbType.Int32;
            param1.Value = feederID;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            result = await ReadDataAsync(command);
            return result;
        }

        //public async Task<HashSet<Dictionary<string, object>>> SLDRingItemsDesktopAsync(int feederID)
        //{
        //    HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
        //    Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_sld_ring_trace_desktop");
        //    var param1 = command.CreateParameter();
        //    param1.ParameterName = "pfid";
        //    param1.DbType = DbType.Int32;
        //    param1.Value = feederID;
        //    command.Parameters.Add(param1);

        //    command.CommandType = CommandType.StoredProcedure;
        //    result = await ReadDataAsync(command);
        //    return result;
        //}

        public async Task<HashSet<Dictionary<string, object>>> SLDRingReportAsync(int feederID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_sld_ring_report");
            var param1 = command.CreateParameter();
            param1.ParameterName = "pfid";
            param1.DbType = DbType.Int32;
            param1.Value = feederID;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            result = await ReadDataAsync(command);
            return result;
        }
        public async Task<HashSet<Dictionary<string, object>>> SLDMultiRingTraceAsync(string Geometry)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            var coords = Geometry.Split(',');
            string wkt = @"POLYGON ((" + coords[0] + " " + coords[1] + "," + coords[2] + " " + coords[1] + "," + coords[2] + " " + coords[3] + "," + coords[0] + " " + coords[3] + "," + coords[0] + " " + coords[1] + "))";

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_sld_multi_feeder");
            var param1 = command.CreateParameter();
            param1.ParameterName = "geometry";
            param1.DbType = DbType.String;
            param1.Value = wkt;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            result = await ReadDataAsync(command);
            return result;
        }

        //public async Task<HashSet<Dictionary<string, object>>> SLDMultiRingDesktopAsync(string[] GlobalIds)
        //{
        //    HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();

        //    Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_sld_multi_feeder_desktop");
        //    var param1 = command.CreateParameter();
        //    param1.ParameterName = "pglobalids";
        //    param1.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Text;
        //    param1.Value = GlobalIds;
        //    command.Parameters.Add(param1);

        //    command.CommandType = CommandType.StoredProcedure;
        //    result = await ReadDataAsync(command);
        //    return result;
        //}
        /*public async Task<HashSet<Dictionary<string, object>>> SLDCustomerCountTraceItemsAsync(int feederID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_sld_cust_cnt_trace");
            var param1 = command.CreateParameter();
            param1.ParameterName = "pfid";
            param1.DbType = DbType.Int32;
            param1.Value = feederID;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            result = await ReadDataAsync(command);
            return result;
        }*/
        //Sandip
        public async Task<HashSet<Dictionary<string, object>>> SLDCustomerCountTraceItemsAsync(int FeederId, int ElementId, int[] Barriers, int Direction = 0)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_sld_cust_cnt_trace");
            var param1 = command.CreateParameter();
            param1.ParameterName = "pfid";
            param1.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
            param1.Value = FeederId;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "peid";
            param2.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Bigint;
            param2.Value = ElementId;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pbarriers";
            param3.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer;
            param3.Value = Barriers;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "pdirection";
            param4.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Integer;
            param4.Value = Direction;
            command.Parameters.Add(param4);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> SLDCustomerCountTraceReportAsync(int feederID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_sld_customer_count_ring_trace_report");
            var param1 = command.CreateParameter();
            param1.ParameterName = "pfid";
            param1.DbType = DbType.Int32;
            param1.Value = feederID;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            result = await ReadDataAsync(command);
            return result;
        }

        //public async Task<HashSet<Dictionary<string, object>>> GISRingTraceItemsForDesktopAsync(int SLDFeederID)
        //{
        //    HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
        //    Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_trace_gis_ring_by_sld_ring");
        //    var param1 = command.CreateParameter();
        //    param1.ParameterName = "psldfid";
        //    param1.DbType = DbType.Int32;
        //    param1.Value = SLDFeederID;
        //    command.Parameters.Add(param1);

        //    command.CommandType = CommandType.StoredProcedure;
        //    result = await ReadDataAsync(command);
        //    return result;
        //}

        public async Task<HashSet<Dictionary<string, object>>> GetSLDSingleFeederAsync(string Geometry = null, string[] GlobalIds = null)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_sld_single_feeder");
            command.CommandType = CommandType.StoredProcedure;
            if (!string.IsNullOrEmpty(Geometry))
            {
                var coords = Geometry.Split(',');
                string wkt = @"POLYGON ((" + coords[0] + " " + coords[1] + "," + coords[2] + " " + coords[1] + "," + coords[2] + " " + coords[3] + "," + coords[0] + " " + coords[3] + "," + coords[0] + " " + coords[1] + "))";

                var param1 = command.CreateParameter();
                param1.ParameterName = "pgeometry";
                param1.DbType = DbType.String;
                param1.Value = wkt;
                command.Parameters.Add(param1);
            }
            if (GlobalIds != null && GlobalIds.Length > 0)
            {
                var param2 = command.CreateParameter();
                param2.ParameterName = "pglobalids";
                param2.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Text;
                param2.Value = GlobalIds;
                command.Parameters.Add(param2);
            }

            return await ReadDataAsync(command);
        }

        public async Task<HashSet<Dictionary<string, object>>> GetSLDMultiFeederAsync(string Geometry = null, string[] GlobalIds = null)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_sld_multi_feeder");
            command.CommandType = CommandType.StoredProcedure;
            if (!string.IsNullOrEmpty(Geometry))
            {
                var coords = Geometry.Split(',');
                string wkt = @"POLYGON ((" + coords[0] + " " + coords[1] + "," + coords[2] + " " + coords[1] + "," + coords[2] + " " + coords[3] + "," + coords[0] + " " + coords[3] + "," + coords[0] + " " + coords[1] + "))";

                var param1 = command.CreateParameter();
                param1.ParameterName = "pgeometry";
                param1.DbType = DbType.String;
                param1.Value = wkt;
                command.Parameters.Add(param1);
            }
            if (GlobalIds != null && GlobalIds.Length > 0)
            {
                var param2 = command.CreateParameter();
                param2.ParameterName = "pglobalids";
                param2.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Text;
                param2.Value = GlobalIds;
                command.Parameters.Add(param2);
            }

            return await ReadDataAsync(command);
        }

        public async Task<HashSet<Dictionary<string, object>>> SLDPropagateRingAsync(int SLDFeederID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_sld_propagate_ring");

            var param1 = command.CreateParameter();
            param1.ParameterName = "psldfid";
            param1.DbType = DbType.Int32;
            param1.Value = SLDFeederID;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            result = await ReadDataAsync(command);
            return result;
        }

        public async Task<HashSet<Dictionary<string, object>>> SLDXVoltSubstationsAsync(string Name = null, string RequestFlag = "XVOLT")
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_sld_xvolt_substations");

            var param1 = command.CreateParameter();
            param1.ParameterName = "psubname";
            param1.DbType = DbType.String;

            if (string.IsNullOrEmpty(Name))
                param1.Value = DBNull.Value;
            else
                param1.Value = string.Format("{0}%", Name.Trim());

            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "prequestflag";
            param2.DbType = DbType.String;
            param2.Value = RequestFlag.ToUpper();
            command.Parameters.Add(param2);

            command.CommandType = CommandType.StoredProcedure;
            result = await ReadDataAsync(command);
            return result;
        }

        public async Task<HashSet<Dictionary<string, object>>> SLDXVoltTransformersAsync(string MRC)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_sld_xvolt_transformers");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pmrc";
            param1.DbType = DbType.String;
            param1.Value = MRC;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            result = await ReadDataAsync(command);
            return result;
        }
        public async Task<HashSet<Dictionary<string, object>>> SLDSingleFeedersAync(string Geometry, bool IsMultiFeeder = false)
        {
            var coords = Geometry.Split(',');
            string wkt = @"POLYGON ((" + coords[0] + " " + coords[1] + "," + coords[2] + " " + coords[1] + "," + coords[2] + " " + coords[3] + "," + coords[0] + " " + coords[3] + "," + coords[0] + " " + coords[1] + "))";

            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_sld_single_feeder");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pgeometry";
            param1.DbType = DbType.String;
            param1.Value = wkt;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pmultifeeder";
            param2.DbType = DbType.Boolean;
            param2.Value = IsMultiFeeder;
            command.Parameters.Add(param2);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> SLDXVoltageSelectBarriersAsync(string Geometry)
        {
            var coords = Geometry.Split(',');
            string wkt = @"POLYGON ((" + coords[0] + " " + coords[1] + "," + coords[2] + " " + coords[1] + "," + coords[2] + " " + coords[3] + "," + coords[0] + " " + coords[3] + "," + coords[0] + " " + coords[1] + "))";

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_sld_xvolt_barriers");
            var param1 = command.CreateParameter();
            param1.ParameterName = "pgeometry";
            param1.DbType = DbType.String;
            param1.Value = wkt;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }

        public async Task<HashSet<Dictionary<string, object>>> SLDXVoltTraceFeedersAsync(int TransformerId, int Voltage, int[] Barriers = null)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_sld_xvolt_trace");

            var param1 = command.CreateParameter();
            param1.ParameterName = "ptid";
            param1.DbType = DbType.Int64;
            param1.Value = TransformerId;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pvoltage";
            param2.DbType = DbType.Int32;
            param2.Value = Voltage;
            command.Parameters.Add(param2);

            if (Barriers != null && Barriers.Length > 0)
            {
                var param3 = command.CreateParameter();
                param3.ParameterName = "pbarriers";
                param3.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Integer;
                param3.Value = Barriers;
                command.Parameters.Add(param3);
            }

            command.CommandType = CommandType.StoredProcedure;
            result = await ReadDataAsync(command);
            return result;
        }

        public async Task<HashSet<Dictionary<string, object>>> SLDXVoltTraceFeedersReportAsync(int TransformerId, int Voltage, string[] Barriers = null)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_sld_xvolt_trace_report");

            var param1 = command.CreateParameter();
            param1.ParameterName = "ptid";
            param1.DbType = DbType.Int64;
            param1.Value = TransformerId;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pvoltage";
            param2.DbType = DbType.Int32;
            param2.Value = Voltage;
            command.Parameters.Add(param2);

            command.CommandType = CommandType.StoredProcedure;
            result = await ReadDataAsync(command);
            return result;
        }

        public async Task<HashSet<Dictionary<string, object>>> GetOGBAsync(string Geometry = null, string GlobalId = null)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_gis_select_ogb");
            command.CommandType = CommandType.StoredProcedure;
            if (!string.IsNullOrEmpty(Geometry))
            {
                var coords = Geometry.Split(',');
                string wkt = @"POLYGON ((" + coords[0] + " " + coords[1] + "," + coords[2] + " " + coords[1] + "," + coords[2] + " " + coords[3] + "," + coords[0] + " " + coords[3] + "," + coords[0] + " " + coords[1] + "))";

                var param1 = command.CreateParameter();
                param1.ParameterName = "pgeometry";
                param1.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text;
                param1.Value = wkt;
                command.Parameters.Add(param1);
            }
            if (!string.IsNullOrEmpty(GlobalId))
            {
                var param2 = command.CreateParameter();
                param2.ParameterName = "pglobalid";
                param2.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Text;
                param2.Value = GlobalId;
                command.Parameters.Add(param2);
            }

            return await ReadDataAsync(command);
        }

        public async Task<HashSet<Dictionary<string, object>>> OGBUpstreamTraceAsync(long SwitchBoardID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_gis_trace_ogb_upstream");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pswbid";
            param1.DbType = DbType.Int64;
            param1.Value = SwitchBoardID;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            result = await ReadDataAsync(command);
            return result;
        }

        public async Task<HashSet<Dictionary<string, object>>> OGBDownstreamTraceAsync(long SwitchBoardID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_gis_trace_ogb_downstream");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pswbid";
            param1.DbType = DbType.Int64;
            param1.Value = SwitchBoardID;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            result = await ReadDataAsync(command);
            return result;
        }

        public async Task<HashSet<Dictionary<string, object>>> OGBFirstLegTraceAsync(long SwitchBoardID)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_gis_trace_ogb_firstleg");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pswbid";
            param1.DbType = DbType.Int64;
            param1.Value = SwitchBoardID;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            result = await ReadDataAsync(command);
            return result;
        }
    }
}
