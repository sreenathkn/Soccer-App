namespace SoccerApp
{
    partial class SoccerApp
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
            this.lblHomeTeam = new System.Windows.Forms.Label();
            this.lblAwayTeam = new System.Windows.Forms.Label();
            this.pnlHomeFlag = new System.Windows.Forms.Panel();
            this.pnlAwayFlag = new System.Windows.Forms.Panel();
            this.btnhomeplus = new System.Windows.Forms.Button();
            this.btnawayplus = new System.Windows.Forms.Button();
            this.btnHomeminus = new System.Windows.Forms.Button();
            this.btnawayminus = new System.Windows.Forms.Button();
            this.lblHomeScore = new System.Windows.Forms.Label();
            this.lblAwayScore = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblCounter = new System.Windows.Forms.Label();
            this.btnstartstop = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.SNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatchPart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EventMem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TeamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlayerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbMatch = new System.Windows.Forms.ComboBox();
            this.cmbMatchPart = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnFoul = new System.Windows.Forms.Button();
            this.cmbCorner = new System.Windows.Forms.Button();
            this.btnShots = new System.Windows.Forms.Button();
            this.btnShotsOff = new System.Windows.Forms.Button();
            this.btnYellow = new System.Windows.Forms.Button();
            this.btnRed = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHomeTeam
            // 
            this.lblHomeTeam.AutoSize = true;
            this.lblHomeTeam.BackColor = System.Drawing.Color.Gray;
            this.lblHomeTeam.Font = new System.Drawing.Font("Segoe UI", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHomeTeam.ForeColor = System.Drawing.Color.DarkGray;
            this.lblHomeTeam.Location = new System.Drawing.Point(75, 111);
            this.lblHomeTeam.Name = "lblHomeTeam";
            this.lblHomeTeam.Size = new System.Drawing.Size(99, 32);
            this.lblHomeTeam.TabIndex = 2;
            this.lblHomeTeam.Text = "lblHom";
            this.lblHomeTeam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHomeTeam.DoubleClick += new System.EventHandler(this.lblHomeTeam_DoubleClick);
            // 
            // lblAwayTeam
            // 
            this.lblAwayTeam.AutoSize = true;
            this.lblAwayTeam.BackColor = System.Drawing.Color.Gray;
            this.lblAwayTeam.Font = new System.Drawing.Font("Segoe UI", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAwayTeam.ForeColor = System.Drawing.Color.DarkGray;
            this.lblAwayTeam.Location = new System.Drawing.Point(671, 110);
            this.lblAwayTeam.Name = "lblAwayTeam";
            this.lblAwayTeam.Size = new System.Drawing.Size(170, 32);
            this.lblAwayTeam.TabIndex = 3;
            this.lblAwayTeam.Text = "lblAwayTeam";
            this.lblAwayTeam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAwayTeam.DoubleClick += new System.EventHandler(this.lblAwayTeam_DoubleClick);
            // 
            // pnlHomeFlag
            // 
            this.pnlHomeFlag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlHomeFlag.Location = new System.Drawing.Point(19, 112);
            this.pnlHomeFlag.Name = "pnlHomeFlag";
            this.pnlHomeFlag.Size = new System.Drawing.Size(51, 28);
            this.pnlHomeFlag.TabIndex = 4;
            // 
            // pnlAwayFlag
            // 
            this.pnlAwayFlag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlAwayFlag.Location = new System.Drawing.Point(821, 111);
            this.pnlAwayFlag.Name = "pnlAwayFlag";
            this.pnlAwayFlag.Size = new System.Drawing.Size(51, 28);
            this.pnlAwayFlag.TabIndex = 5;
            // 
            // btnhomeplus
            // 
            this.btnhomeplus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnhomeplus.Location = new System.Drawing.Point(321, 109);
            this.btnhomeplus.Name = "btnhomeplus";
            this.btnhomeplus.Size = new System.Drawing.Size(27, 30);
            this.btnhomeplus.TabIndex = 6;
            this.btnhomeplus.Text = "+";
            this.btnhomeplus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnhomeplus.UseCompatibleTextRendering = true;
            this.btnhomeplus.UseVisualStyleBackColor = true;
            this.btnhomeplus.Click += new System.EventHandler(this.btnhomeplus_Click);
            // 
            // btnawayplus
            // 
            this.btnawayplus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnawayplus.Location = new System.Drawing.Point(642, 111);
            this.btnawayplus.Name = "btnawayplus";
            this.btnawayplus.Size = new System.Drawing.Size(27, 30);
            this.btnawayplus.TabIndex = 7;
            this.btnawayplus.Text = "+";
            this.btnawayplus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnawayplus.UseCompatibleTextRendering = true;
            this.btnawayplus.UseVisualStyleBackColor = true;
            this.btnawayplus.Click += new System.EventHandler(this.btnawayplus_Click);
            // 
            // btnHomeminus
            // 
            this.btnHomeminus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHomeminus.Location = new System.Drawing.Point(225, 110);
            this.btnHomeminus.Name = "btnHomeminus";
            this.btnHomeminus.Size = new System.Drawing.Size(27, 30);
            this.btnHomeminus.TabIndex = 8;
            this.btnHomeminus.Text = "-";
            this.btnHomeminus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnHomeminus.UseCompatibleTextRendering = true;
            this.btnHomeminus.UseVisualStyleBackColor = true;
            this.btnHomeminus.Click += new System.EventHandler(this.btnHomeminus_Click);
            // 
            // btnawayminus
            // 
            this.btnawayminus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnawayminus.Location = new System.Drawing.Point(546, 109);
            this.btnawayminus.Name = "btnawayminus";
            this.btnawayminus.Size = new System.Drawing.Size(27, 30);
            this.btnawayminus.TabIndex = 9;
            this.btnawayminus.Text = "-";
            this.btnawayminus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnawayminus.UseCompatibleTextRendering = true;
            this.btnawayminus.UseVisualStyleBackColor = true;
            this.btnawayminus.Click += new System.EventHandler(this.btnawayminus_Click);
            // 
            // lblHomeScore
            // 
            this.lblHomeScore.AutoSize = true;
            this.lblHomeScore.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHomeScore.Location = new System.Drawing.Point(260, 104);
            this.lblHomeScore.Name = "lblHomeScore";
            this.lblHomeScore.Size = new System.Drawing.Size(56, 45);
            this.lblHomeScore.TabIndex = 10;
            this.lblHomeScore.Text = "12";
            // 
            // lblAwayScore
            // 
            this.lblAwayScore.AutoSize = true;
            this.lblAwayScore.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAwayScore.Location = new System.Drawing.Point(580, 104);
            this.lblAwayScore.Name = "lblAwayScore";
            this.lblAwayScore.Size = new System.Drawing.Size(56, 45);
            this.lblAwayScore.TabIndex = 11;
            this.lblAwayScore.Text = "12";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblCounter
            // 
            this.lblCounter.AutoSize = true;
            this.lblCounter.Font = new System.Drawing.Font("Segoe UI", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCounter.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblCounter.Location = new System.Drawing.Point(361, 175);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(164, 71);
            this.lblCounter.TabIndex = 12;
            this.lblCounter.Text = "00:00\r\n";
            // 
            // btnstartstop
            // 
            this.btnstartstop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnstartstop.Location = new System.Drawing.Point(349, 261);
            this.btnstartstop.Name = "btnstartstop";
            this.btnstartstop.Size = new System.Drawing.Size(195, 38);
            this.btnstartstop.TabIndex = 13;
            this.btnstartstop.Text = "Start";
            this.btnstartstop.UseVisualStyleBackColor = true;
            this.btnstartstop.Click += new System.EventHandler(this.btnstartstop_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SNO,
            this.Time,
            this.MatchPart,
            this.EventMem,
            this.TeamName,
            this.PlayerName});
            this.dataGridView1.Location = new System.Drawing.Point(55, 305);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(868, 139);
            this.dataGridView1.TabIndex = 14;
            // 
            // SNO
            // 
            this.SNO.HeaderText = "S.No.";
            this.SNO.Name = "SNO";
            this.SNO.ReadOnly = true;
            this.SNO.Width = 50;
            // 
            // Time
            // 
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            // 
            // MatchPart
            // 
            this.MatchPart.HeaderText = "Match Part";
            this.MatchPart.Name = "MatchPart";
            this.MatchPart.ReadOnly = true;
            this.MatchPart.Width = 150;
            // 
            // EventMem
            // 
            this.EventMem.HeaderText = "Event Memo";
            this.EventMem.Name = "EventMem";
            this.EventMem.ReadOnly = true;
            this.EventMem.Width = 150;
            // 
            // TeamName
            // 
            this.TeamName.HeaderText = "Team Name";
            this.TeamName.Name = "TeamName";
            this.TeamName.ReadOnly = true;
            this.TeamName.Width = 200;
            // 
            // PlayerName
            // 
            this.PlayerName.HeaderText = "Player Name";
            this.PlayerName.Name = "PlayerName";
            this.PlayerName.ReadOnly = true;
            this.PlayerName.Width = 215;
            // 
            // cmbMatch
            // 
            this.cmbMatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMatch.FormattingEnabled = true;
            this.cmbMatch.Location = new System.Drawing.Point(19, 52);
            this.cmbMatch.Name = "cmbMatch";
            this.cmbMatch.Size = new System.Drawing.Size(155, 21);
            this.cmbMatch.TabIndex = 15;
            this.cmbMatch.SelectedIndexChanged += new System.EventHandler(this.cmbMatch_SelectedIndexChanged);
            // 
            // cmbMatchPart
            // 
            this.cmbMatchPart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMatchPart.FormattingEnabled = true;
            this.cmbMatchPart.Location = new System.Drawing.Point(225, 52);
            this.cmbMatchPart.Name = "cmbMatchPart";
            this.cmbMatchPart.Size = new System.Drawing.Size(136, 21);
            this.cmbMatchPart.TabIndex = 16;
            this.cmbMatchPart.SelectedIndexChanged += new System.EventHandler(this.cmbMatchPart_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 465);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1155, 484);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(62, 263);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 32);
            this.button1.TabIndex = 18;
            this.button1.Text = "Substitute";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnFoul
            // 
            this.btnFoul.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFoul.Location = new System.Drawing.Point(172, 263);
            this.btnFoul.Name = "btnFoul";
            this.btnFoul.Size = new System.Drawing.Size(80, 32);
            this.btnFoul.TabIndex = 19;
            this.btnFoul.Text = "Foul";
            this.btnFoul.UseVisualStyleBackColor = true;
            this.btnFoul.Click += new System.EventHandler(this.btnFoul_Click);
            // 
            // cmbCorner
            // 
            this.cmbCorner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCorner.Location = new System.Drawing.Point(261, 264);
            this.cmbCorner.Name = "cmbCorner";
            this.cmbCorner.Size = new System.Drawing.Size(80, 32);
            this.cmbCorner.TabIndex = 20;
            this.cmbCorner.Text = "Corner";
            this.cmbCorner.UseVisualStyleBackColor = true;
            this.cmbCorner.Click += new System.EventHandler(this.cmbCorner_Click);
            // 
            // btnShots
            // 
            this.btnShots.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShots.Location = new System.Drawing.Point(556, 264);
            this.btnShots.Name = "btnShots";
            this.btnShots.Size = new System.Drawing.Size(80, 32);
            this.btnShots.TabIndex = 21;
            this.btnShots.Text = "Shots ON";
            this.btnShots.UseVisualStyleBackColor = true;
            this.btnShots.Click += new System.EventHandler(this.btnShots_Click);
            // 
            // btnShotsOff
            // 
            this.btnShotsOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShotsOff.Location = new System.Drawing.Point(651, 264);
            this.btnShotsOff.Name = "btnShotsOff";
            this.btnShotsOff.Size = new System.Drawing.Size(80, 32);
            this.btnShotsOff.TabIndex = 22;
            this.btnShotsOff.Text = "Shots OFF";
            this.btnShotsOff.UseVisualStyleBackColor = true;
            this.btnShotsOff.Click += new System.EventHandler(this.btnShotsOff_Click);
            // 
            // btnYellow
            // 
            this.btnYellow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYellow.Location = new System.Drawing.Point(743, 264);
            this.btnYellow.Name = "btnYellow";
            this.btnYellow.Size = new System.Drawing.Size(80, 32);
            this.btnYellow.TabIndex = 23;
            this.btnYellow.Text = "Yellow Card";
            this.btnYellow.UseVisualStyleBackColor = true;
            this.btnYellow.Click += new System.EventHandler(this.btnYellow_Click);
            // 
            // btnRed
            // 
            this.btnRed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRed.Location = new System.Drawing.Point(829, 264);
            this.btnRed.Name = "btnRed";
            this.btnRed.Size = new System.Drawing.Size(80, 32);
            this.btnRed.TabIndex = 24;
            this.btnRed.Text = "Red Card";
            this.btnRed.UseVisualStyleBackColor = true;
            this.btnRed.Click += new System.EventHandler(this.btnRed_Click);
            // 
            // SoccerApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 949);
            this.Controls.Add(this.btnRed);
            this.Controls.Add(this.btnYellow);
            this.Controls.Add(this.btnShotsOff);
            this.Controls.Add(this.btnShots);
            this.Controls.Add(this.cmbCorner);
            this.Controls.Add(this.btnFoul);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.cmbMatchPart);
            this.Controls.Add(this.cmbMatch);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnstartstop);
            this.Controls.Add(this.lblCounter);
            this.Controls.Add(this.lblAwayScore);
            this.Controls.Add(this.lblHomeScore);
            this.Controls.Add(this.btnawayminus);
            this.Controls.Add(this.btnHomeminus);
            this.Controls.Add(this.btnawayplus);
            this.Controls.Add(this.btnhomeplus);
            this.Controls.Add(this.pnlAwayFlag);
            this.Controls.Add(this.pnlHomeFlag);
            this.Controls.Add(this.lblAwayTeam);
            this.Controls.Add(this.lblHomeTeam);
            this.Name = "SoccerApp";
            this.Text = "Soccer Application";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHomeTeam;
        private System.Windows.Forms.Label lblAwayTeam;
        private System.Windows.Forms.Panel pnlHomeFlag;
        private System.Windows.Forms.Panel pnlAwayFlag;
        private System.Windows.Forms.Button btnhomeplus;
        private System.Windows.Forms.Button btnawayplus;
        private System.Windows.Forms.Button btnHomeminus;
        private System.Windows.Forms.Button btnawayminus;
        private System.Windows.Forms.Label lblHomeScore;
        private System.Windows.Forms.Label lblAwayScore;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.Button btnstartstop;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatchPart;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventMem;
        private System.Windows.Forms.DataGridViewTextBoxColumn TeamName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlayerName;
        private System.Windows.Forms.ComboBox cmbMatch;
        private System.Windows.Forms.ComboBox cmbMatchPart;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnFoul;
        private System.Windows.Forms.Button cmbCorner;
        private System.Windows.Forms.Button btnShots;
        private System.Windows.Forms.Button btnShotsOff;
        private System.Windows.Forms.Button btnYellow;
        private System.Windows.Forms.Button btnRed;

    }
}