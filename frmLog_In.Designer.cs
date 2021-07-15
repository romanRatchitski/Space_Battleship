namespace Space_Battleships
{
    partial class frmLog_In
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtEnterUserName = new System.Windows.Forms.TextBox();
            this.lblEnterUserName = new System.Windows.Forms.Label();
            this.btnLogIn = new System.Windows.Forms.Button();
            this.lblGoBack = new System.Windows.Forms.Label();
            this.DeleteBTN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtEnterUserName
            // 
            this.txtEnterUserName.Font = new System.Drawing.Font("Agency FB", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEnterUserName.Location = new System.Drawing.Point(554, 94);
            this.txtEnterUserName.Name = "txtEnterUserName";
            this.txtEnterUserName.Size = new System.Drawing.Size(90, 36);
            this.txtEnterUserName.TabIndex = 13;
            // 
            // lblEnterUserName
            // 
            this.lblEnterUserName.AutoSize = true;
            this.lblEnterUserName.BackColor = System.Drawing.Color.Transparent;
            this.lblEnterUserName.Font = new System.Drawing.Font("Agency FB", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnterUserName.ForeColor = System.Drawing.Color.SeaShell;
            this.lblEnterUserName.Location = new System.Drawing.Point(522, 40);
            this.lblEnterUserName.Name = "lblEnterUserName";
            this.lblEnterUserName.Size = new System.Drawing.Size(159, 34);
            this.lblEnterUserName.TabIndex = 12;
            this.lblEnterUserName.Text = "Enter User Name";
            // 
            // btnLogIn
            // 
            this.btnLogIn.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnLogIn.Font = new System.Drawing.Font("Agency FB", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogIn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnLogIn.Location = new System.Drawing.Point(452, 156);
            this.btnLogIn.Name = "btnLogIn";
            this.btnLogIn.Size = new System.Drawing.Size(291, 45);
            this.btnLogIn.TabIndex = 11;
            this.btnLogIn.Text = "START";
            this.btnLogIn.UseVisualStyleBackColor = false;
            this.btnLogIn.Click += new System.EventHandler(this.btnLogIn_Click);
            // 
            // lblGoBack
            // 
            this.lblGoBack.AutoSize = true;
            this.lblGoBack.BackColor = System.Drawing.Color.Transparent;
            this.lblGoBack.Font = new System.Drawing.Font("Agency FB", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGoBack.ForeColor = System.Drawing.Color.Orange;
            this.lblGoBack.Location = new System.Drawing.Point(12, 590);
            this.lblGoBack.Name = "lblGoBack";
            this.lblGoBack.Size = new System.Drawing.Size(81, 25);
            this.lblGoBack.TabIndex = 32;
            this.lblGoBack.Text = "<- Go Back";
            this.lblGoBack.Click += new System.EventHandler(this.lblGoBack_Click);
            // 
            // DeleteBTN
            // 
            this.DeleteBTN.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.DeleteBTN.Font = new System.Drawing.Font("Agency FB", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteBTN.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.DeleteBTN.Location = new System.Drawing.Point(913, 567);
            this.DeleteBTN.Name = "DeleteBTN";
            this.DeleteBTN.Size = new System.Drawing.Size(291, 45);
            this.DeleteBTN.TabIndex = 33;
            this.DeleteBTN.Text = "Delete User";
            this.DeleteBTN.UseVisualStyleBackColor = false;
            this.DeleteBTN.Click += new System.EventHandler(this.DeleteBTN_Click);
            // 
            // frmLog_In
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Space_Battleships.Properties.Resources.qsXsZ;
            this.ClientSize = new System.Drawing.Size(1216, 624);
            this.Controls.Add(this.DeleteBTN);
            this.Controls.Add(this.lblGoBack);
            this.Controls.Add(this.txtEnterUserName);
            this.Controls.Add(this.lblEnterUserName);
            this.Controls.Add(this.btnLogIn);
            this.Name = "frmLog_In";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLog_In";
            this.Load += new System.EventHandler(this.frmLog_In_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEnterUserName;
        private System.Windows.Forms.Label lblEnterUserName;
        private System.Windows.Forms.Button btnLogIn;
        private System.Windows.Forms.Label lblGoBack;
        private System.Windows.Forms.Button DeleteBTN;
    }
}