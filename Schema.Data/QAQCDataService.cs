using Schema.Core.Data;
using Schema.Core.Models;
using Schema.Core.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Schema.Data
{
    public class QAQCDataService : PgDbBase, IQAQCDataService
    {
        public QAQCDataService(IConfigService configService, ILoggingService loggingService)
          : base(configService, loggingService)
        { }
        public async Task<HashSet<Dictionary<string, object>>> GetAllUsersAsync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_qaqc_allusers");
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
            /*Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select distinct user_name as username from swift.QAQCErrors where user_name is not null order by user_name";*/
        }
        public async Task<HashSet<Dictionary<string, object>>> GetAllErrorCategoriesAsync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_qaqc_allerrors_category");
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
            /*Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select distinct error_category as errorcategory from swift.QAQCErrors where error_category is not null order by error_category";*/
        }
        public async Task<HashSet<Dictionary<string, object>>> GetLast3MonthErrCatgAsync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_qaqc_last3month_error_category");
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
            /*Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select distinct ERROR_CATEGORY as errorcategory, count (*) from  swift.QAQCErrors WHERE QAQC_DATE >  CURRENT_DATE - INTERVAL '3 months' group by ERROR_CATEGORY, QAQC_DATE";*/
            //"select GLOBALID, FEATURE_CLASS, ERROR_TYPE, ERROR_CATEGORY, ERROR_DESCRIPTION, USER_NAME, CONVERT(varchar, QAQC_DATE, 106) QAQC_DATE from swift.QAQCErrors WHERE DATEDIFF(MONTH, QAQC_DATE, GETDATE()) <= 3";         
        }
        public async Task<HashSet<Dictionary<string, object>>> GetLast3MonthTotalErrCatgAsync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_qaqc_last3month_total_error_category");
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);
            /*Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select distinct EXTRACT(MONTH FROM QAQC_DATE)::smallint as month, EXTRACT(YEAR FROM QAQC_DATE)::smallint as year, count (*) from  swift.QAQCErrors WHERE QAQC_DATE >  CURRENT_DATE - INTERVAL '3 months' group by month, year";*/
            //"select GLOBALID, FEATURE_CLASS, ERROR_TYPE, ERROR_CATEGORY, ERROR_DESCRIPTION, USER_NAME, CONVERT(varchar, QAQC_DATE, 106) QAQC_DATE from swift.QAQCErrors WHERE DATEDIFF(MONTH, QAQC_DATE, GETDATE()) <= 3";
        }
        public async Task<HashSet<Dictionary<string, object>>> GetLast3MonthErrorsAsync()
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_qaqc_last3month_errors");
            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);

            /*Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            command.CommandText = "select distinct ERROR_CATEGORY as errorcategory, EXTRACT(MONTH FROM QAQC_DATE)::smallint as month, EXTRACT(YEAR FROM QAQC_DATE)::smallint as year, count (*) from  swift.QAQCErrors WHERE QAQC_DATE >  CURRENT_DATE - INTERVAL '3 months' group by ERROR_CATEGORY, QAQC_DATE";*/
            //"select GLOBALID, FEATURE_CLASS, ERROR_TYPE, ERROR_CATEGORY, ERROR_DESCRIPTION, USER_NAME, CONVERT(varchar, QAQC_DATE, 106) QAQC_DATE from swift.QAQCErrors WHERE DATEDIFF(MONTH, QAQC_DATE, GETDATE()) <= 3";
        }

        public async Task<HashSet<Dictionary<string, object>>> GetTop10UserQAQCErrorsAsync(int Year, string Month)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_qaqc_top10user_errors");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pyear";
            param1.DbType = DbType.Int16;
            param1.Value = Year;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pmonth";
            param2.DbType = DbType.String;
            param2.Value = Month;
            command.Parameters.Add(param2);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);

            /*  string sqlQuery = string.Empty;
            string whereClause = string.Empty;
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            sqlQuery = "select distinct user_name as username, count(ERROR_CATEGORY) as count from swift.QAQCErrors where user_name is not null";

            if (Month.ToUpper() != "ALL")
                whereClause = " AND EXTRACT(MONTH FROM QAQC_DATE) = " + Convert.ToInt16(Month) + " and EXTRACT(YEAR FROM QAQC_DATE) = " + Year;
            else
                whereClause = " AND EXTRACT(YEAR FROM QAQC_DATE) = " + Year;

            sqlQuery = sqlQuery + whereClause + " group by user_name order by count desc LIMIT 10";
            command.CommandText = sqlQuery;*/
        }
        public async Task<HashSet<Dictionary<string, object>>> GetAllErrorsListAsync(int Year, string Month, string ErrCatg, string Username)
        {
            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand("swift.api_get_qaqc_allerrors_list");

            var param1 = command.CreateParameter();
            param1.ParameterName = "pyear";
            param1.DbType = DbType.Int16;
            param1.Value = Year;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "pmonth";
            param2.DbType = DbType.String;
            param2.Value = Month;
            command.Parameters.Add(param2);

            var param3 = command.CreateParameter();
            param3.ParameterName = "perrcatg";
            param3.DbType = DbType.String;
            param3.Value = ErrCatg;
            command.Parameters.Add(param3);

            var param4 = command.CreateParameter();
            param4.ParameterName = "pusername";
            param4.DbType = DbType.String;
            param4.Value = Username;
            command.Parameters.Add(param4);

            command.CommandType = CommandType.StoredProcedure;
            return await ReadDataAsync(command);

            /*string sqlQuery = string.Empty;
            string whereClause = string.Empty;

            Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand();
            sqlQuery = "select GLOBALID, FEATURE_CLASS as featureclass, ERROR_TYPE as errortype, ERROR_CATEGORY as errorcategory, ERROR_DESCRIPTION as errordescription, USER_NAME as  username, DATE(QAQC_DATE) as qaqcdate from swift.QAQCErrors WHERE";

            if (Month.ToUpper() != "ALL")
                whereClause = " EXTRACT(MONTH FROM QAQC_DATE) = " + Convert.ToInt16(Month) + " and EXTRACT(YEAR FROM QAQC_DATE) = " + Year;
            else
                whereClause = " EXTRACT(YEAR FROM QAQC_DATE) = " + Year;
                
            if (ErrCatg != "ALL")
                whereClause = whereClause + " AND UPPER(ERROR_CATEGORY) = '" + ErrCatg.ToUpper() + "'";

            if (Username != "ALL")
                whereClause = whereClause + " AND UPPER(USER_NAME) = '" + Username.ToUpper() + "'";

            sqlQuery = sqlQuery + whereClause;
            command.CommandText = sqlQuery;
            return await ReadDataAsync(command);*/
        }
    }
}
