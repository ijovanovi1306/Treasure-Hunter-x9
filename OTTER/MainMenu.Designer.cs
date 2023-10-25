namespace OTTER
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.btnNG = new System.Windows.Forms.Button();
            this.btnHS = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnHelp = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNG
            // 
            this.btnNG.Location = new System.Drawing.Point(322, 260);
            this.btnNG.Name = "btnNG";
            this.btnNG.Size = new System.Drawing.Size(118, 38);
            this.btnNG.TabIndex = 0;
            this.btnNG.Text = "New Game";
            this.btnNG.UseVisualStyleBackColor = true;
            this.btnNG.Click += new System.EventHandler(this.btnNG_Click);
            // 
            // btnHS
            // 
            this.btnHS.Location = new System.Drawing.Point(322, 304);
            this.btnHS.Name = "btnHS";
            this.btnHS.Size = new System.Drawing.Size(118, 38);
            this.btnHS.TabIndex = 1;
            this.btnHS.Text = "Scoreboard";
            this.btnHS.UseVisualStyleBackColor = true;
            this.btnHS.Click += new System.EventHandler(this.btnHS_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(322, 392);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(118, 38);
            this.btnQuit.TabIndex = 2;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQ_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(87, 54);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(600, 200);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(322, 348);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(118, 38);
            this.btnHelp.TabIndex = 4;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnHS);
            this.Controls.Add(this.btnNG);
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNG;
        private System.Windows.Forms.Button btnHS;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnHelp;
    }
}