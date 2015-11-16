using Orion.Framework;

namespace Orion.Modules.Logging
{
	[OrionModule("Orion Logging Module", "Nyx Studios", Description = "Provides logging functionality")]
	public class LogModule : OrionModuleBase, ILogProvider
	{
		private log4net.ILog _log;
		private log4net.ILog _fileLog;

		public LogModule(Orion core) 
			: base(core)
		{
		}

		public override void Initialize()
		{
			base.Initialize();

			log4net.Config.XmlConfigurator.ConfigureAndWatch(
				new System.IO.FileInfo(System.IO.Path.Combine(Core.OrionConfigurationPath, "LogConfig.xml")));

			_log = log4net.LogManager.GetLogger("ConsoleLog");
			_fileLog = log4net.LogManager.GetLogger("FileLog");
		}

		public void Write(System.Diagnostics.TraceLevel level, LogOutputFlag location, string message)
		{
			switch (level)
			{
				case System.Diagnostics.TraceLevel.Info:
					LogInfo(location, message);
					break;

				case System.Diagnostics.TraceLevel.Verbose:
					LogDebug(location, message);
					break;

				case System.Diagnostics.TraceLevel.Warning:
					LogWarning(location, message);
					break;

				case System.Diagnostics.TraceLevel.Error:
					LogError(location, message);
					break;

				default:
					break;
			}
		}

		public void LogInfo(LogOutputFlag location, string message)
		{
			if (location.HasFlag(LogOutputFlag.Console))
			{
				_log.Info(message);
			}
			if (location.HasFlag(LogOutputFlag.File))
			{
				_fileLog.Info(message);
			}
		}

		public void LogDebug(LogOutputFlag location, string message)
		{
			if (location.HasFlag(LogOutputFlag.Console))
			{
				_log.Debug(message);
			}
			if (location.HasFlag(LogOutputFlag.File))
			{
				_fileLog.Debug(message);
			}
		}

		public void LogWarning(LogOutputFlag location, string message)
		{
			if (location.HasFlag(LogOutputFlag.Console))
			{
				_log.Warn(message);
			}
			if (location.HasFlag(LogOutputFlag.File))
			{
				_fileLog.Warn(message);
			}
		}

		public void LogError(LogOutputFlag location, string message)
		{
			if (location.HasFlag(LogOutputFlag.Console))
			{
				_log.Error(message);
			}
			if (location.HasFlag(LogOutputFlag.File))
			{
				_fileLog.Error(message);
			}
		}
	}
}