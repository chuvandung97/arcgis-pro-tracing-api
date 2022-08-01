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
    public class POVerificationDataService : PgDbBase, IPOVerificationDataService
    {
        public POVerificationDataService(IConfigService configService, ILoggingService loggingService)
            : base(configService, loggingService)
        { }
        //added by Sandip on 1st Sept 2019 for RFC0021956 -- This api returns the complete list of PO Verification IDs that are assigned to the logged in Project Officer.
        public async Task<HashSet<Dictionary<string, object>>> GetPOJobInfoAsync(string ProjectOfficerID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_pov_jobinfo");

            var param1 = command.CreateParameter();
            param1.ParameterName = "poid";
            param1.DbType = DbType.String;
            param1.Value = ProjectOfficerID.ToUpper();
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on  8th Sept 2019 for RFC0021956 -- When PO approves/rejects As-Built data in Schema application, the same will be updated in PostgreSQL database.
        public async Task<Dictionary<string, object>> UpdatePOJobInfoAsync(List<POVerficationListItem> rowInfo)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_pov_jobinfo_by_po");

            var param1 = command.CreateParameter();
            param1.ParameterName = "ppovid";
            param1.DbType = DbType.Int64;
            param1.Value = rowInfo[0].povid;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "ppostatus";
            param2.DbType = DbType.String;
            if (rowInfo[0].postatus != null)
                param2.Value = rowInfo[0].postatus;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pporemarks";
            param3.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].poremarks))
                param3.Value = rowInfo[0].poremarks;
            else
                param3.Value = DBNull.Value;
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
        //added by Sandip on  19th Sept 2019 for RFC0021956 -- This api will be called from GEMS, based on jobstatus it will return true or false.
        public async Task<HashSet<Dictionary<string, object>>> GetPOVGemsEnablePOButtonAsync(Int64 SessionID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_pov_gems_enable_po_button");

            var param1 = command.CreateParameter();
            param1.ParameterName = "psessionid";
            param1.DbType = DbType.Int64;
            param1.Value = SessionID;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on  20th Sept 2019 for RFC0021956 -- This api will be called from GEMS, based on jobstatus it will return true or false.
        public async Task<HashSet<Dictionary<string, object>>> GetPOVGemsEnableReconcileButtonAsync(Int64 SessionID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_pov_gems_enable_reconcile_button");

            var param1 = command.CreateParameter();
            param1.ParameterName = "psessionid";
            param1.DbType = DbType.Int64;
            param1.Value = SessionID;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on  9th Oct 2019 for RFC0021956 -- This api will be called from GEMS, it will return the current job status of each session.
        public async Task<HashSet<Dictionary<string, object>>> GetPOVGemsJobStatusAsync(Int64 SessionID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_pov_gems_current_job_status");

            var param1 = command.CreateParameter();
            param1.ParameterName = "psessionid";
            param1.DbType = DbType.Int64;
            param1.Value = SessionID;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on  25th Sept 2019 for RFC0021956 -- This api will be called from GEMS, it will update various stages of landbase status.
        public async Task<Dictionary<string, object>> UpdatePOVGemsLandbaseStatusAsync(List<POVerficationListItem> rowInfo)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_pov_gems_landbase_status");

            var param1 = command.CreateParameter();
            param1.ParameterName = "psessionid";
            param1.DbType = DbType.Int64;
            param1.Value = rowInfo[0].sessionid;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pjobstatus";
            param2.DbType = DbType.String;
            if (rowInfo[0].jobstatus != null)
                param2.Value = rowInfo[0].jobstatus;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "psessionname";
            param3.DbType = DbType.String;
            param3.Value = rowInfo[0].sessionname;
            command.Parameters.Add(param3);


            var param4 = command.CreateParameter();
            param4.ParameterName = "pjsonfilename";
            param4.DbType = DbType.String;
            if (rowInfo[0].jsonname != null)
                param4.Value = rowInfo[0].jsonname;
            else
                param4.Value = DBNull.Value;
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "pmearemarks";
            param5.DbType = DbType.String;
            if (rowInfo[0].mearemarks != null)
                param5.Value = rowInfo[0].mearemarks;
            else
                param5.Value = DBNull.Value;
            command.Parameters.Add(param5);

            var param6 = command.CreateParameter();
            param6.ParameterName = "rowcount";
            param6.DbType = DbType.Int16;
            param6.Direction = ParameterDirection.Output;
            command.Parameters.Add(param6);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateDataAsync(command);
            return result;
        }
        //added by Sandip on  30th Sept 2019 for RFC0021956 -- This api will be called from GEMS, when MEA wants to reassign the Session back to PO for his/her verification.
        public async Task<Dictionary<string, object>> UpdatePOVGemsMEAReAssignAsync(List<POVerficationListItem> rowInfo)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_pov_gems_reassign_by_mea");

            var param0 = command.CreateParameter();
            param0.ParameterName = "psessionid";
            param0.DbType = DbType.Int64;
            param0.Value = rowInfo[0].sessionid;
            command.Parameters.Add(param0);

            var param1 = command.CreateParameter();
            param1.ParameterName = "psessionname";
            param1.DbType = DbType.String;
            param1.Value = rowInfo[0].sessionname;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pmearemarks";
            param2.DbType = DbType.String;
            param2.Value = rowInfo[0].mearemarks;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "rowcount";
            param3.DbType = DbType.Int16;
            param3.Direction = ParameterDirection.Output;
            command.Parameters.Add(param3);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateDataAsync(command);
            return result;
        }
        //added by Sandip on  24th Sept 2019 for RFC0021956 -- This api will be called from GEMS, it will update the jobstatus when the data is finally posted in GEMS.
        public async Task<Dictionary<string, object>> UpdatePOVGemsPostStatusAsync(List<POVerficationListItem> rowInfo)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_pov_gems_post_status");

            var param0 = command.CreateParameter();
            param0.ParameterName = "psessionid";
            param0.DbType = DbType.Int64;
            param0.Value = rowInfo[0].sessionid;
            command.Parameters.Add(param0);

            var param1 = command.CreateParameter();
            param1.ParameterName = "psessionname";
            param1.DbType = DbType.String;
            param1.Value = rowInfo[0].sessionname;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "ppostby";
            param2.DbType = DbType.String;
            if (rowInfo[0].postedby != null)
                param2.Value = rowInfo[0].postedby;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "rowcount";
            param3.DbType = DbType.Int16;
            param3.Direction = ParameterDirection.Output;
            command.Parameters.Add(param3);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateDataAsync(command);
            return result;
        }
        //added by Sandip on  27th Sept 2019 for RFC0021956 -- This api will be called from GEMS to update the PDFName against session name.
        public async Task<Dictionary<string, object>> UpdatePOVGemsPDFNameAsync(List<POVerficationListItem> rowInfo)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_pov_gems_pdfname");

            var param0 = command.CreateParameter();
            param0.ParameterName = "psessionid";
            param0.DbType = DbType.Int64;
            param0.Value = rowInfo[0].sessionid;
            command.Parameters.Add(param0);

            var param1 = command.CreateParameter();
            param1.ParameterName = "psessionname";
            param1.DbType = DbType.String;
            param1.Value = rowInfo[0].sessionname;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "ppdfname";
            param2.DbType = DbType.String;
            param2.Value = rowInfo[0].pdfname;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "rowcount";
            param3.DbType = DbType.Int16;
            param3.Direction = ParameterDirection.Output;
            command.Parameters.Add(param3);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateDataAsync(command);
            return result;
        }
        //added by Sandip on  19th Sept 2019 for RFC0021956 -- This api will be called from GEMS, based on jobstatus it will return true or false.
        public async Task<HashSet<Dictionary<string, object>>> GetEmailsIDsOfPOVOfficersAsync(Int64 SessionID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_pov_gems_emailids");

            var param1 = command.CreateParameter();
            param1.ParameterName = "psessionid";
            param1.DbType = DbType.Int64;
            param1.Value = SessionID;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 5th Nov 2019 for RFC0021956 -- This api will be called from GEMS to update the MEA Editor details for each session ids.
        public async Task<Dictionary<string, object>> UpdatePOVGemsMEAIDAsync(List<POVerficationListItem> rowInfo)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_pov_gems_meaid");

            var param1 = command.CreateParameter();
            param1.ParameterName = "psessionid";
            param1.DbType = DbType.Int64;
            param1.Value = rowInfo[0].sessionid;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pmeaid";
            param2.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].meaeditorid))
                param2.Value = rowInfo[0].meaeditorid.ToUpper();
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pmeaname";
            param3.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].meaeditorname))
                param3.Value = rowInfo[0].meaeditorname.ToUpper();
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "pmeaemail";
            param4.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].meaeditoremail))
                param4.Value = rowInfo[0].meaeditoremail.ToUpper();
            else
                param4.Value = DBNull.Value;
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "psessionname";
            param5.DbType = DbType.String;
            param5.Value = rowInfo[0].sessionname.ToUpper();
            command.Parameters.Add(param5);

            var param6 = command.CreateParameter();
            param6.ParameterName = "rowcount";
            param6.DbType = DbType.Int16;
            param6.Direction = ParameterDirection.Output;
            command.Parameters.Add(param6);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateDataAsync(command);
            return result;
        }
        //added by Sandip on  20th July 2021 for RFC0034115 -- This api will be called from GEMS, it will update totalfeatures, totallbfeatures, totallberror and qaqcpct.
        public async Task<Dictionary<string, object>> UpdatePOVGemsQAQCPctAsync(List<POVerficationListItem> rowInfo)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_pov_gems_qaqcpct");

            var param0 = command.CreateParameter();
            param0.ParameterName = "psessionid";
            param0.DbType = DbType.Int64;
            param0.Value = rowInfo[0].sessionid;
            command.Parameters.Add(param0);

            var param1 = command.CreateParameter();
            param1.ParameterName = "ptotalfeatures";
            param1.DbType = DbType.Int64;
            param1.Value = Convert.ToInt64(rowInfo[0].totalfeatures);
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "ptotallbfeatures";
            param2.DbType = DbType.Int64;
            param2.Value = Convert.ToInt64(rowInfo[0].totallbfeatures);
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "ptotallberror";
            param3.DbType = DbType.Int64;
            param3.Value = Convert.ToInt64(rowInfo[0].totallberror);
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "pqaqcpct";
            param4.DbType = DbType.Double;
            param4.Value = Convert.ToDouble(rowInfo[0].qaqcpct);
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
    }
}
