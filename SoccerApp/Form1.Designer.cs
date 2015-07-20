namespace SoccerApp
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.team1 = new Team.Team();
            this.team2 = new Team.Team();
            this.controller1 = new Controller.Match();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.42234F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.57766F));
            this.tableLayoutPanel1.Controls.Add(this.team1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.team2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 103);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.88925F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1550, 589);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // team1
            // 
            this.team1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.team1.Location = new System.Drawing.Point(4, 4);
            this.team1.Name = "team1";
            this.team1.Size = new System.Drawing.Size(758, 581);
            this.team1.TabIndex = 0;
            // 
            // team2
            // 
            this.team2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.team2.Location = new System.Drawing.Point(769, 4);
            this.team2.Name = "team2";
            this.team2.Size = new System.Drawing.Size(777, 581);
            this.team2.TabIndex = 1;
            // 
            // controller1
            // 
            this.controller1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controller1.Location = new System.Drawing.Point(0, 0);
            this.controller1.Name = "controller1";
            this.controller1.Size = new System.Drawing.Size(1550, 692);
            this.controller1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1550, 692);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.controller1);
            this.Name = "Form1";
            this.Text = "Soccer Form";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controller.Match controller1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Team.Team team1;
        private Team.Team team2;


    }
}

