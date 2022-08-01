using System;

namespace Schema.Core.Services
{
    public interface ILoggingService
    {
        void Debug(string message);
        void Debug(string message, params object[] args);
        void Debug(Exception exception);
        void Error(string message);
        void Error(string message, params object[] args);
        void Error(Exception exception);
        void Fatal(string message);
        void Fatal(string message, params object[] args);
        void Fatal(Exception exception);
        void Info(string message);
        void Info(string message, params object[] args);
        void Info(Exception exception);
        void Trace(string message);
        void Trace(string message, params object[] args);
        void Trace(Exception exception);
        void Warn(string message);
        void Warn(string message, params object[] args);
        void Warn(Exception exception);
    }
}
