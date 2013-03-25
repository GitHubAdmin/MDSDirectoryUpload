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
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace MDSDirectoryUpload
{
	/// <summary>
	/// Description of FolderUpload.
	/// </summary>
	public static class FolderUpload
	{
		public const string kAccountIdHeader = "account-id";
		public const string kAccountPasswordHeader = "account-password";
		public const string kNotifyEmailParamName = "notify_email";
		public const string kNotifyEmail = "support@abaqis.com";
		public const string kMDSPostURL = "https://trinity-dsieh.abaqis.int/stagei/mds30_composite_file";
		public const string kMDSContentType = "application/octet-stream";
		public const string kMDSFileParameterName = "composite_file";
		
		public static LinkedList<MDSResponse> ProcessMDSFolder(string mdsFolderPath, string accountId, string accountPassword) {
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
				
				uploadResults.AddLast(new MDSResponse(file, results));
				// Need to do something with the results.
				MessageBox.Show(results, string.Format("Results for: {0}", file));
			}
			
			return uploadResults;
		}
	}
}
