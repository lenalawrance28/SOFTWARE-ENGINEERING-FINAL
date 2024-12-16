namespace Software_Engineering1
{
    partial class feedbackForm
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
            this.roundedPanel3 = new Software_Engineering1.RoundedPanel();
            this.button6 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.roundedPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // roundedPanel3
            // 
            this.roundedPanel3.BackColor = System.Drawing.Color.Crimson;
            this.roundedPanel3.BorderRadius = 50;
            this.roundedPanel3.Controls.Add(this.button6);
            this.roundedPanel3.Controls.Add(this.label9);
            this.roundedPanel3.Controls.Add(this.label8);
            this.roundedPanel3.Controls.Add(this.textBox2);
            this.roundedPanel3.Controls.Add(this.textBox1);
            this.roundedPanel3.Controls.Add(this.label7);
            this.roundedPanel3.Controls.Add(this.label6);
            this.roundedPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roundedPanel3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.roundedPanel3.Location = new System.Drawing.Point(0, 0);
            this.roundedPanel3.Name = "roundedPanel3";
            this.roundedPanel3.Size = new System.Drawing.Size(502, 450);
            this.roundedPanel3.TabIndex = 20;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.Orange;
            this.button6.Location = new System.Drawing.Point(344, 318);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(108, 23);
            this.button6.TabIndex = 26;
            this.button6.Text = "SUBMIT";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(23, 155);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 16);
            this.label9.TabIndex = 25;
            this.label9.Text = "FEEDBACK";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 114);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 16);
            this.label8.TabIndex = 24;
            this.label8.Text = "NAME";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(107, 111);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(216, 22);
            this.textBox2.TabIndex = 23;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(107, 152);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(345, 145);
            this.textBox1.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Orange;
            this.label7.Location = new System.Drawing.Point(22, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(313, 22);
            this.label7.TabIndex = 21;
            this.label7.Text = "Give us your FEEDBACK down below";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(20, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(294, 35);
            this.label6.TabIndex = 20;
            this.label6.Text = "Your Suggestions Matter";
            // 
            // feedbackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 450);
            this.Controls.Add(this.roundedPanel3);
            this.Name = "feedbackForm";
            this.Text = "feedbackForm";
            this.roundedPanel3.ResumeLayout(false);
            this.roundedPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private RoundedPanel roundedPanel3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}