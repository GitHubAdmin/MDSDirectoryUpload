using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

namespace MDSDirectoryUpload
{
	/// <summary>
	/// The Upload class is responsible for managing the process of submitting
	/// a file to a web service as a multi-part form item.
	/// </summary>
	public static class Upload
	{
		/// <summary>
		/// Posts the file to the specified URI.
		/// </summary>
		/// <param name="requestUri">The URI to which the file upload should be submitted.</param>
		/// <param name="postData">The extra form parameters for the request</param>
		/// <param name="fileData">A stream with the contents of the file to be uploaded.</param>
		/// <param name="fileName">The original name of the file to be uploaded.</param>
		/// <param name="fileContentType">The content type of the contents of the file.</param>
		/// <param name="fileFieldName">The form-field name to use for the file upload.</param>
		/// <param name="cookies">A container of cookies to use with the request.</param>
		/// <param name="headers">Any request headers that should be provided with the request.</param>
		/// <returns>Reference to the response of the HTTP call.</returns>
		public static WebResponse PostFile(Uri requestUri, 
		                                   NameValueCollection postData, 
		                                   Stream fileData, 
		                                   string fileName, 
		                                   string fileContentType, 
		                                   string fileFieldName, 
		                                   CookieContainer cookies, 
		                                   NameValueCollection headers) {

			HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create (requestUri);

			if (headers != null) {
				// Set the headers
				foreach (string key in headers.AllKeys) {
					string[] values = headers.GetValues(key);
					if (values != null) {
						foreach (string value in values) {
							webrequest.Headers.Add(key, value);
						}
					}
				}
			}

			webrequest.Method = "POST";

			if (cookies != null) {
				webrequest.CookieContainer = cookies;
			}

			string boundary = "----------" + DateTime.Now.Ticks.ToString("x", CultureInfo.InvariantCulture);

			webrequest.ContentType = "multipart/form-data; boundary=" + boundary;

			StringBuilder sbHeader = new StringBuilder ();

			// Add form fields, if any
			if (postData != null) {
				foreach (string key in postData.AllKeys) {
					string[] values = postData.GetValues(key);
					if (values != null) {
						foreach (string value in values) {
							sbHeader.AppendFormat("--{0}\r\n", boundary);
							sbHeader.AppendFormat("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n", key, value);
						}
					}
				}
			}

			// Now put in the file stuff.
			if (fileData != null) {
				sbHeader.AppendFormat("--{0}\r\n", boundary);
				sbHeader.AppendFormat("Content-Disposition: form-data; name=\"{0}\"; {1}\r\n", 
				                      fileFieldName,
					                  string.IsNullOrEmpty(fileName) ? "" : string.Format(CultureInfo.InvariantCulture, "filename=\"{0}\"",
				                                                    Path.GetFileName(fileName)));
					
				sbHeader.AppendFormat("Content-Type: {0}\r\nContent-Transfer-Encoding: binary\r\nContent-Length: {1}\r\n\r\n", fileContentType, fileData.Length);			
			}

			byte[] header = Encoding.UTF8.GetBytes(sbHeader.ToString());
			byte[] footer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
			long contentLength = header.Length + (fileData != null ? fileData.Length : 0) + footer.Length;
				
			webrequest.ContentLength = contentLength;
				
			using (Stream requestStream = webrequest.GetRequestStream()) {
				requestStream.Write(header, 0, header.Length);

				if (fileData != null) {
					// write the file data, if any
					byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)fileData.Length))];
					int bytesRead;
					while ((bytesRead = fileData.Read(buffer, 0, buffer.Length)) != 0) {
						requestStream.Write(buffer, 0, bytesRead);
					}
				}

				// write footer
				requestStream.Write(footer, 0, footer.Length);
					
				return webrequest.GetResponse();
			}
		}

		/// <summary>
		/// Helper method to post a file given the file name.
		/// </summary>
		/// <param name="requestUri">The URI of the Web Service</param>
		/// <param name="postData">The form parameters for the request.</param>
		/// <param name="fileName">The path to the file to be uploaded.</param>
		/// <param name="fileContentType">The content type of the file being uploaded.</param>
		/// <param name="fileFieldName">The form parameter name of the uploaded file.</param>
		/// <param name="cookies">The cookies to be sent to the server.</param>
		/// <param name="headers">The request headers required by the request.</param>
		/// <returns></returns>
		public static WebResponse PostFile(Uri requestUri, 
		                                   NameValueCollection postData, 
		                                   string fileName,
		                                   string fileContentType, 
		                                   string fileFieldName, 
		                                   CookieContainer cookies,
		                                   NameValueCollection headers) {

			using (FileStream fileData = File.Open(fileName, 
			                                       FileMode.Open, 
			                                       FileAccess.Read,
			                                       FileShare.Read)) {
				return PostFile(requestUri, 
				                postData, 
				                fileData, 
				                fileName, 
				                fileContentType, 
				                fileFieldName, 
				                cookies,
					            headers);
			}
		}
	}
}

