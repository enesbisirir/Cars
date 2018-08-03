namespace Cars
{
    partial class Form1
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
            this.LblMiddle = new System.Windows.Forms.Label();
            this.LblRight = new System.Windows.Forms.Label();
            this.LblLeft = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LblMiddle
            // 
            this.LblMiddle.BackColor = System.Drawing.Color.CornflowerBlue;
            this.LblMiddle.Location = new System.Drawing.Point(228, 0);
            this.LblMiddle.Name = "LblMiddle";
            this.LblMiddle.Size = new System.Drawing.Size(6, 614);
            this.LblMiddle.TabIndex = 0;
            // 
            // LblRight
            // 
            this.LblRight.BackColor = System.Drawing.Color.CornflowerBlue;
            this.LblRight.Location = new System.Drawing.Point(349, 0);
            this.LblRight.Name = "LblRight";
            this.LblRight.Size = new System.Drawing.Size(3, 614);
            this.LblRight.TabIndex = 0;
            // 
            // LblLeft
            // 
            this.LblLeft.BackColor = System.Drawing.Color.CornflowerBlue;
            this.LblLeft.Location = new System.Drawing.Point(110, 0);
            this.LblLeft.Name = "LblLeft";
            this.LblLeft.Size = new System.Drawing.Size(3, 614);
            this.LblLeft.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkBlue;
            this.ClientSize = new System.Drawing.Size(462, 613);
            this.Controls.Add(this.LblMiddle);
            this.Controls.Add(this.LblLeft);
            this.Controls.Add(this.LblRight);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Cars";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LblMiddle;
        private System.Windows.Forms.Label LblRight;
        private System.Windows.Forms.Label LblLeft;
    }
}

