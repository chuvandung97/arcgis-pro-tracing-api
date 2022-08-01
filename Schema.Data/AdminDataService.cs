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

namespace Schema.Data
{
    public class AdminDataService : PgDbBase, IAdminDataService
    {
        public AdminDataService(IConfigService configService, ILoggingService loggingService)
          : base(configService, loggingService)
        { }
        public async Task<HashSet<Dictionary<string, object>>> GetMapServiceCount()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_mapservicecounter_total");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pinputdate";
            param1.DbType = DbType.Date;
            param1.Value = DateTime.Now;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pnumberofdate";
            param2.DbType = DbType.Int16;
            param2.Value = 90;
            command.Parameters.Add(param2);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetFunctionUsageCount()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_apicounter_total");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pinputdate";
            param1.DbType = DbType.Date;
            param1.Value = DateTime.Now;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pnumberofdate";
            param2.DbType = DbType.Int16;
            param2.Value = 90;
            command.Parameters.Add(param2);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetLoginCount()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_userlogincounter_total");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pinputdate";
            param1.DbType = DbType.Date;
            param1.Value = DateTime.Now;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pnumberofdate";
            param2.DbType = DbType.Int16;
            param2.Value = 90;
            command.Parameters.Add(param2);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetErrorCount()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_errorcounter_total");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pinputdate";
            param1.DbType = DbType.Date;
            param1.Value = DateTime.Now;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pnumberofdate";
            param2.DbType = DbType.Int16;
            param2.Value = 90;
            command.Parameters.Add(param2);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetWeeklyLoginActivityAsync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_userlogincounter_daily");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pinputdate";
            param1.DbType = DbType.Date;
            param1.Value = DateTime.Now;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pnumberofdate";
            param2.DbType = DbType.Int16;
            param2.Value = 7;
            command.Parameters.Add(param2);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }

        public async Task<HashSet<Dictionary<string, object>>> GetEachMapServiceUsageAsync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_mapservicecounter_daily");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pinputdate";
            param1.DbType = DbType.Date;
            param1.Value = DateTime.Now;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pnumberofdate";
            param2.DbType = DbType.Int16;
            param2.Value = 90;
            command.Parameters.Add(param2);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }

        public async Task<HashSet<Dictionary<string, object>>> GetEachFunctionUsageAync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_apicounter_daily");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pinputdate";
            param1.DbType = DbType.Date;
            param1.Value = DateTime.Now;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pnumberofdate";
            param2.DbType = DbType.Int16;
            param2.Value = 90;
            command.Parameters.Add(param2);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }

        public async Task<HashSet<Dictionary<string, object>>> GetEachErrorUsageAsync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_errorcounter_total");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pinputdate";
            param1.DbType = DbType.Date;
            param1.Value = DateTime.Now;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pnumberofdate";
            param2.DbType = DbType.Int16;
            param2.Value = 90;
            command.Parameters.Add(param2);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }

        public async Task<HashSet<Dictionary<string, object>>> GetDailyUserActivitiesAsync(int Year, int Month)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_userlogincounter_monthly");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pinputyear";
            param1.DbType = DbType.Int16;
            param1.Value = Year;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pinputmonth";
            param2.DbType = DbType.Int16;
            param2.Value = Month;
            command.Parameters.Add(param2);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetBrowserActivitiesAsync(int Year, int Month)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_browsercounter_monthly");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pinputyear";
            param1.DbType = DbType.Int16;
            param1.Value = Year;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pinputmonth";
            param2.DbType = DbType.Int16;
            param2.Value = Month;
            command.Parameters.Add(param2);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<Dictionary<string, object>> InsertConfigUrlMappingAsync(string URLKey, string ServiceType, string URL, int? IsTracking)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_insert_config_url_mapping");

            var param1 = command.CreateParameter();
            param1.ParameterName = "purlkey";
            param1.DbType = DbType.String;
            param1.Value = URLKey.ToUpper();
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pservicetype";
            param2.DbType = DbType.String;
            param2.Value = ServiceType;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "purl";
            param3.DbType = DbType.String;
            param3.Value = URL;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "pistracking";
            param4.DbType = DbType.Int16;
            param4.Value = IsTracking;
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "rowcount";
            param5.DbType = DbType.Int16;
            param5.Direction = ParameterDirection.Output;
            command.Parameters.Add(param5);

            command.CommandType = CommandType.StoredProcedure;
            return await UpdateAdminDataAsync(command);
        }
        public async Task<Dictionary<string, object>> UpdateConfigUrlMappingAsync(string URLKey, string ServiceType, string URL, int? IsTracking)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_config_url_mapping");

            var param1 = command.CreateParameter();
            param1.ParameterName = "purlkey";
            param1.DbType = DbType.String;
            param1.Value = URLKey.ToUpper();
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pservicetype";
            param2.DbType = DbType.String;
            param2.Value = ServiceType;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "purl";
            param3.DbType = DbType.String;
            param3.Value = URL;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "pistracking";
            param4.DbType = DbType.Int16;
            param4.Value = IsTracking;
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "rowcount";
            param5.DbType = DbType.Int16;
            param5.Direction = ParameterDirection.Output;
            command.Parameters.Add(param5);

            command.CommandType = CommandType.StoredProcedure;
            return await UpdateAdminDataAsync(command);
        }
        public async Task<Dictionary<string, object>> DeleteConfigUrlMappingAsync(string URLKey)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_delete_urlkey_config_url_mapping");

            var param1 = command.CreateParameter();
            param1.ParameterName = "purlkey";
            param1.DbType = DbType.String;
            param1.Value = URLKey.ToUpper();
            command.Parameters.Add(param1);

            var param4 = command.CreateParameter();
            param4.ParameterName = "rowcount";
            param4.DbType = DbType.Int16;
            param4.Direction = ParameterDirection.Output;
            command.Parameters.Add(param4);

            command.CommandType = CommandType.StoredProcedure;
            return await UpdateAdminDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetUrlKeysFromConfigURLAsync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_urlkeys_config_url_mapping");
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<Dictionary<string, object>> CheckIfAPIExists(string APIName)
        {
            /*Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select count(*) from (select distinct regexp_split_to_table(url,'/') as url from swift.config_urlmapping where servicetype = 'API' )A  where  upper(A.url) = '" + APIName + "' ";*/

            HashSet<Dictionary<string, object>> adGroupValue = new HashSet<Dictionary<string, object>>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_admin_check_api_exists");

            var param1 = command.CreateParameter();
            param1.ParameterName = "papiname";
            param1.DbType = DbType.String;
            param1.Value = APIName.ToUpper();
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            adGroupValue = await ReadDataAsync(command);

            Dictionary<string, object> apiDic = new Dictionary<string, object>();
            string apiCnt = string.Empty;
            foreach (Dictionary<string, object> item in (IEnumerable)adGroupValue)
            {
                if (item.ContainsKey("count"))
                    apiCnt = item["count"].ToString();
            }
            apiDic.Add("APICount", apiCnt);
            return apiDic;
        }
        public async Task<Dictionary<string, object>> InsertACMAPIAsync(string Module, string FunctionName)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_insert_acm_api");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pmodule";
            param1.DbType = DbType.String;
            param1.Value = Module;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pfunctionname";
            param2.DbType = DbType.String;
            param2.Value = FunctionName;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "rowcount";
            param3.DbType = DbType.Int16;
            param3.Direction = ParameterDirection.Output;
            command.Parameters.Add(param3);

            command.CommandType = CommandType.StoredProcedure;
            return await UpdateAdminDataAsync(command);
        }
        public async Task<Dictionary<string, object>> UpdateACMAPIAsync(Int32 ApiKey, string Module, string FunctionName)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_acm_api");

            var param1 = command.CreateParameter();
            param1.ParameterName = "papikey";
            param1.DbType = DbType.Int32;
            param1.Value = ApiKey;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pmodule";
            param2.DbType = DbType.String;
            param2.Value = Module;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pfunctionname";
            param3.DbType = DbType.String;
            param3.Value = FunctionName;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "rowcount";
            param4.DbType = DbType.Int16;
            param4.Direction = ParameterDirection.Output;
            command.Parameters.Add(param4);

            command.CommandType = CommandType.StoredProcedure;
            return await UpdateAdminDataAsync(command);
        }
        public async Task<Dictionary<string, object>> DeleteACMAPIAsync(Int32 ApiKey)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_delete_acm_api");

            var param1 = command.CreateParameter();
            param1.ParameterName = "papikey";
            param1.DbType = DbType.Int32;
            param1.Value = ApiKey;
            command.Parameters.Add(param1);

            var param4 = command.CreateParameter();
            param4.ParameterName = "rowcount";
            param4.DbType = DbType.Int16;
            param4.Direction = ParameterDirection.Output;
            command.Parameters.Add(param4);

            command.CommandType = CommandType.StoredProcedure;
            return await UpdateAdminDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetAPINameAsync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_admin_apiname");
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<Dictionary<string, object>> InsertACMFunctionalityAsync(string Module, string Functionality)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_insert_acm_functionality");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pmodule";
            param1.DbType = DbType.String;
            param1.Value = Module;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pfunctionality";
            param2.DbType = DbType.String;
            param2.Value = Functionality;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "rowcount";
            param3.DbType = DbType.Int16;
            param3.Direction = ParameterDirection.Output;
            command.Parameters.Add(param3);

            command.CommandType = CommandType.StoredProcedure;
            return await UpdateAdminDataAsync(command);
        }
        public async Task<Dictionary<string, object>> UpdateACMFunctionalityAsync(Int32 FunctionalityKey, string Module, string Functionality)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_acm_functionality");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pfunctionalitykey";
            param1.DbType = DbType.Int32;
            param1.Value = FunctionalityKey;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pmodule";
            param2.DbType = DbType.String;
            param2.Value = Module;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pfunctionality";
            param3.DbType = DbType.String;
            param3.Value = Functionality;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "rowcount";
            param4.DbType = DbType.Int16;
            param4.Direction = ParameterDirection.Output;
            command.Parameters.Add(param4);

            command.CommandType = CommandType.StoredProcedure;
            return await UpdateAdminDataAsync(command);
        }
        public async Task<Dictionary<string, object>> DeleteACMFunctionalityAsync(Int32 FunctionalityKey)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_delete_acm_functionality");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pfunctionalitykey";
            param1.DbType = DbType.Int32;
            param1.Value = FunctionalityKey;
            command.Parameters.Add(param1);

            var param4 = command.CreateParameter();
            param4.ParameterName = "rowcount";
            param4.DbType = DbType.Int16;
            param4.Direction = ParameterDirection.Output;
            command.Parameters.Add(param4);

            command.CommandType = CommandType.StoredProcedure;
            return await UpdateAdminDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetACMModuleNameAsync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_admin_acm_module_name");
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<Dictionary<string, object>> CheckIfADGroupExists(string ADGroup)
        {
            /*Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select count(*) from (select distinct regexp_split_to_table(memberof,';') as memberof from sde.sp_gems_activedirectoryinfo) A where upper(A.memberof) = '" + ADGroup.ToUpper() + "'";*/

            HashSet<Dictionary<string, object>> adGroupValue = new HashSet<Dictionary<string, object>>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_admin_check_adgroup_exists");

            var param1 = command.CreateParameter();
            param1.ParameterName = "padgroup";
            param1.DbType = DbType.String;
            param1.Value = ADGroup.ToUpper();
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            adGroupValue = await ReadDataAsync(command);

            Dictionary<string, object> adGrpDicVal = new Dictionary<string, object>();
            string adGrpCnt = string.Empty;
            foreach (Dictionary<string, object> item in (IEnumerable)adGroupValue)
            {
                if (item.ContainsKey("count"))
                    adGrpCnt = item["count"].ToString();
            }
            adGrpDicVal.Add("ADGroupCount", adGrpCnt);
            return adGrpDicVal;
        }
        public async Task<Dictionary<string, object>> CheckIfADGroupExistsInACMADGroup(string ADGroup)
        {
            /*Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select count(*) from (select distinct regexp_split_to_table(adgroup,'\\\\') as adgroup from swift.acm_adgroup) A where upper(A.adgroup) = '" + ADGroup + "'";*/

            HashSet<Dictionary<string, object>> adGroupValue = new HashSet<Dictionary<string, object>>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_admin_check_adgroup_exists_in_acm_adgroup");

            var param1 = command.CreateParameter();
            param1.ParameterName = "padgroup";
            param1.DbType = DbType.String;
            param1.Value = ADGroup.ToUpper();
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            adGroupValue = await ReadDataAsync(command);

            Dictionary<string, object> acmADGrpDicVal = new Dictionary<string, object>();
            string acmADGrpCnt = string.Empty;
            foreach (Dictionary<string, object> item in (IEnumerable)adGroupValue)
            {
                if (item.ContainsKey("count"))
                    acmADGrpCnt = item["count"].ToString();
            }
            acmADGrpDicVal.Add("ACMADGroupCount", acmADGrpCnt);
            return acmADGrpDicVal;
        }
        public async Task<Dictionary<string, object>> InsertACMADGroupAsync(string ADGroup, string Role)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_insert_acm_adgroup");

            var param1 = command.CreateParameter();
            param1.ParameterName = "padgroup";
            param1.DbType = DbType.String;
            param1.Value = ADGroup;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "prole";
            param2.DbType = DbType.String;
            param2.Value = Role;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "rowcount";
            param3.DbType = DbType.Int16;
            param3.Direction = ParameterDirection.Output;
            command.Parameters.Add(param3);

            command.CommandType = CommandType.StoredProcedure;
            return await UpdateAdminDataAsync(command);
        }
        public async Task<Dictionary<string, object>> UpdateACMADGroupAsync(Int32 ADGroupKey, string ADGroup, string Role)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_acm_adgroup");

            var param1 = command.CreateParameter();
            param1.ParameterName = "padgroupkey";
            param1.DbType = DbType.Int32;
            param1.Value = ADGroupKey;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "padgroup";
            param2.DbType = DbType.String;
            param2.Value = ADGroup;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "prole";
            param3.DbType = DbType.String;
            param3.Value = Role;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "rowcount";
            param4.DbType = DbType.Int16;
            param4.Direction = ParameterDirection.Output;
            command.Parameters.Add(param4);

            command.CommandType = CommandType.StoredProcedure;
            return await UpdateAdminDataAsync(command);
        }
        public async Task<Dictionary<string, object>> DeleteACMADGroupAsync(Int32 ADGroupKey)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_delete_acm_adgroup");

            var param1 = command.CreateParameter();
            param1.ParameterName = "padgroupkey";
            param1.DbType = DbType.Int32;
            param1.Value = ADGroupKey;
            command.Parameters.Add(param1);

            var param4 = command.CreateParameter();
            param4.ParameterName = "rowcount";
            param4.DbType = DbType.Int16;
            param4.Direction = ParameterDirection.Output;
            command.Parameters.Add(param4);

            command.CommandType = CommandType.StoredProcedure;
            return await UpdateAdminDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetACMADGroupAsync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_admin_acm_adgroup");
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<Dictionary<string, object>> InsertADGroupToFunctionMappingAsync(Int32 ADGroupKey, string FunctionalityMultipleKeys)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_insert_adgroup_to_function_mapping");

            var param1 = command.CreateParameter();
            param1.ParameterName = "padgroupkey";
            param1.DbType = DbType.Int32;
            param1.Value = ADGroupKey;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pfunctionalitykey";
            param2.DbType = DbType.String;
            param2.Value = FunctionalityMultipleKeys;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "rowcount";
            param3.DbType = DbType.Int16;
            param3.Direction = ParameterDirection.Output;
            command.Parameters.Add(param3);

            command.CommandType = CommandType.StoredProcedure;
            return await UpdateAdminDataAsync(command);
        }
        public async Task<Dictionary<string, object>> DeleteADGroupToFunctionMappingAsync(Int32 ADGroupKey, Int32 FunctionalityKey)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_delete_adgroup_to_function_mapping");

            var param1 = command.CreateParameter();
            param1.ParameterName = "padgroupkey";
            param1.DbType = DbType.Int32;
            param1.Value = ADGroupKey;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pfunctionalitykey";
            param2.DbType = DbType.Int32;
            param2.Value = FunctionalityKey;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "rowcount";
            param3.DbType = DbType.Int16;
            param3.Direction = ParameterDirection.Output;
            command.Parameters.Add(param3);

            command.CommandType = CommandType.StoredProcedure;
            return await UpdateAdminDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetADGroupToFunctionMappingAsync(int FunctionalityKey, int ADGroupKey)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_acm_adgrouptofunctionalitymapping");
            command.CommandType = CommandType.StoredProcedure;

            var param1 = command.CreateParameter();
            param1.ParameterName = "pfunctionalitykey";
            param1.DbType = DbType.Int16;
            if (FunctionalityKey != 0)
                param1.Value = FunctionalityKey;
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "padgroupkey";
            param2.DbType = DbType.Int16;
            if (ADGroupKey != 0)
                param2.Value = ADGroupKey;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            return await ReadDataAsync(command);
        }
        public async Task<Dictionary<string, object>> InsertADGroupToAPIMappingAsync(Int32 ADGroupKey, string APIMultipleKeys)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_insert_adgroup_to_api_mapping");

            var param1 = command.CreateParameter();
            param1.ParameterName = "padgroupkey";
            param1.DbType = DbType.Int32;
            param1.Value = ADGroupKey;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "papikey";
            param2.DbType = DbType.String;
            param2.Value = APIMultipleKeys;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "rowcount";
            param3.DbType = DbType.Int16;
            param3.Direction = ParameterDirection.Output;
            command.Parameters.Add(param3);

            command.CommandType = CommandType.StoredProcedure;
            return await UpdateAdminDataAsync(command);
        }
        public async Task<Dictionary<string, object>> DeleteADGroupToAPIMappingAsync(Int32 ADGroupKey, Int32 APIKey)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_delete_adgroup_to_api_mapping");

            var param1 = command.CreateParameter();
            param1.ParameterName = "padgroupkey";
            param1.DbType = DbType.Int32;
            param1.Value = ADGroupKey;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "papikey";
            param2.DbType = DbType.Int32;
            param2.Value = APIKey;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "rowcount";
            param3.DbType = DbType.Int16;
            param3.Direction = ParameterDirection.Output;
            command.Parameters.Add(param3);

            command.CommandType = CommandType.StoredProcedure;
            return await UpdateAdminDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetADGroupToAPIMappingAsync(int APIKey, int ADGroupKey)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_acm_adgrouptoapimapping");
            command.CommandType = CommandType.StoredProcedure;

            var param1 = command.CreateParameter();
            param1.ParameterName = "papikey";
            param1.DbType = DbType.Int16;
            if (APIKey != 0)
                param1.Value = APIKey;
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "padgroupkey";
            param2.DbType = DbType.Int16;
            if (ADGroupKey != 0)
                param2.Value = ADGroupKey;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetAPIMapSheetUserListAsync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("SWIFT.API_GET_UCON_LISTHEADNAME");
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 22nd Oct 2019 for RFC0021956 -- This api returns list of Project officers/MEA Editors based on UserType.
        public async Task<HashSet<Dictionary<string, object>>> GetPOMEAListAsync(string ProjectOfficerID, string UserType)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_pov_admin_polist");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pusertype";
            param1.DbType = DbType.String;
            param1.Value = UserType.ToUpper();
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "ppoid";
            param2.DbType = DbType.String;
            param2.Value = ProjectOfficerID.ToUpper();
            command.Parameters.Add(param2);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 4th Sept 2019 for RFC0021956 -- This api returns complete list of PO JobInfo table. It is used in admin portal.
        public async Task<HashSet<Dictionary<string, object>>> GetCompletePOJobInfoDataAsync(string UserType, string OfficerID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_pov_admin_complete_jobinfo_data");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pusertype";
            param1.DbType = DbType.String;
            param1.Value = UserType.ToUpper();
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pofficerid";
            param2.DbType = DbType.String;
            if (!string.IsNullOrEmpty(OfficerID))
                param2.Value = OfficerID.ToUpper();
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 22nd Aug 2019 for RFC0021956 -- This api returns complete list of POVID's that are assigned to PO for verification. It is used in admin portal.
        public async Task<HashSet<Dictionary<string, object>>> GetAllPOVIDSToUpdatePOInfoAsync(string RejectSession = null)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_pov_jobinfo_admin_po_mea");
            if (!string.IsNullOrEmpty(RejectSession))
            {
                var param1 = command.CreateParameter();
                param1.ParameterName = "prejectsession";
                param1.DbType = DbType.String;
                param1.Value = RejectSession.ToUpper();
                command.Parameters.Add(param1);
            }
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on  14th Aug 2019 for RFC0021956 -- This api will be consumed from admin portal to update Project Officer ID for a particular POVID.
        public async Task<Dictionary<string, object>> UpdatePOIDAsync(List<POVerficationListItem> rowInfo)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_pov_admin_poid");

            var param1 = command.CreateParameter();
            param1.ParameterName = "ppovid";
            param1.DbType = DbType.Int64;
            param1.Value = rowInfo[0].povid;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "ppoid";
            param2.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].poids))
                param2.Value = rowInfo[0].poids.ToUpper();
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pponame";
            param3.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].poname))
                param3.Value = rowInfo[0].poname.ToUpper();
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "ppoemail";
            param4.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].poemail))
                param4.Value = rowInfo[0].poemail.ToUpper();
            else
                param4.Value = DBNull.Value;
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "pusertype";
            param5.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].usertype))
                param5.Value = rowInfo[0].usertype.ToUpper();
            else
                param5.Value = DBNull.Value;
            command.Parameters.Add(param5);

            var param6 = command.CreateParameter();
            param6.ParameterName = "rowcount";
            param6.DbType = DbType.Int16;
            param6.Direction = ParameterDirection.Output;
            command.Parameters.Add(param6);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateAdminDataAsync(command);
            return result;
        }
        //added by Sandip on  22nd Sept 2019 for RFC0021956 -- This api will be consumed from admin portal to reject sessions by MEA.
        public async Task<Dictionary<string, object>> UpdateMEARejectSessionAsync(List<POVerficationListItem> rowInfo)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_pov_admin_reject_session");

            var param1 = command.CreateParameter();
            param1.ParameterName = "ppovid";
            param1.DbType = DbType.Int64;
            param1.Value = rowInfo[0].povid;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pmeaname";
            param2.DbType = DbType.String;
            param2.Value = rowInfo[0].meaeditorname;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pmearemarks";
            param3.DbType = DbType.String;
            param3.Value = rowInfo[0].mearemarks;
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
        //added by Sandip on 25th Oct 2019 for RFC0021956 -- This api returns complete contractor info list.
        public async Task<HashSet<Dictionary<string, object>>> GetPOVContractorInfoAsync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_pov_admin_contractor_info");

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 25th Oct 2019 for RFC0021956 -- This api updates contractor information based on unique contractor id.
        public async Task<Dictionary<string, object>> UpdatePOVContractorInfoAsync(List<POVerficationListItem> rowInfo, string OfficerID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_pov_admin_contractor_info");

            var param1 = command.CreateParameter();
            param1.ParameterName = "ploginname";
            param1.DbType = DbType.String;
            param1.Value = rowInfo[0].contractorid;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pstatus";
            param2.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].status))
                param2.Value = rowInfo[0].status;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pemail";
            param3.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].email))
                param3.Value = rowInfo[0].email;
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "pdescription";
            param4.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].description))
                param4.Value = rowInfo[0].description;
            else
                param4.Value = DBNull.Value;
            command.Parameters.Add(param4);

            //added by Sandip on 04th Aug 2021 for RFC0034769 - To include Hand Phone Number and logged in userid.
            var param5 = command.CreateParameter();
            param5.ParameterName = "phpno";
            param5.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].hpno))
                param5.Value = rowInfo[0].hpno;
            else
                param5.Value = DBNull.Value;
            command.Parameters.Add(param5);

            var param6 = command.CreateParameter();
            param6.ParameterName = "puserid";
            param6.DbType = DbType.String;
            if (!string.IsNullOrEmpty(OfficerID))
                param6.Value = OfficerID;
            else
                param6.Value = DBNull.Value;
            command.Parameters.Add(param6);

            var param7 = command.CreateParameter();
            param7.ParameterName = "rowcount";
            param7.DbType = DbType.Int16;
            param7.Direction = ParameterDirection.Output;
            command.Parameters.Add(param7);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateAdminDataAsync(command);
            return result;
        }
        //added by Sandip on 25th Oct 2019 for RFC0021956 -- This api inserts contractor information in the table.
        public async Task<Dictionary<string, object>> InsertPOVContractorInfoAsync(List<POVerficationListItem> rowInfo, string OfficerID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_insert_pov_admin_contractor_info");

            var param1 = command.CreateParameter();
            param1.ParameterName = "ploginname";
            param1.DbType = DbType.String;
            param1.Value = rowInfo[0].contractorid;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pemail";
            param2.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].email))
                param2.Value = rowInfo[0].email;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pdescription";
            param3.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].description))
                param3.Value = rowInfo[0].description;
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);

            //added by Sandip on 04th Aug 2021 for RFC0034769 - To include Hand Phone Number and logged in userid.
            var param4 = command.CreateParameter();
            param4.ParameterName = "phpno";
            param4.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].hpno))
                param4.Value = rowInfo[0].hpno;
            else
                param4.Value = DBNull.Value;
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "puserid";
            param5.DbType = DbType.String;
            if (!string.IsNullOrEmpty(OfficerID))
                param5.Value = OfficerID;
            else
                param5.Value = DBNull.Value;
            command.Parameters.Add(param5);

            var param6 = command.CreateParameter();
            param6.ParameterName = "rowcount";
            param6.DbType = DbType.Int16;
            param6.Direction = ParameterDirection.Output;
            command.Parameters.Add(param6);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateAdminDataAsync(command);
            return result;
        }
        //added by Sandip on 4th Nov 2019 for RFC0021956 -- This api returns complete history of the session from created to final posted stage.
        public async Task<HashSet<Dictionary<string, object>>> GetPOVSessionHistoryAsync(string UserType = null, string OfficerID = null, string FromDate = null, string ToDate = null, string POVID = null)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_pov_admin_session_history");
            command.CommandType = CommandType.StoredProcedure;

            var param1 = command.CreateParameter();
            param1.ParameterName = "pusertype";
            param1.DbType = DbType.String;
            if (!string.IsNullOrEmpty(UserType))
                param1.Value = UserType.ToUpper();
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pofficerid";
            param2.DbType = DbType.String;
            if (!string.IsNullOrEmpty(OfficerID))
                param2.Value = OfficerID.ToUpper();
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

            var param5 = command.CreateParameter();
            param5.ParameterName = "ppovid";
            param5.DbType = DbType.Int64;
            if (!string.IsNullOrEmpty(POVID))
                param5.Value = Convert.ToInt64(POVID);
            else
                param5.Value = DBNull.Value;
            command.Parameters.Add(param5);

            return await ReadDataAsync(command);
        }
        public async Task<Dictionary<string, object>> UpdatePOVJobStatusAsync(List<POVerficationListItem> rowInfo, string OfficerID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_pov_admin_job_status");

            var param1 = command.CreateParameter();
            param1.ParameterName = "ppovid";
            param1.DbType = DbType.Int64;
            param1.Value = rowInfo[0].povid;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pstatus";
            param2.DbType = DbType.Int16;
            param2.Value = Convert.ToInt16(rowInfo[0].status);
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "ploginname";
            param3.DbType = DbType.String;
            param3.Value = OfficerID.ToUpper();
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
        //added by Sandip on 2nd Mar 2020 for RFC0024551 -- This api returns the list of jobs details.
        public async Task<HashSet<Dictionary<string, object>>> GetJobSyncAsync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_sppg_sp_jobsync");
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 2nd Mar 2020 for RFC0024551 -- This api inserts new job info details in the job sync table.
        public async Task<Dictionary<string, object>> InsertJobSyncAsync(List<IncidentManagementItems> rowInfo, string UserID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_insert_sppg_sp_jobsync");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pkeyname";
            param1.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].keyname))
                param1.Value = rowInfo[0].keyname;
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pkeyvalue";
            param2.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].keyvalue))
                param2.Value = rowInfo[0].keyvalue;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pkeydesc";
            param3.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].keydesc))
                param3.Value = rowInfo[0].keydesc;
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "puserid";
            param4.DbType = DbType.String;
            param4.Value = UserID.ToUpper();
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "rowcount";
            param5.DbType = DbType.Int16;
            param5.Direction = ParameterDirection.Output;
            command.Parameters.Add(param5);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateAdminDataAsync(command);
            return result;
        }
        //added by Sandip on 2nd Mar 2020 for RFC0024551 -- This api updates existing job info details in the job sync table.
        public async Task<Dictionary<string, object>> UpdateJobSyncAsync(List<IncidentManagementItems> rowInfo, string UserID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_update_sppg_sp_jobsync");

            var param0 = command.CreateParameter();
            param0.ParameterName = "pid";
            param0.DbType = DbType.Int64;
            param0.Value = rowInfo[0].id;
            command.Parameters.Add(param0);

            var param1 = command.CreateParameter();
            param1.ParameterName = "pkeyname";
            param1.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].keyname))
                param1.Value = rowInfo[0].keyname;
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pkeyvalue";
            param2.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].keyvalue))
                param2.Value = rowInfo[0].keyvalue;
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "pkeydesc";
            param3.DbType = DbType.String;
            if (!string.IsNullOrEmpty(rowInfo[0].keydesc))
                param3.Value = rowInfo[0].keydesc;
            else
                param3.Value = DBNull.Value;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "puserid";
            param4.DbType = DbType.String;
            param4.Value = UserID.ToUpper();
            command.Parameters.Add(param4);

            var param5 = command.CreateParameter();
            param5.ParameterName = "rowcount";
            param5.DbType = DbType.Int16;
            param5.Direction = ParameterDirection.Output;
            command.Parameters.Add(param5);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateAdminDataAsync(command);
            return result;
        }
        //added by Sandip on 17th Oct 2020 for RFC0029411 -- This api returns the list of problematic cable joints for the admin portal.
        public async Task<HashSet<Dictionary<string, object>>> GetProblematicCableJointAsync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_gis_problematic_cable_joint");
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 17th Oct 2020 for RFC0029411 -- This api returns the list of problematic cable joints for the admin portal.
        public async Task<HashSet<Dictionary<string, object>>> GetProblematicCableJointReportPathAsync(string ReportPath)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_schema_config_value");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pconfigkey";
            param1.DbType = DbType.String;
            param1.Value = ReportPath;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetValidateProblematicCableJointDataAsync(string JsonObj)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_validation_problematic_cable_joint");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pinput";
            param1.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb;
            param1.Value = JsonObj;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 17th Oct 2020 for RFC0029411 -- This api inserts new problematic cable joint data.
        public async Task<Dictionary<string, object>> InsertProblematicCableJointDataAsync(string JsonObj, string UserID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_insert_problematic_cable_joint");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pinput";
            param1.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb;
            param1.Value = JsonObj;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "puserid";
            param2.DbType = DbType.String;
            param2.Value = UserID.ToUpper();
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "rowcount";
            param3.DbType = DbType.Int16;
            param3.Direction = ParameterDirection.Output;
            command.Parameters.Add(param3);

            command.CommandType = CommandType.StoredProcedure;
            result = await UpdateAdminDataAsync(command);
            return result;
        }

        /*public async Task<HashSet<Dictionary<string, object>>> GetAPIMapSheetReportAsync(string InputDate, string ReportType, string Username)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();

            if (ReportType.ToUpper() == "DAILY")
                command = new Npgsql.NpgsqlCommand("SWIFT.API_GET_MON_APIMAPSHEETCOUNTER_DAILYRPT");
            else if (ReportType.ToUpper() == "WEEKLY")
                command = new Npgsql.NpgsqlCommand("SWIFT.API_GET_MON_APIMAPSHEETCOUNTER_WEEKLYRPT");
            else if (ReportType.ToUpper() == "MONTHLY")
                command = new Npgsql.NpgsqlCommand("SWIFT.API_GET_MON_APIMAPSHEETCOUNTER_MONTHLYRPT");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pinputdate";
            param1.DbType = DbType.Date;
            if (!string.IsNullOrEmpty(InputDate) && !string.IsNullOrWhiteSpace(InputDate))
            {
                DateTime inputDate1 = DateTime.Parse(InputDate, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                param1.Value = inputDate1;
            }
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "puser";
            param2.DbType = DbType.String;
            param2.Value = Username;
            command.Parameters.Add(param2);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetAPIMapSheetSummaryReportAsync(string Username)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("SWIFT.API_GET_MON_APIMAPSHEETCOUNTER_SUMARY");

            var param1 = command.CreateParameter();
            param1.ParameterName = "puser";
            param1.DbType = DbType.String;
            param1.Value = Username;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetAPIMapSheetGeometryAsync(string InputDate, string ReportType, string Username)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("SWIFT.API_GET_MON_APIMAPSHEETCOUNTER_MAPSHEETS");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pinputdate";
            param1.DbType = DbType.Date;
            if (!string.IsNullOrEmpty(InputDate) && !string.IsNullOrWhiteSpace(InputDate))
            {
                DateTime inputDate1 = DateTime.Parse(InputDate, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                param1.Value = inputDate1;
            }
            else
                param1.Value = DBNull.Value;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "puser";
            param2.DbType = DbType.String;
            param2.Value = Username;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "ptype";
            param3.DbType = DbType.String;
            param3.Value = ReportType.ToUpper();
            command.Parameters.Add(param3);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }*/
    }
}
