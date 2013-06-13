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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.gb_systemStatus = new System.Windows.Forms.GroupBox();
            this.l_peers = new System.Windows.Forms.Label();
            this.l_connected = new System.Windows.Forms.Label();
            this.l_network = new System.Windows.Forms.Label();
            this.l_bwDown = new System.Windows.Forms.Label();
            this.l_bwUp = new System.Windows.Forms.Label();
            this.t_tick = new System.Windows.Forms.Timer(this.components);
            this.lb_friends = new System.Windows.Forms.ListBox();
            this.bt_peerNew = new System.Windows.Forms.Button();
            this.bt_peerSave = new System.Windows.Forms.Button();
            this.gb_ip = new System.Windows.Forms.GroupBox();
            this.l_peerIPExt = new System.Windows.Forms.Label();
            this.l_peerDynDns = new System.Windows.Forms.Label();
            this.l_peerIPInt = new System.Windows.Forms.Label();
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
            this.tp_setup = new System.Windows.Forms.TabPage();
            this.gb_settings = new System.Windows.Forms.GroupBox();
            this.cb_settingsSave = new System.Windows.Forms.CheckBox();
            this.cb_settingsSavePW = new System.Windows.Forms.CheckBox();
            this.cb_settingsSaveChat = new System.Windows.Forms.CheckBox();
            this.cb_settingsReadSpeed = new System.Windows.Forms.ComboBox();
            this.l_settingsReadSpeed = new System.Windows.Forms.Label();
            this.gb_connection = new System.Windows.Forms.GroupBox();
            this.bt_shutdown = new System.Windows.Forms.Button();
            this.bt_settingsClearLog = new System.Windows.Forms.Button();
            this.tp_chat = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tp_chat_chat = new System.Windows.Forms.TabPage();
            this.bt_leaveChatLobby = new System.Windows.Forms.Button();
            this.rtb_chat = new System.Windows.Forms.RichTextBox();
            this.bt_joinChatLobby = new System.Windows.Forms.Button();
            this.tb_chatMsg = new System.Windows.Forms.TextBox();
            this.bt_chatSend = new System.Windows.Forms.Button();
            this.clb_chatUser = new System.Windows.Forms.CheckedListBox();
            this.clb_chatLobbies = new System.Windows.Forms.CheckedListBox();
            this.gb_nickname = new System.Windows.Forms.GroupBox();
            this.tb_chatNickname = new System.Windows.Forms.TextBox();
            this.bt_setNickname = new System.Windows.Forms.Button();
            this.tp_chat_autoResponse = new System.Windows.Forms.TabPage();
            this.gb_chat_arOptions = new System.Windows.Forms.GroupBox();
            this.bt_chat_arRemove = new System.Windows.Forms.Button();
            this.bt_chat_arNew = new System.Windows.Forms.Button();
            this.bt_chat_arSave = new System.Windows.Forms.Button();
            this.tb_chat_arPrefix = new System.Windows.Forms.TextBox();
            this.l_chat_arPrefix = new System.Windows.Forms.Label();
            this.l_chat_arUsesFunction = new System.Windows.Forms.Label();
            this.cb_chat_arUsesFunction = new System.Windows.Forms.CheckBox();
            this.l_chat_arAnswer = new System.Windows.Forms.Label();
            this.l_chat_arSearchFor = new System.Windows.Forms.Label();
            this.tb_chat_arAnswer = new System.Windows.Forms.TextBox();
            this.tb_chat_arSearchFor = new System.Windows.Forms.TextBox();
            this.tb_chat_arName = new System.Windows.Forms.TextBox();
            this.cb_chat_arCaseSensitive = new System.Windows.Forms.CheckBox();
            this.cb_chat_arWithSpaces = new System.Windows.Forms.CheckBox();
            this.cb_chat_arOnly = new System.Windows.Forms.CheckBox();
            this.l_chat_arName = new System.Windows.Forms.Label();
            this.l_chat_arList = new System.Windows.Forms.Label();
            this.clb_chat_arList = new System.Windows.Forms.CheckedListBox();
            this.cb_chat_arEnable = new System.Windows.Forms.CheckBox();
            this.tp_friends = new System.Windows.Forms.TabPage();
            this.gb_peerNew = new System.Windows.Forms.GroupBox();
            this.tb_peedNew = new System.Windows.Forms.TextBox();
            this.gb_peerInfo = new System.Windows.Forms.GroupBox();
            this.bt_peerRemove = new System.Windows.Forms.Button();
            this.tp_files = new System.Windows.Forms.TabPage();
            this.tv_files = new System.Windows.Forms.TabControl();
            this.tp_filesDownload = new System.Windows.Forms.TabPage();
            this.lb_filesDownloads = new System.Windows.Forms.ListBox();
            this.bt_filesAddCollection = new System.Windows.Forms.Button();
            this.bt_filesPause = new System.Windows.Forms.Button();
            this.dgv_filesDownloads = new System.Windows.Forms.DataGridView();
            this.dlSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dlDone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dlSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dlName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dlSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dlHash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bt_filesCancel = new System.Windows.Forms.Button();
            this.bt_filesContinue = new System.Windows.Forms.Button();
            this.bt_filesWait = new System.Windows.Forms.Button();
            this.bt_filesForceCheck = new System.Windows.Forms.Button();
            this.bt_filesRestart = new System.Windows.Forms.Button();
            this.tp_filesUploads = new System.Windows.Forms.TabPage();
            this.dgv_filesUploads = new System.Windows.Forms.DataGridView();
            this.ulSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ulName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ulSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ulHash = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lb_filesUploads = new System.Windows.Forms.ListBox();
            this.tb_search = new System.Windows.Forms.TabPage();
            this.bt_searchAddToDL = new System.Windows.Forms.Button();
            this.bt_searchRemove = new System.Windows.Forms.Button();
            this.lb_searches = new System.Windows.Forms.ListBox();
            this.lb_searchResults = new System.Windows.Forms.ListBox();
            this.tb_searchKeyWords = new System.Windows.Forms.TextBox();
            this.bt_searchSearch = new System.Windows.Forms.Button();
            this.l_searchKeyWords = new System.Windows.Forms.Label();
            this.ofd_collection = new System.Windows.Forms.OpenFileDialog();
            this.gb_systemStatus.SuspendLayout();
            this.gb_ip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_peerPortExt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_peerPortInt)).BeginInit();
            this.tc_main.SuspendLayout();
            this.tp_setup.SuspendLayout();
            this.gb_settings.SuspendLayout();
            this.gb_connection.SuspendLayout();
            this.tp_chat.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tp_chat_chat.SuspendLayout();
            this.gb_nickname.SuspendLayout();
            this.tp_chat_autoResponse.SuspendLayout();
            this.gb_chat_arOptions.SuspendLayout();
            this.tp_friends.SuspendLayout();
            this.gb_peerNew.SuspendLayout();
            this.gb_peerInfo.SuspendLayout();
            this.tp_files.SuspendLayout();
            this.tv_files.SuspendLayout();
            this.tp_filesDownload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_filesDownloads)).BeginInit();
            this.tp_filesUploads.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_filesUploads)).BeginInit();
            this.tb_search.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_connect
            // 
            this.bt_connect.Location = new System.Drawing.Point(9, 120);
            this.bt_connect.Name = "bt_connect";
            this.bt_connect.Size = new System.Drawing.Size(77, 23);
            this.bt_connect.TabIndex = 5;
            this.bt_connect.Text = "connect";
            this.bt_connect.UseVisualStyleBackColor = true;
            this.bt_connect.Click += new System.EventHandler(this.bt_connect_Click);
            // 
            // bt_disconnect
            // 
            this.bt_disconnect.Location = new System.Drawing.Point(103, 120);
            this.bt_disconnect.Name = "bt_disconnect";
            this.bt_disconnect.Size = new System.Drawing.Size(75, 23);
            this.bt_disconnect.TabIndex = 6;
            this.bt_disconnect.Text = "disconnect";
            this.bt_disconnect.UseVisualStyleBackColor = true;
            this.bt_disconnect.Click += new System.EventHandler(this.bt_disconnect_Click);
            // 
            // tb_out
            // 
            this.tb_out.Location = new System.Drawing.Point(381, 295);
            this.tb_out.Multiline = true;
            this.tb_out.Name = "tb_out";
            this.tb_out.Size = new System.Drawing.Size(194, 184);
            this.tb_out.TabIndex = 2;
            // 
            // tb_user
            // 
            this.tb_user.Location = new System.Drawing.Point(76, 65);
            this.tb_user.Name = "tb_user";
            this.tb_user.Size = new System.Drawing.Size(102, 20);
            this.tb_user.TabIndex = 3;
            // 
            // tb_pw
            // 
            this.tb_pw.Location = new System.Drawing.Point(76, 91);
            this.tb_pw.Name = "tb_pw";
            this.tb_pw.Size = new System.Drawing.Size(102, 20);
            this.tb_pw.TabIndex = 4;
            this.tb_pw.UseSystemPasswordChar = true;
            this.tb_pw.Enter += new System.EventHandler(this.tb_pw_Enter);
            // 
            // tb_host
            // 
            this.tb_host.Location = new System.Drawing.Point(76, 13);
            this.tb_host.Name = "tb_host";
            this.tb_host.Size = new System.Drawing.Size(102, 20);
            this.tb_host.TabIndex = 1;
            // 
            // tb_port
            // 
            this.tb_port.Location = new System.Drawing.Point(76, 39);
            this.tb_port.Name = "tb_port";
            this.tb_port.Size = new System.Drawing.Size(102, 20);
            this.tb_port.TabIndex = 2;
            // 
            // cb_con
            // 
            this.cb_con.AutoSize = true;
            this.cb_con.Location = new System.Drawing.Point(103, 153);
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
            this.l_pw.Location = new System.Drawing.Point(6, 94);
            this.l_pw.Name = "l_pw";
            this.l_pw.Size = new System.Drawing.Size(53, 13);
            this.l_pw.TabIndex = 11;
            this.l_pw.Text = "Password";
            // 
            // l_user
            // 
            this.l_user.AutoSize = true;
            this.l_user.Location = new System.Drawing.Point(6, 68);
            this.l_user.Name = "l_user";
            this.l_user.Size = new System.Drawing.Size(55, 13);
            this.l_user.TabIndex = 10;
            this.l_user.Text = "Username";
            // 
            // l_port
            // 
            this.l_port.AutoSize = true;
            this.l_port.Location = new System.Drawing.Point(6, 42);
            this.l_port.Name = "l_port";
            this.l_port.Size = new System.Drawing.Size(26, 13);
            this.l_port.TabIndex = 9;
            this.l_port.Text = "Port";
            // 
            // l_ip
            // 
            this.l_ip.AutoSize = true;
            this.l_ip.Location = new System.Drawing.Point(6, 16);
            this.l_ip.Name = "l_ip";
            this.l_ip.Size = new System.Drawing.Size(64, 13);
            this.l_ip.TabIndex = 0;
            this.l_ip.Text = "(IP) Address";
            // 
            // gb_systemStatus
            // 
            this.gb_systemStatus.Controls.Add(this.l_peers);
            this.gb_systemStatus.Controls.Add(this.l_connected);
            this.gb_systemStatus.Controls.Add(this.l_network);
            this.gb_systemStatus.Controls.Add(this.l_bwDown);
            this.gb_systemStatus.Controls.Add(this.l_bwUp);
            this.gb_systemStatus.Location = new System.Drawing.Point(195, 4);
            this.gb_systemStatus.Name = "gb_systemStatus";
            this.gb_systemStatus.Size = new System.Drawing.Size(194, 100);
            this.gb_systemStatus.TabIndex = 12;
            this.gb_systemStatus.TabStop = false;
            this.gb_systemStatus.Text = "System Status";
            // 
            // l_peers
            // 
            this.l_peers.AutoSize = true;
            this.l_peers.Location = new System.Drawing.Point(6, 48);
            this.l_peers.Name = "l_peers";
            this.l_peers.Size = new System.Drawing.Size(33, 13);
            this.l_peers.TabIndex = 5;
            this.l_peers.Text = "peers";
            // 
            // l_connected
            // 
            this.l_connected.AutoSize = true;
            this.l_connected.Location = new System.Drawing.Point(104, 48);
            this.l_connected.Name = "l_connected";
            this.l_connected.Size = new System.Drawing.Size(58, 13);
            this.l_connected.TabIndex = 4;
            this.l_connected.Text = "connected";
            // 
            // l_network
            // 
            this.l_network.AutoSize = true;
            this.l_network.Location = new System.Drawing.Point(6, 22);
            this.l_network.Name = "l_network";
            this.l_network.Size = new System.Drawing.Size(45, 13);
            this.l_network.TabIndex = 3;
            this.l_network.Text = "network";
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
            this.bt_peerNew.Location = new System.Drawing.Point(347, 211);
            this.bt_peerNew.Name = "bt_peerNew";
            this.bt_peerNew.Size = new System.Drawing.Size(72, 23);
            this.bt_peerNew.TabIndex = 25;
            this.bt_peerNew.Text = "New";
            this.bt_peerNew.UseVisualStyleBackColor = true;
            this.bt_peerNew.Click += new System.EventHandler(this.bt_peerNew_Click);
            // 
            // bt_peerSave
            // 
            this.bt_peerSave.Location = new System.Drawing.Point(241, 18);
            this.bt_peerSave.Name = "bt_peerSave";
            this.bt_peerSave.Size = new System.Drawing.Size(74, 23);
            this.bt_peerSave.TabIndex = 24;
            this.bt_peerSave.Text = "Save";
            this.bt_peerSave.UseVisualStyleBackColor = true;
            this.bt_peerSave.Click += new System.EventHandler(this.bt_peerSave_Click);
            // 
            // gb_ip
            // 
            this.gb_ip.Controls.Add(this.l_peerIPExt);
            this.gb_ip.Controls.Add(this.l_peerDynDns);
            this.gb_ip.Controls.Add(this.l_peerIPInt);
            this.gb_ip.Controls.Add(this.tb_dyndns);
            this.gb_ip.Controls.Add(this.tb_peerIPInt);
            this.gb_ip.Controls.Add(this.tb_peerIPExt);
            this.gb_ip.Controls.Add(this.nud_peerPortExt);
            this.gb_ip.Controls.Add(this.nud_peerPortInt);
            this.gb_ip.Location = new System.Drawing.Point(241, 46);
            this.gb_ip.Name = "gb_ip";
            this.gb_ip.Size = new System.Drawing.Size(178, 140);
            this.gb_ip.TabIndex = 23;
            this.gb_ip.TabStop = false;
            this.gb_ip.Text = "IPs/Ports";
            // 
            // l_peerIPExt
            // 
            this.l_peerIPExt.AutoSize = true;
            this.l_peerIPExt.Location = new System.Drawing.Point(6, 55);
            this.l_peerIPExt.Name = "l_peerIPExt";
            this.l_peerIPExt.Size = new System.Drawing.Size(44, 13);
            this.l_peerIPExt.TabIndex = 24;
            this.l_peerIPExt.Text = "external";
            // 
            // l_peerDynDns
            // 
            this.l_peerDynDns.AutoSize = true;
            this.l_peerDynDns.Location = new System.Drawing.Point(6, 94);
            this.l_peerDynDns.Name = "l_peerDynDns";
            this.l_peerDynDns.Size = new System.Drawing.Size(45, 13);
            this.l_peerDynDns.TabIndex = 23;
            this.l_peerDynDns.Text = "DynDns";
            // 
            // l_peerIPInt
            // 
            this.l_peerIPInt.AutoSize = true;
            this.l_peerIPInt.Location = new System.Drawing.Point(6, 16);
            this.l_peerIPInt.Name = "l_peerIPInt";
            this.l_peerIPInt.Size = new System.Drawing.Size(41, 13);
            this.l_peerIPInt.TabIndex = 22;
            this.l_peerIPInt.Text = "internal";
            // 
            // tb_dyndns
            // 
            this.tb_dyndns.Enabled = false;
            this.tb_dyndns.Location = new System.Drawing.Point(6, 110);
            this.tb_dyndns.Name = "tb_dyndns";
            this.tb_dyndns.Size = new System.Drawing.Size(163, 20);
            this.tb_dyndns.TabIndex = 21;
            // 
            // tb_peerIPInt
            // 
            this.tb_peerIPInt.Location = new System.Drawing.Point(6, 32);
            this.tb_peerIPInt.Name = "tb_peerIPInt";
            this.tb_peerIPInt.Size = new System.Drawing.Size(97, 20);
            this.tb_peerIPInt.TabIndex = 18;
            // 
            // tb_peerIPExt
            // 
            this.tb_peerIPExt.Location = new System.Drawing.Point(6, 71);
            this.tb_peerIPExt.Name = "tb_peerIPExt";
            this.tb_peerIPExt.Size = new System.Drawing.Size(97, 20);
            this.tb_peerIPExt.TabIndex = 17;
            // 
            // nud_peerPortExt
            // 
            this.nud_peerPortExt.Location = new System.Drawing.Point(109, 71);
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
            this.nud_peerPortInt.Location = new System.Drawing.Point(109, 32);
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
            this.lb_locations.Location = new System.Drawing.Point(6, 72);
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
            this.tb_peerName.Location = new System.Drawing.Point(71, 20);
            this.tb_peerName.Name = "tb_peerName";
            this.tb_peerName.Size = new System.Drawing.Size(164, 20);
            this.tb_peerName.TabIndex = 19;
            // 
            // tb_peerID
            // 
            this.tb_peerID.Location = new System.Drawing.Point(71, 46);
            this.tb_peerID.Name = "tb_peerID";
            this.tb_peerID.Size = new System.Drawing.Size(164, 20);
            this.tb_peerID.TabIndex = 20;
            // 
            // l_peerName
            // 
            this.l_peerName.AutoSize = true;
            this.l_peerName.Location = new System.Drawing.Point(6, 23);
            this.l_peerName.Name = "l_peerName";
            this.l_peerName.Size = new System.Drawing.Size(35, 13);
            this.l_peerName.TabIndex = 15;
            this.l_peerName.Text = "Name";
            // 
            // l_peerID
            // 
            this.l_peerID.AutoSize = true;
            this.l_peerID.Location = new System.Drawing.Point(6, 49);
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
            this.bt_test.Location = new System.Drawing.Point(381, 258);
            this.bt_test.Name = "bt_test";
            this.bt_test.Size = new System.Drawing.Size(194, 31);
            this.bt_test.TabIndex = 11;
            this.bt_test.Text = "Test";
            this.bt_test.UseVisualStyleBackColor = true;
            this.bt_test.Click += new System.EventHandler(this.bt_test_Click);
            // 
            // tc_main
            // 
            this.tc_main.Controls.Add(this.tp_setup);
            this.tc_main.Controls.Add(this.tp_chat);
            this.tc_main.Controls.Add(this.tp_friends);
            this.tc_main.Controls.Add(this.tp_files);
            this.tc_main.Controls.Add(this.tb_search);
            this.tc_main.Location = new System.Drawing.Point(12, 12);
            this.tc_main.Name = "tc_main";
            this.tc_main.SelectedIndex = 0;
            this.tc_main.Size = new System.Drawing.Size(649, 538);
            this.tc_main.TabIndex = 15;
            // 
            // tp_setup
            // 
            this.tp_setup.Controls.Add(this.gb_settings);
            this.tp_setup.Controls.Add(this.gb_connection);
            this.tp_setup.Controls.Add(this.bt_settingsClearLog);
            this.tp_setup.Controls.Add(this.bt_test);
            this.tp_setup.Controls.Add(this.gb_systemStatus);
            this.tp_setup.Controls.Add(this.tb_out);
            this.tp_setup.Location = new System.Drawing.Point(4, 22);
            this.tp_setup.Name = "tp_setup";
            this.tp_setup.Size = new System.Drawing.Size(641, 512);
            this.tp_setup.TabIndex = 4;
            this.tp_setup.Text = "Setup";
            this.tp_setup.UseVisualStyleBackColor = true;
            // 
            // gb_settings
            // 
            this.gb_settings.Controls.Add(this.cb_settingsSave);
            this.gb_settings.Controls.Add(this.cb_settingsSavePW);
            this.gb_settings.Controls.Add(this.cb_settingsSaveChat);
            this.gb_settings.Controls.Add(this.cb_settingsReadSpeed);
            this.gb_settings.Controls.Add(this.l_settingsReadSpeed);
            this.gb_settings.Location = new System.Drawing.Point(4, 188);
            this.gb_settings.Name = "gb_settings";
            this.gb_settings.Size = new System.Drawing.Size(185, 119);
            this.gb_settings.TabIndex = 14;
            this.gb_settings.TabStop = false;
            this.gb_settings.Text = "Settings";
            // 
            // cb_settingsSave
            // 
            this.cb_settingsSave.AutoSize = true;
            this.cb_settingsSave.Location = new System.Drawing.Point(6, 19);
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
            this.cb_settingsSavePW.Location = new System.Drawing.Point(6, 42);
            this.cb_settingsSavePW.Name = "cb_settingsSavePW";
            this.cb_settingsSavePW.Size = new System.Drawing.Size(156, 17);
            this.cb_settingsSavePW.TabIndex = 1;
            this.cb_settingsSavePW.Text = "save password (as Base64)";
            this.cb_settingsSavePW.UseVisualStyleBackColor = true;
            // 
            // cb_settingsSaveChat
            // 
            this.cb_settingsSaveChat.AutoSize = true;
            this.cb_settingsSaveChat.Location = new System.Drawing.Point(6, 65);
            this.cb_settingsSaveChat.Name = "cb_settingsSaveChat";
            this.cb_settingsSaveChat.Size = new System.Drawing.Size(170, 17);
            this.cb_settingsSaveChat.TabIndex = 2;
            this.cb_settingsSaveChat.Text = "save chat and AutoResponse ";
            this.cb_settingsSaveChat.UseVisualStyleBackColor = true;
            // 
            // cb_settingsReadSpeed
            // 
            this.cb_settingsReadSpeed.Enabled = false;
            this.cb_settingsReadSpeed.FormattingEnabled = true;
            this.cb_settingsReadSpeed.Location = new System.Drawing.Point(108, 86);
            this.cb_settingsReadSpeed.Name = "cb_settingsReadSpeed";
            this.cb_settingsReadSpeed.Size = new System.Drawing.Size(70, 21);
            this.cb_settingsReadSpeed.TabIndex = 4;
            this.cb_settingsReadSpeed.SelectedIndexChanged += new System.EventHandler(this.cb_settingsReadSpeed_SelectedIndexChanged);
            // 
            // l_settingsReadSpeed
            // 
            this.l_settingsReadSpeed.AutoSize = true;
            this.l_settingsReadSpeed.Enabled = false;
            this.l_settingsReadSpeed.Location = new System.Drawing.Point(7, 89);
            this.l_settingsReadSpeed.Name = "l_settingsReadSpeed";
            this.l_settingsReadSpeed.Size = new System.Drawing.Size(95, 13);
            this.l_settingsReadSpeed.TabIndex = 3;
            this.l_settingsReadSpeed.Text = "read speed (KiB/s)";
            // 
            // gb_connection
            // 
            this.gb_connection.Controls.Add(this.tb_host);
            this.gb_connection.Controls.Add(this.bt_shutdown);
            this.gb_connection.Controls.Add(this.tb_pw);
            this.gb_connection.Controls.Add(this.bt_connect);
            this.gb_connection.Controls.Add(this.cb_con);
            this.gb_connection.Controls.Add(this.bt_disconnect);
            this.gb_connection.Controls.Add(this.l_ip);
            this.gb_connection.Controls.Add(this.l_port);
            this.gb_connection.Controls.Add(this.tb_user);
            this.gb_connection.Controls.Add(this.l_pw);
            this.gb_connection.Controls.Add(this.tb_port);
            this.gb_connection.Controls.Add(this.l_user);
            this.gb_connection.Location = new System.Drawing.Point(4, 4);
            this.gb_connection.Name = "gb_connection";
            this.gb_connection.Size = new System.Drawing.Size(185, 178);
            this.gb_connection.TabIndex = 13;
            this.gb_connection.TabStop = false;
            this.gb_connection.Text = "Connection";
            // 
            // bt_shutdown
            // 
            this.bt_shutdown.Enabled = false;
            this.bt_shutdown.Location = new System.Drawing.Point(9, 149);
            this.bt_shutdown.Name = "bt_shutdown";
            this.bt_shutdown.Size = new System.Drawing.Size(77, 23);
            this.bt_shutdown.TabIndex = 12;
            this.bt_shutdown.Text = "shutdown";
            this.bt_shutdown.UseVisualStyleBackColor = true;
            this.bt_shutdown.Click += new System.EventHandler(this.bt_shutdown_Click);
            // 
            // bt_settingsClearLog
            // 
            this.bt_settingsClearLog.Location = new System.Drawing.Point(473, 179);
            this.bt_settingsClearLog.Name = "bt_settingsClearLog";
            this.bt_settingsClearLog.Size = new System.Drawing.Size(75, 23);
            this.bt_settingsClearLog.TabIndex = 5;
            this.bt_settingsClearLog.Text = "clear log";
            this.bt_settingsClearLog.UseVisualStyleBackColor = true;
            this.bt_settingsClearLog.Click += new System.EventHandler(this.bt_settingsClearLog_Click);
            // 
            // tp_chat
            // 
            this.tp_chat.Controls.Add(this.tabControl1);
            this.tp_chat.Location = new System.Drawing.Point(4, 22);
            this.tp_chat.Name = "tp_chat";
            this.tp_chat.Padding = new System.Windows.Forms.Padding(3);
            this.tp_chat.Size = new System.Drawing.Size(641, 512);
            this.tp_chat.TabIndex = 1;
            this.tp_chat.Text = "Chat";
            this.tp_chat.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tp_chat_chat);
            this.tabControl1.Controls.Add(this.tp_chat_autoResponse);
            this.tabControl1.Location = new System.Drawing.Point(6, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(629, 503);
            this.tabControl1.TabIndex = 16;
            // 
            // tp_chat_chat
            // 
            this.tp_chat_chat.Controls.Add(this.bt_leaveChatLobby);
            this.tp_chat_chat.Controls.Add(this.rtb_chat);
            this.tp_chat_chat.Controls.Add(this.bt_joinChatLobby);
            this.tp_chat_chat.Controls.Add(this.tb_chatMsg);
            this.tp_chat_chat.Controls.Add(this.bt_chatSend);
            this.tp_chat_chat.Controls.Add(this.clb_chatUser);
            this.tp_chat_chat.Controls.Add(this.clb_chatLobbies);
            this.tp_chat_chat.Controls.Add(this.gb_nickname);
            this.tp_chat_chat.Location = new System.Drawing.Point(4, 22);
            this.tp_chat_chat.Name = "tp_chat_chat";
            this.tp_chat_chat.Padding = new System.Windows.Forms.Padding(3);
            this.tp_chat_chat.Size = new System.Drawing.Size(621, 477);
            this.tp_chat_chat.TabIndex = 0;
            this.tp_chat_chat.Text = "Chat";
            this.tp_chat_chat.UseVisualStyleBackColor = true;
            // 
            // bt_leaveChatLobby
            // 
            this.bt_leaveChatLobby.Enabled = false;
            this.bt_leaveChatLobby.Location = new System.Drawing.Point(81, 149);
            this.bt_leaveChatLobby.Name = "bt_leaveChatLobby";
            this.bt_leaveChatLobby.Size = new System.Drawing.Size(65, 23);
            this.bt_leaveChatLobby.TabIndex = 12;
            this.bt_leaveChatLobby.Text = "leave";
            this.bt_leaveChatLobby.UseVisualStyleBackColor = true;
            this.bt_leaveChatLobby.Click += new System.EventHandler(this.bt_leaveChatLobby_Click);
            // 
            // rtb_chat
            // 
            this.rtb_chat.Location = new System.Drawing.Point(153, 3);
            this.rtb_chat.Name = "rtb_chat";
            this.rtb_chat.Size = new System.Drawing.Size(462, 360);
            this.rtb_chat.TabIndex = 7;
            this.rtb_chat.Text = "";
            // 
            // bt_joinChatLobby
            // 
            this.bt_joinChatLobby.Enabled = false;
            this.bt_joinChatLobby.Location = new System.Drawing.Point(6, 149);
            this.bt_joinChatLobby.Name = "bt_joinChatLobby";
            this.bt_joinChatLobby.Size = new System.Drawing.Size(69, 23);
            this.bt_joinChatLobby.TabIndex = 11;
            this.bt_joinChatLobby.Text = "join";
            this.bt_joinChatLobby.UseVisualStyleBackColor = true;
            this.bt_joinChatLobby.Click += new System.EventHandler(this.bt_joinChatLobby_Click);
            // 
            // tb_chatMsg
            // 
            this.tb_chatMsg.Location = new System.Drawing.Point(153, 371);
            this.tb_chatMsg.Name = "tb_chatMsg";
            this.tb_chatMsg.Size = new System.Drawing.Size(382, 20);
            this.tb_chatMsg.TabIndex = 3;
            this.tb_chatMsg.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_chatMsg_KeyUp);
            // 
            // bt_chatSend
            // 
            this.bt_chatSend.Location = new System.Drawing.Point(541, 369);
            this.bt_chatSend.Name = "bt_chatSend";
            this.bt_chatSend.Size = new System.Drawing.Size(74, 23);
            this.bt_chatSend.TabIndex = 4;
            this.bt_chatSend.Text = "Senden";
            this.bt_chatSend.UseVisualStyleBackColor = true;
            this.bt_chatSend.Click += new System.EventHandler(this.bt_chatSend_Click);
            // 
            // clb_chatUser
            // 
            this.clb_chatUser.Enabled = false;
            this.clb_chatUser.FormattingEnabled = true;
            this.clb_chatUser.Location = new System.Drawing.Point(6, 178);
            this.clb_chatUser.Name = "clb_chatUser";
            this.clb_chatUser.Size = new System.Drawing.Size(140, 289);
            this.clb_chatUser.TabIndex = 10;
            // 
            // clb_chatLobbies
            // 
            this.clb_chatLobbies.FormattingEnabled = true;
            this.clb_chatLobbies.Location = new System.Drawing.Point(6, 3);
            this.clb_chatLobbies.Name = "clb_chatLobbies";
            this.clb_chatLobbies.Size = new System.Drawing.Size(140, 139);
            this.clb_chatLobbies.TabIndex = 6;
            this.clb_chatLobbies.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clb_chatLobbies_ItemCheck);
            this.clb_chatLobbies.SelectedIndexChanged += new System.EventHandler(this.clb_chatLobbies_SelectedIndexChanged);
            this.clb_chatLobbies.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.clb_chatLobbies_MouseDoubleClick);
            // 
            // gb_nickname
            // 
            this.gb_nickname.Controls.Add(this.tb_chatNickname);
            this.gb_nickname.Controls.Add(this.bt_setNickname);
            this.gb_nickname.Location = new System.Drawing.Point(153, 397);
            this.gb_nickname.Name = "gb_nickname";
            this.gb_nickname.Size = new System.Drawing.Size(120, 74);
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
            // tp_chat_autoResponse
            // 
            this.tp_chat_autoResponse.Controls.Add(this.gb_chat_arOptions);
            this.tp_chat_autoResponse.Controls.Add(this.l_chat_arList);
            this.tp_chat_autoResponse.Controls.Add(this.clb_chat_arList);
            this.tp_chat_autoResponse.Controls.Add(this.cb_chat_arEnable);
            this.tp_chat_autoResponse.Location = new System.Drawing.Point(4, 22);
            this.tp_chat_autoResponse.Name = "tp_chat_autoResponse";
            this.tp_chat_autoResponse.Padding = new System.Windows.Forms.Padding(3);
            this.tp_chat_autoResponse.Size = new System.Drawing.Size(621, 477);
            this.tp_chat_autoResponse.TabIndex = 1;
            this.tp_chat_autoResponse.Text = "Auto Response System";
            this.tp_chat_autoResponse.UseVisualStyleBackColor = true;
            // 
            // gb_chat_arOptions
            // 
            this.gb_chat_arOptions.Controls.Add(this.bt_chat_arRemove);
            this.gb_chat_arOptions.Controls.Add(this.bt_chat_arNew);
            this.gb_chat_arOptions.Controls.Add(this.bt_chat_arSave);
            this.gb_chat_arOptions.Controls.Add(this.tb_chat_arPrefix);
            this.gb_chat_arOptions.Controls.Add(this.l_chat_arPrefix);
            this.gb_chat_arOptions.Controls.Add(this.l_chat_arUsesFunction);
            this.gb_chat_arOptions.Controls.Add(this.cb_chat_arUsesFunction);
            this.gb_chat_arOptions.Controls.Add(this.l_chat_arAnswer);
            this.gb_chat_arOptions.Controls.Add(this.l_chat_arSearchFor);
            this.gb_chat_arOptions.Controls.Add(this.tb_chat_arAnswer);
            this.gb_chat_arOptions.Controls.Add(this.tb_chat_arSearchFor);
            this.gb_chat_arOptions.Controls.Add(this.tb_chat_arName);
            this.gb_chat_arOptions.Controls.Add(this.cb_chat_arCaseSensitive);
            this.gb_chat_arOptions.Controls.Add(this.cb_chat_arWithSpaces);
            this.gb_chat_arOptions.Controls.Add(this.cb_chat_arOnly);
            this.gb_chat_arOptions.Controls.Add(this.l_chat_arName);
            this.gb_chat_arOptions.Location = new System.Drawing.Point(132, 6);
            this.gb_chat_arOptions.Name = "gb_chat_arOptions";
            this.gb_chat_arOptions.Size = new System.Drawing.Size(383, 181);
            this.gb_chat_arOptions.TabIndex = 3;
            this.gb_chat_arOptions.TabStop = false;
            this.gb_chat_arOptions.Text = "Auto Response Options";
            // 
            // bt_chat_arRemove
            // 
            this.bt_chat_arRemove.Location = new System.Drawing.Point(172, 149);
            this.bt_chat_arRemove.Name = "bt_chat_arRemove";
            this.bt_chat_arRemove.Size = new System.Drawing.Size(75, 23);
            this.bt_chat_arRemove.TabIndex = 15;
            this.bt_chat_arRemove.Text = "remove";
            this.bt_chat_arRemove.UseVisualStyleBackColor = true;
            this.bt_chat_arRemove.Click += new System.EventHandler(this.bt_chat_arRemove_Click);
            // 
            // bt_chat_arNew
            // 
            this.bt_chat_arNew.Location = new System.Drawing.Point(90, 149);
            this.bt_chat_arNew.Name = "bt_chat_arNew";
            this.bt_chat_arNew.Size = new System.Drawing.Size(75, 23);
            this.bt_chat_arNew.TabIndex = 14;
            this.bt_chat_arNew.Text = "add new";
            this.bt_chat_arNew.UseVisualStyleBackColor = true;
            this.bt_chat_arNew.Click += new System.EventHandler(this.bt_chat_arNew_Click);
            // 
            // bt_chat_arSave
            // 
            this.bt_chat_arSave.Location = new System.Drawing.Point(9, 149);
            this.bt_chat_arSave.Name = "bt_chat_arSave";
            this.bt_chat_arSave.Size = new System.Drawing.Size(75, 23);
            this.bt_chat_arSave.TabIndex = 13;
            this.bt_chat_arSave.Text = "Save";
            this.bt_chat_arSave.UseVisualStyleBackColor = true;
            this.bt_chat_arSave.Click += new System.EventHandler(this.bt_chat_arSave_Click);
            // 
            // tb_chat_arPrefix
            // 
            this.tb_chat_arPrefix.Location = new System.Drawing.Point(66, 72);
            this.tb_chat_arPrefix.Name = "tb_chat_arPrefix";
            this.tb_chat_arPrefix.Size = new System.Drawing.Size(20, 20);
            this.tb_chat_arPrefix.TabIndex = 6;
            // 
            // l_chat_arPrefix
            // 
            this.l_chat_arPrefix.AutoSize = true;
            this.l_chat_arPrefix.Location = new System.Drawing.Point(6, 75);
            this.l_chat_arPrefix.Name = "l_chat_arPrefix";
            this.l_chat_arPrefix.Size = new System.Drawing.Size(32, 13);
            this.l_chat_arPrefix.TabIndex = 11;
            this.l_chat_arPrefix.Text = "prefix";
            // 
            // l_chat_arUsesFunction
            // 
            this.l_chat_arUsesFunction.AutoSize = true;
            this.l_chat_arUsesFunction.Location = new System.Drawing.Point(293, 49);
            this.l_chat_arUsesFunction.Name = "l_chat_arUsesFunction";
            this.l_chat_arUsesFunction.Size = new System.Drawing.Size(70, 13);
            this.l_chat_arUsesFunction.TabIndex = 10;
            this.l_chat_arUsesFunction.Text = "uses function";
            // 
            // cb_chat_arUsesFunction
            // 
            this.cb_chat_arUsesFunction.AutoSize = true;
            this.cb_chat_arUsesFunction.Enabled = false;
            this.cb_chat_arUsesFunction.Location = new System.Drawing.Point(277, 48);
            this.cb_chat_arUsesFunction.Name = "cb_chat_arUsesFunction";
            this.cb_chat_arUsesFunction.Size = new System.Drawing.Size(15, 14);
            this.cb_chat_arUsesFunction.TabIndex = 9;
            this.cb_chat_arUsesFunction.UseVisualStyleBackColor = true;
            // 
            // l_chat_arAnswer
            // 
            this.l_chat_arAnswer.AutoSize = true;
            this.l_chat_arAnswer.Location = new System.Drawing.Point(6, 101);
            this.l_chat_arAnswer.Name = "l_chat_arAnswer";
            this.l_chat_arAnswer.Size = new System.Drawing.Size(41, 13);
            this.l_chat_arAnswer.TabIndex = 8;
            this.l_chat_arAnswer.Text = "answer";
            // 
            // l_chat_arSearchFor
            // 
            this.l_chat_arSearchFor.AutoSize = true;
            this.l_chat_arSearchFor.Location = new System.Drawing.Point(6, 48);
            this.l_chat_arSearchFor.Name = "l_chat_arSearchFor";
            this.l_chat_arSearchFor.Size = new System.Drawing.Size(54, 13);
            this.l_chat_arSearchFor.TabIndex = 7;
            this.l_chat_arSearchFor.Text = "search for";
            // 
            // tb_chat_arAnswer
            // 
            this.tb_chat_arAnswer.Location = new System.Drawing.Point(66, 98);
            this.tb_chat_arAnswer.Multiline = true;
            this.tb_chat_arAnswer.Name = "tb_chat_arAnswer";
            this.tb_chat_arAnswer.Size = new System.Drawing.Size(297, 45);
            this.tb_chat_arAnswer.TabIndex = 7;
            // 
            // tb_chat_arSearchFor
            // 
            this.tb_chat_arSearchFor.Location = new System.Drawing.Point(66, 45);
            this.tb_chat_arSearchFor.Name = "tb_chat_arSearchFor";
            this.tb_chat_arSearchFor.Size = new System.Drawing.Size(109, 20);
            this.tb_chat_arSearchFor.TabIndex = 5;
            // 
            // tb_chat_arName
            // 
            this.tb_chat_arName.Location = new System.Drawing.Point(66, 19);
            this.tb_chat_arName.Name = "tb_chat_arName";
            this.tb_chat_arName.Size = new System.Drawing.Size(109, 20);
            this.tb_chat_arName.TabIndex = 4;
            // 
            // cb_chat_arCaseSensitive
            // 
            this.cb_chat_arCaseSensitive.AutoSize = true;
            this.cb_chat_arCaseSensitive.Location = new System.Drawing.Point(181, 48);
            this.cb_chat_arCaseSensitive.Name = "cb_chat_arCaseSensitive";
            this.cb_chat_arCaseSensitive.Size = new System.Drawing.Size(93, 17);
            this.cb_chat_arCaseSensitive.TabIndex = 3;
            this.cb_chat_arCaseSensitive.Text = "case sensitive";
            this.cb_chat_arCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // cb_chat_arWithSpaces
            // 
            this.cb_chat_arWithSpaces.AutoSize = true;
            this.cb_chat_arWithSpaces.Location = new System.Drawing.Point(277, 21);
            this.cb_chat_arWithSpaces.Name = "cb_chat_arWithSpaces";
            this.cb_chat_arWithSpaces.Size = new System.Drawing.Size(73, 17);
            this.cb_chat_arWithSpaces.TabIndex = 2;
            this.cb_chat_arWithSpaces.Text = "separated";
            this.cb_chat_arWithSpaces.UseVisualStyleBackColor = true;
            // 
            // cb_chat_arOnly
            // 
            this.cb_chat_arOnly.AutoSize = true;
            this.cb_chat_arOnly.Location = new System.Drawing.Point(181, 21);
            this.cb_chat_arOnly.Name = "cb_chat_arOnly";
            this.cb_chat_arOnly.Size = new System.Drawing.Size(90, 17);
            this.cb_chat_arOnly.TabIndex = 1;
            this.cb_chat_arOnly.Text = "forbid context";
            this.cb_chat_arOnly.UseVisualStyleBackColor = true;
            // 
            // l_chat_arName
            // 
            this.l_chat_arName.AutoSize = true;
            this.l_chat_arName.Location = new System.Drawing.Point(6, 22);
            this.l_chat_arName.Name = "l_chat_arName";
            this.l_chat_arName.Size = new System.Drawing.Size(35, 13);
            this.l_chat_arName.TabIndex = 0;
            this.l_chat_arName.Text = "Name";
            // 
            // l_chat_arList
            // 
            this.l_chat_arList.AutoSize = true;
            this.l_chat_arList.Location = new System.Drawing.Point(6, 46);
            this.l_chat_arList.Name = "l_chat_arList";
            this.l_chat_arList.Size = new System.Drawing.Size(85, 13);
            this.l_chat_arList.TabIndex = 2;
            this.l_chat_arList.Text = "Auto Responses";
            // 
            // clb_chat_arList
            // 
            this.clb_chat_arList.FormattingEnabled = true;
            this.clb_chat_arList.Location = new System.Drawing.Point(6, 62);
            this.clb_chat_arList.Name = "clb_chat_arList";
            this.clb_chat_arList.Size = new System.Drawing.Size(120, 409);
            this.clb_chat_arList.TabIndex = 1;
            this.clb_chat_arList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.clb_chat_arList_ItemCheck);
            this.clb_chat_arList.SelectedIndexChanged += new System.EventHandler(this.clb_chat_arList_SelectedIndexChanged);
            // 
            // cb_chat_arEnable
            // 
            this.cb_chat_arEnable.AutoSize = true;
            this.cb_chat_arEnable.Location = new System.Drawing.Point(6, 6);
            this.cb_chat_arEnable.Name = "cb_chat_arEnable";
            this.cb_chat_arEnable.Size = new System.Drawing.Size(58, 17);
            this.cb_chat_arEnable.TabIndex = 0;
            this.cb_chat_arEnable.Text = "enable";
            this.cb_chat_arEnable.UseVisualStyleBackColor = true;
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
            this.gb_peerNew.Enabled = false;
            this.gb_peerNew.Location = new System.Drawing.Point(207, 207);
            this.gb_peerNew.Name = "gb_peerNew";
            this.gb_peerNew.Size = new System.Drawing.Size(428, 298);
            this.gb_peerNew.TabIndex = 28;
            this.gb_peerNew.TabStop = false;
            this.gb_peerNew.Text = "Add a new friend";
            // 
            // tb_peedNew
            // 
            this.tb_peedNew.Location = new System.Drawing.Point(6, 19);
            this.tb_peedNew.Multiline = true;
            this.tb_peedNew.Name = "tb_peedNew";
            this.tb_peedNew.Size = new System.Drawing.Size(413, 186);
            this.tb_peedNew.TabIndex = 26;
            // 
            // gb_peerInfo
            // 
            this.gb_peerInfo.Controls.Add(this.bt_peerRemove);
            this.gb_peerInfo.Controls.Add(this.lb_locations);
            this.gb_peerInfo.Controls.Add(this.tb_peerName);
            this.gb_peerInfo.Controls.Add(this.gb_ip);
            this.gb_peerInfo.Controls.Add(this.tb_peerID);
            this.gb_peerInfo.Controls.Add(this.tb_peerLocation);
            this.gb_peerInfo.Controls.Add(this.l_peerName);
            this.gb_peerInfo.Controls.Add(this.bt_peerSave);
            this.gb_peerInfo.Controls.Add(this.l_peerID);
            this.gb_peerInfo.Controls.Add(this.l_peerLocationID);
            this.gb_peerInfo.Controls.Add(this.tb_peerLocationID);
            this.gb_peerInfo.Controls.Add(this.l_peerLocation);
            this.gb_peerInfo.Location = new System.Drawing.Point(207, 7);
            this.gb_peerInfo.Name = "gb_peerInfo";
            this.gb_peerInfo.Size = new System.Drawing.Size(428, 194);
            this.gb_peerInfo.TabIndex = 27;
            this.gb_peerInfo.TabStop = false;
            this.gb_peerInfo.Text = "Information";
            // 
            // bt_peerRemove
            // 
            this.bt_peerRemove.Enabled = false;
            this.bt_peerRemove.Location = new System.Drawing.Point(321, 18);
            this.bt_peerRemove.Name = "bt_peerRemove";
            this.bt_peerRemove.Size = new System.Drawing.Size(98, 23);
            this.bt_peerRemove.TabIndex = 25;
            this.bt_peerRemove.Text = "Block/Remove";
            this.bt_peerRemove.UseVisualStyleBackColor = true;
            this.bt_peerRemove.Click += new System.EventHandler(this.bt_peerRemove_Click);
            // 
            // tp_files
            // 
            this.tp_files.Controls.Add(this.tv_files);
            this.tp_files.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tp_files.Location = new System.Drawing.Point(4, 22);
            this.tp_files.Name = "tp_files";
            this.tp_files.Size = new System.Drawing.Size(641, 512);
            this.tp_files.TabIndex = 2;
            this.tp_files.Text = "Files";
            this.tp_files.UseVisualStyleBackColor = true;
            // 
            // tv_files
            // 
            this.tv_files.Controls.Add(this.tp_filesDownload);
            this.tv_files.Controls.Add(this.tp_filesUploads);
            this.tv_files.Location = new System.Drawing.Point(3, 3);
            this.tv_files.Name = "tv_files";
            this.tv_files.SelectedIndex = 0;
            this.tv_files.Size = new System.Drawing.Size(627, 506);
            this.tv_files.TabIndex = 13;
            // 
            // tp_filesDownload
            // 
            this.tp_filesDownload.Controls.Add(this.lb_filesDownloads);
            this.tp_filesDownload.Controls.Add(this.bt_filesAddCollection);
            this.tp_filesDownload.Controls.Add(this.bt_filesPause);
            this.tp_filesDownload.Controls.Add(this.dgv_filesDownloads);
            this.tp_filesDownload.Controls.Add(this.bt_filesCancel);
            this.tp_filesDownload.Controls.Add(this.bt_filesContinue);
            this.tp_filesDownload.Controls.Add(this.bt_filesWait);
            this.tp_filesDownload.Controls.Add(this.bt_filesForceCheck);
            this.tp_filesDownload.Controls.Add(this.bt_filesRestart);
            this.tp_filesDownload.Location = new System.Drawing.Point(4, 22);
            this.tp_filesDownload.Name = "tp_filesDownload";
            this.tp_filesDownload.Padding = new System.Windows.Forms.Padding(3);
            this.tp_filesDownload.Size = new System.Drawing.Size(619, 480);
            this.tp_filesDownload.TabIndex = 0;
            this.tp_filesDownload.Text = "Downloads";
            this.tp_filesDownload.UseVisualStyleBackColor = true;
            // 
            // lb_filesDownloads
            // 
            this.lb_filesDownloads.FormattingEnabled = true;
            this.lb_filesDownloads.Location = new System.Drawing.Point(6, 6);
            this.lb_filesDownloads.Name = "lb_filesDownloads";
            this.lb_filesDownloads.Size = new System.Drawing.Size(607, 342);
            this.lb_filesDownloads.TabIndex = 0;
            this.lb_filesDownloads.SelectedIndexChanged += new System.EventHandler(this.lb_filesDownloads_SelectedIndexChanged);
            // 
            // bt_filesAddCollection
            // 
            this.bt_filesAddCollection.Enabled = false;
            this.bt_filesAddCollection.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.85F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_filesAddCollection.Location = new System.Drawing.Point(524, 451);
            this.bt_filesAddCollection.Name = "bt_filesAddCollection";
            this.bt_filesAddCollection.Size = new System.Drawing.Size(89, 23);
            this.bt_filesAddCollection.TabIndex = 10;
            this.bt_filesAddCollection.Text = "add collection";
            this.bt_filesAddCollection.UseVisualStyleBackColor = true;
            this.bt_filesAddCollection.Click += new System.EventHandler(this.bt_filesAddCollection_Click);
            // 
            // bt_filesPause
            // 
            this.bt_filesPause.Location = new System.Drawing.Point(6, 451);
            this.bt_filesPause.Name = "bt_filesPause";
            this.bt_filesPause.Size = new System.Drawing.Size(66, 23);
            this.bt_filesPause.TabIndex = 4;
            this.bt_filesPause.Text = "pause";
            this.bt_filesPause.UseVisualStyleBackColor = true;
            this.bt_filesPause.Click += new System.EventHandler(this.bt_filesPause_Click);
            // 
            // dgv_filesDownloads
            // 
            this.dgv_filesDownloads.AllowUserToAddRows = false;
            this.dgv_filesDownloads.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_filesDownloads.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_filesDownloads.ColumnHeadersHeight = 21;
            this.dgv_filesDownloads.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dlSelect,
            this.dlDone,
            this.dlSpeed,
            this.dlName,
            this.dlSize,
            this.dlHash});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_filesDownloads.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_filesDownloads.Enabled = false;
            this.dgv_filesDownloads.Location = new System.Drawing.Point(6, 356);
            this.dgv_filesDownloads.Name = "dgv_filesDownloads";
            this.dgv_filesDownloads.ReadOnly = true;
            this.dgv_filesDownloads.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_filesDownloads.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_filesDownloads.Size = new System.Drawing.Size(607, 89);
            this.dgv_filesDownloads.TabIndex = 11;
            // 
            // dlSelect
            // 
            this.dlSelect.HeaderText = "select";
            this.dlSelect.Name = "dlSelect";
            this.dlSelect.ReadOnly = true;
            this.dlSelect.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dlSelect.Visible = false;
            this.dlSelect.Width = 40;
            // 
            // dlDone
            // 
            this.dlDone.HeaderText = "% done";
            this.dlDone.Name = "dlDone";
            this.dlDone.ReadOnly = true;
            this.dlDone.Width = 50;
            // 
            // dlSpeed
            // 
            this.dlSpeed.HeaderText = "speed [KiB/s]";
            this.dlSpeed.Name = "dlSpeed";
            this.dlSpeed.ReadOnly = true;
            // 
            // dlName
            // 
            this.dlName.HeaderText = "file name";
            this.dlName.Name = "dlName";
            this.dlName.ReadOnly = true;
            this.dlName.Width = 314;
            // 
            // dlSize
            // 
            this.dlSize.HeaderText = "file size";
            this.dlSize.Name = "dlSize";
            this.dlSize.ReadOnly = true;
            // 
            // dlHash
            // 
            this.dlHash.HeaderText = "hash";
            this.dlHash.Name = "dlHash";
            this.dlHash.ReadOnly = true;
            this.dlHash.Visible = false;
            // 
            // bt_filesCancel
            // 
            this.bt_filesCancel.Location = new System.Drawing.Point(429, 451);
            this.bt_filesCancel.Name = "bt_filesCancel";
            this.bt_filesCancel.Size = new System.Drawing.Size(89, 23);
            this.bt_filesCancel.TabIndex = 5;
            this.bt_filesCancel.Text = "cancel";
            this.bt_filesCancel.UseVisualStyleBackColor = true;
            this.bt_filesCancel.Click += new System.EventHandler(this.bt_filesCancel_Click);
            // 
            // bt_filesContinue
            // 
            this.bt_filesContinue.Location = new System.Drawing.Point(239, 451);
            this.bt_filesContinue.Name = "bt_filesContinue";
            this.bt_filesContinue.Size = new System.Drawing.Size(89, 23);
            this.bt_filesContinue.TabIndex = 6;
            this.bt_filesContinue.Text = "queue top";
            this.bt_filesContinue.UseVisualStyleBackColor = true;
            this.bt_filesContinue.Click += new System.EventHandler(this.bt_filesContinue_Click);
            // 
            // bt_filesWait
            // 
            this.bt_filesWait.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_filesWait.Location = new System.Drawing.Point(144, 451);
            this.bt_filesWait.Name = "bt_filesWait";
            this.bt_filesWait.Size = new System.Drawing.Size(89, 23);
            this.bt_filesWait.TabIndex = 9;
            this.bt_filesWait.Text = "queue bottom";
            this.bt_filesWait.UseVisualStyleBackColor = true;
            this.bt_filesWait.Click += new System.EventHandler(this.bt_filesWait_Click);
            // 
            // bt_filesForceCheck
            // 
            this.bt_filesForceCheck.Location = new System.Drawing.Point(334, 451);
            this.bt_filesForceCheck.Name = "bt_filesForceCheck";
            this.bt_filesForceCheck.Size = new System.Drawing.Size(89, 23);
            this.bt_filesForceCheck.TabIndex = 7;
            this.bt_filesForceCheck.Text = "force check";
            this.bt_filesForceCheck.UseVisualStyleBackColor = true;
            this.bt_filesForceCheck.Click += new System.EventHandler(this.bt_filesForceCheck_Click);
            // 
            // bt_filesRestart
            // 
            this.bt_filesRestart.Location = new System.Drawing.Point(78, 451);
            this.bt_filesRestart.Name = "bt_filesRestart";
            this.bt_filesRestart.Size = new System.Drawing.Size(60, 23);
            this.bt_filesRestart.TabIndex = 8;
            this.bt_filesRestart.Text = "restart";
            this.bt_filesRestart.UseVisualStyleBackColor = true;
            this.bt_filesRestart.Click += new System.EventHandler(this.bt_filesRestart_Click);
            // 
            // tp_filesUploads
            // 
            this.tp_filesUploads.Controls.Add(this.dgv_filesUploads);
            this.tp_filesUploads.Controls.Add(this.lb_filesUploads);
            this.tp_filesUploads.Location = new System.Drawing.Point(4, 22);
            this.tp_filesUploads.Name = "tp_filesUploads";
            this.tp_filesUploads.Padding = new System.Windows.Forms.Padding(3);
            this.tp_filesUploads.Size = new System.Drawing.Size(619, 480);
            this.tp_filesUploads.TabIndex = 1;
            this.tp_filesUploads.Text = "Uploads";
            this.tp_filesUploads.UseVisualStyleBackColor = true;
            // 
            // dgv_filesUploads
            // 
            this.dgv_filesUploads.AllowUserToAddRows = false;
            this.dgv_filesUploads.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_filesUploads.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_filesUploads.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_filesUploads.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ulSpeed,
            this.ulName,
            this.ulSize,
            this.ulHash});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_filesUploads.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_filesUploads.Enabled = false;
            this.dgv_filesUploads.Location = new System.Drawing.Point(6, 375);
            this.dgv_filesUploads.Name = "dgv_filesUploads";
            this.dgv_filesUploads.ReadOnly = true;
            this.dgv_filesUploads.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_filesUploads.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_filesUploads.Size = new System.Drawing.Size(607, 99);
            this.dgv_filesUploads.TabIndex = 12;
            // 
            // ulSpeed
            // 
            this.ulSpeed.HeaderText = "speed [KiB/s]";
            this.ulSpeed.Name = "ulSpeed";
            this.ulSpeed.ReadOnly = true;
            this.ulSpeed.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ulSpeed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ulName
            // 
            this.ulName.HeaderText = "file name";
            this.ulName.Name = "ulName";
            this.ulName.ReadOnly = true;
            this.ulName.Width = 364;
            // 
            // ulSize
            // 
            this.ulSize.HeaderText = "file size";
            this.ulSize.Name = "ulSize";
            this.ulSize.ReadOnly = true;
            // 
            // ulHash
            // 
            this.ulHash.HeaderText = "hash";
            this.ulHash.Name = "ulHash";
            this.ulHash.ReadOnly = true;
            this.ulHash.Visible = false;
            // 
            // lb_filesUploads
            // 
            this.lb_filesUploads.FormattingEnabled = true;
            this.lb_filesUploads.Location = new System.Drawing.Point(6, 6);
            this.lb_filesUploads.Name = "lb_filesUploads";
            this.lb_filesUploads.Size = new System.Drawing.Size(607, 355);
            this.lb_filesUploads.TabIndex = 1;
            this.lb_filesUploads.SelectedIndexChanged += new System.EventHandler(this.lb_filesUploads_SelectedIndexChanged);
            // 
            // tb_search
            // 
            this.tb_search.Controls.Add(this.bt_searchAddToDL);
            this.tb_search.Controls.Add(this.bt_searchRemove);
            this.tb_search.Controls.Add(this.lb_searches);
            this.tb_search.Controls.Add(this.lb_searchResults);
            this.tb_search.Controls.Add(this.tb_searchKeyWords);
            this.tb_search.Controls.Add(this.bt_searchSearch);
            this.tb_search.Controls.Add(this.l_searchKeyWords);
            this.tb_search.Location = new System.Drawing.Point(4, 22);
            this.tb_search.Name = "tb_search";
            this.tb_search.Size = new System.Drawing.Size(641, 512);
            this.tb_search.TabIndex = 3;
            this.tb_search.Text = "Search";
            this.tb_search.UseVisualStyleBackColor = true;
            // 
            // bt_searchAddToDL
            // 
            this.bt_searchAddToDL.Location = new System.Drawing.Point(538, 458);
            this.bt_searchAddToDL.Name = "bt_searchAddToDL";
            this.bt_searchAddToDL.Size = new System.Drawing.Size(100, 23);
            this.bt_searchAddToDL.TabIndex = 6;
            this.bt_searchAddToDL.Text = "add to downloads";
            this.bt_searchAddToDL.UseVisualStyleBackColor = true;
            this.bt_searchAddToDL.Click += new System.EventHandler(this.bt_searchAddToDL_Click);
            // 
            // bt_searchRemove
            // 
            this.bt_searchRemove.Location = new System.Drawing.Point(3, 458);
            this.bt_searchRemove.Name = "bt_searchRemove";
            this.bt_searchRemove.Size = new System.Drawing.Size(143, 23);
            this.bt_searchRemove.TabIndex = 5;
            this.bt_searchRemove.Text = "remove selected search";
            this.bt_searchRemove.UseVisualStyleBackColor = true;
            this.bt_searchRemove.Click += new System.EventHandler(this.bt_searchRemove_Click);
            // 
            // lb_searches
            // 
            this.lb_searches.FormattingEnabled = true;
            this.lb_searches.Location = new System.Drawing.Point(3, 32);
            this.lb_searches.Name = "lb_searches";
            this.lb_searches.Size = new System.Drawing.Size(143, 420);
            this.lb_searches.TabIndex = 4;
            this.lb_searches.SelectedIndexChanged += new System.EventHandler(this.lb_searches_SelectedIndexChanged);
            // 
            // lb_searchResults
            // 
            this.lb_searchResults.FormattingEnabled = true;
            this.lb_searchResults.Location = new System.Drawing.Point(152, 32);
            this.lb_searchResults.Name = "lb_searchResults";
            this.lb_searchResults.Size = new System.Drawing.Size(486, 420);
            this.lb_searchResults.TabIndex = 3;
            // 
            // tb_searchKeyWords
            // 
            this.tb_searchKeyWords.Location = new System.Drawing.Point(64, 5);
            this.tb_searchKeyWords.Name = "tb_searchKeyWords";
            this.tb_searchKeyWords.Size = new System.Drawing.Size(493, 20);
            this.tb_searchKeyWords.TabIndex = 2;
            this.tb_searchKeyWords.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_searchKeyWords_KeyUp);
            // 
            // bt_searchSearch
            // 
            this.bt_searchSearch.Location = new System.Drawing.Point(563, 3);
            this.bt_searchSearch.Name = "bt_searchSearch";
            this.bt_searchSearch.Size = new System.Drawing.Size(75, 23);
            this.bt_searchSearch.TabIndex = 1;
            this.bt_searchSearch.Text = "search";
            this.bt_searchSearch.UseVisualStyleBackColor = false;
            this.bt_searchSearch.Click += new System.EventHandler(this.bt_searchSearch_Click);
            // 
            // l_searchKeyWords
            // 
            this.l_searchKeyWords.AutoSize = true;
            this.l_searchKeyWords.Location = new System.Drawing.Point(3, 8);
            this.l_searchKeyWords.Name = "l_searchKeyWords";
            this.l_searchKeyWords.Size = new System.Drawing.Size(55, 13);
            this.l_searchKeyWords.TabIndex = 0;
            this.l_searchKeyWords.Text = "key words";
            // 
            // ofd_collection
            // 
            this.ofd_collection.FileName = "openFileDialog1";
            this.ofd_collection.Filter = "Collection (.rscollection)|*.rscollection";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 562);
            this.Controls.Add(this.tc_main);
            this.Name = "MainForm";
            this.Text = "RetroShare SSH Client by sehraf";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.gb_systemStatus.ResumeLayout(false);
            this.gb_systemStatus.PerformLayout();
            this.gb_ip.ResumeLayout(false);
            this.gb_ip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_peerPortExt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_peerPortInt)).EndInit();
            this.tc_main.ResumeLayout(false);
            this.tp_setup.ResumeLayout(false);
            this.tp_setup.PerformLayout();
            this.gb_settings.ResumeLayout(false);
            this.gb_settings.PerformLayout();
            this.gb_connection.ResumeLayout(false);
            this.gb_connection.PerformLayout();
            this.tp_chat.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tp_chat_chat.ResumeLayout(false);
            this.tp_chat_chat.PerformLayout();
            this.gb_nickname.ResumeLayout(false);
            this.gb_nickname.PerformLayout();
            this.tp_chat_autoResponse.ResumeLayout(false);
            this.tp_chat_autoResponse.PerformLayout();
            this.gb_chat_arOptions.ResumeLayout(false);
            this.gb_chat_arOptions.PerformLayout();
            this.tp_friends.ResumeLayout(false);
            this.gb_peerNew.ResumeLayout(false);
            this.gb_peerNew.PerformLayout();
            this.gb_peerInfo.ResumeLayout(false);
            this.gb_peerInfo.PerformLayout();
            this.tp_files.ResumeLayout(false);
            this.tv_files.ResumeLayout(false);
            this.tp_filesDownload.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_filesDownloads)).EndInit();
            this.tp_filesUploads.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_filesUploads)).EndInit();
            this.tb_search.ResumeLayout(false);
            this.tb_search.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.GroupBox gb_systemStatus;
        private System.Windows.Forms.Timer t_tick;
        private System.Windows.Forms.Label l_bwDown;
        private System.Windows.Forms.Label l_bwUp;
        private System.Windows.Forms.Label l_network;
        private System.Windows.Forms.Label l_peers;
        private System.Windows.Forms.Label l_connected;
        internal System.Windows.Forms.ListBox lb_friends;
        private System.Windows.Forms.Button bt_peerSave;
        private System.Windows.Forms.GroupBox gb_ip;
        internal System.Windows.Forms.TextBox tb_peerIPInt;
        internal System.Windows.Forms.TextBox tb_peerIPExt;
        internal System.Windows.Forms.NumericUpDown nud_peerPortExt;
        internal System.Windows.Forms.NumericUpDown nud_peerPortInt;
        internal System.Windows.Forms.TextBox tb_peerLocationID;
        internal System.Windows.Forms.ListBox lb_locations;
        internal System.Windows.Forms.TextBox tb_peerLocation;
        internal System.Windows.Forms.TextBox tb_peerName;
        internal System.Windows.Forms.TextBox tb_peerID;
        private System.Windows.Forms.Label l_peerName;
        private System.Windows.Forms.Label l_peerID;
        private System.Windows.Forms.Label l_peerLocation;
        private System.Windows.Forms.Label l_peerLocationID;
        internal System.Windows.Forms.TextBox tb_dyndns;
        private System.Windows.Forms.Button bt_peerNew;
        private System.Windows.Forms.Button bt_test;
        private System.Windows.Forms.TabControl tc_main;
        private System.Windows.Forms.TabPage tp_friends;
        private System.Windows.Forms.TabPage tp_chat;
        private System.Windows.Forms.TabPage tp_files;
        private System.Windows.Forms.GroupBox gb_peerNew;
        internal System.Windows.Forms.TextBox tb_peedNew;
        private System.Windows.Forms.GroupBox gb_peerInfo;
        private System.Windows.Forms.Button bt_chatSend;
        internal System.Windows.Forms.TextBox tb_chatMsg;
        internal System.Windows.Forms.CheckedListBox clb_chatLobbies;
        internal System.Windows.Forms.RichTextBox rtb_chat;
        internal System.Windows.Forms.GroupBox gb_nickname;
        internal System.Windows.Forms.TextBox tb_chatNickname;
        private System.Windows.Forms.Button bt_setNickname;
        private System.Windows.Forms.Button bt_peerRemove;
        internal System.Windows.Forms.CheckedListBox clb_chatUser;
        private System.Windows.Forms.CheckBox cb_settingsSave;
        private System.Windows.Forms.CheckBox cb_settingsSavePW;
        private System.Windows.Forms.CheckBox cb_settingsSaveChat;
        private System.Windows.Forms.Label l_peerIPExt;
        private System.Windows.Forms.Label l_peerDynDns;
        private System.Windows.Forms.Label l_peerIPInt;
        private System.Windows.Forms.Button bt_shutdown;
        internal System.Windows.Forms.ListBox lb_filesUploads;
        internal System.Windows.Forms.ListBox lb_filesDownloads;
        private System.Windows.Forms.TabPage tb_search;
        private System.Windows.Forms.Button bt_filesContinue;
        private System.Windows.Forms.Button bt_filesCancel;
        private System.Windows.Forms.Button bt_filesPause;
        private System.Windows.Forms.Button bt_filesRestart;
        private System.Windows.Forms.Button bt_filesForceCheck;
        private System.Windows.Forms.Button bt_filesWait;
        internal System.Windows.Forms.TextBox tb_searchKeyWords;
        private System.Windows.Forms.Button bt_searchSearch;
        private System.Windows.Forms.Label l_searchKeyWords;
        internal System.Windows.Forms.ListBox lb_searchResults;
        internal System.Windows.Forms.ListBox lb_searches;
        private System.Windows.Forms.Button bt_searchAddToDL;
        private System.Windows.Forms.Button bt_searchRemove;
        private System.Windows.Forms.Button bt_filesAddCollection;
        private System.Windows.Forms.OpenFileDialog ofd_collection;
        private System.Windows.Forms.ComboBox cb_settingsReadSpeed;
        private System.Windows.Forms.Label l_settingsReadSpeed;
        private System.Windows.Forms.Button bt_settingsClearLog;
        private System.Windows.Forms.Button bt_leaveChatLobby;
        private System.Windows.Forms.Button bt_joinChatLobby;
        private System.Windows.Forms.TabPage tp_setup;
        private System.Windows.Forms.GroupBox gb_settings;
        private System.Windows.Forms.GroupBox gb_connection;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tp_chat_chat;
        private System.Windows.Forms.TabPage tp_chat_autoResponse;
        private System.Windows.Forms.GroupBox gb_chat_arOptions;
        private System.Windows.Forms.Label l_chat_arName;
        private System.Windows.Forms.Label l_chat_arList;
        private System.Windows.Forms.Label l_chat_arAnswer;
        private System.Windows.Forms.Label l_chat_arSearchFor;
        private System.Windows.Forms.Label l_chat_arUsesFunction;
        internal System.Windows.Forms.TextBox tb_chat_arName;
        internal System.Windows.Forms.CheckBox cb_chat_arCaseSensitive;
        internal System.Windows.Forms.CheckBox cb_chat_arWithSpaces;
        internal System.Windows.Forms.CheckBox cb_chat_arOnly;
        internal System.Windows.Forms.CheckedListBox clb_chat_arList;
        internal System.Windows.Forms.TextBox tb_chat_arAnswer;
        internal System.Windows.Forms.TextBox tb_chat_arSearchFor;
        internal System.Windows.Forms.CheckBox cb_chat_arUsesFunction;
        private System.Windows.Forms.Label l_chat_arPrefix;
        internal System.Windows.Forms.TextBox tb_chat_arPrefix;
        private System.Windows.Forms.Button bt_chat_arNew;
        private System.Windows.Forms.Button bt_chat_arSave;
        internal System.Windows.Forms.CheckBox cb_chat_arEnable;
        private System.Windows.Forms.Button bt_chat_arRemove;
        internal System.Windows.Forms.DataGridView dgv_filesUploads;
        internal System.Windows.Forms.DataGridView dgv_filesDownloads;
        private System.Windows.Forms.TabControl tv_files;
        private System.Windows.Forms.TabPage tp_filesDownload;
        private System.Windows.Forms.TabPage tp_filesUploads;
        private System.Windows.Forms.DataGridViewTextBoxColumn ulSpeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn ulName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ulSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn ulHash;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dlSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn dlDone;
        private System.Windows.Forms.DataGridViewTextBoxColumn dlSpeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn dlName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dlSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn dlHash;
    }
}

