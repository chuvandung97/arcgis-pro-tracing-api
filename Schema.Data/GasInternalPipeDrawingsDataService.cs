using Npgsql;
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
    public class GasInternalPipeDrawingsDataService : PgDbBase, IGasInternalPipeDrawingsDataService
    {
        public GasInternalPipeDrawingsDataService(IConfigService configService, ILoggingService loggingService)
           : base(configService, loggingService)
        { }
        public async Task<HashSet<Dictionary<string, object>>> GetGasInternalPipeDrawingsAsync(string PostalCode)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_gas_internal_pipe_drawings_validate");

            var param1 = command.CreateParameter();
            param1.ParameterName = "ppostalcode";
            param1.DbType = DbType.String;
            param1.Value = PostalCode;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetGasInternalPipeDrawingsHistoryAsync()
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_gas_internal_pipe_drawings_info");

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetGasInternalPipeDrawingsByPostalCodeAsync(string PostalCode)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_gas_internal_pipe_drawings_info");

            var param1 = command.CreateParameter();
            param1.ParameterName = "ppostalcode";
            param1.DbType = DbType.String;
            param1.Value = PostalCode;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<Dictionary<string, object>> UpdatePDFFileAsync(List<GasInternalPipeDrawingsItems> rowInfo, string UserID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_insert_gas_internal_pipe_drawings_pdf");

            var param1 = command.CreateParameter();
            param1.ParameterName = "ppostalcode";
            param1.DbType = DbType.String;
            param1.Value = rowInfo[0].PostalCode;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "ppdfname";
            param2.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].PDFName))
                param2.Value = rowInfo[0].PDFName;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);
            // Start - Added by tulasi on 18/10/2021 for RFC00000 - added new param to include pdf size
            var param3 = command.CreateParameter();
            param3.ParameterName = "ppdfsizekb";
            param3.DbType = DbType.Int32;
            param3.Value = rowInfo[0].PDFSize;
            command.Parameters.Add(param3);
            // End RFC00000
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
            return result;
        }
        public async Task<Dictionary<string, object>> DeletePDFFileAsync(List<GasInternalPipeDrawingsItems> rowInfo, string UserID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_delete_gas_internal_pipe_drawings_pdf");

            var param1 = command.CreateParameter();
            param1.ParameterName = "ppostalcode";
            param1.DbType = DbType.String;
            param1.Value = rowInfo[0].PostalCode;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "ppdfname";
            param2.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].PDFName))
                param2.Value = rowInfo[0].PDFName;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

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
            return result;
        }
        public async Task<Dictionary<string, object>> CreateGasInternalPipeDrawingsAsync(List<GasInternalPipeDrawingsItems> rowInfo, string UserID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_insert_gas_internal_pipe_drawings_info");

            var param1 = command.CreateParameter();
            param1.ParameterName = "ppostalcode";
            param1.DbType = DbType.String;
            param1.Value = rowInfo[0].PostalCode;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pprojectname";
            param2.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].ProjectName))
                param2.Value = rowInfo[0].ProjectName;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

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
            return result;
        }
        public async Task<Dictionary<string, object>> UpdateGasInternalPipeDrawingsAsync(List<GasInternalPipeDrawingsItems> rowInfo, string UserID)
        {

            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_gas_internal_pipe_drawings_Info");

            var param1 = command.CreateParameter();
            param1.ParameterName = "ppostalcode";
            param1.DbType = DbType.String;
            param1.Value = rowInfo[0].PostalCode;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pprojectname";
            param2.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].ProjectName))
                param2.Value = rowInfo[0].ProjectName;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "puserid";
            param3.DbType = DbType.String;
            param3.Value = UserID.ToUpper();
            command.Parameters.Add(param3);

            var param8 = command.CreateParameter();
            param8.ParameterName = "rowcount";
            param8.DbType = DbType.Int64;
            param8.Direction = ParameterDirection.Output;
            command.Parameters.Add(param8);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateDataAsync(command);
            return result;

        }
        public async Task<Dictionary<string, object>> DeleteGasInternalPipeDrawingsAsync(List<GasInternalPipeDrawingsItems> rowInfo, string UserID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_delete_gas_internal_pipe_drawings_info");

            var param1 = command.CreateParameter();
            param1.ParameterName = "ppostalcode";
            param1.DbType = DbType.String;
            param1.Value = rowInfo[0].PostalCode;
            command.Parameters.Add(param1);

            var param3 = command.CreateParameter();
            param3.ParameterName = "puserid";
            param3.DbType = DbType.String;
            param3.Value = UserID.ToUpper();
            command.Parameters.Add(param3);

            var param8 = command.CreateParameter();
            param8.ParameterName = "rowcount";
            param8.DbType = DbType.Int64;
            param8.Direction = ParameterDirection.Output;
            command.Parameters.Add(param8);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateDataAsync(command);
            return result;
        }
    }
}
