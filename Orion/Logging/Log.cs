using System.IO;
using log4net;
using log4net.Config;

namespace Orion.Logging
{
	/// <summary>
	/// Default Log4Net logger
	/// </summary>
	public sealed class Log : ILog
	{
	    private log4net.ILog _log;
	    private log4net.ILog _fileLog;

		/// <summary>
		/// Creates a new instance of this logger using the config file found in <see cref="configPath"/>
		/// </summary>
		/// <param name="configPath">path to the log4net config file</param>
	    public Log(string configPath)
	    {
            XmlConfigurator.Configure(new FileInfo(configPath));

	        _log = LogManager.GetLogger("OrionConsoleAndFile");
	        _fileLog = LogManager.GetLogger("OrionFile");
	    }

        public bool MayWriteType(LogLevel type)
	    {
	        switch (type)
	        {
                case LogLevel.Error:
	                return _log.IsErrorEnabled;

                case LogLevel.Info:
	                return _log.IsInfoEnabled;

                case LogLevel.Warning:
	                return _log.IsWarnEnabled;

                case LogLevel.Debug:
	                return _log.IsDebugEnabled;

                default:
	                return false;
	        }
	    }

	    public void ConsoleInfo(string message)
	    {
	        _log.Info(message);
	    }

	    public void ConsoleInfo(string format, params object[] args)
	    {
            _log.InfoFormat(format, args);
	    }

	    public void ConsoleError(string message)
	    {
	        _log.Error(message);
	    }

	    public void ConsoleError(string format, params object[] args)
	    {
	        _log.ErrorFormat(format, args);
	    }

	    public void Warn(string message)
	    {
	        _fileLog.Warn(message);
	    }

	    public void Warn(string format, params object[] args)
	    {
	        _fileLog.WarnFormat(format, args);
	    }

	    public void Error(string message)
	    {
	        _fileLog.Error(message);
	    }

	    public void Error(string format, params object[] args)
	    {
	        _fileLog.ErrorFormat(format, args);
	    }

	    public void Info(string message)
	    {
	        _fileLog.Info(message);
	    }

	    public void Info(string format, params object[] args)
	    {
	        _fileLog.InfoFormat(format, args);
	    }

        public void Write(string message, LogLevel level)
	    {
	        switch (level)
	        {
	            case LogLevel.Info:
	                _fileLog.Info(message);
	                break;

                case LogLevel.Warning:
                    _fileLog.Warn(message);
	                break;

                case LogLevel.Debug:
	                _fileLog.Debug(message);
	                break;

                case LogLevel.Error:
	                _fileLog.Error(message);
	                break;
	        }
	    }

	    public void Debug(string message)
	    {
	        _fileLog.Debug(message);
	    }

	    public void Debug(string format, params object[] args)
	    {
	        _fileLog.DebugFormat(format, args);
	    }

	    public void Dispose()
	    {
	        _log.Logger.Repository.Shutdown();
            _fileLog.Logger.Repository.Shutdown();
	    }
	}
}