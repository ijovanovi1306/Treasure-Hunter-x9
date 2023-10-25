namespace OTTER
{
    partial class GameOver
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
            this.btnPA = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnPA
            // 
            this.btnPA.Location = new System.Drawing.Point(315, 276);
            this.btnPA.Name = "btnPA";
            this.btnPA.Size = new System.Drawing.Size(141, 36);
            this.btnPA.TabIndex = 0;
            this.btnPA.Text = "Play Again";
            this.btnPA.UseVisualStyleBackColor = true;
            this.btnPA.Click += new System.EventHandler(this.btnPA_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(315, 318);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(141, 35);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.ForeColor = System.Drawing.SystemColors.Control;
            this.lblText.Location = new System.Drawing.Point(280, 135);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(35, 13);
            this.lblText.TabIndex = 2;
            this.lblText.Text = "label1";
            // 
            // GameOver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPA);
            this.Name = "GameOver";
            this.Text = "GameOver";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPA;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblText;
    }
}