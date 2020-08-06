namespace Lyrical
{
    partial class MainForm
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
            this.txtLyricsBox = new System.Windows.Forms.TextBox();
            this.btnUpdateOAuth = new System.Windows.Forms.Button();
            this.timerLyricsScraper = new System.Windows.Forms.Timer(this.components);
            this.numFontSize = new System.Windows.Forms.NumericUpDown();
            this.lblFontSize = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // txtLyricsBox
            // 
            this.txtLyricsBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLyricsBox.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLyricsBox.Location = new System.Drawing.Point(12, 12);
            this.txtLyricsBox.Multiline = true;
            this.txtLyricsBox.Name = "txtLyricsBox";
            this.txtLyricsBox.ReadOnly = true;
            this.txtLyricsBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLyricsBox.Size = new System.Drawing.Size(776, 426);
            this.txtLyricsBox.TabIndex = 0;
            this.txtLyricsBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnUpdateOAuth
            // 
            this.btnUpdateOAuth.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnUpdateOAuth.Location = new System.Drawing.Point(332, 444);
            this.btnUpdateOAuth.Name = "btnUpdateOAuth";
            this.btnUpdateOAuth.Size = new System.Drawing.Size(95, 23);
            this.btnUpdateOAuth.TabIndex = 3;
            this.btnUpdateOAuth.Text = "Login to Spotify";
            this.btnUpdateOAuth.UseVisualStyleBackColor = true;
            this.btnUpdateOAuth.Click += new System.EventHandler(this.btnUpdateOAuth_Click);
            // 
            // timerLyricsScraper
            // 
            this.timerLyricsScraper.Enabled = true;
            this.timerLyricsScraper.Interval = 1000;
            this.timerLyricsScraper.Tick += new System.EventHandler(this.timerLyricsScraper_TickAsync);
            // 
            // numFontSize
            // 
            this.numFontSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numFontSize.Location = new System.Drawing.Point(744, 447);
            this.numFontSize.Name = "numFontSize";
            this.numFontSize.Size = new System.Drawing.Size(44, 20);
            this.numFontSize.TabIndex = 4;
            this.numFontSize.ValueChanged += new System.EventHandler(this.numFontSize_ValueChanged);
            // 
            // lblFontSize
            // 
            this.lblFontSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFontSize.AutoSize = true;
            this.lblFontSize.Location = new System.Drawing.Point(684, 449);
            this.lblFontSize.Name = "lblFontSize";
            this.lblFontSize.Size = new System.Drawing.Size(54, 13);
            this.lblFontSize.TabIndex = 5;
            this.lblFontSize.Text = "Font Size:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 476);
            this.Controls.Add(this.lblFontSize);
            this.Controls.Add(this.numFontSize);
            this.Controls.Add(this.btnUpdateOAuth);
            this.Controls.Add(this.txtLyricsBox);
            this.Name = "MainForm";
            this.Text = "Lyrical: Waiting for song...";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLyricsBox;
        private System.Windows.Forms.Button btnUpdateOAuth;
        private System.Windows.Forms.Timer timerLyricsScraper;
        private System.Windows.Forms.NumericUpDown numFontSize;
        private System.Windows.Forms.Label lblFontSize;
    }
}

