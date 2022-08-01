using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Schema.Core.Data;
using System.Data;
using Schema.Core.Services;

namespace Schema.Data
{
    public class SLDReportDataService : PgDbBase, ISLDReportDataService
    {
        public SLDReportDataService(IConfigService configService, ILoggingService loggingService)
            : base(configService, loggingService)
        { }
        public async Task<HashSet<Dictionary<string, object>>> SubstationListAsync(string Zoneval, string Voltage)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.mvc_sld_report_substation_list");
            var parameter = command.CreateParameter();
            parameter.ParameterName = "pzoneval";
            parameter.DbType = DbType.AnsiString;
            parameter.Value = Zoneval.ToUpper();
            command.Parameters.Add(parameter);

            var parameter1 = command.CreateParameter();
            parameter1.ParameterName = "pvoltageval";
            parameter1.DbType = DbType.AnsiString;
            if (Voltage == "22/6.6kV")
                parameter1.Value = "ALL";
            else
                parameter1.Value = Voltage.ToUpper();
            command.Parameters.Add(parameter1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> MaxLoadReadingAsync(int Voltage, string ReportType)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            if (ReportType == "Trace Results")
                command = new Npgsql.NpgsqlCommand("swift.mvc_get_sld_report_maxloadreading");
            else if (ReportType == "Ambiguous Trace Results")
                command = new Npgsql.NpgsqlCommand("swift.mvc_get_sld_report_maxloadreading_ambiguous");
            else if (ReportType == "With Alternative Source")
                command = new Npgsql.NpgsqlCommand("swift.mvc_get_sld_report_maxloadreading_thirdinjection");

            var parameter = command.CreateParameter();
            parameter.ParameterName = "pvoltage";
            parameter.DbType = DbType.Int16;
            parameter.Value = Voltage;
            command.Parameters.Add(parameter);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> TotalNetworkTransformerAsync(string mvaRating)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.mvc_get_sld_report_total_network_transformer");

            var parameter = command.CreateParameter();
            parameter.ParameterName = "pmvarating";
            parameter.DbType = DbType.String;
            parameter.Value = mvaRating;
            command.Parameters.Add(parameter);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> MaxMinTransformerCapacityReadingAsync(string searchTerm, int Voltage, string ReportType, string SearchCriteria)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            if (ReportType == "Trace Results")
                command = new Npgsql.NpgsqlCommand("swift.mvc_get_sld_report_trf_maxcapreading");
            else if (ReportType == "Ambiguous Trace Results")
                command = new Npgsql.NpgsqlCommand("swift.mvc_get_sld_report_trf_maxcapreading_ambiguous");
            //else if (ReportType == "Third Injection Results")
            //    command = new Npgsql.NpgsqlCommand("swift.mvc_sld_report_maxloadreading_thirdinjection_tracereport");

            var parameter = command.CreateParameter();
            parameter.ParameterName = "psource_substation";
            parameter.DbType = DbType.String;
            if (SearchCriteria == "Source Substation")
                parameter.Value = searchTerm.ToUpper();
            else
                parameter.Value = DBNull.Value;
            command.Parameters.Add(parameter);

            if (ReportType == "Trace Results")
            {
                var parameter1 = command.CreateParameter();
                parameter1.ParameterName = "ptarget_substation";
                parameter1.DbType = DbType.String;
                if (SearchCriteria == "Target Substation")
                    parameter1.Value = searchTerm.ToUpper();
                else
                    parameter1.Value = DBNull.Value;
                command.Parameters.Add(parameter1);
            }
            var parameter2 = command.CreateParameter();
            parameter2.ParameterName = "pvoltage";
            parameter2.DbType = DbType.Int16;
            parameter2.Value = Voltage;
            command.Parameters.Add(parameter2);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> CableTransformerRingAsync(string searchTerm, int Voltage, string ReportType, string SearchCriteria)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();

            if (ReportType == "Cable")
                command = new Npgsql.NpgsqlCommand("swift.mvc_get_sld_report_cable_ring");
            else if (ReportType == "Transformer")
            {
                if (SearchCriteria == "Substation")
                    command = new Npgsql.NpgsqlCommand("swift.mvc_get_sld_report_trf_ring_filter");
                else
                    command = new Npgsql.NpgsqlCommand("swift.mvc_get_sld_report_trf_ring");
            }
            var parameter = command.CreateParameter();
            parameter.ParameterName = "psource_substation";
            parameter.DbType = DbType.String;
            parameter.Value = searchTerm;
            command.Parameters.Add(parameter);

            var parameter1 = command.CreateParameter();
            parameter1.ParameterName = "pvoltage";
            parameter1.DbType = DbType.Int16;
            parameter1.Value = Voltage;
            command.Parameters.Add(parameter1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
    }
}
