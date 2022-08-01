using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Schema.Core.Data
{
    public interface ICustomAuthorizeDataService
    {
        //Task<Dictionary<string, object>> InsertFunctionLogInfoInDB(string username, string url, string parameters, string functionName, string clientIP, string referrer, string browser, string useragent, string type);     
        Task<Dictionary<string, object>> InsertFunctionLogInfoInDB();
        Task<Dictionary<string, object>> InsertErrorLogInfoInDB(string errorMsg);
        Task<Dictionary<string, object>> InsertMapServiceRequestInDB(string Username, string URL, string Parameter, string ClientIP, string Referrer, string Browser, string UserAgent, double ResponseTime);
        Task<Dictionary<string, object>> InsertMapServiceErrorInDB(string Username, string URL, string Parameter, string FunctionName, string ClientIP, string Referrer, string ErrorMsg, string Type);
    }
}
