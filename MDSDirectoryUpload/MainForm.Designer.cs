/*
 * Created by SharpDevelop.
 * User: Developers
 * Date: 3/21/2013
 * Time: 2:06 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace MDSDirectoryUpload
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.tbAccountId = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.tbAccountPassword = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tbMDSFolder = new System.Windows.Forms.TextBox();
			this.btnSelectMDSFolder = new System.Windows.Forms.Button();
			this.btnUpload = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lbUploadedItems = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Account ID:";
			// 
			// tbAccountId
			// 
			this.tbAccountId.Location = new System.Drawing.Point(120, 13);
			this.tbAccountId.Name = "tbAccountId";
			this.tbAccountId.Size = new System.Drawing.Size(100, 20);
			this.tbAccountId.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(13, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Account Password:";
			// 
			// tbAccountPassword
			// 
			this.tbAccountPassword.Location = new System.Drawing.Point(120, 40);
			this.tbAccountPassword.Name = "tbAccountPassword";
			this.tbAccountPassword.Size = new System.Drawing.Size(100, 20);
			this.tbAccountPassword.TabIndex = 3;
			this.tbAccountPassword.UseSystemPasswordChar = true;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(13, 67);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 23);
			this.label3.TabIndex = 4;
			this.label3.Text = "MDS Folder:";
			// 
			// tbMDSFolder
			// 
			this.tbMDSFolder.Location = new System.Drawing.Point(120, 67);
			this.tbMDSFolder.Name = "tbMDSFolder";
			this.tbMDSFolder.ReadOnly = true;
			this.tbMDSFolder.Size = new System.Drawing.Size(221, 20);
			this.tbMDSFolder.TabIndex = 5;
			// 
			// btnSelectMDSFolder
			// 
			this.btnSelectMDSFolder.Location = new System.Drawing.Point(348, 63);
			this.btnSelectMDSFolder.Name = "btnSelectMDSFolder";
			this.btnSelectMDSFolder.Size = new System.Drawing.Size(112, 23);
			this.btnSelectMDSFolder.TabIndex = 6;
			this.btnSelectMDSFolder.Text = "Select MDS Folder";
			this.btnSelectMDSFolder.UseVisualStyleBackColor = true;
			this.btnSelectMDSFolder.Click += new System.EventHandler(this.BtnSelectMDSFolderClick);
			// 
			// btnUpload
			// 
			this.btnUpload.Location = new System.Drawing.Point(410, 238);
			this.btnUpload.Name = "btnUpload";
			this.btnUpload.Size = new System.Drawing.Size(75, 23);
			this.btnUpload.TabIndex = 7;
			this.btnUpload.Text = "Upload";
			this.btnUpload.UseVisualStyleBackColor = true;
			this.btnUpload.Click += new System.EventHandler(this.BtnUploadClick);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(329, 237);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 8;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
			// 
			// lbUploadedItems
			// 
			this.lbUploadedItems.FormattingEnabled = true;
			this.lbUploadedItems.Location = new System.Drawing.Point(13, 94);
			this.lbUploadedItems.Name = "lbUploadedItems";
			this.lbUploadedItems.Size = new System.Drawing.Size(472, 134);
			this.lbUploadedItems.TabIndex = 9;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(497, 273);
			this.Controls.Add(this.lbUploadedItems);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnUpload);
			this.Controls.Add(this.btnSelectMDSFolder);
			this.Controls.Add(this.tbMDSFolder);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.tbAccountPassword);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.tbAccountId);
			this.Controls.Add(this.label1);
			this.Name = "MainForm";
			this.Text = "MDSDirectoryUpload";
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ListBox lbUploadedItems;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnUpload;
		private System.Windows.Forms.Button btnSelectMDSFolder;
		private System.Windows.Forms.TextBox tbMDSFolder;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tbAccountPassword;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tbAccountId;
		private System.Windows.Forms.Label label1;
	}
}
