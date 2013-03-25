/*
 * Created by SharpDevelop.
 * User: Developers
 * Date: 3/25/2013
 * Time: 10:30 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace MDSDirectoryUpload
{
	/// <summary>
	/// This class is responsible for handling the archival of
	/// MDS files. It manages two primary directories for archival:
	/// one for successfully processed files and one for files that
	/// had an error during the upload process.
	/// </summary>
	public class FileManager
	{
		private string _mdsUploadPath;
		private string _archiveTimestamp;
		
		/// <summary>
		/// Constructs a new FileManager object for the specified MDS directory.
		/// </summary>
		/// <param name="mdsUploadPath">The base path for the archive directories.</param>
		public FileManager(string mdsUploadPath)
		{
			_mdsUploadPath = mdsUploadPath;
			_archiveTimestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
		}
		
		public string MDSUploadPath {
			get { return _mdsUploadPath; }
		}
		
		public string ArchiveTimestamp {
			get { return _archiveTimestamp; }
		}
		
		/// <summary>
		/// Moves the specified file to the "Processed" archive directory.
		/// </summary>
		/// <param name="processedFilePath">The path to the file that was successfully uploaded.</param>
		public void ArchiveProcessedFile(string processedFilePath) {
			ArchiveFile("Processed", processedFilePath);
		}
		
		/// <summary>
		/// Moves the specified file to the "Errored" archive directory.
		/// </summary>
		/// <param name="erroredFilePath">The path to the file that failed the upload.</param>
		public void ArchiveErroredFile(string erroredFilePath) {
			ArchiveFile("Errored", erroredFilePath);
		}
		
		/// <summary>
		/// A helper method to manage the archivate of the specified file.
		/// </summary>
		/// <param name="archiveName">The name of the archive (e.g. "Processed" or "Errored"</param>
		/// <param name="fileToArchive">The path of the file to be archived.</param>
		private void ArchiveFile(string archiveName, string fileToArchive) {
			// Make sure the specified file exists
			if (! File.Exists(fileToArchive)) {
				// Nothing to do; file doesn't exist.
				return;
			}
			string basePath = Path.GetDirectoryName(fileToArchive);	
			string fileName = Path.GetFileName(fileToArchive);
			string archivePath = CreatedNamedDatedDirectory(basePath, archiveName);
			File.Move(fileToArchive, Path.Combine(archivePath, fileName));
		}
			
		/// <summary>
		/// A helper method to manage the naming and creation of the archival directories.
		/// </summary>
		/// <param name="basePath">The path in which the archival directories are to exist.</param>
		/// <param name="namedSubdirectory">The name of the archive (e.g. "Processed" or "Errored".</param>
		/// <returns></returns>
		private string CreatedNamedDatedDirectory(string basePath, string namedSubdirectory) {
			string dateStamp = ArchiveTimestamp;
			string combinedName = Path.Combine(basePath, namedSubdirectory, dateStamp);
			if (! Directory.Exists(combinedName)) {
				Directory.CreateDirectory(combinedName);
			}
			return combinedName;
		}
	}
}
