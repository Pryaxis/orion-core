using System;
using log4net;

namespace Orion.Logging
{
	public sealed class Log
	{
		public void Info(Type type, string msg)
		{
			LogManager.GetLogger(type).Info(msg);
		}
	}
}