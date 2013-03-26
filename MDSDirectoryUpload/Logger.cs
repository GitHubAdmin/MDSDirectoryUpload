/*
 * Created by SharpDevelop.
 * User: Developers
 * Date: 3/26/2013
 * Time: 8:24 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace MDSDirectoryUpload
{
	/// <summary>
	/// The Logger class provides simple logging functionality for the application.
	/// It appends log entries to a file.
	/// </summary>
	public class Logger
	{
		private StreamWriter _writer;
		
		/// <summary>
		/// Constructs a logger with a file in the specified path.
		/// </summary>
		/// <param name="applicationPath">The path to the log file to managed.</param>
		public Logger(string applicationPath)
		{
			string logPath = Path.Combine(applicationPath, "MDSDirectoryUpload.log");
			_writer = new StreamWriter(logPath, true);
		}
		
		/// <summary>
		/// Logs an entry to the file.
		/// </summary>
		/// <param name="entry">The string to log.</param>
		public void LogEntry(string entry) {
			_writer.WriteLine(entry);
		}
		
		/// <summary>
		/// Close the log file.
		/// </summary>
		public void Close() {
			_writer.Close();
		}
	}
}
