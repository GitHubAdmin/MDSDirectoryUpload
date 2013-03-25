/*
 * Created by SharpDevelop.
 * User: Developers
 * Date: 3/21/2013
 * Time: 2:06 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Net;

namespace MDSDirectoryUpload
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };		
		}
		
		
		void BtnSelectMDSFolderClick(object sender, EventArgs e)
		{
			using (FolderBrowserDialog dialog = new FolderBrowserDialog()) {
				dialog.ShowNewFolderButton = false;
				if (dialog.ShowDialog() == DialogResult.OK) {
					tbMDSFolder.Text = dialog.SelectedPath;
				}
			}
		}
		
		void BtnUploadClick(object sender, EventArgs e)
		{
			string accountId = tbAccountId.Text;
			string accountPassword = tbAccountPassword.Text;
			string mdsFolder = tbMDSFolder.Text;
			
			StringBuilder sb = new StringBuilder();
			if (accountId.Length == 0) {
				sb.Append("Must specify an Account ID");
				
			}
			if (accountPassword.Length == 0) {
				if (sb.Length > 0) {
					sb.Append("\n");
				}
				sb.Append("Must specify an Account Password");
			}
			if (mdsFolder.Length == 0) {
				if (sb.Length > 0) {
					sb.Append("\n");
				}
				sb.Append("Must specify an MDS folder");
			}
			if (sb.Length == 0) {
				LinkedList<MDSResponse> responses = FolderUpload.ProcessMDSFolder(mdsFolder, accountId, accountPassword);
				lbUploadedItems.BeginUpdate();
				foreach (MDSResponse response in responses) {
					lbUploadedItems.Items.Add(response.ToString());
				}
				lbUploadedItems.EndUpdate();
			} else {
				MessageBox.Show(sb.ToString(), "MDS Upload");
			}
		}
		
		void BtnCancelClick(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}
