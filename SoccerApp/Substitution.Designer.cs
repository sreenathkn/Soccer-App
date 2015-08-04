namespace SoccerApp
{
    partial class Substitution
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
            this.cmbOut = new System.Windows.Forms.ComboBox();
            this.cmbJerOUT = new System.Windows.Forms.ComboBox();
            this.cmbJerIN = new System.Windows.Forms.ComboBox();
            this.cmbIn = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbTeam = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbOut
            // 
            this.cmbOut.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbOut.FormattingEnabled = true;
            this.cmbOut.Location = new System.Drawing.Point(88, 36);
            this.cmbOut.Name = "cmbOut";
            this.cmbOut.Size = new System.Drawing.Size(121, 21);
            this.cmbOut.TabIndex = 0;
            this.cmbOut.SelectedIndexChanged += new System.EventHandler(this.cmbOut_SelectedIndexChanged);
            // 
            // cmbJerOUT
            // 
            this.cmbJerOUT.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbJerOUT.FormattingEnabled = true;
            this.cmbJerOUT.Location = new System.Drawing.Point(11, 36);
            this.cmbJerOUT.Name = "cmbJerOUT";
            this.cmbJerOUT.Size = new System.Drawing.Size(58, 21);
            this.cmbJerOUT.TabIndex = 1;
            this.cmbJerOUT.SelectedIndexChanged += new System.EventHandler(this.cmbJerOUT_SelectedIndexChanged);
            // 
            // cmbJerIN
            // 
            this.cmbJerIN.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbJerIN.FormattingEnabled = true;
            this.cmbJerIN.Location = new System.Drawing.Point(436, 46);
            this.cmbJerIN.Name = "cmbJerIN";
            this.cmbJerIN.Size = new System.Drawing.Size(58, 21);
            this.cmbJerIN.TabIndex = 3;
            this.cmbJerIN.SelectedIndexChanged += new System.EventHandler(this.cmbJerIN_SelectedIndexChanged);
            // 
            // cmbIn
            // 
            this.cmbIn.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbIn.FormattingEnabled = true;
            this.cmbIn.Location = new System.Drawing.Point(518, 46);
            this.cmbIn.Name = "cmbIn";
            this.cmbIn.Size = new System.Drawing.Size(121, 21);
            this.cmbIn.TabIndex = 2;
            this.cmbIn.SelectedIndexChanged += new System.EventHandler(this.cmbIn_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(263, 120);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbOut);
            this.groupBox1.Controls.Add(this.cmbJerOUT);
            this.groupBox1.Location = new System.Drawing.Point(205, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(215, 100);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Outgoing Player";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(426, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(221, 100);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Incoming Player";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Jersey No";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Player Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(96, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Player Name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Jersey No";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbTeam);
            this.groupBox3.Location = new System.Drawing.Point(4, 14);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(169, 100);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Select Team";
            // 
            // cmbTeam
            // 
            this.cmbTeam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.cmbTeam.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTeam.FormattingEnabled = true;
            this.cmbTeam.Location = new System.Drawing.Point(7, 18);
            this.cmbTeam.Name = "cmbTeam";
            this.cmbTeam.Size = new System.Drawing.Size(155, 72);
            this.cmbTeam.TabIndex = 0;
            this.cmbTeam.SelectedIndexChanged += new System.EventHandler(this.cmbTeam_SelectedIndexChanged);
            // 
            // Substitution
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 145);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbJerIN);
            this.Controls.Add(this.cmbIn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Substitution";
            this.Text = "Substitution";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbOut;
        private System.Windows.Forms.ComboBox cmbJerOUT;
        private System.Windows.Forms.ComboBox cmbJerIN;
        private System.Windows.Forms.ComboBox cmbIn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbTeam;
    }
}