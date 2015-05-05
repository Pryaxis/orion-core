using log4net.Appender;
using log4net.Config;

namespace Orion.Logging
{
	public sealed class LogAppender : RollingFileAppender
	{
	}

	public sealed class Log
	{
		public Log()
		{
			XmlConfigurator.Configure();
		}
	}
}