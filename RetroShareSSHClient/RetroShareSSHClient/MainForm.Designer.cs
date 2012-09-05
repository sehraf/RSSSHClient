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
            this.tc_main = new System.Windows.Forms.TabControl();
            this.tp_friends = new System.Windows.Forms.TabPage();
            this.gb_peerNew = new System.Windows.Forms.GroupBox();
            this.tb_peedNew = new System.Windows.Forms.TextBox();
            this.gb_peerInfo = new System.Windows.Forms.GroupBox();
            this.bt_peerRemove = new System.Windows.Forms.Button();
            this.tp_chat = new System.Windows.Forms.TabPage();
            this.gb_autoResp = new System.Windows.Forms.GroupBox();
            this.tb_chatAutoRespAnswer = new System.Windows.Forms.TextBox();
            this.l_chatAutoRespSearch = new System.Windows.Forms.Label();
            this.tb_chatAutoRespSearch = new System.Windows.Forms.TextBox();
            this.cb_chatAutoRespEnable = new System.Windows.Forms.CheckBox();
            this.l_chatAutoRespAnswer = new System.Windows.Forms.Label();
            this.clb_chatUser = new System.Windows.Forms.CheckedListBox();
            this.gb_nickname = new System.Windows.Forms.GroupBox();
            this.tb_chatNickname = new System.Windows.Forms.TextBox();
            this.bt_setNickname = new System.Windows.Forms.Button();
            this.rtb_chat = new System.Windows.Forms.RichTextBox();
            this.clb_chatLobbies = new System.Windows.Forms.CheckedListBox();
            this.bt_chatSend = new System.Windows.Forms.Button();
            this.tb_chatMsg = new System.Windows.Forms.TextBox();
            this.tp_files = new System.Windows.Forms.TabPage();
            this.tb_optios = new System.Windows.Forms.TabControl();
            this.tb_connection = new System.Windows.Forms.TabPage();
            this.tb_options = new System.Windows.Forms.TabPage();
            this.cb_settingsSave = new System.Windows.Forms.CheckBox();
            this.cb_settingsSavePW = new System.Windows.Forms.CheckBox();
            this.bg_systemStatus.SuspendLayout();
            this.gb_ip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_peerPortExt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_peerPortInt)).BeginInit();
            this.tc_main.SuspendLayout();
            this.tp_friends.SuspendLayout();
            this.gb_peerNew.SuspendLayout();
            this.gb_peerInfo.SuspendLayout();
            this.tp_chat.SuspendLayout();
            this.gb_autoResp.SuspendLayout();
            this.gb_nickname.SuspendLayout();
            this.tb_optios.SuspendLayout();
            this.tb_connection.SuspendLayout();
            this.tb_options.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_connect
            // 
            this.bt_connect.Location = new System.Drawing.Point(9, 113);
            this.bt_connect.Name = "bt_connect";
            this.bt_connect.Size = new System.Drawing.Size(77, 23);
            this.bt_connect.TabIndex = 5;
            this.bt_connect.Text = "connect";
            this.bt_connect.UseVisualStyleBackColor = true;
            this.bt_connect.Click += new System.EventHandler(this.bt_connect_Click);
            // 
            // bt_disconnect
            // 
            this.bt_disconnect.Location = new System.Drawing.Point(103, 113);
            this.bt_disconnect.Name = "bt_disconnect";
            this.bt_disconnect.Size = new System.Drawing.Size(75, 23);
            this.bt_disconnect.TabIndex = 1;
            this.bt_disconnect.Text = "disconnect";
            this.bt_disconnect.UseVisualStyleBackColor = true;
            this.bt_disconnect.Click += new System.EventHandler(this.bt_disconnect_Click);
            // 
            // tb_out
            // 
            this.tb_out.Location = new System.Drawing.Point(12, 355);
            this.tb_out.Multiline = true;
            this.tb_out.Name = "tb_out";
            this.tb_out.Size = new System.Drawing.Size(155, 195);
            this.tb_out.TabIndex = 2;
            // 
            // tb_user
            // 
            this.tb_user.Location = new System.Drawing.Point(76, 58);
            this.tb_user.Name = "tb_user";
            this.tb_user.Size = new System.Drawing.Size(102, 20);
            this.tb_user.TabIndex = 3;
            // 
            // tb_pw
            // 
            this.tb_pw.Location = new System.Drawing.Point(76, 84);
            this.tb_pw.Name = "tb_pw";
            this.tb_pw.Size = new System.Drawing.Size(102, 20);
            this.tb_pw.TabIndex = 4;
            this.tb_pw.UseSystemPasswordChar = true;
            this.tb_pw.Enter += new System.EventHandler(this.tb_pw_Enter);
            // 
            // tb_host
            // 
            this.tb_host.Location = new System.Drawing.Point(76, 6);
            this.tb_host.Name = "tb_host";
            this.tb_host.Size = new System.Drawing.Size(102, 20);
            this.tb_host.TabIndex = 1;
            // 
            // tb_port
            // 
            this.tb_port.Location = new System.Drawing.Point(76, 32);
            this.tb_port.Name = "tb_port";
            this.tb_port.Size = new System.Drawing.Size(102, 20);
            this.tb_port.TabIndex = 2;
            // 
            // cb_con
            // 
            this.cb_con.AutoSize = true;
            this.cb_con.Location = new System.Drawing.Point(9, 145);
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
            this.l_pw.Location = new System.Drawing.Point(6, 87);
            this.l_pw.Name = "l_pw";
            this.l_pw.Size = new System.Drawing.Size(53, 13);
            this.l_pw.TabIndex = 11;
            this.l_pw.Text = "Password";
            // 
            // l_user
            // 
            this.l_user.AutoSize = true;
            this.l_user.Location = new System.Drawing.Point(6, 61);
            this.l_user.Name = "l_user";
            this.l_user.Size = new System.Drawing.Size(55, 13);
            this.l_user.TabIndex = 10;
            this.l_user.Text = "Username";
            // 
            // l_port
            // 
            this.l_port.AutoSize = true;
            this.l_port.Location = new System.Drawing.Point(6, 35);
            this.l_port.Name = "l_port";
            this.l_port.Size = new System.Drawing.Size(26, 13);
            this.l_port.TabIndex = 9;
            this.l_port.Text = "Port";
            // 
            // l_ip
            // 
            this.l_ip.AutoSize = true;
            this.l_ip.Location = new System.Drawing.Point(6, 9);
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
            this.bg_systemStatus.Location = new System.Drawing.Point(12, 212);
            this.bg_systemStatus.Name = "bg_systemStatus";
            this.bg_systemStatus.Size = new System.Drawing.Size(194, 100);
            this.bg_systemStatus.TabIndex = 12;
            this.bg_systemStatus.TabStop = false;
            this.bg_systemStatus.Text = "System Status";
            // 
            // l_peers
            // 
            this.l_peers.AutoSize = true;
            this.l_peers.Location = new System.Drawing.Point(104, 48);
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
            this.l_network.Location = new System.Drawing.Point(104, 22);
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
            this.l_bwDown.Location = new System.Drawing.Point(104, 76);
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
            this.lb_friends.Location = new System.Drawing.Point(6, 7);
            this.lb_friends.Name = "lb_friends";
            this.lb_friends.Size = new System.Drawing.Size(195, 498);
            this.lb_friends.TabIndex = 13;
            this.lb_friends.SelectedIndexChanged += new System.EventHandler(this.lb_friends_SelectedIndexChanged);
            // 
            // bt_peerNew
            // 
            this.bt_peerNew.Location = new System.Drawing.Point(350, 272);
            this.bt_peerNew.Name = "bt_peerNew";
            this.bt_peerNew.Size = new System.Drawing.Size(72, 23);
            this.bt_peerNew.TabIndex = 25;
            this.bt_peerNew.Text = "New";
            this.bt_peerNew.UseVisualStyleBackColor = true;
            // 
            // bt_peerSave
            // 
            this.bt_peerSave.Location = new System.Drawing.Point(241, 122);
            this.bt_peerSave.Name = "bt_peerSave";
            this.bt_peerSave.Size = new System.Drawing.Size(85, 23);
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
            this.gb_ip.Location = new System.Drawing.Point(241, 16);
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
            this.tb_peerLocationID.Location = new System.Drawing.Point(71, 162);
            this.tb_peerLocationID.Name = "tb_peerLocationID";
            this.tb_peerLocationID.Size = new System.Drawing.Size(164, 20);
            this.tb_peerLocationID.TabIndex = 22;
            // 
            // lb_locations
            // 
            this.lb_locations.FormattingEnabled = true;
            this.lb_locations.Location = new System.Drawing.Point(6, 22);
            this.lb_locations.Name = "lb_locations";
            this.lb_locations.Size = new System.Drawing.Size(229, 56);
            this.lb_locations.TabIndex = 14;
            this.lb_locations.SelectedIndexChanged += new System.EventHandler(this.lb_locations_SelectedIndexChanged);
            // 
            // tb_peerLocation
            // 
            this.tb_peerLocation.Location = new System.Drawing.Point(71, 136);
            this.tb_peerLocation.Name = "tb_peerLocation";
            this.tb_peerLocation.Size = new System.Drawing.Size(164, 20);
            this.tb_peerLocation.TabIndex = 21;
            // 
            // tb_peerName
            // 
            this.tb_peerName.Location = new System.Drawing.Point(71, 84);
            this.tb_peerName.Name = "tb_peerName";
            this.tb_peerName.Size = new System.Drawing.Size(164, 20);
            this.tb_peerName.TabIndex = 19;
            // 
            // tb_peerID
            // 
            this.tb_peerID.Location = new System.Drawing.Point(71, 110);
            this.tb_peerID.Name = "tb_peerID";
            this.tb_peerID.Size = new System.Drawing.Size(164, 20);
            this.tb_peerID.TabIndex = 20;
            // 
            // l_peerName
            // 
            this.l_peerName.AutoSize = true;
            this.l_peerName.Location = new System.Drawing.Point(3, 87);
            this.l_peerName.Name = "l_peerName";
            this.l_peerName.Size = new System.Drawing.Size(35, 13);
            this.l_peerName.TabIndex = 15;
            this.l_peerName.Text = "Name";
            // 
            // l_peerID
            // 
            this.l_peerID.AutoSize = true;
            this.l_peerID.Location = new System.Drawing.Point(3, 113);
            this.l_peerID.Name = "l_peerID";
            this.l_peerID.Size = new System.Drawing.Size(18, 13);
            this.l_peerID.TabIndex = 16;
            this.l_peerID.Text = "ID";
            // 
            // l_peerLocation
            // 
            this.l_peerLocation.AutoSize = true;
            this.l_peerLocation.Location = new System.Drawing.Point(3, 139);
            this.l_peerLocation.Name = "l_peerLocation";
            this.l_peerLocation.Size = new System.Drawing.Size(48, 13);
            this.l_peerLocation.TabIndex = 17;
            this.l_peerLocation.Text = "Location";
            // 
            // l_peerLocationID
            // 
            this.l_peerLocationID.AutoSize = true;
            this.l_peerLocationID.Location = new System.Drawing.Point(3, 165);
            this.l_peerLocationID.Name = "l_peerLocationID";
            this.l_peerLocationID.Size = new System.Drawing.Size(62, 13);
            this.l_peerLocationID.TabIndex = 18;
            this.l_peerLocationID.Text = "Location ID";
            // 
            // bt_test
            // 
            this.bt_test.Location = new System.Drawing.Point(12, 318);
            this.bt_test.Name = "bt_test";
            this.bt_test.Size = new System.Drawing.Size(155, 31);
            this.bt_test.TabIndex = 11;
            this.bt_test.Text = "Test";
            this.bt_test.UseVisualStyleBackColor = true;
            this.bt_test.Click += new System.EventHandler(this.bt_test_Click);
            // 
            // tc_main
            // 
            this.tc_main.Controls.Add(this.tp_friends);
            this.tc_main.Controls.Add(this.tp_chat);
            this.tc_main.Controls.Add(this.tp_files);
            this.tc_main.Location = new System.Drawing.Point(216, 12);
            this.tc_main.Name = "tc_main";
            this.tc_main.SelectedIndex = 0;
            this.tc_main.Size = new System.Drawing.Size(649, 538);
            this.tc_main.TabIndex = 15;
            // 
            // tp_friends
            // 
            this.tp_friends.Controls.Add(this.gb_peerNew);
            this.tp_friends.Controls.Add(this.gb_peerInfo);
            this.tp_friends.Controls.Add(this.lb_friends);
            this.tp_friends.Location = new System.Drawing.Point(4, 22);
            this.tp_friends.Name = "tp_friends";
            this.tp_friends.Padding = new System.Windows.Forms.Padding(3);
            this.tp_friends.Size = new System.Drawing.Size(641, 512);
            this.tp_friends.TabIndex = 0;
            this.tp_friends.Text = "Friends";
            this.tp_friends.UseVisualStyleBackColor = true;
            // 
            // gb_peerNew
            // 
            this.gb_peerNew.Controls.Add(this.tb_peedNew);
            this.gb_peerNew.Controls.Add(this.bt_peerNew);
            this.gb_peerNew.Location = new System.Drawing.Point(207, 204);
            this.gb_peerNew.Name = "gb_peerNew";
            this.gb_peerNew.Size = new System.Drawing.Size(428, 301);
            this.gb_peerNew.TabIndex = 28;
            this.gb_peerNew.TabStop = false;
            this.gb_peerNew.Text = "Add a new friend";
            // 
            // tb_peedNew
            // 
            this.tb_peedNew.Location = new System.Drawing.Point(6, 22);
            this.tb_peedNew.Multiline = true;
            this.tb_peedNew.Name = "tb_peedNew";
            this.tb_peedNew.Size = new System.Drawing.Size(413, 244);
            this.tb_peedNew.TabIndex = 26;
            // 
            // gb_peerInfo
            // 
            this.gb_peerInfo.Controls.Add(this.bt_peerRemove);
            this.gb_peerInfo.Controls.Add(this.lb_locations);
            this.gb_peerInfo.Controls.Add(this.tb_peerName);
            this.gb_peerInfo.Controls.Add(this.tb_peerID);
            this.gb_peerInfo.Controls.Add(this.tb_peerLocation);
            this.gb_peerInfo.Controls.Add(this.l_peerName);
            this.gb_peerInfo.Controls.Add(this.bt_peerSave);
            this.gb_peerInfo.Controls.Add(this.l_peerID);
            this.gb_peerInfo.Controls.Add(this.l_peerLocationID);
            this.gb_peerInfo.Controls.Add(this.tb_peerLocationID);
            this.gb_peerInfo.Controls.Add(this.gb_ip);
            this.gb_peerInfo.Controls.Add(this.l_peerLocation);
            this.gb_peerInfo.Location = new System.Drawing.Point(207, 7);
            this.gb_peerInfo.Name = "gb_peerInfo";
            this.gb_peerInfo.Size = new System.Drawing.Size(428, 191);
            this.gb_peerInfo.TabIndex = 27;
            this.gb_peerInfo.TabStop = false;
            this.gb_peerInfo.Text = "Information";
            // 
            // bt_peerRemove
            // 
            this.bt_peerRemove.Location = new System.Drawing.Point(334, 122);
            this.bt_peerRemove.Name = "bt_peerRemove";
            this.bt_peerRemove.Size = new System.Drawing.Size(85, 23);
            this.bt_peerRemove.TabIndex = 25;
            this.bt_peerRemove.Text = "Block/Remove";
            this.bt_peerRemove.UseVisualStyleBackColor = true;
            // 
            // tp_chat
            // 
            this.tp_chat.Controls.Add(this.gb_autoResp);
            this.tp_chat.Controls.Add(this.clb_chatUser);
            this.tp_chat.Controls.Add(this.gb_nickname);
            this.tp_chat.Controls.Add(this.rtb_chat);
            this.tp_chat.Controls.Add(this.clb_chatLobbies);
            this.tp_chat.Controls.Add(this.bt_chatSend);
            this.tp_chat.Controls.Add(this.tb_chatMsg);
            this.tp_chat.Location = new System.Drawing.Point(4, 22);
            this.tp_chat.Name = "tp_chat";
            this.tp_chat.Padding = new System.Windows.Forms.Padding(3);
            this.tp_chat.Size = new System.Drawing.Size(641, 512);
            this.tp_chat.TabIndex = 1;
            this.tp_chat.Text = "Chat";
            this.tp_chat.UseVisualStyleBackColor = true;
            // 
            // gb_autoResp
            // 
            this.gb_autoResp.Controls.Add(this.tb_chatAutoRespAnswer);
            this.gb_autoResp.Controls.Add(this.l_chatAutoRespSearch);
            this.gb_autoResp.Controls.Add(this.tb_chatAutoRespSearch);
            this.gb_autoResp.Controls.Add(this.cb_chatAutoRespEnable);
            this.gb_autoResp.Controls.Add(this.l_chatAutoRespAnswer);
            this.gb_autoResp.Location = new System.Drawing.Point(278, 400);
            this.gb_autoResp.Name = "gb_autoResp";
            this.gb_autoResp.Size = new System.Drawing.Size(357, 100);
            this.gb_autoResp.TabIndex = 8;
            this.gb_autoResp.TabStop = false;
            this.gb_autoResp.Text = "Auto Response";
            // 
            // tb_chatAutoRespAnswer
            // 
            this.tb_chatAutoRespAnswer.Location = new System.Drawing.Point(78, 50);
            this.tb_chatAutoRespAnswer.Multiline = true;
            this.tb_chatAutoRespAnswer.Name = "tb_chatAutoRespAnswer";
            this.tb_chatAutoRespAnswer.Size = new System.Drawing.Size(273, 44);
            this.tb_chatAutoRespAnswer.TabIndex = 7;
            this.tb_chatAutoRespAnswer.Text = "[AutoResponse] afk";
            // 
            // l_chatAutoRespSearch
            // 
            this.l_chatAutoRespSearch.AutoSize = true;
            this.l_chatAutoRespSearch.Location = new System.Drawing.Point(6, 22);
            this.l_chatAutoRespSearch.Name = "l_chatAutoRespSearch";
            this.l_chatAutoRespSearch.Size = new System.Drawing.Size(54, 13);
            this.l_chatAutoRespSearch.TabIndex = 4;
            this.l_chatAutoRespSearch.Text = "search for";
            // 
            // tb_chatAutoRespSearch
            // 
            this.tb_chatAutoRespSearch.Location = new System.Drawing.Point(78, 20);
            this.tb_chatAutoRespSearch.Name = "tb_chatAutoRespSearch";
            this.tb_chatAutoRespSearch.Size = new System.Drawing.Size(198, 20);
            this.tb_chatAutoRespSearch.TabIndex = 6;
            // 
            // cb_chatAutoRespEnable
            // 
            this.cb_chatAutoRespEnable.AutoSize = true;
            this.cb_chatAutoRespEnable.Location = new System.Drawing.Point(282, 22);
            this.cb_chatAutoRespEnable.Name = "cb_chatAutoRespEnable";
            this.cb_chatAutoRespEnable.Size = new System.Drawing.Size(58, 17);
            this.cb_chatAutoRespEnable.TabIndex = 3;
            this.cb_chatAutoRespEnable.Text = "enable";
            this.cb_chatAutoRespEnable.UseVisualStyleBackColor = true;
            // 
            // l_chatAutoRespAnswer
            // 
            this.l_chatAutoRespAnswer.AutoSize = true;
            this.l_chatAutoRespAnswer.Location = new System.Drawing.Point(6, 50);
            this.l_chatAutoRespAnswer.Name = "l_chatAutoRespAnswer";
            this.l_chatAutoRespAnswer.Size = new System.Drawing.Size(41, 13);
            this.l_chatAutoRespAnswer.TabIndex = 5;
            this.l_chatAutoRespAnswer.Text = "answer";
            // 
            // clb_chatUser
            // 
            this.clb_chatUser.FormattingEnabled = true;
            this.clb_chatUser.Location = new System.Drawing.Point(4, 152);
            this.clb_chatUser.Name = "clb_chatUser";
            this.clb_chatUser.Size = new System.Drawing.Size(140, 349);
            this.clb_chatUser.TabIndex = 10;
            // 
            // gb_nickname
            // 
            this.gb_nickname.Controls.Add(this.tb_chatNickname);
            this.gb_nickname.Controls.Add(this.bt_setNickname);
            this.gb_nickname.Location = new System.Drawing.Point(150, 400);
            this.gb_nickname.Name = "gb_nickname";
            this.gb_nickname.Size = new System.Drawing.Size(122, 100);
            this.gb_nickname.TabIndex = 9;
            this.gb_nickname.TabStop = false;
            this.gb_nickname.Text = "Nickname";
            // 
            // tb_chatNickname
            // 
            this.tb_chatNickname.Location = new System.Drawing.Point(6, 19);
            this.tb_chatNickname.Name = "tb_chatNickname";
            this.tb_chatNickname.Size = new System.Drawing.Size(107, 20);
            this.tb_chatNickname.TabIndex = 2;
            // 
            // bt_setNickname
            // 
            this.bt_setNickname.Location = new System.Drawing.Point(6, 45);
            this.bt_setNickname.Name = "bt_setNickname";
            this.bt_setNickname.Size = new System.Drawing.Size(107, 23);
            this.bt_setNickname.TabIndex = 1;
            this.bt_setNickname.Text = "set new nickname";
            this.bt_setNickname.UseVisualStyleBackColor = true;
            this.bt_setNickname.Click += new System.EventHandler(this.bt_setNickname_Click);
            // 
            // rtb_chat
            // 
            this.rtb_chat.Location = new System.Drawing.Point(150, 6);
            this.rtb_chat.Name = "rtb_chat";
            this.rtb_chat.Size = new System.Drawing.Size(485, 360);
            this.rtb_chat.TabIndex = 7;
            this.rtb_chat.Text = "";
            // 
            // clb_chatLobbies
            // 
            this.clb_chatLobbies.FormattingEnabled = true;
            this.clb_chatLobbies.Location = new System.Drawing.Point(3, 6);
            this.clb_chatLobbies.Name = "clb_chatLobbies";
            this.clb_chatLobbies.Size = new System.Drawing.Size(141, 139);
            this.clb_chatLobbies.TabIndex = 6;
            this.clb_chatLobbies.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clb_chatLobbies_ItemCheck);
            this.clb_chatLobbies.SelectedIndexChanged += new System.EventHandler(this.clb_chatLobbies_SelectedIndexChanged);
            // 
            // bt_chatSend
            // 
            this.bt_chatSend.Location = new System.Drawing.Point(560, 372);
            this.bt_chatSend.Name = "bt_chatSend";
            this.bt_chatSend.Size = new System.Drawing.Size(75, 23);
            this.bt_chatSend.TabIndex = 4;
            this.bt_chatSend.Text = "Senden";
            this.bt_chatSend.UseVisualStyleBackColor = true;
            this.bt_chatSend.Click += new System.EventHandler(this.bt_chatSend_Click);
            // 
            // tb_chatMsg
            // 
            this.tb_chatMsg.Location = new System.Drawing.Point(150, 374);
            this.tb_chatMsg.Name = "tb_chatMsg";
            this.tb_chatMsg.Size = new System.Drawing.Size(404, 20);
            this.tb_chatMsg.TabIndex = 3;
            this.tb_chatMsg.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_chatMsg_KeyUp);
            // 
            // tp_files
            // 
            this.tp_files.Location = new System.Drawing.Point(4, 22);
            this.tp_files.Name = "tp_files";
            this.tp_files.Size = new System.Drawing.Size(641, 512);
            this.tp_files.TabIndex = 2;
            this.tp_files.Text = "Files";
            this.tp_files.UseVisualStyleBackColor = true;
            // 
            // tb_optios
            // 
            this.tb_optios.Controls.Add(this.tb_connection);
            this.tb_optios.Controls.Add(this.tb_options);
            this.tb_optios.Location = new System.Drawing.Point(12, 12);
            this.tb_optios.Name = "tb_optios";
            this.tb_optios.SelectedIndex = 0;
            this.tb_optios.Size = new System.Drawing.Size(198, 194);
            this.tb_optios.TabIndex = 16;
            // 
            // tb_connection
            // 
            this.tb_connection.Controls.Add(this.cb_con);
            this.tb_connection.Controls.Add(this.l_ip);
            this.tb_connection.Controls.Add(this.l_pw);
            this.tb_connection.Controls.Add(this.tb_host);
            this.tb_connection.Controls.Add(this.l_user);
            this.tb_connection.Controls.Add(this.tb_port);
            this.tb_connection.Controls.Add(this.tb_user);
            this.tb_connection.Controls.Add(this.l_port);
            this.tb_connection.Controls.Add(this.bt_disconnect);
            this.tb_connection.Controls.Add(this.bt_connect);
            this.tb_connection.Controls.Add(this.tb_pw);
            this.tb_connection.Location = new System.Drawing.Point(4, 22);
            this.tb_connection.Name = "tb_connection";
            this.tb_connection.Padding = new System.Windows.Forms.Padding(3);
            this.tb_connection.Size = new System.Drawing.Size(190, 168);
            this.tb_connection.TabIndex = 0;
            this.tb_connection.Text = "Connection";
            this.tb_connection.UseVisualStyleBackColor = true;
            // 
            // tb_options
            // 
            this.tb_options.Controls.Add(this.cb_settingsSavePW);
            this.tb_options.Controls.Add(this.cb_settingsSave);
            this.tb_options.Location = new System.Drawing.Point(4, 22);
            this.tb_options.Name = "tb_options";
            this.tb_options.Padding = new System.Windows.Forms.Padding(3);
            this.tb_options.Size = new System.Drawing.Size(190, 168);
            this.tb_options.TabIndex = 1;
            this.tb_options.Text = "Settings";
            this.tb_options.UseVisualStyleBackColor = true;
            // 
            // cb_settingsSave
            // 
            this.cb_settingsSave.AutoSize = true;
            this.cb_settingsSave.Location = new System.Drawing.Point(6, 8);
            this.cb_settingsSave.Name = "cb_settingsSave";
            this.cb_settingsSave.Size = new System.Drawing.Size(88, 17);
            this.cb_settingsSave.TabIndex = 0;
            this.cb_settingsSave.Text = "save settings";
            this.cb_settingsSave.UseVisualStyleBackColor = true;
            this.cb_settingsSave.CheckedChanged += new System.EventHandler(this.cb_settingsSave_CheckedChanged);
            // 
            // cb_settingsSavePW
            // 
            this.cb_settingsSavePW.AutoSize = true;
            this.cb_settingsSavePW.Location = new System.Drawing.Point(6, 31);
            this.cb_settingsSavePW.Name = "cb_settingsSavePW";
            this.cb_settingsSavePW.Size = new System.Drawing.Size(156, 17);
            this.cb_settingsSavePW.TabIndex = 1;
            this.cb_settingsSavePW.Text = "save password (as Base64)";
            this.cb_settingsSavePW.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 562);
            this.Controls.Add(this.tb_optios);
            this.Controls.Add(this.tc_main);
            this.Controls.Add(this.bt_test);
            this.Controls.Add(this.bg_systemStatus);
            this.Controls.Add(this.tb_out);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.bg_systemStatus.ResumeLayout(false);
            this.bg_systemStatus.PerformLayout();
            this.gb_ip.ResumeLayout(false);
            this.gb_ip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_peerPortExt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_peerPortInt)).EndInit();
            this.tc_main.ResumeLayout(false);
            this.tp_friends.ResumeLayout(false);
            this.gb_peerNew.ResumeLayout(false);
            this.gb_peerNew.PerformLayout();
            this.gb_peerInfo.ResumeLayout(false);
            this.gb_peerInfo.PerformLayout();
            this.tp_chat.ResumeLayout(false);
            this.tp_chat.PerformLayout();
            this.gb_autoResp.ResumeLayout(false);
            this.gb_autoResp.PerformLayout();
            this.gb_nickname.ResumeLayout(false);
            this.gb_nickname.PerformLayout();
            this.tb_optios.ResumeLayout(false);
            this.tb_connection.ResumeLayout(false);
            this.tb_connection.PerformLayout();
            this.tb_options.ResumeLayout(false);
            this.tb_options.PerformLayout();
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
        private System.Windows.Forms.TabControl tc_main;
        private System.Windows.Forms.TabPage tp_friends;
        private System.Windows.Forms.TabPage tp_chat;
        private System.Windows.Forms.TabPage tp_files;
        private System.Windows.Forms.GroupBox gb_peerNew;
        private System.Windows.Forms.TextBox tb_peedNew;
        private System.Windows.Forms.GroupBox gb_peerInfo;
        private System.Windows.Forms.Button bt_chatSend;
        private System.Windows.Forms.TextBox tb_chatMsg;
        private System.Windows.Forms.CheckedListBox clb_chatLobbies;
        private System.Windows.Forms.RichTextBox rtb_chat;
        private System.Windows.Forms.GroupBox gb_nickname;
        private System.Windows.Forms.TextBox tb_chatNickname;
        private System.Windows.Forms.Button bt_setNickname;
        private System.Windows.Forms.Button bt_peerRemove;
        private System.Windows.Forms.GroupBox gb_autoResp;
        private System.Windows.Forms.TextBox tb_chatAutoRespAnswer;
        private System.Windows.Forms.Label l_chatAutoRespSearch;
        private System.Windows.Forms.TextBox tb_chatAutoRespSearch;
        private System.Windows.Forms.CheckBox cb_chatAutoRespEnable;
        private System.Windows.Forms.Label l_chatAutoRespAnswer;
        private System.Windows.Forms.CheckedListBox clb_chatUser;
        private System.Windows.Forms.TabControl tb_optios;
        private System.Windows.Forms.TabPage tb_connection;
        private System.Windows.Forms.TabPage tb_options;
        private System.Windows.Forms.CheckBox cb_settingsSave;
        private System.Windows.Forms.CheckBox cb_settingsSavePW;
    }
}

