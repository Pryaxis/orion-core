/*
TShock, a server mod for Terraria
Copyright (C) 2011-2015 Nyx Studios (fka. The TShock Team)

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using TerrariaApi.Server;

namespace Orion.Logging
{
	/// <summary>
	/// Class inheriting ILog for writing logs to a text file
	/// </summary>
	public sealed class TextLog : ILog, IDisposable
	{
		private readonly Orion _orion;

		private readonly StreamWriter _logWriter;

		/// <summary>
		/// File name of the Text log
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// Creates the log file stream
		/// </summary>
		/// <param name="orion"></param>
		/// <param name="filename">The output filename. This file will be overwritten if 'clear' is set.</param>
		/// <param name="clear">Whether or not to clear the log file on initialization.</param>
		public TextLog(Orion orion, string filename, bool clear)
		{
			_orion = orion;
			FileName = filename;
			_logWriter = new StreamWriter(filename, !clear);
		}

		public bool MayWriteType(LogLevel type)
		{
		    return true;
		}

		/// <summary>
		/// Writes an error to the log file.
		/// </summary>
		/// <param name="message">The message to be written.</param>
		public void Error(string message)
		{
            Write(message, LogLevel.Error);
		}

		/// <summary>
		/// Writes an error to the log file.
		/// </summary>
		/// <param name="format">The format of the message to be written.</param>
		/// <param name="args">The format arguments.</param>
		public void Error(string format, params object[] args)
		{
			Error(String.Format(format, args));
		}

		/// <summary>
		/// Writes an error to the log file.
		/// </summary>
		/// <param name="message">The message to be written.</param>
		public void ConsoleError(string message)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(message);
			Console.ForegroundColor = ConsoleColor.Gray;
            Write(message, LogLevel.Error);
		}

		/// <summary>
		/// Writes an error to the log file.
		/// </summary>
		/// <param name="format">The format of the message to be written.</param>
		/// <param name="args">The format arguments.</param>
		public void ConsoleError(string format, params object[] args)
		{
			ConsoleError(String.Format(format, args));
		}

		/// <summary>
		/// Writes a warning to the log file.
		/// </summary>
		/// <param name="message">The message to be written.</param>
		public void Warn(string message)
		{
            Write(message, LogLevel.Warning);
		}

		/// <summary>
		/// Writes a warning to the log file.
		/// </summary>
		/// <param name="format">The format of the message to be written.</param>
		/// <param name="args">The format arguments.</param>
		public void Warn(string format, params object[] args)
		{
			Warn(String.Format(format, args));
		}

		/// <summary>
		/// Writes an informative string to the log file.
		/// </summary>
		/// <param name="message">The message to be written.</param>
		public void Info(string message)
		{
            Write(message, LogLevel.Info);
		}

		/// <summary>
		/// Writes an informative string to the log file.
		/// </summary>
		/// <param name="format">The format of the message to be written.</param>
		/// <param name="args">The format arguments.</param>
		public void Info(string format, params object[] args)
		{
			Info(String.Format(format, args));
		}

		/// <summary>
		/// Writes an informative string to the log file. Also outputs to the console.
		/// </summary>
		/// <param name="message">The message to be written.</param>
		public void ConsoleInfo(string message)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(message);
			Console.ForegroundColor = ConsoleColor.Gray;
            Write(message, LogLevel.Info);
		}

		/// <summary>
		/// Writes an informative string to the log file. Also outputs to the console.
		/// </summary>
		/// <param name="format">The format of the message to be written.</param>
		/// <param name="args">The format arguments.</param>
		public void ConsoleInfo(string format, params object[] args)
		{
			ConsoleInfo(String.Format(format, args));
		}

		/// <summary>
		/// Writes a debug string to the log file.
		/// </summary>
		/// <param name="message">The message to be written.</param>
		public void Debug(string message)
		{
            Write(message, LogLevel.Debug);
		}

		/// <summary>
		/// Writes a debug string to the log file.
		/// </summary>
		/// <param name="format">The format of the message to be written.</param>
		/// <param name="args">The format arguments.</param>
		public void Debug(string format, params object[] args)
		{
			Debug(String.Format(format, args));
		}

		/// <summary>
		/// Writes a message to the log
		/// </summary>
		/// <param name="message"></param>
		/// <param name="level"></param>
        public void Write(string message, LogLevel level)
		{
			if (!MayWriteType(level))
				return;

			string caller = "TShock";

			StackFrame frame = new StackTrace().GetFrame(2);
			if (frame != null)
			{
				MethodBase meth = frame.GetMethod();
				if (meth != null && meth.DeclaringType != null)
					caller = meth.DeclaringType.Name;
			}

			string logEntry = String.Format("{0} - {1}: {2}: {3}", 
				DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), caller, level.ToString().ToUpper(), message);
			try
			{
				_logWriter.WriteLine(logEntry);
				_logWriter.Flush();
			}
			catch (ObjectDisposedException)
			{
				ServerApi.LogWriter.PluginWriteLine(_orion, logEntry, TraceLevel.Error);
				Console.WriteLine(Strings.TextLogDisposedOutput);
				Console.WriteLine("{0} - {1}: {2}: {3}",
					DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
					caller, level.ToString().ToUpper(), message);
			}
		}

		public void Dispose()
		{
			if (_logWriter != null)
			{
				_logWriter.Dispose();
			}
		}
	}
}