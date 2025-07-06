namespace FFX265_Batch_Converter {
    partial class FormMain {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose( );
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent( ) {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.panel_User = new System.Windows.Forms.Panel();
            this.panel_List = new System.Windows.Forms.Panel();
            this.listBoxFiles = new System.Windows.Forms.ListBox();
            this.listBoxFolder = new System.Windows.Forms.ListBox();
            this.panel_Lavfi = new System.Windows.Forms.Panel();
            this.textBox_add_lavfi = new System.Windows.Forms.TextBox();
            this.checkBox_add_lavfi = new System.Windows.Forms.CheckBox();
            this.panel_Screen = new System.Windows.Forms.Panel();
            this.label_scan_interlaced = new System.Windows.Forms.Label();
            this.checkBox_vfr = new System.Windows.Forms.CheckBox();
            this.checkBox_autoCrop = new System.Windows.Forms.CheckBox();
            this.checkBox_useDAR = new System.Windows.Forms.CheckBox();
            this.label_scale = new System.Windows.Forms.Label();
            this.contextMenuStrip_Scale = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox长边像素 = new System.Windows.Forms.ToolStripTextBox();
            this.panel_Stream = new System.Windows.Forms.Panel();
            this.checkBox_map0s = new System.Windows.Forms.CheckBox();
            this.checkBox_map0a = new System.Windows.Forms.CheckBox();
            this.label_acodec = new System.Windows.Forms.Label();
            this.checkBox_Skip_NewEncodec = new System.Windows.Forms.CheckBox();
            this.panel_AppSet = new System.Windows.Forms.Panel();
            this.checkBox_lockedSet = new System.Windows.Forms.CheckBox();
            this.checkBox_FinishDelSour = new System.Windows.Forms.CheckBox();
            this.panel_Params = new System.Windows.Forms.Panel();
            this.panel_Gop = new System.Windows.Forms.Panel();
            this.numericUpDown_gop = new System.Windows.Forms.NumericUpDown();
            this.checkBox_keyintMax = new System.Windows.Forms.CheckBox();
            this.checkBox_rc_lookahead_halfkeyint = new System.Windows.Forms.CheckBox();
            this.panel_frames = new System.Windows.Forms.Panel();
            this.checkBox_bframes_thirdfps = new System.Windows.Forms.CheckBox();
            this.checkBox_fades = new System.Windows.Forms.CheckBox();
            this.checkBox_hist_scenecut = new System.Windows.Forms.CheckBox();
            this.numericUpDown_aq_mode = new System.Windows.Forms.NumericUpDown();
            this.checkBox_aq_mode = new System.Windows.Forms.CheckBox();
            this.label_keyFrame = new System.Windows.Forms.Label();
            this.panel_dnl = new System.Windows.Forms.Panel();
            this.numericUpDown_nr_inter = new System.Windows.Forms.NumericUpDown();
            this.checkBox_nr_inter = new System.Windows.Forms.CheckBox();
            this.numericUpDown_nr_intra = new System.Windows.Forms.NumericUpDown();
            this.checkBox_nr_intra = new System.Windows.Forms.CheckBox();
            this.checkBox_mcstf = new System.Windows.Forms.CheckBox();
            this.panel_Search = new System.Windows.Forms.Panel();
            this.checkBox_umh = new System.Windows.Forms.CheckBox();
            this.checkBox_analyze_src_pics = new System.Windows.Forms.CheckBox();
            this.checkBox_single_sei = new System.Windows.Forms.CheckBox();
            this.panel_VBV = new System.Windows.Forms.Panel();
            this.checkBox_vbv = new System.Windows.Forms.CheckBox();
            this.checkBox_hrd = new System.Windows.Forms.CheckBox();
            this.checkBox_frame_dup = new System.Windows.Forms.CheckBox();
            this.label_vbv = new System.Windows.Forms.Label();
            this.panel_Prest = new System.Windows.Forms.Panel();
            this.panel_Quality = new System.Windows.Forms.Panel();
            this.comboBoxPresets = new System.Windows.Forms.ComboBox();
            this.numericUpDown_qpmax = new System.Windows.Forms.NumericUpDown();
            this.label_qp_min_max = new System.Windows.Forms.Label();
            this.numericUpDown_qpmin = new System.Windows.Forms.NumericUpDown();
            this.checkBox_qp_min_max = new System.Windows.Forms.CheckBox();
            this.numericUpDownCRF = new System.Windows.Forms.NumericUpDown();
            this.labe_crf = new System.Windows.Forms.Label();
            this.labelCRFRange = new System.Windows.Forms.Label();
            this.panel_run = new System.Windows.Forms.Panel();
            this.checkBoxAM8Sleep = new System.Windows.Forms.CheckBox();
            this.panel_process = new System.Windows.Forms.Panel();
            this.label_0_NumProcess = new System.Windows.Forms.Label();
            this.checkBox_OneKey = new System.Windows.Forms.CheckBox();
            this.numericUpDown_NumProcess = new System.Windows.Forms.NumericUpDown();
            this.labeloneThread = new System.Windows.Forms.Label();
            this.buttonEncoding = new System.Windows.Forms.Button();
            this.toolTipList = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.panel_User.SuspendLayout();
            this.panel_List.SuspendLayout();
            this.panel_Lavfi.SuspendLayout();
            this.panel_Screen.SuspendLayout();
            this.contextMenuStrip_Scale.SuspendLayout();
            this.panel_Stream.SuspendLayout();
            this.panel_AppSet.SuspendLayout();
            this.panel_Params.SuspendLayout();
            this.panel_Gop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_gop)).BeginInit();
            this.panel_frames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_aq_mode)).BeginInit();
            this.panel_dnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_nr_inter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_nr_intra)).BeginInit();
            this.panel_Search.SuspendLayout();
            this.panel_VBV.SuspendLayout();
            this.panel_Prest.SuspendLayout();
            this.panel_Quality.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_qpmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_qpmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCRF)).BeginInit();
            this.panel_run.SuspendLayout();
            this.panel_process.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_NumProcess)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.textBoxInfo);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.panel_User);
            this.splitContainerMain.Size = new System.Drawing.Size(1264, 723);
            this.splitContainerMain.SplitterDistance = 781;
            this.splitContainerMain.TabIndex = 0;
            this.splitContainerMain.TabStop = false;
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.AllowDrop = true;
            this.textBoxInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxInfo.Location = new System.Drawing.Point(0, 0);
            this.textBoxInfo.Multiline = true;
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.Size = new System.Drawing.Size(781, 723);
            this.textBoxInfo.TabIndex = 0;
            this.textBoxInfo.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBoxInfo_DragDrop);
            this.textBoxInfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
            this.textBoxInfo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxInfo_KeyUp);
            this.textBoxInfo.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBoxInfo_MouseDoubleClick);
            this.textBoxInfo.MouseEnter += new System.EventHandler(this.textBoxInfo_MouseEnter);
            this.textBoxInfo.MouseLeave += new System.EventHandler(this.textBoxInfo_MouseLeave);
            // 
            // panel_User
            // 
            this.panel_User.Controls.Add(this.panel_List);
            this.panel_User.Controls.Add(this.panel_Params);
            this.panel_User.Controls.Add(this.panel_Prest);
            this.panel_User.Controls.Add(this.panel_run);
            this.panel_User.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_User.Location = new System.Drawing.Point(0, 0);
            this.panel_User.Name = "panel_User";
            this.panel_User.Size = new System.Drawing.Size(479, 723);
            this.panel_User.TabIndex = 5;
            // 
            // panel_List
            // 
            this.panel_List.Controls.Add(this.listBoxFiles);
            this.panel_List.Controls.Add(this.listBoxFolder);
            this.panel_List.Controls.Add(this.panel_Lavfi);
            this.panel_List.Controls.Add(this.panel_Screen);
            this.panel_List.Controls.Add(this.panel_Stream);
            this.panel_List.Controls.Add(this.panel_AppSet);
            this.panel_List.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_List.Location = new System.Drawing.Point(0, 0);
            this.panel_List.Name = "panel_List";
            this.panel_List.Size = new System.Drawing.Size(479, 513);
            this.panel_List.TabIndex = 5;
            // 
            // listBoxFiles
            // 
            this.listBoxFiles.BackColor = System.Drawing.Color.WhiteSmoke;
            this.listBoxFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxFiles.FormattingEnabled = true;
            this.listBoxFiles.IntegralHeight = false;
            this.listBoxFiles.ItemHeight = 17;
            this.listBoxFiles.Location = new System.Drawing.Point(0, 23);
            this.listBoxFiles.Name = "listBoxFiles";
            this.listBoxFiles.Size = new System.Drawing.Size(479, 334);
            this.listBoxFiles.TabIndex = 0;
            this.listBoxFiles.TabStop = false;
            this.toolTipList.SetToolTip(this.listBoxFiles, "双击添加自动监听文件夹");
            this.listBoxFiles.DoubleClick += new System.EventHandler(this.listBoxFiles_DoubleClick);
            // 
            // listBoxFolder
            // 
            this.listBoxFolder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listBoxFolder.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBoxFolder.FormattingEnabled = true;
            this.listBoxFolder.ItemHeight = 17;
            this.listBoxFolder.Location = new System.Drawing.Point(0, 357);
            this.listBoxFolder.Name = "listBoxFolder";
            this.listBoxFolder.Size = new System.Drawing.Size(479, 89);
            this.listBoxFolder.TabIndex = 1;
            this.listBoxFolder.TabStop = false;
            this.toolTipList.SetToolTip(this.listBoxFolder, "双击取消监听文件夹");
            this.listBoxFolder.Visible = false;
            this.listBoxFolder.DoubleClick += new System.EventHandler(this.listBoxFolder_DoubleClick);
            // 
            // panel_Lavfi
            // 
            this.panel_Lavfi.BackColor = System.Drawing.Color.Gainsboro;
            this.panel_Lavfi.Controls.Add(this.textBox_add_lavfi);
            this.panel_Lavfi.Controls.Add(this.checkBox_add_lavfi);
            this.panel_Lavfi.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Lavfi.Location = new System.Drawing.Point(0, 446);
            this.panel_Lavfi.Name = "panel_Lavfi";
            this.panel_Lavfi.Size = new System.Drawing.Size(479, 25);
            this.panel_Lavfi.TabIndex = 8;
            // 
            // textBox_add_lavfi
            // 
            this.textBox_add_lavfi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_add_lavfi.Location = new System.Drawing.Point(63, 0);
            this.textBox_add_lavfi.Name = "textBox_add_lavfi";
            this.textBox_add_lavfi.Size = new System.Drawing.Size(416, 23);
            this.textBox_add_lavfi.TabIndex = 8;
            this.textBox_add_lavfi.Text = "nlmeans=1.2:7:5:3:3 , unsharp=5:5:1.0:5:5:0.0";
            this.toolTipList.SetToolTip(this.textBox_add_lavfi, "滤镜需符合ffmpeg语法，顺序加在最后");
            // 
            // checkBox_add_lavfi
            // 
            this.checkBox_add_lavfi.AutoSize = true;
            this.checkBox_add_lavfi.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_add_lavfi.Location = new System.Drawing.Point(0, 0);
            this.checkBox_add_lavfi.Name = "checkBox_add_lavfi";
            this.checkBox_add_lavfi.Size = new System.Drawing.Size(63, 25);
            this.checkBox_add_lavfi.TabIndex = 7;
            this.checkBox_add_lavfi.Text = "加滤镜";
            this.toolTipList.SetToolTip(this.checkBox_add_lavfi, "选中时启用文本框中的滤镜");
            this.checkBox_add_lavfi.UseVisualStyleBackColor = true;
            this.checkBox_add_lavfi.CheckedChanged += new System.EventHandler(this.checkBox_add_lavfi_CheckedChanged);
            // 
            // panel_Screen
            // 
            this.panel_Screen.BackColor = System.Drawing.Color.Gainsboro;
            this.panel_Screen.Controls.Add(this.label_scan_interlaced);
            this.panel_Screen.Controls.Add(this.checkBox_vfr);
            this.panel_Screen.Controls.Add(this.checkBox_autoCrop);
            this.panel_Screen.Controls.Add(this.checkBox_useDAR);
            this.panel_Screen.Controls.Add(this.label_scale);
            this.panel_Screen.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Screen.Location = new System.Drawing.Point(0, 471);
            this.panel_Screen.Name = "panel_Screen";
            this.panel_Screen.Size = new System.Drawing.Size(479, 21);
            this.panel_Screen.TabIndex = 6;
            // 
            // label_scan_interlaced
            // 
            this.label_scan_interlaced.AutoSize = true;
            this.label_scan_interlaced.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_scan_interlaced.ForeColor = System.Drawing.Color.Green;
            this.label_scan_interlaced.Location = new System.Drawing.Point(0, 0);
            this.label_scan_interlaced.Name = "label_scan_interlaced";
            this.label_scan_interlaced.Size = new System.Drawing.Size(140, 17);
            this.label_scan_interlaced.TabIndex = 9;
            this.label_scan_interlaced.Text = "自动反交错：以帧为依据";
            this.label_scan_interlaced.Click += new System.EventHandler(this.label_scan_interlaced_Click);
            // 
            // checkBox_vfr
            // 
            this.checkBox_vfr.AutoSize = true;
            this.checkBox_vfr.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkBox_vfr.Location = new System.Drawing.Point(202, 0);
            this.checkBox_vfr.Name = "checkBox_vfr";
            this.checkBox_vfr.Size = new System.Drawing.Size(75, 21);
            this.checkBox_vfr.TabIndex = 2;
            this.checkBox_vfr.Text = "可变帧率";
            this.toolTipList.SetToolTip(this.checkBox_vfr, "识别重复画面并删除，压制为可变帧率视频");
            this.checkBox_vfr.UseVisualStyleBackColor = true;
            this.checkBox_vfr.CheckedChanged += new System.EventHandler(this.checkBox_vfr_CheckedChanged);
            // 
            // checkBox_autoCrop
            // 
            this.checkBox_autoCrop.AutoSize = true;
            this.checkBox_autoCrop.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkBox_autoCrop.ForeColor = System.Drawing.Color.Black;
            this.checkBox_autoCrop.Location = new System.Drawing.Point(277, 0);
            this.checkBox_autoCrop.Name = "checkBox_autoCrop";
            this.checkBox_autoCrop.Size = new System.Drawing.Size(87, 21);
            this.checkBox_autoCrop.TabIndex = 3;
            this.checkBox_autoCrop.Text = "自动裁黑边";
            this.toolTipList.SetToolTip(this.checkBox_autoCrop, "扫描黑边需要额外耗费一些时间");
            this.checkBox_autoCrop.UseVisualStyleBackColor = true;
            this.checkBox_autoCrop.CheckedChanged += new System.EventHandler(this.CheckBox_autoCrop_CheckedChanged);
            // 
            // checkBox_useDAR
            // 
            this.checkBox_useDAR.AutoSize = true;
            this.checkBox_useDAR.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkBox_useDAR.Location = new System.Drawing.Point(364, 0);
            this.checkBox_useDAR.Name = "checkBox_useDAR";
            this.checkBox_useDAR.Size = new System.Drawing.Size(75, 21);
            this.checkBox_useDAR.TabIndex = 7;
            this.checkBox_useDAR.Text = "比例校正";
            this.toolTipList.SetToolTip(this.checkBox_useDAR, "根据信息中的DAR比例自动修正宽度编码");
            this.checkBox_useDAR.UseVisualStyleBackColor = true;
            this.checkBox_useDAR.CheckedChanged += new System.EventHandler(this.checkBox_useDAR_CheckedChanged);
            // 
            // label_scale
            // 
            this.label_scale.AutoSize = true;
            this.label_scale.ContextMenuStrip = this.contextMenuStrip_Scale;
            this.label_scale.Dock = System.Windows.Forms.DockStyle.Right;
            this.label_scale.ForeColor = System.Drawing.Color.Blue;
            this.label_scale.Location = new System.Drawing.Point(439, 0);
            this.label_scale.Name = "label_scale";
            this.label_scale.Size = new System.Drawing.Size(40, 17);
            this.label_scale.TabIndex = 4;
            this.label_scale.Text = "100%";
            this.toolTipList.SetToolTip(this.label_scale, "点击选择缩小");
            this.label_scale.Click += new System.EventHandler(this.label_scale_Click);
            // 
            // contextMenuStrip_Scale
            // 
            this.contextMenuStrip_Scale.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip_Scale.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripMenuItem4,
            this.toolStripSeparator2,
            this.toolStripMenuItem1,
            this.toolStripSeparator1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripSeparator3,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.toolStripMenuItem10,
            this.toolStripMenuItem11,
            this.toolStripTextBox长边像素});
            this.contextMenuStrip_Scale.Name = "contextMenuStrip_Scale";
            this.contextMenuStrip_Scale.Size = new System.Drawing.Size(337, 377);
            this.contextMenuStrip_Scale.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip_Scale_ItemClicked);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.toolStripMenuItem5.Image = global::FFX265_Batch_Converter.Properties.Resources.FHD64_B;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(336, 30);
            this.toolStripMenuItem5.Text = "↓1920×1080p（只缩小，不放大）";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(336, 30);
            this.toolStripMenuItem6.Text = "↓2560×1440p（只缩小，不放大）";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Image = global::FFX265_Batch_Converter.Properties.Resources.UHD64px_B;
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(336, 30);
            this.toolStripMenuItem7.Text = "↓3840×2160p（只缩小，不放大）";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(336, 30);
            this.toolStripMenuItem4.Text = "↓1280×720p（只缩小，不放大）";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(333, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(336, 30);
            this.toolStripMenuItem1.Text = "★100%（原始尺寸）";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(333, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(336, 30);
            this.toolStripMenuItem2.Text = "↓50%（画面缩小为四分之一）";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(336, 30);
            this.toolStripMenuItem3.Text = "↓25%（画面缩小为八分之一）";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(333, 6);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.toolStripMenuItem8.Image = global::FFX265_Batch_Converter.Properties.Resources.FHD64_B;
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.toolStripMenuItem8.Size = new System.Drawing.Size(336, 30);
            this.toolStripMenuItem8.Text = "长边1920像素（Max(w,h)=1920px)";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.toolStripMenuItem9.Size = new System.Drawing.Size(336, 30);
            this.toolStripMenuItem9.Text = "长边2560像素（Max(w,h)=2560px)";
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Image = global::FFX265_Batch_Converter.Properties.Resources.UHD64px_B;
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.toolStripMenuItem10.Size = new System.Drawing.Size(336, 30);
            this.toolStripMenuItem10.Text = "长边3840像素（Max(w,h)=3840px)";
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(336, 30);
            this.toolStripMenuItem11.Text = "长边1280像素（Max(w,h)=1280px)";
            // 
            // toolStripTextBox长边像素
            // 
            this.toolStripTextBox长边像素.MaxLength = 5;
            this.toolStripTextBox长边像素.Name = "toolStripTextBox长边像素";
            this.toolStripTextBox长边像素.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox长边像素.Text = "1024";
            this.toolStripTextBox长边像素.ToolTipText = "长边像素（Max(w,h)=输入的值)";
            this.toolStripTextBox长边像素.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBox长边像素_KeyPress);
            // 
            // panel_Stream
            // 
            this.panel_Stream.BackColor = System.Drawing.Color.Gainsboro;
            this.panel_Stream.Controls.Add(this.checkBox_map0s);
            this.panel_Stream.Controls.Add(this.checkBox_map0a);
            this.panel_Stream.Controls.Add(this.label_acodec);
            this.panel_Stream.Controls.Add(this.checkBox_Skip_NewEncodec);
            this.panel_Stream.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Stream.Location = new System.Drawing.Point(0, 492);
            this.panel_Stream.Name = "panel_Stream";
            this.panel_Stream.Size = new System.Drawing.Size(479, 21);
            this.panel_Stream.TabIndex = 1;
            // 
            // checkBox_map0s
            // 
            this.checkBox_map0s.AutoSize = true;
            this.checkBox_map0s.Checked = true;
            this.checkBox_map0s.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_map0s.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_map0s.Location = new System.Drawing.Point(230, 0);
            this.checkBox_map0s.Name = "checkBox_map0s";
            this.checkBox_map0s.Size = new System.Drawing.Size(75, 21);
            this.checkBox_map0s.TabIndex = 5;
            this.checkBox_map0s.Text = "复制字幕";
            this.toolTipList.SetToolTip(this.checkBox_map0s, "-map 0:s -c:s copy");
            this.checkBox_map0s.UseVisualStyleBackColor = true;
            this.checkBox_map0s.CheckedChanged += new System.EventHandler(this.checkBox_map0s_CheckedChanged);
            // 
            // checkBox_map0a
            // 
            this.checkBox_map0a.AutoSize = true;
            this.checkBox_map0a.Checked = true;
            this.checkBox_map0a.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_map0a.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_map0a.Location = new System.Drawing.Point(167, 0);
            this.checkBox_map0a.Name = "checkBox_map0a";
            this.checkBox_map0a.Size = new System.Drawing.Size(63, 21);
            this.checkBox_map0a.TabIndex = 0;
            this.checkBox_map0a.Text = "多音轨";
            this.toolTipList.SetToolTip(this.checkBox_map0a, "-map 0 :a | -map 0:a:0");
            this.checkBox_map0a.UseVisualStyleBackColor = true;
            this.checkBox_map0a.CheckedChanged += new System.EventHandler(this.checkBox_map0a_CheckedChanged);
            // 
            // label_acodec
            // 
            this.label_acodec.AutoSize = true;
            this.label_acodec.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_acodec.ForeColor = System.Drawing.Color.Purple;
            this.label_acodec.Location = new System.Drawing.Point(111, 0);
            this.label_acodec.Name = "label_acodec";
            this.label_acodec.Size = new System.Drawing.Size(56, 17);
            this.label_acodec.TabIndex = 6;
            this.label_acodec.Text = "复制音轨";
            this.toolTipList.SetToolTip(this.label_acodec, "-acodec copy");
            this.label_acodec.Click += new System.EventHandler(this.label_acodec_Click);
            // 
            // checkBox_Skip_NewEncodec
            // 
            this.checkBox_Skip_NewEncodec.AutoSize = true;
            this.checkBox_Skip_NewEncodec.Checked = true;
            this.checkBox_Skip_NewEncodec.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Skip_NewEncodec.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_Skip_NewEncodec.Location = new System.Drawing.Point(0, 0);
            this.checkBox_Skip_NewEncodec.Name = "checkBox_Skip_NewEncodec";
            this.checkBox_Skip_NewEncodec.Size = new System.Drawing.Size(111, 21);
            this.checkBox_Skip_NewEncodec.TabIndex = 7;
            this.checkBox_Skip_NewEncodec.Text = "跳过更高阶编码";
            this.checkBox_Skip_NewEncodec.UseVisualStyleBackColor = true;
            this.checkBox_Skip_NewEncodec.CheckedChanged += new System.EventHandler(this.checkBox_Skip_NewEncodec_CheckedChanged);
            // 
            // panel_AppSet
            // 
            this.panel_AppSet.Controls.Add(this.checkBox_lockedSet);
            this.panel_AppSet.Controls.Add(this.checkBox_FinishDelSour);
            this.panel_AppSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_AppSet.Location = new System.Drawing.Point(0, 0);
            this.panel_AppSet.Name = "panel_AppSet";
            this.panel_AppSet.Size = new System.Drawing.Size(479, 23);
            this.panel_AppSet.TabIndex = 10;
            // 
            // checkBox_lockedSet
            // 
            this.checkBox_lockedSet.AutoSize = true;
            this.checkBox_lockedSet.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkBox_lockedSet.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBox_lockedSet.Location = new System.Drawing.Point(381, 0);
            this.checkBox_lockedSet.Name = "checkBox_lockedSet";
            this.checkBox_lockedSet.Size = new System.Drawing.Size(98, 23);
            this.checkBox_lockedSet.TabIndex = 9;
            this.checkBox_lockedSet.Text = "设置防误触";
            this.checkBox_lockedSet.UseVisualStyleBackColor = true;
            this.checkBox_lockedSet.Visible = false;
            this.checkBox_lockedSet.CheckedChanged += new System.EventHandler(this.checkBox_LockedSet_CheckedChanged);
            // 
            // checkBox_FinishDelSour
            // 
            this.checkBox_FinishDelSour.AutoSize = true;
            this.checkBox_FinishDelSour.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox_FinishDelSour.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBox_FinishDelSour.Location = new System.Drawing.Point(0, 0);
            this.checkBox_FinishDelSour.Name = "checkBox_FinishDelSour";
            this.checkBox_FinishDelSour.Size = new System.Drawing.Size(479, 23);
            this.checkBox_FinishDelSour.TabIndex = 7;
            this.checkBox_FinishDelSour.Text = "完成转码后删除视频源";
            this.checkBox_FinishDelSour.UseVisualStyleBackColor = true;
            this.checkBox_FinishDelSour.CheckedChanged += new System.EventHandler(this.checkBox_FinishDelSour_CheckedChanged);
            // 
            // panel_Params
            // 
            this.panel_Params.Controls.Add(this.panel_Gop);
            this.panel_Params.Controls.Add(this.panel_frames);
            this.panel_Params.Controls.Add(this.panel_dnl);
            this.panel_Params.Controls.Add(this.panel_Search);
            this.panel_Params.Controls.Add(this.panel_VBV);
            this.panel_Params.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Params.Location = new System.Drawing.Point(0, 513);
            this.panel_Params.Name = "panel_Params";
            this.panel_Params.Size = new System.Drawing.Size(479, 123);
            this.panel_Params.TabIndex = 0;
            this.panel_Params.Visible = false;
            // 
            // panel_Gop
            // 
            this.panel_Gop.Controls.Add(this.numericUpDown_gop);
            this.panel_Gop.Controls.Add(this.checkBox_keyintMax);
            this.panel_Gop.Controls.Add(this.checkBox_rc_lookahead_halfkeyint);
            this.panel_Gop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Gop.Location = new System.Drawing.Point(0, 63);
            this.panel_Gop.Name = "panel_Gop";
            this.panel_Gop.Size = new System.Drawing.Size(479, 21);
            this.panel_Gop.TabIndex = 5;
            // 
            // numericUpDown_gop
            // 
            this.numericUpDown_gop.Dock = System.Windows.Forms.DockStyle.Left;
            this.numericUpDown_gop.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_gop.Location = new System.Drawing.Point(210, 0);
            this.numericUpDown_gop.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numericUpDown_gop.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_gop.Name = "numericUpDown_gop";
            this.numericUpDown_gop.Size = new System.Drawing.Size(59, 23);
            this.numericUpDown_gop.TabIndex = 4;
            this.toolTipList.SetToolTip(this.numericUpDown_gop, "--gop=");
            this.numericUpDown_gop.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_gop.Visible = false;
            this.numericUpDown_gop.ValueChanged += new System.EventHandler(this.numericUpDown_gop_ValueChanged);
            // 
            // checkBox_keyintMax
            // 
            this.checkBox_keyintMax.AutoSize = true;
            this.checkBox_keyintMax.Checked = true;
            this.checkBox_keyintMax.CheckState = System.Windows.Forms.CheckState.Indeterminate;
            this.checkBox_keyintMax.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_keyintMax.ForeColor = System.Drawing.Color.Orange;
            this.checkBox_keyintMax.Location = new System.Drawing.Point(123, 0);
            this.checkBox_keyintMax.Name = "checkBox_keyintMax";
            this.checkBox_keyintMax.Size = new System.Drawing.Size(87, 21);
            this.checkBox_keyintMax.TabIndex = 2;
            this.checkBox_keyintMax.Text = "最大关键帧";
            this.checkBox_keyintMax.ThreeState = true;
            this.toolTipList.SetToolTip(this.checkBox_keyintMax, "--keyint");
            this.checkBox_keyintMax.UseVisualStyleBackColor = true;
            this.checkBox_keyintMax.CheckStateChanged += new System.EventHandler(this.checkBox_keyintMax_CheckStateChanged);
            // 
            // checkBox_rc_lookahead_halfkeyint
            // 
            this.checkBox_rc_lookahead_halfkeyint.AutoSize = true;
            this.checkBox_rc_lookahead_halfkeyint.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_rc_lookahead_halfkeyint.Location = new System.Drawing.Point(0, 0);
            this.checkBox_rc_lookahead_halfkeyint.Name = "checkBox_rc_lookahead_halfkeyint";
            this.checkBox_rc_lookahead_halfkeyint.Size = new System.Drawing.Size(123, 21);
            this.checkBox_rc_lookahead_halfkeyint.TabIndex = 2;
            this.checkBox_rc_lookahead_halfkeyint.Text = "加范围搜索帧类型";
            this.toolTipList.SetToolTip(this.checkBox_rc_lookahead_halfkeyint, "--rc-lookahead=keyint÷2 （大幅消耗内存）");
            this.checkBox_rc_lookahead_halfkeyint.UseVisualStyleBackColor = true;
            this.checkBox_rc_lookahead_halfkeyint.CheckedChanged += new System.EventHandler(this.checkBox_rc_lookahead_halfkeyint_CheckedChanged);
            // 
            // panel_frames
            // 
            this.panel_frames.BackColor = System.Drawing.Color.Gainsboro;
            this.panel_frames.Controls.Add(this.checkBox_bframes_thirdfps);
            this.panel_frames.Controls.Add(this.checkBox_fades);
            this.panel_frames.Controls.Add(this.checkBox_hist_scenecut);
            this.panel_frames.Controls.Add(this.numericUpDown_aq_mode);
            this.panel_frames.Controls.Add(this.checkBox_aq_mode);
            this.panel_frames.Controls.Add(this.label_keyFrame);
            this.panel_frames.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_frames.Location = new System.Drawing.Point(0, 42);
            this.panel_frames.Name = "panel_frames";
            this.panel_frames.Size = new System.Drawing.Size(479, 21);
            this.panel_frames.TabIndex = 4;
            // 
            // checkBox_bframes_thirdfps
            // 
            this.checkBox_bframes_thirdfps.AutoSize = true;
            this.checkBox_bframes_thirdfps.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_bframes_thirdfps.Location = new System.Drawing.Point(257, 0);
            this.checkBox_bframes_thirdfps.Name = "checkBox_bframes_thirdfps";
            this.checkBox_bframes_thirdfps.Size = new System.Drawing.Size(99, 21);
            this.checkBox_bframes_thirdfps.TabIndex = 0;
            this.checkBox_bframes_thirdfps.Text = "加双向参考帧";
            this.toolTipList.SetToolTip(this.checkBox_bframes_thirdfps, "--weightb=1 --bframes=fps÷3");
            this.checkBox_bframes_thirdfps.UseVisualStyleBackColor = true;
            // 
            // checkBox_fades
            // 
            this.checkBox_fades.AutoSize = true;
            this.checkBox_fades.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_fades.Location = new System.Drawing.Point(206, 0);
            this.checkBox_fades.Name = "checkBox_fades";
            this.checkBox_fades.Size = new System.Drawing.Size(51, 21);
            this.checkBox_fades.TabIndex = 1;
            this.checkBox_fades.Text = "淡入";
            this.toolTipList.SetToolTip(this.checkBox_fades, "--fades");
            this.checkBox_fades.UseVisualStyleBackColor = true;
            // 
            // checkBox_hist_scenecut
            // 
            this.checkBox_hist_scenecut.AutoSize = true;
            this.checkBox_hist_scenecut.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_hist_scenecut.Location = new System.Drawing.Point(143, 0);
            this.checkBox_hist_scenecut.Name = "checkBox_hist_scenecut";
            this.checkBox_hist_scenecut.Size = new System.Drawing.Size(63, 21);
            this.checkBox_hist_scenecut.TabIndex = 5;
            this.checkBox_hist_scenecut.Text = "直方图";
            this.toolTipList.SetToolTip(this.checkBox_hist_scenecut, "--hist-scenecut");
            this.checkBox_hist_scenecut.UseVisualStyleBackColor = true;
            // 
            // numericUpDown_aq_mode
            // 
            this.numericUpDown_aq_mode.Dock = System.Windows.Forms.DockStyle.Left;
            this.numericUpDown_aq_mode.Location = new System.Drawing.Point(107, 0);
            this.numericUpDown_aq_mode.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown_aq_mode.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_aq_mode.Name = "numericUpDown_aq_mode";
            this.numericUpDown_aq_mode.Size = new System.Drawing.Size(36, 23);
            this.numericUpDown_aq_mode.TabIndex = 8;
            this.toolTipList.SetToolTip(this.numericUpDown_aq_mode, "--aq-mode=4");
            this.numericUpDown_aq_mode.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown_aq_mode.Visible = false;
            this.numericUpDown_aq_mode.ValueChanged += new System.EventHandler(this.numericUpDown_aq_mode_ValueChanged);
            // 
            // checkBox_aq_mode
            // 
            this.checkBox_aq_mode.AutoSize = true;
            this.checkBox_aq_mode.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_aq_mode.Location = new System.Drawing.Point(44, 0);
            this.checkBox_aq_mode.Name = "checkBox_aq_mode";
            this.checkBox_aq_mode.Size = new System.Drawing.Size(63, 21);
            this.checkBox_aq_mode.TabIndex = 7;
            this.checkBox_aq_mode.Text = "自量化";
            this.toolTipList.SetToolTip(this.checkBox_aq_mode, "--aq-mode=1~4");
            this.checkBox_aq_mode.UseVisualStyleBackColor = true;
            this.checkBox_aq_mode.CheckedChanged += new System.EventHandler(this.checkBox_aq_mode_CheckedChanged);
            // 
            // label_keyFrame
            // 
            this.label_keyFrame.AutoSize = true;
            this.label_keyFrame.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_keyFrame.Location = new System.Drawing.Point(0, 0);
            this.label_keyFrame.Name = "label_keyFrame";
            this.label_keyFrame.Size = new System.Drawing.Size(44, 17);
            this.label_keyFrame.TabIndex = 6;
            this.label_keyFrame.Text = "帧检测";
            // 
            // panel_dnl
            // 
            this.panel_dnl.BackColor = System.Drawing.Color.Gainsboro;
            this.panel_dnl.Controls.Add(this.numericUpDown_nr_inter);
            this.panel_dnl.Controls.Add(this.checkBox_nr_inter);
            this.panel_dnl.Controls.Add(this.numericUpDown_nr_intra);
            this.panel_dnl.Controls.Add(this.checkBox_nr_intra);
            this.panel_dnl.Controls.Add(this.checkBox_mcstf);
            this.panel_dnl.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_dnl.Location = new System.Drawing.Point(0, 21);
            this.panel_dnl.Name = "panel_dnl";
            this.panel_dnl.Size = new System.Drawing.Size(479, 21);
            this.panel_dnl.TabIndex = 3;
            // 
            // numericUpDown_nr_inter
            // 
            this.numericUpDown_nr_inter.Dock = System.Windows.Forms.DockStyle.Left;
            this.numericUpDown_nr_inter.Increment = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown_nr_inter.Location = new System.Drawing.Point(279, 0);
            this.numericUpDown_nr_inter.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDown_nr_inter.Name = "numericUpDown_nr_inter";
            this.numericUpDown_nr_inter.Size = new System.Drawing.Size(54, 23);
            this.numericUpDown_nr_inter.TabIndex = 4;
            this.numericUpDown_nr_inter.Visible = false;
            this.numericUpDown_nr_inter.ValueChanged += new System.EventHandler(this.numericUpDown_nr_inter_ValueChanged);
            // 
            // checkBox_nr_inter
            // 
            this.checkBox_nr_inter.AutoSize = true;
            this.checkBox_nr_inter.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_nr_inter.Location = new System.Drawing.Point(204, 0);
            this.checkBox_nr_inter.Name = "checkBox_nr_inter";
            this.checkBox_nr_inter.Size = new System.Drawing.Size(75, 21);
            this.checkBox_nr_inter.TabIndex = 3;
            this.checkBox_nr_inter.Text = "多帧降噪";
            this.toolTipList.SetToolTip(this.checkBox_nr_inter, "--nr-inter=1~2000");
            this.checkBox_nr_inter.UseVisualStyleBackColor = true;
            this.checkBox_nr_inter.CheckedChanged += new System.EventHandler(this.checkBox_nr_inter_CheckedChanged);
            // 
            // numericUpDown_nr_intra
            // 
            this.numericUpDown_nr_intra.Dock = System.Windows.Forms.DockStyle.Left;
            this.numericUpDown_nr_intra.Increment = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDown_nr_intra.Location = new System.Drawing.Point(150, 0);
            this.numericUpDown_nr_intra.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDown_nr_intra.Name = "numericUpDown_nr_intra";
            this.numericUpDown_nr_intra.Size = new System.Drawing.Size(54, 23);
            this.numericUpDown_nr_intra.TabIndex = 2;
            this.numericUpDown_nr_intra.Visible = false;
            this.numericUpDown_nr_intra.ValueChanged += new System.EventHandler(this.numericUpDown_nr_intra_ValueChanged);
            // 
            // checkBox_nr_intra
            // 
            this.checkBox_nr_intra.AutoSize = true;
            this.checkBox_nr_intra.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_nr_intra.Location = new System.Drawing.Point(75, 0);
            this.checkBox_nr_intra.Name = "checkBox_nr_intra";
            this.checkBox_nr_intra.Size = new System.Drawing.Size(75, 21);
            this.checkBox_nr_intra.TabIndex = 1;
            this.checkBox_nr_intra.Text = "单帧降噪";
            this.toolTipList.SetToolTip(this.checkBox_nr_intra, "--nr-intra=1~2000");
            this.checkBox_nr_intra.UseVisualStyleBackColor = true;
            this.checkBox_nr_intra.CheckedChanged += new System.EventHandler(this.checkBox_nr_intra_CheckedChanged);
            // 
            // checkBox_mcstf
            // 
            this.checkBox_mcstf.AutoSize = true;
            this.checkBox_mcstf.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_mcstf.Location = new System.Drawing.Point(0, 0);
            this.checkBox_mcstf.Name = "checkBox_mcstf";
            this.checkBox_mcstf.Size = new System.Drawing.Size(75, 21);
            this.checkBox_mcstf.TabIndex = 0;
            this.checkBox_mcstf.Text = "空间降噪";
            this.toolTipList.SetToolTip(this.checkBox_mcstf, "--mcstf");
            this.checkBox_mcstf.UseVisualStyleBackColor = true;
            this.checkBox_mcstf.CheckedChanged += new System.EventHandler(this.checkBox_mcstf_CheckedChanged);
            this.checkBox_mcstf.Click += new System.EventHandler(this.checkBox_mcstf_Click);
            this.checkBox_mcstf.MouseClick += new System.Windows.Forms.MouseEventHandler(this.checkBox_mcstf_MouseClick);
            // 
            // panel_Search
            // 
            this.panel_Search.BackColor = System.Drawing.Color.Gainsboro;
            this.panel_Search.Controls.Add(this.checkBox_umh);
            this.panel_Search.Controls.Add(this.checkBox_analyze_src_pics);
            this.panel_Search.Controls.Add(this.checkBox_single_sei);
            this.panel_Search.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_Search.Location = new System.Drawing.Point(0, 0);
            this.panel_Search.Name = "panel_Search";
            this.panel_Search.Size = new System.Drawing.Size(479, 21);
            this.panel_Search.TabIndex = 2;
            // 
            // checkBox_umh
            // 
            this.checkBox_umh.AutoSize = true;
            this.checkBox_umh.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_umh.Location = new System.Drawing.Point(186, 0);
            this.checkBox_umh.Name = "checkBox_umh";
            this.checkBox_umh.Size = new System.Drawing.Size(135, 21);
            this.checkBox_umh.TabIndex = 1;
            this.checkBox_umh.Text = "不对称多六边形搜索";
            this.toolTipList.SetToolTip(this.checkBox_umh, "--me=umh");
            this.checkBox_umh.UseVisualStyleBackColor = true;
            // 
            // checkBox_analyze_src_pics
            // 
            this.checkBox_analyze_src_pics.AutoSize = true;
            this.checkBox_analyze_src_pics.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_analyze_src_pics.Location = new System.Drawing.Point(99, 0);
            this.checkBox_analyze_src_pics.Name = "checkBox_analyze_src_pics";
            this.checkBox_analyze_src_pics.Size = new System.Drawing.Size(87, 21);
            this.checkBox_analyze_src_pics.TabIndex = 0;
            this.checkBox_analyze_src_pics.Text = "搜索片源帧";
            this.toolTipList.SetToolTip(this.checkBox_analyze_src_pics, "--analyze-src-pics");
            this.checkBox_analyze_src_pics.UseVisualStyleBackColor = true;
            // 
            // checkBox_single_sei
            // 
            this.checkBox_single_sei.AutoSize = true;
            this.checkBox_single_sei.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_single_sei.Location = new System.Drawing.Point(0, 0);
            this.checkBox_single_sei.Name = "checkBox_single_sei";
            this.checkBox_single_sei.Size = new System.Drawing.Size(99, 21);
            this.checkBox_single_sei.TabIndex = 1;
            this.checkBox_single_sei.Text = "不显编码信息";
            this.checkBox_single_sei.ThreeState = true;
            this.toolTipList.SetToolTip(this.checkBox_single_sei, "默认显示编码信息");
            this.checkBox_single_sei.UseVisualStyleBackColor = true;
            this.checkBox_single_sei.MouseClick += new System.Windows.Forms.MouseEventHandler(this.checkBox_single_sei_MouseClick);
            // 
            // panel_VBV
            // 
            this.panel_VBV.BackColor = System.Drawing.Color.Gainsboro;
            this.panel_VBV.Controls.Add(this.checkBox_vbv);
            this.panel_VBV.Controls.Add(this.checkBox_hrd);
            this.panel_VBV.Controls.Add(this.checkBox_frame_dup);
            this.panel_VBV.Controls.Add(this.label_vbv);
            this.panel_VBV.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_VBV.Location = new System.Drawing.Point(0, 105);
            this.panel_VBV.Name = "panel_VBV";
            this.panel_VBV.Size = new System.Drawing.Size(479, 18);
            this.panel_VBV.TabIndex = 0;
            this.panel_VBV.Visible = false;
            // 
            // checkBox_vbv
            // 
            this.checkBox_vbv.AutoSize = true;
            this.checkBox_vbv.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_vbv.Location = new System.Drawing.Point(194, 0);
            this.checkBox_vbv.Name = "checkBox_vbv";
            this.checkBox_vbv.Size = new System.Drawing.Size(75, 18);
            this.checkBox_vbv.TabIndex = 2;
            this.checkBox_vbv.Text = "量化缓冲";
            this.toolTipList.SetToolTip(this.checkBox_vbv, "vbv");
            this.checkBox_vbv.UseVisualStyleBackColor = true;
            // 
            // checkBox_hrd
            // 
            this.checkBox_hrd.AutoSize = true;
            this.checkBox_hrd.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_hrd.Location = new System.Drawing.Point(119, 0);
            this.checkBox_hrd.Name = "checkBox_hrd";
            this.checkBox_hrd.Size = new System.Drawing.Size(75, 18);
            this.checkBox_hrd.TabIndex = 1;
            this.checkBox_hrd.Text = "假设信息";
            this.toolTipList.SetToolTip(this.checkBox_hrd, "--hrd");
            this.checkBox_hrd.UseVisualStyleBackColor = true;
            // 
            // checkBox_frame_dup
            // 
            this.checkBox_frame_dup.AutoSize = true;
            this.checkBox_frame_dup.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_frame_dup.Location = new System.Drawing.Point(56, 0);
            this.checkBox_frame_dup.Name = "checkBox_frame_dup";
            this.checkBox_frame_dup.Size = new System.Drawing.Size(63, 18);
            this.checkBox_frame_dup.TabIndex = 5;
            this.checkBox_frame_dup.Text = "帧复制";
            this.toolTipList.SetToolTip(this.checkBox_frame_dup, "--frame-dup");
            this.checkBox_frame_dup.UseVisualStyleBackColor = true;
            this.checkBox_frame_dup.CheckedChanged += new System.EventHandler(this.checkBox_frame_dup_CheckedChanged);
            // 
            // label_vbv
            // 
            this.label_vbv.AutoSize = true;
            this.label_vbv.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_vbv.Location = new System.Drawing.Point(0, 0);
            this.label_vbv.Name = "label_vbv";
            this.label_vbv.Size = new System.Drawing.Size(56, 17);
            this.label_vbv.TabIndex = 0;
            this.label_vbv.Text = "流媒体用";
            // 
            // panel_Prest
            // 
            this.panel_Prest.BackColor = System.Drawing.Color.Silver;
            this.panel_Prest.Controls.Add(this.panel_Quality);
            this.panel_Prest.Controls.Add(this.labelCRFRange);
            this.panel_Prest.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_Prest.Location = new System.Drawing.Point(0, 636);
            this.panel_Prest.Name = "panel_Prest";
            this.panel_Prest.Size = new System.Drawing.Size(479, 44);
            this.panel_Prest.TabIndex = 1;
            // 
            // panel_Quality
            // 
            this.panel_Quality.BackColor = System.Drawing.Color.Gainsboro;
            this.panel_Quality.Controls.Add(this.comboBoxPresets);
            this.panel_Quality.Controls.Add(this.numericUpDown_qpmax);
            this.panel_Quality.Controls.Add(this.label_qp_min_max);
            this.panel_Quality.Controls.Add(this.numericUpDown_qpmin);
            this.panel_Quality.Controls.Add(this.checkBox_qp_min_max);
            this.panel_Quality.Controls.Add(this.numericUpDownCRF);
            this.panel_Quality.Controls.Add(this.labe_crf);
            this.panel_Quality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Quality.Location = new System.Drawing.Point(0, 17);
            this.panel_Quality.Name = "panel_Quality";
            this.panel_Quality.Size = new System.Drawing.Size(479, 27);
            this.panel_Quality.TabIndex = 5;
            // 
            // comboBoxPresets
            // 
            this.comboBoxPresets.AutoCompleteCustomSource.AddRange(new string[] {
            "medium（标准速度）",
            "slow（稍微慢一点）",
            "slower（更加慢一点）",
            "veryslow（非常慢）",
            "placebo（最慢）",
            "copy（复制视频流）",
            "ultrafast（最快）",
            "superfast（超级快）",
            "veryfast（非常快）",
            "faster（更加一些）",
            "fast（稍微快一些）"});
            this.comboBoxPresets.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxPresets.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxPresets.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.comboBoxPresets.FormattingEnabled = true;
            this.comboBoxPresets.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.comboBoxPresets.IntegralHeight = false;
            this.comboBoxPresets.Items.AddRange(new object[] {
            "medium（标准速度）",
            "slow（稍微慢一点）",
            "slower（更加慢一点）",
            "veryslow（非常慢）",
            "placebo（最慢）",
            "copy（复制视频流）",
            "ultrafast（最快）",
            "superfast（超级快）",
            "veryfast（非常快）",
            "faster（更加一些）",
            "fast（稍微快一些）"});
            this.comboBoxPresets.Location = new System.Drawing.Point(275, 2);
            this.comboBoxPresets.Name = "comboBoxPresets";
            this.comboBoxPresets.Size = new System.Drawing.Size(204, 25);
            this.comboBoxPresets.TabIndex = 3;
            this.comboBoxPresets.Text = "medium（标准速度）";
            this.toolTipList.SetToolTip(this.comboBoxPresets, "速度的预设");
            this.comboBoxPresets.SelectedIndexChanged += new System.EventHandler(this.comboBoxPresets_SelectedIndexChanged);
            // 
            // numericUpDown_qpmax
            // 
            this.numericUpDown_qpmax.DecimalPlaces = 2;
            this.numericUpDown_qpmax.Dock = System.Windows.Forms.DockStyle.Left;
            this.numericUpDown_qpmax.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.numericUpDown_qpmax.Location = new System.Drawing.Point(219, 0);
            this.numericUpDown_qpmax.Maximum = new decimal(new int[] {
            69,
            0,
            0,
            0});
            this.numericUpDown_qpmax.Name = "numericUpDown_qpmax";
            this.numericUpDown_qpmax.Size = new System.Drawing.Size(56, 25);
            this.numericUpDown_qpmax.TabIndex = 4;
            this.numericUpDown_qpmax.Value = new decimal(new int[] {
            69,
            0,
            0,
            0});
            this.numericUpDown_qpmax.Visible = false;
            this.numericUpDown_qpmax.ValueChanged += new System.EventHandler(this.numericUpDown_qp_range_ValueChanged);
            // 
            // label_qp_min_max
            // 
            this.label_qp_min_max.AutoSize = true;
            this.label_qp_min_max.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_qp_min_max.Location = new System.Drawing.Point(202, 0);
            this.label_qp_min_max.Name = "label_qp_min_max";
            this.label_qp_min_max.Size = new System.Drawing.Size(17, 17);
            this.label_qp_min_max.TabIndex = 5;
            this.label_qp_min_max.Text = "~";
            this.label_qp_min_max.Visible = false;
            // 
            // numericUpDown_qpmin
            // 
            this.numericUpDown_qpmin.DecimalPlaces = 2;
            this.numericUpDown_qpmin.Dock = System.Windows.Forms.DockStyle.Left;
            this.numericUpDown_qpmin.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.numericUpDown_qpmin.Location = new System.Drawing.Point(146, 0);
            this.numericUpDown_qpmin.Maximum = new decimal(new int[] {
            63,
            0,
            0,
            0});
            this.numericUpDown_qpmin.Name = "numericUpDown_qpmin";
            this.numericUpDown_qpmin.Size = new System.Drawing.Size(56, 25);
            this.numericUpDown_qpmin.TabIndex = 2;
            this.numericUpDown_qpmin.Visible = false;
            this.numericUpDown_qpmin.ValueChanged += new System.EventHandler(this.numericUpDown_qp_range_ValueChanged);
            // 
            // checkBox_qp_min_max
            // 
            this.checkBox_qp_min_max.AutoSize = true;
            this.checkBox_qp_min_max.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_qp_min_max.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.checkBox_qp_min_max.Location = new System.Drawing.Point(90, 0);
            this.checkBox_qp_min_max.Name = "checkBox_qp_min_max";
            this.checkBox_qp_min_max.Size = new System.Drawing.Size(56, 27);
            this.checkBox_qp_min_max.TabIndex = 1;
            this.checkBox_qp_min_max.Text = "约束";
            this.toolTipList.SetToolTip(this.checkBox_qp_min_max, "--qpmin=  --qpmax=");
            this.checkBox_qp_min_max.UseVisualStyleBackColor = true;
            this.checkBox_qp_min_max.CheckedChanged += new System.EventHandler(this.checkBox_qp_min_max_CheckedChanged);
            // 
            // numericUpDownCRF
            // 
            this.numericUpDownCRF.DecimalPlaces = 1;
            this.numericUpDownCRF.Dock = System.Windows.Forms.DockStyle.Left;
            this.numericUpDownCRF.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.numericUpDownCRF.Location = new System.Drawing.Point(34, 0);
            this.numericUpDownCRF.Maximum = new decimal(new int[] {
            52,
            0,
            0,
            0});
            this.numericUpDownCRF.Name = "numericUpDownCRF";
            this.numericUpDownCRF.Size = new System.Drawing.Size(56, 25);
            this.numericUpDownCRF.TabIndex = 2;
            this.numericUpDownCRF.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTipList.SetToolTip(this.numericUpDownCRF, "-crf ");
            this.numericUpDownCRF.Value = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.numericUpDownCRF.MouseClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDownCRF_MouseClick);
            // 
            // labe_crf
            // 
            this.labe_crf.AutoSize = true;
            this.labe_crf.Dock = System.Windows.Forms.DockStyle.Left;
            this.labe_crf.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.labe_crf.ForeColor = System.Drawing.Color.Black;
            this.labe_crf.Location = new System.Drawing.Point(0, 0);
            this.labe_crf.Name = "labe_crf";
            this.labe_crf.Size = new System.Drawing.Size(34, 20);
            this.labe_crf.TabIndex = 6;
            this.labe_crf.Text = "CRF";
            // 
            // labelCRFRange
            // 
            this.labelCRFRange.AutoSize = true;
            this.labelCRFRange.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelCRFRange.Location = new System.Drawing.Point(0, 0);
            this.labelCRFRange.Name = "labelCRFRange";
            this.labelCRFRange.Size = new System.Drawing.Size(455, 17);
            this.labelCRFRange.TabIndex = 0;
            this.labelCRFRange.Text = "CRF=画质精细｛原盘：8~14；好：18~23；中：23~28；低：28~32；糊：32+｝";
            // 
            // panel_run
            // 
            this.panel_run.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel_run.Controls.Add(this.checkBoxAM8Sleep);
            this.panel_run.Controls.Add(this.panel_process);
            this.panel_run.Controls.Add(this.buttonEncoding);
            this.panel_run.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_run.Location = new System.Drawing.Point(0, 680);
            this.panel_run.Name = "panel_run";
            this.panel_run.Size = new System.Drawing.Size(479, 43);
            this.panel_run.TabIndex = 2;
            // 
            // checkBoxAM8Sleep
            // 
            this.checkBoxAM8Sleep.AutoSize = true;
            this.checkBoxAM8Sleep.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBoxAM8Sleep.Location = new System.Drawing.Point(0, 23);
            this.checkBoxAM8Sleep.Name = "checkBoxAM8Sleep";
            this.checkBoxAM8Sleep.Size = new System.Drawing.Size(339, 20);
            this.checkBoxAM8Sleep.TabIndex = 1;
            this.checkBoxAM8Sleep.Text = "省钱模式：(8:00~21:45)峰电休眠，需要配合主板定时唤醒";
            this.toolTipList.SetToolTip(this.checkBoxAM8Sleep, "峰谷电用户特色功能");
            this.checkBoxAM8Sleep.UseVisualStyleBackColor = true;
            this.checkBoxAM8Sleep.CheckedChanged += new System.EventHandler(this.checkBoxDaySleep_CheckedChanged);
            // 
            // panel_process
            // 
            this.panel_process.Controls.Add(this.label_0_NumProcess);
            this.panel_process.Controls.Add(this.checkBox_OneKey);
            this.panel_process.Controls.Add(this.numericUpDown_NumProcess);
            this.panel_process.Controls.Add(this.labeloneThread);
            this.panel_process.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_process.Location = new System.Drawing.Point(0, 0);
            this.panel_process.Name = "panel_process";
            this.panel_process.Size = new System.Drawing.Size(400, 23);
            this.panel_process.TabIndex = 0;
            // 
            // label_0_NumProcess
            // 
            this.label_0_NumProcess.AutoSize = true;
            this.label_0_NumProcess.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label_0_NumProcess.Dock = System.Windows.Forms.DockStyle.Left;
            this.label_0_NumProcess.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label_0_NumProcess.ForeColor = System.Drawing.Color.White;
            this.label_0_NumProcess.Location = new System.Drawing.Point(82, 0);
            this.label_0_NumProcess.Name = "label_0_NumProcess";
            this.label_0_NumProcess.Size = new System.Drawing.Size(37, 20);
            this.label_0_NumProcess.TabIndex = 3;
            this.label_0_NumProcess.Text = "刹车";
            this.toolTipList.SetToolTip(this.label_0_NumProcess, "完成当前任务~暂停");
            this.label_0_NumProcess.Visible = false;
            // 
            // checkBox_OneKey
            // 
            this.checkBox_OneKey.AutoSize = true;
            this.checkBox_OneKey.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkBox_OneKey.Location = new System.Drawing.Point(197, 0);
            this.checkBox_OneKey.Name = "checkBox_OneKey";
            this.checkBox_OneKey.Size = new System.Drawing.Size(203, 23);
            this.checkBox_OneKey.TabIndex = 2;
            this.checkBox_OneKey.Text = "消耗8倍算力与内存缩小10%体积";
            this.toolTipList.SetToolTip(this.checkBox_OneKey, "增加压缩率的都选上");
            this.checkBox_OneKey.UseVisualStyleBackColor = true;
            this.checkBox_OneKey.CheckedChanged += new System.EventHandler(this.checkBox_oneKey_CheckedChanged);
            // 
            // numericUpDown_NumProcess
            // 
            this.numericUpDown_NumProcess.Dock = System.Windows.Forms.DockStyle.Left;
            this.numericUpDown_NumProcess.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.numericUpDown_NumProcess.Location = new System.Drawing.Point(35, 0);
            this.numericUpDown_NumProcess.Maximum = new decimal(new int[] {
            768,
            0,
            0,
            0});
            this.numericUpDown_NumProcess.Name = "numericUpDown_NumProcess";
            this.numericUpDown_NumProcess.Size = new System.Drawing.Size(47, 23);
            this.numericUpDown_NumProcess.TabIndex = 1;
            this.numericUpDown_NumProcess.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTipList.SetToolTip(this.numericUpDown_NumProcess, "检查CPU核心数、内存容量再设置较为稳妥");
            this.numericUpDown_NumProcess.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDown_NumProcess.ValueChanged += new System.EventHandler(this.numericUpDown_NumProcess_ValueChanged);
            this.numericUpDown_NumProcess.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numericUpDown_NumProcess_KeyUp);
            // 
            // labeloneThread
            // 
            this.labeloneThread.AutoSize = true;
            this.labeloneThread.Dock = System.Windows.Forms.DockStyle.Left;
            this.labeloneThread.Font = new System.Drawing.Font("微软雅黑", 9.5F);
            this.labeloneThread.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.labeloneThread.Location = new System.Drawing.Point(0, 0);
            this.labeloneThread.Name = "labeloneThread";
            this.labeloneThread.Size = new System.Drawing.Size(35, 19);
            this.labeloneThread.TabIndex = 4;
            this.labeloneThread.Text = "多开";
            this.labeloneThread.Click += new System.EventHandler(this.labeloneThread_Click);
            // 
            // buttonEncoding
            // 
            this.buttonEncoding.Dock = System.Windows.Forms.DockStyle.Right;
            this.buttonEncoding.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonEncoding.Location = new System.Drawing.Point(400, 0);
            this.buttonEncoding.Name = "buttonEncoding";
            this.buttonEncoding.Size = new System.Drawing.Size(79, 43);
            this.buttonEncoding.TabIndex = 0;
            this.buttonEncoding.Text = "转码(&R)";
            this.toolTipList.SetToolTip(this.buttonEncoding, "一键开始");
            this.buttonEncoding.UseVisualStyleBackColor = true;
            this.buttonEncoding.Click += new System.EventHandler(this.buttonEncoding_Click);
            // 
            // toolTipList
            // 
            this.toolTipList.UseAnimation = false;
            this.toolTipList.UseFading = false;
            // 
            // FormMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 723);
            this.Controls.Add(this.splitContainerMain);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FFX265 Batch Converter";
            this.Activated += new System.EventHandler(this.FormMain_Activated);
            this.Deactivate += new System.EventHandler(this.FormMain_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel1.PerformLayout();
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.panel_User.ResumeLayout(false);
            this.panel_List.ResumeLayout(false);
            this.panel_Lavfi.ResumeLayout(false);
            this.panel_Lavfi.PerformLayout();
            this.panel_Screen.ResumeLayout(false);
            this.panel_Screen.PerformLayout();
            this.contextMenuStrip_Scale.ResumeLayout(false);
            this.contextMenuStrip_Scale.PerformLayout();
            this.panel_Stream.ResumeLayout(false);
            this.panel_Stream.PerformLayout();
            this.panel_AppSet.ResumeLayout(false);
            this.panel_AppSet.PerformLayout();
            this.panel_Params.ResumeLayout(false);
            this.panel_Gop.ResumeLayout(false);
            this.panel_Gop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_gop)).EndInit();
            this.panel_frames.ResumeLayout(false);
            this.panel_frames.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_aq_mode)).EndInit();
            this.panel_dnl.ResumeLayout(false);
            this.panel_dnl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_nr_inter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_nr_intra)).EndInit();
            this.panel_Search.ResumeLayout(false);
            this.panel_Search.PerformLayout();
            this.panel_VBV.ResumeLayout(false);
            this.panel_VBV.PerformLayout();
            this.panel_Prest.ResumeLayout(false);
            this.panel_Prest.PerformLayout();
            this.panel_Quality.ResumeLayout(false);
            this.panel_Quality.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_qpmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_qpmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCRF)).EndInit();
            this.panel_run.ResumeLayout(false);
            this.panel_run.PerformLayout();
            this.panel_process.ResumeLayout(false);
            this.panel_process.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_NumProcess)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.ListBox listBoxFiles;
        private System.Windows.Forms.Panel panel_Params;
        private System.Windows.Forms.NumericUpDown numericUpDownCRF;
        private System.Windows.Forms.Label labelCRFRange;
        private System.Windows.Forms.ComboBox comboBoxPresets;
        private System.Windows.Forms.ListBox listBoxFolder;
        private System.Windows.Forms.ToolTip toolTipList;
        private System.Windows.Forms.Panel panel_Stream;
        private System.Windows.Forms.CheckBox checkBox_nr_intra;
        private System.Windows.Forms.CheckBox checkBox_mcstf;
        private System.Windows.Forms.CheckBox checkBox_nr_inter;
        private System.Windows.Forms.Panel panel_run;
        private System.Windows.Forms.Button buttonEncoding;
        private System.Windows.Forms.NumericUpDown numericUpDown_NumProcess;
        private System.Windows.Forms.Panel panel_process;
        private System.Windows.Forms.CheckBox checkBoxAM8Sleep;
        private System.Windows.Forms.CheckBox checkBox_fades;
        private System.Windows.Forms.CheckBox checkBox_frame_dup;
        private System.Windows.Forms.Panel panel_Prest;
        private System.Windows.Forms.CheckBox checkBox_keyintMax;
        private System.Windows.Forms.Panel panel_Search;
        private System.Windows.Forms.CheckBox checkBox_map0a;
        private System.Windows.Forms.CheckBox checkBox_analyze_src_pics;
        private System.Windows.Forms.CheckBox checkBox_umh;
        private System.Windows.Forms.CheckBox checkBox_rc_lookahead_halfkeyint;
        private System.Windows.Forms.Panel panel_User;
        private System.Windows.Forms.Panel panel_List;
        private System.Windows.Forms.Panel panel_VBV;
        private System.Windows.Forms.CheckBox checkBox_single_sei;
        private System.Windows.Forms.CheckBox checkBox_hrd;
        private System.Windows.Forms.CheckBox checkBox_vbv;
        private System.Windows.Forms.Label label_vbv;
        private System.Windows.Forms.Panel panel_dnl;
        private System.Windows.Forms.NumericUpDown numericUpDown_nr_intra;
        private System.Windows.Forms.NumericUpDown numericUpDown_nr_inter;
        private System.Windows.Forms.CheckBox checkBox_bframes_thirdfps;
        private System.Windows.Forms.NumericUpDown numericUpDown_gop;
        private System.Windows.Forms.Panel panel_frames;
        private System.Windows.Forms.CheckBox checkBox_vfr;
        private System.Windows.Forms.CheckBox checkBox_OneKey;
        private System.Windows.Forms.CheckBox checkBox_autoCrop;
        private System.Windows.Forms.Panel panel_Quality;
        private System.Windows.Forms.NumericUpDown numericUpDown_qpmax;
        private System.Windows.Forms.NumericUpDown numericUpDown_qpmin;
        private System.Windows.Forms.CheckBox checkBox_qp_min_max;
        private System.Windows.Forms.Label label_qp_min_max;
        private System.Windows.Forms.Label label_scale;
        private System.Windows.Forms.CheckBox checkBox_hist_scenecut;
        private System.Windows.Forms.Label label_keyFrame;
        private System.Windows.Forms.CheckBox checkBox_map0s;
        private System.Windows.Forms.NumericUpDown numericUpDown_aq_mode;
        private System.Windows.Forms.CheckBox checkBox_aq_mode;
        private System.Windows.Forms.Panel panel_Gop;
        private System.Windows.Forms.Label label_acodec;
        private System.Windows.Forms.Label label_scan_interlaced;
        private System.Windows.Forms.CheckBox checkBox_useDAR;
        private System.Windows.Forms.Panel panel_Screen;
        private System.Windows.Forms.CheckBox checkBox_Skip_NewEncodec;
        private System.Windows.Forms.CheckBox checkBox_FinishDelSour;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Scale;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label label_0_NumProcess;
        private System.Windows.Forms.Label labeloneThread;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox长边像素;
        private System.Windows.Forms.Label labe_crf;
        private System.Windows.Forms.Panel panel_Lavfi;
        private System.Windows.Forms.CheckBox checkBox_add_lavfi;
        private System.Windows.Forms.TextBox textBox_add_lavfi;
        private System.Windows.Forms.CheckBox checkBox_lockedSet;
        private System.Windows.Forms.Panel panel_AppSet;
    }
}

