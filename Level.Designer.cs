namespace Space_Battleships
{
    partial class Level
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
            this.TMR2 = new System.Windows.Forms.Timer(this.components);
            this.User_Panel = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.LvText = new System.Windows.Forms.Label();
            this.TMR1 = new System.Windows.Forms.Timer(this.components);
            this.Player = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Player)).BeginInit();
            this.SuspendLayout();
            // 
            // TMR2
            // 
            this.TMR2.Interval = 15;
            this.TMR2.Tick += new System.EventHandler(this.EventTimer_Tick);
            // 
            // User_Panel
            // 
            this.User_Panel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.User_Panel.Font = new System.Drawing.Font("Elephant", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.User_Panel.ForeColor = System.Drawing.Color.Blue;
            this.User_Panel.Location = new System.Drawing.Point(1450, 9);
            this.User_Panel.Name = "User_Panel";
            this.User_Panel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.User_Panel.Size = new System.Drawing.Size(122, 68);
            this.User_Panel.TabIndex = 15;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(1450, 80);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(122, 23);
            this.progressBar1.TabIndex = 16;
            this.progressBar1.Value = 100;
            // 
            // LvText
            // 
            this.LvText.BackColor = System.Drawing.Color.Transparent;
            this.LvText.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.LvText.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LvText.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 24F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LvText.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.LvText.Location = new System.Drawing.Point(79, 51);
            this.LvText.Name = "LvText";
            this.LvText.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.LvText.Size = new System.Drawing.Size(835, 461);
            this.LvText.TabIndex = 11;
            // 
            // TMR1
            // 
            this.TMR1.Interval = 30;
            this.TMR1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Player
            // 
            this.Player.BackColor = System.Drawing.Color.Transparent;
            this.Player.Image = global::Space_Battleships.Properties.Resources.Player;
            this.Player.Location = new System.Drawing.Point(790, 739);
            this.Player.Name = "Player";
            this.Player.Size = new System.Drawing.Size(79, 110);
            this.Player.TabIndex = 13;
            this.Player.TabStop = false;
            // 
            // Level
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1584, 861);
            this.ControlBox = false;
            this.Controls.Add(this.Player);
            this.Controls.Add(this.LvText);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.User_Panel);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.ForeColor = System.Drawing.Color.Transparent;
            this.Name = "Level";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form6";
            this.TransparencyKey = System.Drawing.Color.Yellow;
            this.Load += new System.EventHandler(this.Form6_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Level1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.Player)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer TMR2;
        private System.Windows.Forms.Label LvText;
        private System.Windows.Forms.Label User_Panel;
        private System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.PictureBox Player;
        private System.Windows.Forms.Timer TMR1;


    }
}