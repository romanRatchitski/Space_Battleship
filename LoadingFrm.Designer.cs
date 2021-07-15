namespace Space_Battleships
{
    partial class LoadingFrm
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
            this.components = new System.ComponentModel.Container();
            this.Title = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.TMR = new System.Windows.Forms.Timer(this.components);
            this.LdLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.BackColor = System.Drawing.Color.Transparent;
            this.Title.Font = new System.Drawing.Font("Haettenschweiler", 72F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.ForeColor = System.Drawing.Color.Red;
            this.Title.Location = new System.Drawing.Point(222, 40);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(883, 111);
            this.Title.TabIndex = 0;
            this.Title.Text = "S p a c e   B a t t l e s h i p s";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(295, 598);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(704, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // TMR
            // 
            this.TMR.Interval = 50;
            this.TMR.Tick += new System.EventHandler(this.TMR_Tick);
            // 
            // LdLbl
            // 
            this.LdLbl.BackColor = System.Drawing.Color.Transparent;
            this.LdLbl.Font = new System.Drawing.Font("Haettenschweiler", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LdLbl.ForeColor = System.Drawing.Color.Blue;
            this.LdLbl.Location = new System.Drawing.Point(288, 542);
            this.LdLbl.Name = "LdLbl";
            this.LdLbl.Size = new System.Drawing.Size(342, 44);
            this.LdLbl.TabIndex = 2;
            this.LdLbl.Text = "Loading.....";
            // 
            // LoadingFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Space_Battleships.Properties.Resources.MainManu_Pic;
            this.ClientSize = new System.Drawing.Size(1289, 651);
            this.Controls.Add(this.LdLbl);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.Title);
            this.Name = "LoadingFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoadingFrm";
            this.Load += new System.EventHandler(this.LoadingFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer TMR;
        private System.Windows.Forms.Label LdLbl;
    }
}