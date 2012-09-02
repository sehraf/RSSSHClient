namespace RetroShareSSHClient
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.bt_connect = new System.Windows.Forms.Button();
            this.bt_disconnect = new System.Windows.Forms.Button();
            this.tb_out = new System.Windows.Forms.TextBox();
            this.tb_user = new System.Windows.Forms.TextBox();
            this.tb_pw = new System.Windows.Forms.TextBox();
            this.tb_host = new System.Windows.Forms.TextBox();
            this.tb_port = new System.Windows.Forms.TextBox();
            this.gb_connection = new System.Windows.Forms.GroupBox();
            this.cb_con = new System.Windows.Forms.CheckBox();
            this.l_pw = new System.Windows.Forms.Label();
            this.l_user = new System.Windows.Forms.Label();
            this.l_port = new System.Windows.Forms.Label();
            this.l_ip = new System.Windows.Forms.Label();
            this.bg_systemStatus = new System.Windows.Forms.GroupBox();
            this.l_peers = new System.Windows.Forms.Label();
            this.l_connected = new System.Windows.Forms.Label();
            this.l_network = new System.Windows.Forms.Label();
            this.l_systemstatus = new System.Windows.Forms.Label();
            this.l_bwDown = new System.Windows.Forms.Label();
            this.l_bwUp = new System.Windows.Forms.Label();
            this.t_tick = new System.Windows.Forms.Timer(this.components);
            this.lb_friends = new System.Windows.Forms.ListBox();
            this.gb_friends = new System.Windows.Forms.GroupBox();
            this.bt_peerNew = new System.Windows.Forms.Button();
            this.bt_peerSave = new System.Windows.Forms.Button();
            this.gb_ip = new System.Windows.Forms.GroupBox();
            this.tb_dyndns = new System.Windows.Forms.TextBox();
            this.tb_peerIPInt = new System.Windows.Forms.TextBox();
            this.tb_peerIPExt = new System.Windows.Forms.TextBox();
            this.nud_peerPortExt = new System.Windows.Forms.NumericUpDown();
            this.nud_peerPortInt = new System.Windows.Forms.NumericUpDown();
            this.tb_peerLocationID = new System.Windows.Forms.TextBox();
            this.lb_locations = new System.Windows.Forms.ListBox();
            this.tb_peerLocation = new System.Windows.Forms.TextBox();
            this.tb_peerName = new System.Windows.Forms.TextBox();
            this.tb_peerID = new System.Windows.Forms.TextBox();
            this.l_peerName = new System.Windows.Forms.Label();
            this.l_peerID = new System.Windows.Forms.Label();
            this.l_peerLocation = new System.Windows.Forms.Label();
            this.l_peerLocationID = new System.Windows.Forms.Label();
            this.bt_test = new System.Windows.Forms.Button();
            this.gb_connection.SuspendLayout();
            this.bg_systemStatus.SuspendLayout();
            this.gb_friends.SuspendLayout();
            this.gb_ip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_peerPortExt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_peerPortInt)).BeginInit();
            this.SuspendLayout();
            // 
            // bt_connect
            // 
            this.bt_connect.Location = new System.Drawing.Point(9, 126);
            this.bt_connect.Name = "bt_connect";
            this.bt_connect.Size = new System.Drawing.Size(77, 23);
            this.bt_connect.TabIndex = 0;
            this.bt_connect.Text = "connect";
            this.bt_connect.UseVisualStyleBackColor = true;
            this.bt_connect.Click += new System.EventHandler(this.bt_connect_Click);
            // 
            // bt_disconnect
            // 
            this.bt_disconnect.Location = new System.Drawing.Point(103, 126);
            this.bt_disconnect.Name = "bt_disconnect";
            this.bt_disconnect.Size = new System.Drawing.Size(75, 23);
            this.bt_disconnect.TabIndex = 1;
            this.bt_disconnect.Text = "disconnect";
            this.bt_disconnect.UseVisualStyleBackColor = true;
            this.bt_disconnect.Click += new System.EventHandler(this.bt_disconnect_Click);
            // 
            // tb_out
            // 
            this.tb_out.Location = new System.Drawing.Point(720, 188);
            this.tb_out.Multiline = true;
            this.tb_out.Name = "tb_out";
            this.tb_out.Size = new System.Drawing.Size(155, 57);
            this.tb_out.TabIndex = 2;
            // 
            // tb_user
            // 
            this.tb_user.Location = new System.Drawing.Point(76, 71);
            this.tb_user.Name = "tb_user";
            this.tb_user.Size = new System.Drawing.Size(102, 20);
            this.tb_user.TabIndex = 3;
            // 
            // tb_pw
            // 
            this.tb_pw.Location = new System.Drawing.Point(76, 97);
            this.tb_pw.Name = "tb_pw";
            this.tb_pw.Size = new System.Drawing.Size(102, 20);
            this.tb_pw.TabIndex = 4;
            this.tb_pw.UseSystemPasswordChar = true;
            this.tb_pw.Enter += new System.EventHandler(this.tb_pw_Enter);
            // 
            // tb_host
            // 
            this.tb_host.Location = new System.Drawing.Point(76, 19);
            this.tb_host.Name = "tb_host";
            this.tb_host.Size = new System.Drawing.Size(102, 20);
            this.tb_host.TabIndex = 7;
            // 
            // tb_port
            // 
            this.tb_port.Location = new System.Drawing.Point(76, 45);
            this.tb_port.Name = "tb_port";
            this.tb_port.Size = new System.Drawing.Size(102, 20);
            this.tb_port.TabIndex = 8;
            this.tb_port.Text = "7022";
            // 
            // gb_connection
            // 
            this.gb_connection.Controls.Add(this.cb_con);
            this.gb_connection.Controls.Add(this.l_pw);
            this.gb_connection.Controls.Add(this.l_user);
            this.gb_connection.Controls.Add(this.tb_user);
            this.gb_connection.Controls.Add(this.bt_disconnect);
            this.gb_connection.Controls.Add(this.tb_pw);
            this.gb_connection.Controls.Add(this.bt_connect);
            this.gb_connection.Controls.Add(this.l_port);
            this.gb_connection.Controls.Add(this.l_ip);
            this.gb_connection.Controls.Add(this.tb_port);
            this.gb_connection.Controls.Add(this.tb_host);
            this.gb_connection.Location = new System.Drawing.Point(12, 12);
            this.gb_connection.Name = "gb_connection";
            this.gb_connection.Size = new System.Drawing.Size(184, 179);
            this.gb_connection.TabIndex = 9;
            this.gb_connection.TabStop = false;
            this.gb_connection.Text = "Setup Connection";
            // 
            // cb_con
            // 
            this.cb_con.AutoSize = true;
            this.cb_con.Location = new System.Drawing.Point(9, 158);
            this.cb_con.Name = "cb_con";
            this.cb_con.Size = new System.Drawing.Size(77, 17);
            this.cb_con.TabIndex = 10;
            this.cb_con.Text = "connected";
            this.cb_con.ThreeState = true;
            this.cb_con.UseVisualStyleBackColor = true;
            // 
            // l_pw
            // 
            this.l_pw.AutoSize = true;
            this.l_pw.Location = new System.Drawing.Point(6, 100);
            this.l_pw.Name = "l_pw";
            this.l_pw.Size = new System.Drawing.Size(53, 13);
            this.l_pw.TabIndex = 11;
            this.l_pw.Text = "Password";
            // 
            // l_user
            // 
            this.l_user.AutoSize = true;
            this.l_user.Location = new System.Drawing.Point(6, 74);
            this.l_user.Name = "l_user";
            this.l_user.Size = new System.Drawing.Size(55, 13);
            this.l_user.TabIndex = 10;
            this.l_user.Text = "Username";
            // 
            // l_port
            // 
            this.l_port.AutoSize = true;
            this.l_port.Location = new System.Drawing.Point(6, 48);
            this.l_port.Name = "l_port";
            this.l_port.Size = new System.Drawing.Size(26, 13);
            this.l_port.TabIndex = 9;
            this.l_port.Text = "Port";
            // 
            // l_ip
            // 
            this.l_ip.AutoSize = true;
            this.l_ip.Location = new System.Drawing.Point(6, 22);
            this.l_ip.Name = "l_ip";
            this.l_ip.Size = new System.Drawing.Size(64, 13);
            this.l_ip.TabIndex = 0;
            this.l_ip.Text = "(IP) Address";
            // 
            // bg_systemStatus
            // 
            this.bg_systemStatus.Controls.Add(this.l_peers);
            this.bg_systemStatus.Controls.Add(this.l_connected);
            this.bg_systemStatus.Controls.Add(this.l_network);
            this.bg_systemStatus.Controls.Add(this.l_systemstatus);
            this.bg_systemStatus.Controls.Add(this.l_bwDown);
            this.bg_systemStatus.Controls.Add(this.l_bwUp);
            this.bg_systemStatus.Location = new System.Drawing.Point(12, 197);
            this.bg_systemStatus.Name = "bg_systemStatus";
            this.bg_systemStatus.Size = new System.Drawing.Size(184, 100);
            this.bg_systemStatus.TabIndex = 12;
            this.bg_systemStatus.TabStop = false;
            this.bg_systemStatus.Text = "System Status";
            // 
            // l_peers
            // 
            this.l_peers.AutoSize = true;
            this.l_peers.Location = new System.Drawing.Point(89, 48);
            this.l_peers.Name = "l_peers";
            this.l_peers.Size = new System.Drawing.Size(33, 13);
            this.l_peers.TabIndex = 5;
            this.l_peers.Text = "peers";
            // 
            // l_connected
            // 
            this.l_connected.AutoSize = true;
            this.l_connected.Location = new System.Drawing.Point(6, 48);
            this.l_connected.Name = "l_connected";
            this.l_connected.Size = new System.Drawing.Size(58, 13);
            this.l_connected.TabIndex = 4;
            this.l_connected.Text = "connected";
            // 
            // l_network
            // 
            this.l_network.AutoSize = true;
            this.l_network.Location = new System.Drawing.Point(89, 22);
            this.l_network.Name = "l_network";
            this.l_network.Size = new System.Drawing.Size(45, 13);
            this.l_network.TabIndex = 3;
            this.l_network.Text = "network";
            // 
            // l_systemstatus
            // 
            this.l_systemstatus.AutoSize = true;
            this.l_systemstatus.Location = new System.Drawing.Point(6, 22);
            this.l_systemstatus.Name = "l_systemstatus";
            this.l_systemstatus.Size = new System.Drawing.Size(70, 13);
            this.l_systemstatus.TabIndex = 2;
            this.l_systemstatus.Text = "system status";
            // 
            // l_bwDown
            // 
            this.l_bwDown.AutoSize = true;
            this.l_bwDown.Location = new System.Drawing.Point(89, 76);
            this.l_bwDown.Name = "l_bwDown";
            this.l_bwDown.Size = new System.Drawing.Size(15, 13);
            this.l_bwDown.TabIndex = 1;
            this.l_bwDown.Text = "dl";
            // 
            // l_bwUp
            // 
            this.l_bwUp.AutoSize = true;
            this.l_bwUp.Location = new System.Drawing.Point(6, 76);
            this.l_bwUp.Name = "l_bwUp";
            this.l_bwUp.Size = new System.Drawing.Size(15, 13);
            this.l_bwUp.TabIndex = 0;
            this.l_bwUp.Text = "ul";
            // 
            // t_tick
            // 
            this.t_tick.Interval = 1000;
            this.t_tick.Tick += new System.EventHandler(this.t_tick_Tick);
            // 
            // lb_friends
            // 
            this.lb_friends.FormattingEnabled = true;
            this.lb_friends.Location = new System.Drawing.Point(6, 19);
            this.lb_friends.Name = "lb_friends";
            this.lb_friends.Size = new System.Drawing.Size(158, 303);
            this.lb_friends.TabIndex = 13;
            this.lb_friends.SelectedIndexChanged += new System.EventHandler(this.lb_friends_SelectedIndexChanged);
            // 
            // gb_friends
            // 
            this.gb_friends.Controls.Add(this.bt_peerNew);
            this.gb_friends.Controls.Add(this.bt_peerSave);
            this.gb_friends.Controls.Add(this.gb_ip);
            this.gb_friends.Controls.Add(this.tb_peerLocationID);
            this.gb_friends.Controls.Add(this.lb_locations);
            this.gb_friends.Controls.Add(this.tb_peerLocation);
            this.gb_friends.Controls.Add(this.lb_friends);
            this.gb_friends.Controls.Add(this.tb_peerName);
            this.gb_friends.Controls.Add(this.tb_peerID);
            this.gb_friends.Controls.Add(this.l_peerName);
            this.gb_friends.Controls.Add(this.l_peerID);
            this.gb_friends.Controls.Add(this.l_peerLocation);
            this.gb_friends.Controls.Add(this.l_peerLocationID);
            this.gb_friends.Location = new System.Drawing.Point(202, 12);
            this.gb_friends.Name = "gb_friends";
            this.gb_friends.Size = new System.Drawing.Size(360, 329);
            this.gb_friends.TabIndex = 14;
            this.gb_friends.TabStop = false;
            this.gb_friends.Text = "Friends";
            // 
            // bt_peerNew
            // 
            this.bt_peerNew.Location = new System.Drawing.Point(282, 291);
            this.bt_peerNew.Name = "bt_peerNew";
            this.bt_peerNew.Size = new System.Drawing.Size(72, 23);
            this.bt_peerNew.TabIndex = 25;
            this.bt_peerNew.Text = "New";
            this.bt_peerNew.UseVisualStyleBackColor = true;
            // 
            // bt_peerSave
            // 
            this.bt_peerSave.Location = new System.Drawing.Point(179, 291);
            this.bt_peerSave.Name = "bt_peerSave";
            this.bt_peerSave.Size = new System.Drawing.Size(75, 23);
            this.bt_peerSave.TabIndex = 24;
            this.bt_peerSave.Text = "Save";
            this.bt_peerSave.UseVisualStyleBackColor = true;
            this.bt_peerSave.Click += new System.EventHandler(this.bt_peerSave_Click);
            // 
            // gb_ip
            // 
            this.gb_ip.Controls.Add(this.tb_dyndns);
            this.gb_ip.Controls.Add(this.tb_peerIPInt);
            this.gb_ip.Controls.Add(this.tb_peerIPExt);
            this.gb_ip.Controls.Add(this.nud_peerPortExt);
            this.gb_ip.Controls.Add(this.nud_peerPortInt);
            this.gb_ip.Location = new System.Drawing.Point(173, 185);
            this.gb_ip.Name = "gb_ip";
            this.gb_ip.Size = new System.Drawing.Size(178, 100);
            this.gb_ip.TabIndex = 23;
            this.gb_ip.TabStop = false;
            this.gb_ip.Text = "IPs/Ports";
            // 
            // tb_dyndns
            // 
            this.tb_dyndns.Location = new System.Drawing.Point(6, 71);
            this.tb_dyndns.Name = "tb_dyndns";
            this.tb_dyndns.Size = new System.Drawing.Size(163, 20);
            this.tb_dyndns.TabIndex = 21;
            // 
            // tb_peerIPInt
            // 
            this.tb_peerIPInt.Location = new System.Drawing.Point(6, 19);
            this.tb_peerIPInt.Name = "tb_peerIPInt";
            this.tb_peerIPInt.Size = new System.Drawing.Size(97, 20);
            this.tb_peerIPInt.TabIndex = 18;
            // 
            // tb_peerIPExt
            // 
            this.tb_peerIPExt.Location = new System.Drawing.Point(6, 45);
            this.tb_peerIPExt.Name = "tb_peerIPExt";
            this.tb_peerIPExt.Size = new System.Drawing.Size(97, 20);
            this.tb_peerIPExt.TabIndex = 17;
            // 
            // nud_peerPortExt
            // 
            this.nud_peerPortExt.Location = new System.Drawing.Point(109, 45);
            this.nud_peerPortExt.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nud_peerPortExt.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nud_peerPortExt.Name = "nud_peerPortExt";
            this.nud_peerPortExt.Size = new System.Drawing.Size(60, 20);
            this.nud_peerPortExt.TabIndex = 19;
            this.nud_peerPortExt.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // nud_peerPortInt
            // 
            this.nud_peerPortInt.Location = new System.Drawing.Point(109, 19);
            this.nud_peerPortInt.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nud_peerPortInt.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nud_peerPortInt.Name = "nud_peerPortInt";
            this.nud_peerPortInt.Size = new System.Drawing.Size(60, 20);
            this.nud_peerPortInt.TabIndex = 20;
            this.nud_peerPortInt.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // tb_peerLocationID
            // 
            this.tb_peerLocationID.Location = new System.Drawing.Point(238, 159);
            this.tb_peerLocationID.Name = "tb_peerLocationID";
            this.tb_peerLocationID.Size = new System.Drawing.Size(113, 20);
            this.tb_peerLocationID.TabIndex = 22;
            // 
            // lb_locations
            // 
            this.lb_locations.FormattingEnabled = true;
            this.lb_locations.Location = new System.Drawing.Point(173, 19);
            this.lb_locations.Name = "lb_locations";
            this.lb_locations.Size = new System.Drawing.Size(178, 56);
            this.lb_locations.TabIndex = 14;
            this.lb_locations.SelectedIndexChanged += new System.EventHandler(this.lb_locations_SelectedIndexChanged);
            // 
            // tb_peerLocation
            // 
            this.tb_peerLocation.Location = new System.Drawing.Point(238, 133);
            this.tb_peerLocation.Name = "tb_peerLocation";
            this.tb_peerLocation.Size = new System.Drawing.Size(113, 20);
            this.tb_peerLocation.TabIndex = 21;
            // 
            // tb_peerName
            // 
            this.tb_peerName.Location = new System.Drawing.Point(238, 81);
            this.tb_peerName.Name = "tb_peerName";
            this.tb_peerName.Size = new System.Drawing.Size(113, 20);
            this.tb_peerName.TabIndex = 19;
            // 
            // tb_peerID
            // 
            this.tb_peerID.Location = new System.Drawing.Point(238, 107);
            this.tb_peerID.Name = "tb_peerID";
            this.tb_peerID.Size = new System.Drawing.Size(113, 20);
            this.tb_peerID.TabIndex = 20;
            // 
            // l_peerName
            // 
            this.l_peerName.AutoSize = true;
            this.l_peerName.Location = new System.Drawing.Point(170, 84);
            this.l_peerName.Name = "l_peerName";
            this.l_peerName.Size = new System.Drawing.Size(35, 13);
            this.l_peerName.TabIndex = 15;
            this.l_peerName.Text = "Name";
            // 
            // l_peerID
            // 
            this.l_peerID.AutoSize = true;
            this.l_peerID.Location = new System.Drawing.Point(170, 110);
            this.l_peerID.Name = "l_peerID";
            this.l_peerID.Size = new System.Drawing.Size(18, 13);
            this.l_peerID.TabIndex = 16;
            this.l_peerID.Text = "ID";
            // 
            // l_peerLocation
            // 
            this.l_peerLocation.AutoSize = true;
            this.l_peerLocation.Location = new System.Drawing.Point(170, 136);
            this.l_peerLocation.Name = "l_peerLocation";
            this.l_peerLocation.Size = new System.Drawing.Size(48, 13);
            this.l_peerLocation.TabIndex = 17;
            this.l_peerLocation.Text = "Location";
            // 
            // l_peerLocationID
            // 
            this.l_peerLocationID.AutoSize = true;
            this.l_peerLocationID.Location = new System.Drawing.Point(170, 162);
            this.l_peerLocationID.Name = "l_peerLocationID";
            this.l_peerLocationID.Size = new System.Drawing.Size(62, 13);
            this.l_peerLocationID.TabIndex = 18;
            this.l_peerLocationID.Text = "Location ID";
            // 
            // bt_test
            // 
            this.bt_test.Location = new System.Drawing.Point(720, 145);
            this.bt_test.Name = "bt_test";
            this.bt_test.Size = new System.Drawing.Size(155, 31);
            this.bt_test.TabIndex = 11;
            this.bt_test.Text = "Test";
            this.bt_test.UseVisualStyleBackColor = true;
            this.bt_test.Click += new System.EventHandler(this.bt_test_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 352);
            this.Controls.Add(this.bt_test);
            this.Controls.Add(this.gb_friends);
            this.Controls.Add(this.bg_systemStatus);
            this.Controls.Add(this.gb_connection);
            this.Controls.Add(this.tb_out);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.gb_connection.ResumeLayout(false);
            this.gb_connection.PerformLayout();
            this.bg_systemStatus.ResumeLayout(false);
            this.bg_systemStatus.PerformLayout();
            this.gb_friends.ResumeLayout(false);
            this.gb_friends.PerformLayout();
            this.gb_ip.ResumeLayout(false);
            this.gb_ip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_peerPortExt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_peerPortInt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_connect;
        private System.Windows.Forms.Button bt_disconnect;
        private System.Windows.Forms.TextBox tb_user;
        private System.Windows.Forms.TextBox tb_pw;
        private System.Windows.Forms.TextBox tb_host;
        public System.Windows.Forms.TextBox tb_out;
        private System.Windows.Forms.TextBox tb_port;
        private System.Windows.Forms.GroupBox gb_connection;
        private System.Windows.Forms.CheckBox cb_con;
        private System.Windows.Forms.Label l_pw;
        private System.Windows.Forms.Label l_user;
        private System.Windows.Forms.Label l_port;
        private System.Windows.Forms.Label l_ip;
        private System.Windows.Forms.GroupBox bg_systemStatus;
        private System.Windows.Forms.Timer t_tick;
        private System.Windows.Forms.Label l_bwDown;
        private System.Windows.Forms.Label l_bwUp;
        private System.Windows.Forms.Label l_systemstatus;
        private System.Windows.Forms.Label l_network;
        private System.Windows.Forms.Label l_peers;
        private System.Windows.Forms.Label l_connected;
        private System.Windows.Forms.ListBox lb_friends;
        private System.Windows.Forms.GroupBox gb_friends;
        private System.Windows.Forms.Button bt_peerSave;
        private System.Windows.Forms.GroupBox gb_ip;
        private System.Windows.Forms.TextBox tb_peerIPInt;
        private System.Windows.Forms.TextBox tb_peerIPExt;
        private System.Windows.Forms.NumericUpDown nud_peerPortExt;
        private System.Windows.Forms.NumericUpDown nud_peerPortInt;
        private System.Windows.Forms.TextBox tb_peerLocationID;
        private System.Windows.Forms.ListBox lb_locations;
        private System.Windows.Forms.TextBox tb_peerLocation;
        private System.Windows.Forms.TextBox tb_peerName;
        private System.Windows.Forms.TextBox tb_peerID;
        private System.Windows.Forms.Label l_peerName;
        private System.Windows.Forms.Label l_peerID;
        private System.Windows.Forms.Label l_peerLocation;
        private System.Windows.Forms.Label l_peerLocationID;
        private System.Windows.Forms.TextBox tb_dyndns;
        private System.Windows.Forms.Button bt_peerNew;
        private System.Windows.Forms.Button bt_test;
    }
}

