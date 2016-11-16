namespace SoccerApp
{
    partial class ExtraTimeEditor
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
            this.ExtraTime = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ExtraTime)).BeginInit();
            this.SuspendLayout();
            // 
            // ExtraTime
            // 
            this.ExtraTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ExtraTime.Location = new System.Drawing.Point(6, 9);
            this.ExtraTime.Name = "ExtraTime";
            this.ExtraTime.Size = new System.Drawing.Size(164, 20);
            this.ExtraTime.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(51, 34);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ExtraTimeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(173, 58);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.ExtraTime);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExtraTimeEditor";
            this.Text = "ExtraTimeEditor";
            ((System.ComponentModel.ISupportInitialize)(this.ExtraTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown ExtraTime;
        private System.Windows.Forms.Button btnSave;
    }
}