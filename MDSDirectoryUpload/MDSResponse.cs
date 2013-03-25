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
	/// Description of MDSResponse.
	/// </summary>
	public class MDSResponse
	{
		private string _mdsFilePath;
		private string _status;
		private string _submissionDateTime;
		private string _message;
		
		public MDSResponse(string path) {
			MDSFilePath = path;
		}
		
		public MDSResponse(string path, string rawXMLResponse)
		{
			MDSFilePath = path;
			InitFromXML(rawXMLResponse);
		}
		
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
