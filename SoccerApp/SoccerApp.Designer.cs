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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblHomeTeam2 = new System.Windows.Forms.Label();
            this.lblAwayTeam = new System.Windows.Forms.Label();
            this.pnlHomeFlag = new System.Windows.Forms.Panel();
            this.pnlAwayFlag = new System.Windows.Forms.Panel();
            this.btnawayplus = new System.Windows.Forms.Button();
            this.btnawayminus = new System.Windows.Forms.Button();
            this.lblHomeScore = new System.Windows.Forms.Label();
            this.lblAwayScore = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnstartstop = new System.Windows.Forms.Button();
            this.dgvMatchevents = new System.Windows.Forms.DataGridView();
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
            this.lblCounter = new System.Windows.Forms.TextBox();
            this.pnlMatch = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlAwayTeam = new System.Windows.Forms.Panel();
            this.pnlHmTeam = new System.Windows.Forms.Panel();
            this.btnhomeplus = new System.Windows.Forms.Button();
            this.btnHomeminus = new System.Windows.Forms.Button();
            this.pnlCounter = new System.Windows.Forms.Panel();
            this.pnlteamcntrs = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlcntr = new System.Windows.Forms.Panel();
            this.pnlbuttons = new System.Windows.Forms.Panel();
            this.dgvSelectPlayer = new System.Windows.Forms.DataGridView();
            this.pnlActions = new System.Windows.Forms.Panel();
            this.lblMatchevents = new System.Windows.Forms.Label();
            this.pnlPlay = new System.Windows.Forms.Panel();
            this.pnlLabels = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlPlayers = new System.Windows.Forms.Panel();
            this.pnlPlaySelect = new System.Windows.Forms.Panel();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pnlMActions = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatchevents)).BeginInit();
            this.pnlMatch.SuspendLayout();
            this.pnlAwayTeam.SuspendLayout();
            this.pnlHmTeam.SuspendLayout();
            this.pnlCounter.SuspendLayout();
            this.pnlteamcntrs.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.pnlcntr.SuspendLayout();
            this.pnlbuttons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectPlayer)).BeginInit();
            this.pnlActions.SuspendLayout();
            this.pnlPlay.SuspendLayout();
            this.pnlLabels.SuspendLayout();
            this.pnlPlayers.SuspendLayout();
            this.pnlPlaySelect.SuspendLayout();
            this.pnlMActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHomeTeam2
            // 
            this.lblHomeTeam2.BackColor = System.Drawing.Color.Gray;
            this.lblHomeTeam2.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblHomeTeam2.ForeColor = System.Drawing.Color.Black;
            this.lblHomeTeam2.Location = new System.Drawing.Point(57, 37);
            this.lblHomeTeam2.Name = "lblHomeTeam2";
            this.lblHomeTeam2.Size = new System.Drawing.Size(272, 32);
            this.lblHomeTeam2.TabIndex = 2;
            this.lblHomeTeam2.Text = "lblHome";
            this.lblHomeTeam2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHomeTeam2.DoubleClick += new System.EventHandler(this.lblHomeTeam_DoubleClick);
            // 
            // lblAwayTeam
            // 
            this.lblAwayTeam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAwayTeam.BackColor = System.Drawing.Color.Gray;
            this.lblAwayTeam.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblAwayTeam.ForeColor = System.Drawing.Color.Black;
            this.lblAwayTeam.Location = new System.Drawing.Point(140, 31);
            this.lblAwayTeam.Name = "lblAwayTeam";
            this.lblAwayTeam.Size = new System.Drawing.Size(272, 32);
            this.lblAwayTeam.TabIndex = 3;
            this.lblAwayTeam.Text = "lblAwayTeam";
            this.lblAwayTeam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAwayTeam.DoubleClick += new System.EventHandler(this.lblAwayTeam_DoubleClick);
            // 
            // pnlHomeFlag
            // 
            this.pnlHomeFlag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlHomeFlag.Location = new System.Drawing.Point(3, 38);
            this.pnlHomeFlag.Name = "pnlHomeFlag";
            this.pnlHomeFlag.Size = new System.Drawing.Size(51, 28);
            this.pnlHomeFlag.TabIndex = 4;
            // 
            // pnlAwayFlag
            // 
            this.pnlAwayFlag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlAwayFlag.Location = new System.Drawing.Point(418, 35);
            this.pnlAwayFlag.Name = "pnlAwayFlag";
            this.pnlAwayFlag.Size = new System.Drawing.Size(51, 28);
            this.pnlAwayFlag.TabIndex = 4;
            // 
            // btnawayplus
            // 
            this.btnawayplus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnawayplus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnawayplus.Location = new System.Drawing.Point(107, 31);
            this.btnawayplus.Name = "btnawayplus";
            this.btnawayplus.Size = new System.Drawing.Size(27, 30);
            this.btnawayplus.TabIndex = 7;
            this.btnawayplus.Text = "+";
            this.btnawayplus.UseCompatibleTextRendering = true;
            this.btnawayplus.UseVisualStyleBackColor = true;
            this.btnawayplus.Click += new System.EventHandler(this.btnawayplus_Click);
            // 
            // btnawayminus
            // 
            this.btnawayminus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnawayminus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnawayminus.Location = new System.Drawing.Point(3, 31);
            this.btnawayminus.Name = "btnawayminus";
            this.btnawayminus.Size = new System.Drawing.Size(27, 30);
            this.btnawayminus.TabIndex = 9;
            this.btnawayminus.Text = "-";
            this.btnawayminus.UseCompatibleTextRendering = true;
            this.btnawayminus.UseVisualStyleBackColor = true;
            this.btnawayminus.Click += new System.EventHandler(this.btnawayminus_Click);
            // 
            // lblHomeScore
            // 
            this.lblHomeScore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHomeScore.BackColor = System.Drawing.Color.Transparent;
            this.lblHomeScore.Font = new System.Drawing.Font("Segoe UI", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblHomeScore.Location = new System.Drawing.Point(368, 18);
            this.lblHomeScore.Name = "lblHomeScore";
            this.lblHomeScore.Size = new System.Drawing.Size(75, 62);
            this.lblHomeScore.TabIndex = 10;
            this.lblHomeScore.Text = "12";
            this.lblHomeScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAwayScore
            // 
            this.lblAwayScore.BackColor = System.Drawing.Color.Transparent;
            this.lblAwayScore.Font = new System.Drawing.Font("Segoe UI", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblAwayScore.Location = new System.Drawing.Point(20, 19);
            this.lblAwayScore.Name = "lblAwayScore";
            this.lblAwayScore.Size = new System.Drawing.Size(96, 62);
            this.lblAwayScore.TabIndex = 11;
            this.lblAwayScore.Text = "12";
            this.lblAwayScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnstartstop
            // 
            this.btnstartstop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnstartstop.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnstartstop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnstartstop.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnstartstop.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnstartstop.Location = new System.Drawing.Point(135, 78);
            this.btnstartstop.Name = "btnstartstop";
            this.btnstartstop.Size = new System.Drawing.Size(211, 29);
            this.btnstartstop.TabIndex = 13;
            this.btnstartstop.Text = "START/STOP";
            this.btnstartstop.UseVisualStyleBackColor = false;
            this.btnstartstop.Click += new System.EventHandler(this.btnstartstop_Click);
            // 
            // dgvMatchevents
            // 
            this.dgvMatchevents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvMatchevents.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.dgvMatchevents.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMatchevents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvMatchevents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMatchevents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SNO,
            this.Time,
            this.MatchPart,
            this.EventMem,
            this.TeamName,
            this.PlayerName});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMatchevents.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvMatchevents.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvMatchevents.Location = new System.Drawing.Point(0, 34);
            this.dgvMatchevents.Name = "dgvMatchevents";
            this.dgvMatchevents.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvMatchevents.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvMatchevents.RowHeadersVisible = false;
            this.dgvMatchevents.Size = new System.Drawing.Size(1486, 259);
            this.dgvMatchevents.TabIndex = 14;
            // 
            // SNO
            // 
            this.SNO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SNO.HeaderText = "S.No.";
            this.SNO.Name = "SNO";
            this.SNO.ReadOnly = true;
            // 
            // Time
            // 
            this.Time.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            // 
            // MatchPart
            // 
            this.MatchPart.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MatchPart.HeaderText = "Match Part";
            this.MatchPart.Name = "MatchPart";
            this.MatchPart.ReadOnly = true;
            // 
            // EventMem
            // 
            this.EventMem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EventMem.HeaderText = "Event Memo";
            this.EventMem.Name = "EventMem";
            this.EventMem.ReadOnly = true;
            // 
            // TeamName
            // 
            this.TeamName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TeamName.HeaderText = "Team Name";
            this.TeamName.Name = "TeamName";
            this.TeamName.ReadOnly = true;
            // 
            // PlayerName
            // 
            this.PlayerName.HeaderText = "Player Name";
            this.PlayerName.Name = "PlayerName";
            this.PlayerName.ReadOnly = true;
            this.PlayerName.Width = 92;
            // 
            // cmbMatch
            // 
            this.cmbMatch.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cmbMatch.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbMatch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMatch.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.cmbMatch.ForeColor = System.Drawing.Color.Black;
            this.cmbMatch.FormattingEnabled = true;
            this.cmbMatch.Location = new System.Drawing.Point(5, 5);
            this.cmbMatch.Name = "cmbMatch";
            this.cmbMatch.Size = new System.Drawing.Size(204, 27);
            this.cmbMatch.TabIndex = 15;
            this.cmbMatch.Text = "Select Match";
            this.cmbMatch.SelectedIndexChanged += new System.EventHandler(this.cmbMatch_SelectedIndexChanged);
            // 
            // cmbMatchPart
            // 
            this.cmbMatchPart.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cmbMatchPart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMatchPart.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.cmbMatchPart.ForeColor = System.Drawing.Color.Black;
            this.cmbMatchPart.FormattingEnabled = true;
            this.cmbMatchPart.Location = new System.Drawing.Point(215, 5);
            this.cmbMatchPart.Name = "cmbMatchPart";
            this.cmbMatchPart.Size = new System.Drawing.Size(212, 27);
            this.cmbMatchPart.TabIndex = 16;
            this.cmbMatchPart.Text = "Select Part";
            this.cmbMatchPart.SelectedIndexChanged += new System.EventHandler(this.cmbMatchPart_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Gainsboro;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1306, 456);
            this.tableLayoutPanel1.TabIndex = 17;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(1127, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 33);
            this.button1.TabIndex = 18;
            this.button1.Text = "Substitute";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnFoul
            // 
            this.btnFoul.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnFoul.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFoul.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFoul.Location = new System.Drawing.Point(515, 0);
            this.btnFoul.Name = "btnFoul";
            this.btnFoul.Size = new System.Drawing.Size(96, 35);
            this.btnFoul.TabIndex = 19;
            this.btnFoul.Text = "Foul";
            this.btnFoul.UseVisualStyleBackColor = false;
            this.btnFoul.Click += new System.EventHandler(this.btnFoul_Click);
            // 
            // cmbCorner
            // 
            this.cmbCorner.BackColor = System.Drawing.SystemColors.ControlLight;
            this.cmbCorner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCorner.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCorner.Location = new System.Drawing.Point(821, 0);
            this.cmbCorner.Name = "cmbCorner";
            this.cmbCorner.Size = new System.Drawing.Size(96, 33);
            this.cmbCorner.TabIndex = 20;
            this.cmbCorner.Text = "Corner";
            this.cmbCorner.UseVisualStyleBackColor = false;
            this.cmbCorner.Click += new System.EventHandler(this.cmbCorner_Click);
            // 
            // btnShots
            // 
            this.btnShots.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnShots.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShots.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShots.Location = new System.Drawing.Point(923, 0);
            this.btnShots.Name = "btnShots";
            this.btnShots.Size = new System.Drawing.Size(96, 33);
            this.btnShots.TabIndex = 21;
            this.btnShots.Text = "Shots ON";
            this.btnShots.UseVisualStyleBackColor = false;
            this.btnShots.Click += new System.EventHandler(this.btnShots_Click);
            // 
            // btnShotsOff
            // 
            this.btnShotsOff.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnShotsOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShotsOff.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShotsOff.Location = new System.Drawing.Point(1025, 0);
            this.btnShotsOff.Name = "btnShotsOff";
            this.btnShotsOff.Size = new System.Drawing.Size(96, 33);
            this.btnShotsOff.TabIndex = 22;
            this.btnShotsOff.Text = "Shots OFF";
            this.btnShotsOff.UseVisualStyleBackColor = false;
            this.btnShotsOff.Click += new System.EventHandler(this.btnShotsOff_Click);
            // 
            // btnYellow
            // 
            this.btnYellow.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnYellow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYellow.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYellow.Location = new System.Drawing.Point(617, 1);
            this.btnYellow.Name = "btnYellow";
            this.btnYellow.Size = new System.Drawing.Size(96, 33);
            this.btnYellow.TabIndex = 23;
            this.btnYellow.Text = "Yellow";
            this.btnYellow.UseVisualStyleBackColor = false;
            this.btnYellow.Click += new System.EventHandler(this.btnYellow_Click);
            // 
            // btnRed
            // 
            this.btnRed.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnRed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRed.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRed.Location = new System.Drawing.Point(719, 1);
            this.btnRed.Name = "btnRed";
            this.btnRed.Size = new System.Drawing.Size(96, 33);
            this.btnRed.TabIndex = 24;
            this.btnRed.Text = "Red";
            this.btnRed.UseVisualStyleBackColor = false;
            this.btnRed.Click += new System.EventHandler(this.btnRed_Click);
            // 
            // lblCounter
            // 
            this.lblCounter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCounter.BackColor = System.Drawing.Color.White;
            this.lblCounter.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblCounter.Font = new System.Drawing.Font("Tahoma", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblCounter.ForeColor = System.Drawing.Color.Transparent;
            this.lblCounter.Location = new System.Drawing.Point(119, 23);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(238, 58);
            this.lblCounter.TabIndex = 25;
            this.lblCounter.Text = "00:00";
            this.lblCounter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pnlMatch
            // 
            this.pnlMatch.Controls.Add(this.label4);
            this.pnlMatch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMatch.Location = new System.Drawing.Point(10, 10);
            this.pnlMatch.Name = "pnlMatch";
            this.pnlMatch.Size = new System.Drawing.Size(1486, 30);
            this.pnlMatch.TabIndex = 27;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(736, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 24);
            this.label4.TabIndex = 0;
            this.label4.Text = "NAME";
            // 
            // pnlAwayTeam
            // 
            this.pnlAwayTeam.BackColor = System.Drawing.Color.Transparent;
            this.pnlAwayTeam.Controls.Add(this.btnawayminus);
            this.pnlAwayTeam.Controls.Add(this.btnawayplus);
            this.pnlAwayTeam.Controls.Add(this.lblAwayScore);
            this.pnlAwayTeam.Controls.Add(this.lblAwayTeam);
            this.pnlAwayTeam.Controls.Add(this.pnlAwayFlag);
            this.pnlAwayTeam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAwayTeam.Location = new System.Drawing.Point(987, 3);
            this.pnlAwayTeam.Name = "pnlAwayTeam";
            this.pnlAwayTeam.Padding = new System.Windows.Forms.Padding(5);
            this.pnlAwayTeam.Size = new System.Drawing.Size(486, 130);
            this.pnlAwayTeam.TabIndex = 29;
            // 
            // pnlHmTeam
            // 
            this.pnlHmTeam.BackColor = System.Drawing.Color.Transparent;
            this.pnlHmTeam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHmTeam.Controls.Add(this.btnhomeplus);
            this.pnlHmTeam.Controls.Add(this.lblHomeTeam2);
            this.pnlHmTeam.Controls.Add(this.pnlHomeFlag);
            this.pnlHmTeam.Controls.Add(this.lblHomeScore);
            this.pnlHmTeam.Controls.Add(this.btnHomeminus);
            this.pnlHmTeam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHmTeam.Location = new System.Drawing.Point(3, 3);
            this.pnlHmTeam.Name = "pnlHmTeam";
            this.pnlHmTeam.Padding = new System.Windows.Forms.Padding(5);
            this.pnlHmTeam.Size = new System.Drawing.Size(486, 130);
            this.pnlHmTeam.TabIndex = 28;
            // 
            // btnhomeplus
            // 
            this.btnhomeplus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnhomeplus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnhomeplus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnhomeplus.Location = new System.Drawing.Point(436, 38);
            this.btnhomeplus.Name = "btnhomeplus";
            this.btnhomeplus.Size = new System.Drawing.Size(27, 30);
            this.btnhomeplus.TabIndex = 6;
            this.btnhomeplus.Text = "+";
            this.btnhomeplus.UseCompatibleTextRendering = true;
            this.btnhomeplus.UseVisualStyleBackColor = true;
            this.btnhomeplus.Click += new System.EventHandler(this.btnhomeplus_Click);
            // 
            // btnHomeminus
            // 
            this.btnHomeminus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHomeminus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHomeminus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHomeminus.Location = new System.Drawing.Point(335, 38);
            this.btnHomeminus.Name = "btnHomeminus";
            this.btnHomeminus.Size = new System.Drawing.Size(27, 30);
            this.btnHomeminus.TabIndex = 8;
            this.btnHomeminus.Text = "-";
            this.btnHomeminus.UseCompatibleTextRendering = true;
            this.btnHomeminus.UseVisualStyleBackColor = true;
            this.btnHomeminus.Click += new System.EventHandler(this.btnHomeminus_Click);
            // 
            // pnlCounter
            // 
            this.pnlCounter.Controls.Add(this.pnlteamcntrs);
            this.pnlCounter.Controls.Add(this.pnlbuttons);
            this.pnlCounter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCounter.Location = new System.Drawing.Point(10, 40);
            this.pnlCounter.Name = "pnlCounter";
            this.pnlCounter.Size = new System.Drawing.Size(1486, 205);
            this.pnlCounter.TabIndex = 28;
            // 
            // pnlteamcntrs
            // 
            this.pnlteamcntrs.Controls.Add(this.tableLayoutPanel2);
            this.pnlteamcntrs.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlteamcntrs.Location = new System.Drawing.Point(0, 0);
            this.pnlteamcntrs.Name = "pnlteamcntrs";
            this.pnlteamcntrs.Padding = new System.Windows.Forms.Padding(5);
            this.pnlteamcntrs.Size = new System.Drawing.Size(1486, 146);
            this.pnlteamcntrs.TabIndex = 15;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33407F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33408F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33186F));
            this.tableLayoutPanel2.Controls.Add(this.pnlAwayTeam, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.pnlHmTeam, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.pnlcntr, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 136F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1476, 136);
            this.tableLayoutPanel2.TabIndex = 16;
            // 
            // pnlcntr
            // 
            this.pnlcntr.Controls.Add(this.btnstartstop);
            this.pnlcntr.Controls.Add(this.lblCounter);
            this.pnlcntr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlcntr.Location = new System.Drawing.Point(495, 3);
            this.pnlcntr.Name = "pnlcntr";
            this.pnlcntr.Padding = new System.Windows.Forms.Padding(5);
            this.pnlcntr.Size = new System.Drawing.Size(486, 130);
            this.pnlcntr.TabIndex = 31;
            // 
            // pnlbuttons
            // 
            this.pnlbuttons.Controls.Add(this.cmbMatchPart);
            this.pnlbuttons.Controls.Add(this.btnYellow);
            this.pnlbuttons.Controls.Add(this.cmbMatch);
            this.pnlbuttons.Controls.Add(this.btnRed);
            this.pnlbuttons.Controls.Add(this.cmbCorner);
            this.pnlbuttons.Controls.Add(this.button1);
            this.pnlbuttons.Controls.Add(this.btnShots);
            this.pnlbuttons.Controls.Add(this.btnFoul);
            this.pnlbuttons.Controls.Add(this.btnShotsOff);
            this.pnlbuttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlbuttons.Location = new System.Drawing.Point(0, 150);
            this.pnlbuttons.Name = "pnlbuttons";
            this.pnlbuttons.Padding = new System.Windows.Forms.Padding(5);
            this.pnlbuttons.Size = new System.Drawing.Size(1486, 55);
            this.pnlbuttons.TabIndex = 32;
            // 
            // dgvSelectPlayer
            // 
            this.dgvSelectPlayer.AllowUserToAddRows = false;
            this.dgvSelectPlayer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgvSelectPlayer.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgvSelectPlayer.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvSelectPlayer.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvSelectPlayer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelectPlayer.ColumnHeadersVisible = false;
            this.dgvSelectPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSelectPlayer.Location = new System.Drawing.Point(4, 4);
            this.dgvSelectPlayer.MinimumSize = new System.Drawing.Size(0, 434);
            this.dgvSelectPlayer.Name = "dgvSelectPlayer";
            this.dgvSelectPlayer.RowHeadersVisible = false;
            this.dgvSelectPlayer.Size = new System.Drawing.Size(190, 452);
            this.dgvSelectPlayer.TabIndex = 26;
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.lblMatchevents);
            this.pnlActions.Controls.Add(this.dgvMatchevents);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlActions.Location = new System.Drawing.Point(10, 245);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(1486, 293);
            this.pnlActions.TabIndex = 29;
            // 
            // lblMatchevents
            // 
            this.lblMatchevents.AutoSize = true;
            this.lblMatchevents.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.lblMatchevents.Location = new System.Drawing.Point(2, 3);
            this.lblMatchevents.Name = "lblMatchevents";
            this.lblMatchevents.Size = new System.Drawing.Size(101, 19);
            this.lblMatchevents.TabIndex = 15;
            this.lblMatchevents.Text = "Match Events";
            // 
            // pnlPlay
            // 
            this.pnlPlay.BackColor = System.Drawing.Color.White;
            this.pnlPlay.Controls.Add(this.pnlLabels);
            this.pnlPlay.Controls.Add(this.pnlPlayers);
            this.pnlPlay.Controls.Add(this.pnlPlaySelect);
            this.pnlPlay.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPlay.Location = new System.Drawing.Point(0, 560);
            this.pnlPlay.Name = "pnlPlay";
            this.pnlPlay.Padding = new System.Windows.Forms.Padding(10);
            this.pnlPlay.Size = new System.Drawing.Size(1506, 494);
            this.pnlPlay.TabIndex = 32;
            // 
            // pnlLabels
            // 
            this.pnlLabels.BackColor = System.Drawing.Color.Transparent;
            this.pnlLabels.Controls.Add(this.label3);
            this.pnlLabels.Controls.Add(this.label2);
            this.pnlLabels.Controls.Add(this.label1);
            this.pnlLabels.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLabels.Location = new System.Drawing.Point(10, 10);
            this.pnlLabels.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLabels.Name = "pnlLabels";
            this.pnlLabels.Padding = new System.Windows.Forms.Padding(5);
            this.pnlLabels.Size = new System.Drawing.Size(1486, 31);
            this.pnlLabels.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(159, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 29;
            this.label3.Text = "P2";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(133, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 28;
            this.label2.Text = "P1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(2, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 21);
            this.label1.TabIndex = 27;
            this.label1.Text = "Select Graphic";
            // 
            // pnlPlayers
            // 
            this.pnlPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPlayers.BackColor = System.Drawing.Color.Transparent;
            this.pnlPlayers.Controls.Add(this.tableLayoutPanel1);
            this.pnlPlayers.Location = new System.Drawing.Point(200, 44);
            this.pnlPlayers.Name = "pnlPlayers";
            this.pnlPlayers.Size = new System.Drawing.Size(1306, 456);
            this.pnlPlayers.TabIndex = 28;
            // 
            // pnlPlaySelect
            // 
            this.pnlPlaySelect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPlaySelect.Controls.Add(this.dgvSelectPlayer);
            this.pnlPlaySelect.Location = new System.Drawing.Point(0, 40);
            this.pnlPlaySelect.Name = "pnlPlaySelect";
            this.pnlPlaySelect.Padding = new System.Windows.Forms.Padding(4);
            this.pnlPlaySelect.Size = new System.Drawing.Size(198, 460);
            this.pnlPlaySelect.TabIndex = 29;
            // 
            // pnlMActions
            // 
            this.pnlMActions.BackColor = System.Drawing.Color.White;
            this.pnlMActions.Controls.Add(this.pnlActions);
            this.pnlMActions.Controls.Add(this.pnlCounter);
            this.pnlMActions.Controls.Add(this.pnlMatch);
            this.pnlMActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMActions.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlMActions.Location = new System.Drawing.Point(0, 0);
            this.pnlMActions.Name = "pnlMActions";
            this.pnlMActions.Padding = new System.Windows.Forms.Padding(10);
            this.pnlMActions.Size = new System.Drawing.Size(1506, 560);
            this.pnlMActions.TabIndex = 15;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.White;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 555);
            this.splitter1.Margin = new System.Windows.Forms.Padding(2);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1506, 5);
            this.splitter1.TabIndex = 30;
            this.splitter1.TabStop = false;
            // 
            // SoccerApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1506, 1054);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.pnlMActions);
            this.Controls.Add(this.pnlPlay);
            this.DoubleBuffered = true;
            this.Name = "SoccerApp";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Soccer Application";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMatchevents)).EndInit();
            this.pnlMatch.ResumeLayout(false);
            this.pnlMatch.PerformLayout();
            this.pnlAwayTeam.ResumeLayout(false);
            this.pnlHmTeam.ResumeLayout(false);
            this.pnlCounter.ResumeLayout(false);
            this.pnlteamcntrs.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.pnlcntr.ResumeLayout(false);
            this.pnlcntr.PerformLayout();
            this.pnlbuttons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelectPlayer)).EndInit();
            this.pnlActions.ResumeLayout(false);
            this.pnlActions.PerformLayout();
            this.pnlPlay.ResumeLayout(false);
            this.pnlLabels.ResumeLayout(false);
            this.pnlLabels.PerformLayout();
            this.pnlPlayers.ResumeLayout(false);
            this.pnlPlaySelect.ResumeLayout(false);
            this.pnlMActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHomeTeam2;
        private System.Windows.Forms.Label lblAwayTeam;
        private System.Windows.Forms.Panel pnlHomeFlag;
        private System.Windows.Forms.Panel pnlAwayFlag;
        private System.Windows.Forms.Button btnawayplus;
        private System.Windows.Forms.Button btnawayminus;
        private System.Windows.Forms.Label lblHomeScore;
        private System.Windows.Forms.Label lblAwayScore;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnstartstop;
        private System.Windows.Forms.DataGridView dgvMatchevents;
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
        private System.Windows.Forms.TextBox lblCounter;
        private System.Windows.Forms.Panel pnlMatch;
        private System.Windows.Forms.Panel pnlAwayTeam;
        private System.Windows.Forms.Panel pnlHmTeam;
        private System.Windows.Forms.Panel pnlCounter;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnhomeplus;
        private System.Windows.Forms.Button btnHomeminus;
        private System.Windows.Forms.Panel pnlPlay;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView dgvSelectPlayer;
        private System.Windows.Forms.DataGridViewTextBoxColumn SNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatchPart;
        private System.Windows.Forms.DataGridViewTextBoxColumn EventMem;
        private System.Windows.Forms.DataGridViewTextBoxColumn TeamName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlayerName;
        private System.Windows.Forms.Panel pnlcntr;
        private System.Windows.Forms.Panel pnlbuttons;
        private System.Windows.Forms.Panel pnlPlayers;
        private System.Windows.Forms.Panel pnlPlaySelect;
        private System.Windows.Forms.Panel pnlMActions;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel pnlteamcntrs;
        private System.Windows.Forms.Label lblMatchevents;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel pnlLabels;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;

    }
}