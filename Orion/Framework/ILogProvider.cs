namespace Orion.Framework
{ 
	[System.Flags]
	public enum LogOutputFlag
	{
		Console = 1,
		File = 2,
		All = 0x7FFFFFFF
	}

	public interface ILogProvider
	{
		void Write(System.Diagnostics.TraceLevel level, LogOutputFlag location, string message);

		void LogInfo(LogOutputFlag location, string message);

		void LogDebug(LogOutputFlag location, string message);

		void LogWarning(LogOutputFlag location, string message);

		void LogError(LogOutputFlag location, string message);
	}
}
