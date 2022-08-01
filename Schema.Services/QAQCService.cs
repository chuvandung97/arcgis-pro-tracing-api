using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Schema.Core.Data;
using Schema.Core.Models;
using Schema.Core.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Schema.Services
{
    public class QAQCService : IQAQCService
    {
        ILoggingService _loggingService;
        IQAQCDataService _qaqcDataService;

        Dictionary<string, object> errorLogInfo;
        public QAQCService(ILoggingService LoggingService, IQAQCDataService QAQCDataService)
        {
            _loggingService = LoggingService;
            _qaqcDataService = QAQCDataService;
        }
        public ICustomAuthorizeService CustomAuthorizeService
        {
            get
            {
                return DependencyResolver.Current.GetService<ICustomAuthorizeService>();
            }
        }
        public async Task<HashSet<Dictionary<string, object>>> GetAllUsersAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _qaqcDataService.GetAllUsersAsync();
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        public async Task<HashSet<Dictionary<string, object>>> GetAllErrorCategoriesAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _qaqcDataService.GetAllErrorCategoriesAsync();
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        public async Task<SchemaResult> GetLast3MonthErrorsAsync()
        {
            SchemaResult result = new SchemaResult();
            HashSet<Dictionary<string, object>> result1 = new HashSet<Dictionary<string, object>>();
            HashSet<Dictionary<string, object>> result2 = new HashSet<Dictionary<string, object>>();
            HashSet<Dictionary<string, object>> result3 = new HashSet<Dictionary<string, object>>();
                        
            Dictionary<string, object> resultData = new Dictionary<string, object>();            
            try
            {
                result1 = await _qaqcDataService.GetLast3MonthErrCatgAsync();
                resultData.Add("ErrCatgWise", result1);

                result2 = await _qaqcDataService.GetLast3MonthTotalErrCatgAsync();
                resultData.Add("MonthTotalErrCatgWise", result2);

                result3 = await _qaqcDataService.GetLast3MonthErrorsAsync();
                resultData.Add("MonthErrCatgWise", result3);
                result.Result = resultData;
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        public async Task<HashSet<Dictionary<string, object>>> GetTop10UserQAQCErrorsAsync(int Year, string Month)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                if (string.IsNullOrEmpty(Month))
                    Month = "ALL";

                result = await _qaqcDataService.GetTop10UserQAQCErrorsAsync(Year,  Month);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
        public async Task<HashSet<Dictionary<string, object>>> GetAllErrorsListAsync(int Year, string Month, string ErrCatg, string Username)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            string sqlQuery = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(Month))
                    Month = "ALL";
                if (string.IsNullOrEmpty(ErrCatg))
                    ErrCatg = "ALL";
                if (string.IsNullOrEmpty(Username))
                    Username = "ALL";

                result = await _qaqcDataService.GetAllErrorsListAsync(Year, Month, ErrCatg, Username);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
                //_loggingService.Error(ex);
                //throw new Exception("Error");
            }
            return result;
        }
    }
}
