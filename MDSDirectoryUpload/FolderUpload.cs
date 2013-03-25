/*
 * Created by SharpDevelop.
 * User: Developers
 * Date: 3/21/2013
 * Time: 2:45 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace MDSDirectoryUpload
{
	/// <summary>
	/// The FolderUpload class is responsible for taking the contents
	/// of a specified directory and uploading them to the abaqis MDS 
	/// Web Service.
	/// </summary>
	public static class FolderUpload
	{
		public const string kAccountIdHeader = "account-id";
		public const string kAccountPasswordHeader = "account-password";
		public const string kNotifyEmailParamName = "notify_email";
		public const string kMDSContentType = "application/octet-stream";
		public const string kMDSFileParameterName = "composite_file";
		
		/// <summary>
		/// Processes all the MDS .zip files in the specified folder and submits
		/// them to the abaqis MDS web service with the specified credentials.
		/// </summary>
		/// <param name="mdsFolderPath">The path where the MDS files exist.</param>
		/// <param name="accountId">The account ID provided by Providigm for your account.</param>
		/// <param name="accountPassword">The account password provided by Providigm for your account.</param>
		/// <returns>A list of MDS Responses, one for each MDS .zip file in the directory.</returns>
		public static LinkedList<MDSResponse> ProcessMDSFolder(string mdsFolderPath, string accountId, string accountPassword) {
			string kNotifyEmail = ConfigurationManager.AppSettings["WebServiceNotificationEmail"];
			string kMDSPostURL = ConfigurationManager.AppSettings["WebServiceURI"];
			LinkedList<MDSResponse> uploadResults = new LinkedList<MDSResponse>();
			// Grab all the files to be processed.
			string[] files = Directory.GetFiles(mdsFolderPath, "*.zip", SearchOption.TopDirectoryOnly);
			
			// Set up the headers to be sent to the web service
			NameValueCollection headers = new NameValueCollection();
			headers[kAccountIdHeader] = accountId;
			headers[kAccountPasswordHeader] = accountPassword;
			
			// Set up the parameters for the call.
			NameValueCollection parameters = new NameValueCollection();
			parameters[kNotifyEmailParamName] = kNotifyEmail;
			
			FileManager fm = new FileManager(mdsFolderPath);
			        
			foreach (string file in files) {
				if (file.Length > 250) {
					MDSResponse r = new MDSResponse(file);
					r.Status = "ERROR";
					r.Message = string.Format("File name too long to process ({0}), skipping", file);
					uploadResults.AddLast(r);
					MessageBox.Show(r.Message, "Processing MDS Files");
					continue;
				}
				WebResponse response = Upload.PostFile(new Uri(kMDSPostURL), parameters, file, kMDSContentType, kMDSFileParameterName, null, headers);
				StreamReader reader = new StreamReader(response.GetResponseStream());
				string results = reader.ReadToEnd();
				
				MDSResponse mrsp = new MDSResponse(file, results);
				uploadResults.AddLast(mrsp);
				
				// Finally, archive the file
				if (mrsp.UploadError) {
					fm.ArchiveErroredFile(file);
				} else {
					fm.ArchiveProcessedFile(file);
				}
			}
			
			return uploadResults;
		}
	}
}
