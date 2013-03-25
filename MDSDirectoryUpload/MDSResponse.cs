/*
 * Created by SharpDevelop.
 * User: Developers
 * Date: 3/22/2013
 * Time: 2:27 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Xml;

namespace MDSDirectoryUpload
{
	/// <summary>
	/// The MDSResponse class encapsulates the information returned
	/// by the abaqis MDS web service.
	/// </summary>
	public class MDSResponse
	{
		private string _mdsFilePath;
		private string _status;
		private string _submissionDateTime;
		private string _message;
		
		/// <summary>
		/// Constructs a new MDSResponse object for the specified
		/// file path.
		/// </summary>
		/// <param name="path">The path to the file for which this response was created.</param>
		public MDSResponse(string path) {
			MDSFilePath = path;
		}
		
		/// <summary>
		/// Constructs a new MDSResponse object for the specified
		/// file path and the XML response from the web service. The
		/// basic information is parsed from the XML and made available
		/// via the properties of this object.
		/// </summary>
		/// <param name="path">The path to the file for which this response was created.</param>
		/// <param name="rawXMLResponse">The XML response from the abaqis web service.</param>
		public MDSResponse(string path, string rawXMLResponse)
		{
			MDSFilePath = path;
			InitFromXML(rawXMLResponse);
		}
		
		/// <summary>
		/// Returns true if the upload of this file was in error.
		/// </summary>
		public bool UploadError {
			get {
				return "ERROR" == _status;
			}
		}
		
		/// <summary>
		/// Parses the XML for the information with which to populate
		/// the properties of this object.
		/// </summary>
		/// <param name="rawXMLResponse"></param>
		private void InitFromXML(string rawXMLResponse) {
			using (XmlReader reader = XmlReader.Create(new StringReader(rawXMLResponse))) {
				// Parse the file and display each of the nodes.
				string currentElement = null;
        		while (reader.Read()) {
            		switch (reader.NodeType) {
                	case XmlNodeType.Element:
            			currentElement = reader.Name;
                    	break;
                	case XmlNodeType.Text:
                    	if (currentElement == "status") {
                    		Status = reader.Value;
                    	} else if (currentElement == "submission_datetime") {
                    		SubmissionDateTime = reader.Value;
                    	} else if (currentElement == "error_message") {
                    		Message = reader.Value;
                    	}
                    	break;
            		}
        		}

			}
		}
		
		/// <summary>
		/// Provides a nice string to display the status of a file upload.
		/// </summary>
		/// <returns>A string formatted: "status - path"</returns>
		override public string ToString() {
			return string.Format("{0} - {1}", Status, MDSFilePath);
		}
		
		public string MDSFilePath {
			get { return _mdsFilePath; }
			set { _mdsFilePath = value; }
		}
		
		public string Status {
			get { return _status; }
			set { _status = value; }
		}
		
		public string SubmissionDateTime {
			get { return _submissionDateTime; }
			set { _submissionDateTime = value; }
		}
		
		public string Message {
			get { return _message; }
			set { _message = value; }
		}
	}
}
