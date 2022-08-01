using Schema.Core.Services;
using Schema.Core.Utilities;
using Schema.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Drawing;
using Schema.Core.Models;
using System.Configuration;
using System.IO;

namespace Schema.Services
{
    public class GasInternalPipeDrawingsService : IGasInternalPipeDrawingsService
    {
        ILoggingService _loggingService;
        IGasInternalPipeDrawingsDataService _gasInternalPipeDrawingsDataService;
        Dictionary<string, object> errorLogInfo;
        CommonUtilities _commonUtilities = new CommonUtilities();
        public GasInternalPipeDrawingsService(ILoggingService LoggingService, IGasInternalPipeDrawingsDataService gasInternalPipeDrawingsDataService)
        {
            _loggingService = LoggingService;
            _gasInternalPipeDrawingsDataService = gasInternalPipeDrawingsDataService;
        }
        public ICustomAuthorizeService CustomAuthorizeService
        {
            get
            {
                return DependencyResolver.Current.GetService<ICustomAuthorizeService>();
            }
        }
        public async Task<HashSet<Dictionary<string, object>>> GetGasInternalPipeDrawingsAsync(string PostalCode)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _gasInternalPipeDrawingsDataService.GetGasInternalPipeDrawingsAsync(PostalCode);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
            }
            return result;
        }
        public async Task<HashSet<Dictionary<string, object>>> GetGasInternalPipeDrawingsHistoryAsync()
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _gasInternalPipeDrawingsDataService.GetGasInternalPipeDrawingsHistoryAsync();
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
            }
            return result;
        }
        public async Task<HashSet<Dictionary<string, object>>> GetGasInternalPipeDrawingsByPostalCodeAsync(string PostalCode)
        {
            HashSet<Dictionary<string, object>> result = new HashSet<Dictionary<string, object>>();
            try
            {
                result = await _gasInternalPipeDrawingsDataService.GetGasInternalPipeDrawingsByPostalCodeAsync(PostalCode);
            }
            catch (Exception ex)
            {
                errorLogInfo = new Dictionary<string, object>();
                if (ex.Message.Length > 2000)
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message.Substring(0, 2000));
                else
                    errorLogInfo = await CustomAuthorizeService.InsertErrorLogInfoInDB(ex.Message);
            }
            return result;
        }
        public async Task<Dictionary<string, object>> CreateGasInternalPipeDrawingsAsync(object JsonObj, string UserID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "InsertGasInternalPipeDrawings");
                var rowInfo = JsonConvert.DeserializeObject<List<GasInternalPipeDrawingsItems>>(jsonVal[0]);

                result = await _gasInternalPipeDrawingsDataService.CreateGasInternalPipeDrawingsAsync(rowInfo, UserID);

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
        public async Task<Dictionary<string, object>> UpdateGasInternalPipeDrawingsAsync(object JsonObj, string UserID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "UpdateGasInternalPipeDrawings");
                var rowInfo = JsonConvert.DeserializeObject<List<GasInternalPipeDrawingsItems>>(jsonVal[0]);

                result = await _gasInternalPipeDrawingsDataService.UpdateGasInternalPipeDrawingsAsync(rowInfo, UserID);

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
        public async Task<Dictionary<string, object>> DeleteGasInternalPipeDrawingsAsync(object JsonObj, string UserID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "DeleteGasInternalPipeDrawings");
                var rowInfo = JsonConvert.DeserializeObject<List<GasInternalPipeDrawingsItems>>(jsonVal[0]);

                result = await _gasInternalPipeDrawingsDataService.DeleteGasInternalPipeDrawingsAsync(rowInfo, UserID);

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
        public async Task<Dictionary<string, object>> UpdatePDFFileAsync(object JsonObj, string UserID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            string imagePdfType = string.Empty;
            string imagePdfName = string.Empty;
            string finalImageName = string.Empty;
            string AttachmentPath = string.Empty;
            Image image;

            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "UploadPDFFiles");
                var rowInfo = JsonConvert.DeserializeObject<List<GasInternalPipeDrawingsItems>>(jsonVal[0]);

                //To upload multiple files on Web Server.
                imagePdfType = rowInfo[0].PDFName.Split(',')[0].Split('/')[1].Split(';')[0];
                imagePdfName = "SCHEMA_" + rowInfo[0].PostalCode + "_" + DateTime.Now.TimeOfDay.ToString().Replace(":", "").Replace(".", "");
                finalImageName = imagePdfName + "." + imagePdfType;
                AttachmentPath = ConfigurationManager.AppSettings["GasInternalPipeDrawingsPDFAttachment"];

                byte[] bytes = Convert.FromBase64String(rowInfo[0].PDFName.Split(',')[1]);

                if (imagePdfType.ToUpper() == "PDF")
                {
                    using (FileStream stream = File.Create(AttachmentPath + finalImageName))
                    {
                        stream.Write(bytes, 0, bytes.Length);
                    }
                }
                else
                {
                    image = new Bitmap(new MemoryStream(bytes));
                    using (Image imageToExport = image)
                    {
                        imageToExport.Save(AttachmentPath + imagePdfName + "." + imagePdfType);
                    }
                }
                rowInfo[0].PDFName = finalImageName;
                
                result = await _gasInternalPipeDrawingsDataService.UpdatePDFFileAsync(rowInfo, UserID);

                result.Add("FileName", finalImageName);
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
        public async Task<Dictionary<string, object>> DeletePDFFileAsync(object JsonObj, string UserID)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            string imagePdfType = string.Empty;
            string imagePdfName = string.Empty;
            string finalImageName = string.Empty;
            string poAttachmentPath = string.Empty;
            Image image;

            try
            {
                var jsonVal = _commonUtilities.UnWrapObjects(JsonObj, "DeletePDFFiles");
                var rowInfo = JsonConvert.DeserializeObject<List<GasInternalPipeDrawingsItems>>(jsonVal[0]);

                // if exist delete the file from directory
                // 
               
                result = await _gasInternalPipeDrawingsDataService.DeletePDFFileAsync(rowInfo, UserID);

                result.Add("FileName", finalImageName);
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
