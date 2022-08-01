using Schema.Core.Services;
using Schema.Web.AuthorizeUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web.Http;

namespace Schema.Web.Controllers
{
    [RoutePrefix("search")]
    public class SearchController : BaseApiController
    {
        ISearchService _searchService;
        ILoggingService _loggingService;
        public SearchController(ISearchService SearchService, ILoggingService loggingService)
        {
            _searchService = SearchService;
            _loggingService = loggingService;
        }
        [CustomAuthorize]
        [Route("basicsearch")]
        public async Task<IHttpActionResult> GetSearchByPattren(string SearchType, string Text, string UserType= null)
        {
            string Username = ((IPrincipal)User).Identity.Name;
            if (Username.Contains("\\"))
                Username = Username.Split('\\')[1].ToString();

            //WriteErrorLog("BasicSearch:-" + " " + Username.ToUpper() + " " + SearchType + " " + Text);
            var result = await _searchService.GetSearchResultsAsync(SearchType, Username, Text, UserType);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("roadjunction")]
        public async Task<IHttpActionResult> GetRoadJunctionsAsync(string RoadName)
        {
            var result = await _searchService.GetRoadJunctionsAsync(RoadName);
            //WriteErrorLog("BasicSearch:-" + RoadName);
            return Ok(result);
        }
        [CustomAuthorize]
        [Route("roadinfo")]
        public async Task<IHttpActionResult> GetRoadInfoAsync(string RoadName)
        {
            var result = await _searchService.GetRoadInfoAsync(RoadName);
            //WriteErrorLog("BasicSearch:-" + RoadName);
            return Ok(result);
        }
        //added by Sandip on 1st April 2019 for RFC0018439 -- To track and log exact GIS searched location in database 
        [CustomAuthorize]
        [Route("basicsearchlocation")]
        [HttpPost]
        public async Task<IHttpActionResult> InsertSearchedLocationInDBAsync(object JsonObj)
        {        
            var result = await _searchService.InsertSearchedLocationInDBAsync(JsonObj);
            return Ok(result);
        }
        /*public void WriteErrorLog(string message)
        {
            string _filePath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];
            string content = DateTime.Now + Environment.NewLine + message + Environment.NewLine;
            System.IO.File.AppendAllText(_filePath, content);
        }*/
    }
}
