namespace SoccerApp
{
    partial class MatchGraphicPlayerSelector
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
            this.chkP1 = new System.Windows.Forms.CheckBox();
            this.chkP2 = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkP1
            // 
            this.chkP1.AutoSize = true;
            this.chkP1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkP1.Location = new System.Drawing.Point(37, 29);
            this.chkP1.Name = "chkP1";
            this.chkP1.Size = new System.Drawing.Size(36, 17);
            this.chkP1.TabIndex = 0;
            this.chkP1.Text = "P1";
            this.chkP1.UseVisualStyleBackColor = true;
            // 
            // chkP2
            // 
            this.chkP2.AutoSize = true;
            this.chkP2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkP2.Location = new System.Drawing.Point(207, 29);
            this.chkP2.Name = "chkP2";
            this.chkP2.Size = new System.Drawing.Size(36, 17);
            this.chkP2.TabIndex = 1;
            this.chkP2.Text = "P2";
            this.chkP2.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(99, 74);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // MatchGraphicPlayerSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 123);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkP2);
            this.Controls.Add(this.chkP1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MatchGraphicPlayerSelector";
            this.Text = "MatchGraphicPlayerSelector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkP1;
        private System.Windows.Forms.CheckBox chkP2;
        private System.Windows.Forms.Button btnSave;
    }
}