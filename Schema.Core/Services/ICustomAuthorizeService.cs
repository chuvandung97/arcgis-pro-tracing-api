using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Schema.Core.Services
{
    public interface ICustomAuthorizeService
    {
        //Task<Dictionary<string, object>> InsertFunctionLogInfoInDB(string username, string url, string parameters, string functionName, string clientIP, string referrer, string browser, string useragent, string type);       
        Task<Dictionary<string, object>> InsertFunctionLogInfoInDB();        
        Task<Dictionary<string, object>> InsertErrorLogInfoInDB(string errorMsg);
        Task<Dictionary<string, object>> InsertMapServiceRequestInDB(object JsonObj);
        Task<Dictionary<string, object>> InsertMapServiceErrorInDB(object JsonObj);
    }
}
