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
    public class UserDataService : PgDbBase, IUserDataService
    {
        public UserDataService(IConfigService configService, ILoggingService loggingService)
          : base(configService, loggingService)
        { }
        public HashSet<Dictionary<string, object>> GetADGroupsForMethodNames(string MethodName)
        {
            //await InsertFunctionLogInfoInDB();
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_ad_groups_from_method_name");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pmethodname";
            param1.DbType = DbType.String;
            param1.Value = MethodName.ToUpper();
            command.Parameters.Add(param1);
            //WriteErrorLog(MethodName.ToUpper());
            command.CommandType = CommandType.StoredProcedure;
            return ReadData(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetUserDetailsAsync(string[] adGroupVal)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_ad_group_functionality");

            var param1 = command.CreateParameter();
            param1.ParameterName = "padgroups";
            param1.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Array | NpgsqlTypes.NpgsqlDbType.Text;
            param1.Value = adGroupVal;
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }

        public async Task<HashSet<Dictionary<string, object>>> GetLastLoginDateAsync(string Username)
        {
            /*Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select date, to_date(to_char(date, 'YYYY/MM/DD'), 'YYYY/MM/DD') as logindate from swift.schema_app_requestlog where upper(username) = @val and upper(functionname) = @val1 order by date desc limit 1 offset 1";
            command.CommandType = System.Data.CommandType.Text;
            var param1 = new Npgsql.NpgsqlParameter("@val", NpgsqlTypes.NpgsqlDbType.Text);
            param1.Value = string.Format("{0}", Username.ToUpper());
            command.Parameters.Add(param1);

            var param2 = new Npgsql.NpgsqlParameter("@val1", NpgsqlTypes.NpgsqlDbType.Text);
            param2.Value = string.Format("{0}", "GETUSERDETAILS");
            command.Parameters.Add(param2);*/

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_user_last_login_date");

            var param1 = command.CreateParameter();
            param1.ParameterName = "puserid";
            param1.DbType = DbType.String;
            param1.Value = Username.ToUpper();
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadWebLogDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetUserUnsuccessfulCountAsync(string Username, string LoginQueryDate)
        {

            /*Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select count(*) from swift.schema_app_loginerror where upper(loginname) = @val and logintime >= @val1";
            command.CommandType = System.Data.CommandType.Text;

            var param1 = new Npgsql.NpgsqlParameter("@val", NpgsqlTypes.NpgsqlDbType.Text);
            param1.Value = string.Format("{0}", Username.ToUpper());
            command.Parameters.Add(param1);

            var param2 = new Npgsql.NpgsqlParameter("@val1", DbType.DateTime);

            if (!string.IsNullOrEmpty(LoginQueryDate) && !string.IsNullOrWhiteSpace(LoginQueryDate))
                param2.Value = string.Format("{0}", LoginQueryDate);
            else
                param2.Value = string.Format("{0}", DBNull.Value);

            command.Parameters.Add(param2);*/
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_user_unsuccessful_login_date");

            var param1 = command.CreateParameter();
            param1.ParameterName = "puserid";
            param1.DbType = DbType.String;
            param1.Value = Username.ToUpper();
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "plogindate";
            param2.DbType = DbType.DateTime;
            if (!string.IsNullOrEmpty(LoginQueryDate) && !string.IsNullOrWhiteSpace(LoginQueryDate))
            {
                DateTime dateLogin = DateTime.Parse(LoginQueryDate, System.Globalization.CultureInfo.GetCultureInfo("ur-PK").DateTimeFormat);
                param2.Value = dateLogin;
            }
            else
                param2.Value = DBNull.Value;
            command.Parameters.Add(param2);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadWebLogDataAsync(command);
        }
        //added by Sandip on 8th April 2019 for RFC0018439 -- To get user type for the logged in user. By default StatsFlag is false but when accessed through 
        //Schema Stats application the StatsFlag is set to true.
        public async Task<HashSet<Dictionary<string, object>>> GetUserTypeAsync(string Username, bool StatsFlag = false)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_user_type");

            var param1 = command.CreateParameter();
            param1.ParameterName = "puserid";
            param1.DbType = DbType.String;
            param1.Value = Username.ToUpper().ToString();
            command.Parameters.Add(param1);

            if (StatsFlag == true)
            {
                var param2 = command.CreateParameter();
                param2.ParameterName = "pstatflag";
                param2.DbType = DbType.Boolean;
                param2.Value = true;
                command.Parameters.Add(param2);
            }

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        public async Task<HashSet<Dictionary<string, object>>> GetUrlsAsync()
        {

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_config_urls");
            command.CommandType = CommandType.StoredProcedure;

            return await ReadDataAsync(command);
        }
        //added by Sandip on 4th April 2019 for RFC0018439 -- To check whether the logged in user account is blocked or not.
        //It also checks whether the user has access to specific map service or not.
        public async Task<HashSet<Dictionary<string, object>>> CheckUserStatus(string UserID = null)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_user_block_status");

            var param1 = command.CreateParameter();
            param1.ParameterName = "puserid";
            param1.DbType = DbType.String;
            param1.Value = UserID.ToUpper().ToString();
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
        }
        //added by Sandip on 20th May 2019 for RFC0018439 -- To check whether the logged in user account is blocked or not. This condition is checked for each API request.
        public HashSet<Dictionary<string, object>> CheckAPIUserStatus(string UserID)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command = new Npgsql.NpgsqlCommand("swift.api_get_ucon_user_block_status");

            var param1 = command.CreateParameter();
            param1.ParameterName = "puserid";
            param1.DbType = DbType.String;
            param1.Value = UserID.ToUpper().ToString();
            command.Parameters.Add(param1);

            command.CommandType = CommandType.StoredProcedure;
            return ReadData(command);
        }
        /*public void WriteErrorLog(string message)
        {
            string _filePath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
            string content = DateTime.Now + Environment.NewLine + message + Environment.NewLine;
            System.IO.File.AppendAllText(_filePath, content);
        }*/
    }
}
