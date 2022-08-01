using NLog;
using Schema.Core.Services;
using System;

namespace Schema.Web.Services
{
    public class LoggingService : ILoggingService
    {
        private static Logger logger = LogManager.GetLogger("SCHEMA_API :");
        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Debug(string message, params object[] args)
        {
            logger.Debug(message,args);
        }

        public void Debug(Exception exception)
        {
            logger.Debug(exception);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Error(string message, params object[] args)
        {
            logger.Error(message, args);
        }

        public void Error(Exception exception)
        {
            logger.Error(exception);
        }

        public void Fatal(string message)
        {
            logger.Fatal(message);
        }

        public void Fatal(string message, params object[] args)
        {
            logger.Fatal(message, args);
        }

        public void Fatal(Exception exception)
        {
            logger.Fatal(exception);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Info(string message, params object[] args)
        {
            logger.Info(message, args);
        }

        public void Info(Exception exception)
        {
            logger.Info(exception);
        }

        public void Trace(string message)
        {
            logger.Trace(message);
        }

        public void Trace(string message, params object[] args) => logger.Trace(message, args);

        public void Trace(Exception exception)
        {
            logger.Trace(exception);
        }

        public void Warn(string message)
        {
            logger.Warn(message);
        }

        public void Warn(string message, params object[] args)
        {
            logger.Warn(message, args);
        }

        public void Warn(Exception exception)
        {
            logger.Warn(exception);
        }
    }
}