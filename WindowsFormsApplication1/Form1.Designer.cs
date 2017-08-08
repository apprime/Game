namespace WindowsFormsApplication1
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
            this.StartEngine = new System.Windows.Forms.Button();
            this.StopEngine = new System.Windows.Forms.Button();
            this.Action = new System.Windows.Forms.Button();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.CurrentScore = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StartEngine
            // 
            this.StartEngine.Location = new System.Drawing.Point(12, 12);
            this.StartEngine.Name = "StartEngine";
            this.StartEngine.Size = new System.Drawing.Size(75, 23);
            this.StartEngine.TabIndex = 0;
            this.StartEngine.Text = "Start Engine";
            this.StartEngine.UseVisualStyleBackColor = true;
            this.StartEngine.Click += new System.EventHandler(this.StartEngine_Click);
            // 
            // StopEngine
            // 
            this.StopEngine.Location = new System.Drawing.Point(94, 11);
            this.StopEngine.Name = "StopEngine";
            this.StopEngine.Size = new System.Drawing.Size(75, 23);
            this.StopEngine.TabIndex = 1;
            this.StopEngine.Text = "Stop Engine";
            this.StopEngine.UseVisualStyleBackColor = true;
            this.StopEngine.Click += new System.EventHandler(this.StopEngine_Click);
            // 
            // Action
            // 
            this.Action.Location = new System.Drawing.Point(175, 11);
            this.Action.Name = "Action";
            this.Action.Size = new System.Drawing.Size(75, 23);
            this.Action.TabIndex = 2;
            this.Action.Text = "GetPoint";
            this.Action.UseVisualStyleBackColor = true;
            this.Action.Click += new System.EventHandler(this.Action_Click);
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Location = new System.Drawing.Point(12, 38);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(38, 13);
            this.scoreLabel.TabIndex = 3;
            this.scoreLabel.Text = "Score:";
            // 
            // CurrentScore
            // 
            this.CurrentScore.AutoSize = true;
            this.CurrentScore.Location = new System.Drawing.Point(57, 37);
            this.CurrentScore.Name = "CurrentScore";
            this.CurrentScore.Size = new System.Drawing.Size(13, 13);
            this.CurrentScore.TabIndex = 4;
            this.CurrentScore.Text = "0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(15, 55);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Update Score";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CurrentScore);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.Action);
            this.Controls.Add(this.StopEngine);
            this.Controls.Add(this.StartEngine);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartEngine;
        private System.Windows.Forms.Button StopEngine;
        private System.Windows.Forms.Button Action;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Label CurrentScore;
        private System.Windows.Forms.Button button1;
    }
}

