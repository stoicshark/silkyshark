namespace Silky_Shark
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_saveConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_restoreDefaults = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItem_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_settings = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_help = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_about = new System.Windows.Forms.ToolStripMenuItem();
            this.trackBar_smoothingStrength = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox_smoothOnDraw = new System.Windows.Forms.CheckBox();
            this.checkBox_tabletMode = new System.Windows.Forms.CheckBox();
            this.checkBox_stayOnTop = new System.Windows.Forms.CheckBox();
            this.checkBox_manualInterpolation = new System.Windows.Forms.CheckBox();
            this.textBox_smoothingInterpolation = new System.Windows.Forms.TextBox();
            this.textBox_smoothingStrength = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBar_smoothingInterpolation = new System.Windows.Forms.TrackBar();
            this.button_toggleDisplay = new System.Windows.Forms.Button();
            this.toolTip_smoothingOnOff = new System.Windows.Forms.ToolTip(this.components);
            this.button_smoothOnOff = new System.Windows.Forms.Button();
            this.toolTip_toggleOverScreen = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip_cursorColor = new System.Windows.Forms.ToolTip(this.components);
            this.button_colorDialog = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_smoothingStrength)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_smoothingInterpolation)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem,
            this.ToolStripMenuItem_settings,
            this.ToolStripMenuItem_help,
            this.ToolStripMenuItem_about});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(334, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_saveConfig,
            this.ToolStripMenuItem_restoreDefaults,
            this.toolStripSeparator2,
            this.ToolStripMenuItem_exit});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.testToolStripMenuItem.Text = "File";
            // 
            // ToolStripMenuItem_saveConfig
            // 
            this.ToolStripMenuItem_saveConfig.Name = "ToolStripMenuItem_saveConfig";
            this.ToolStripMenuItem_saveConfig.Size = new System.Drawing.Size(159, 22);
            this.ToolStripMenuItem_saveConfig.Text = "Save Config";
            this.ToolStripMenuItem_saveConfig.Click += new System.EventHandler(this.ToolStripMenuItem_saveConfig_Click);
            // 
            // ToolStripMenuItem_restoreDefaults
            // 
            this.ToolStripMenuItem_restoreDefaults.Name = "ToolStripMenuItem_restoreDefaults";
            this.ToolStripMenuItem_restoreDefaults.Size = new System.Drawing.Size(159, 22);
            this.ToolStripMenuItem_restoreDefaults.Text = "Restore Defaults";
            this.ToolStripMenuItem_restoreDefaults.Click += new System.EventHandler(this.ToolStripMenuItem_restoreDefaults_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(156, 6);
            // 
            // ToolStripMenuItem_exit
            // 
            this.ToolStripMenuItem_exit.Name = "ToolStripMenuItem_exit";
            this.ToolStripMenuItem_exit.Size = new System.Drawing.Size(159, 22);
            this.ToolStripMenuItem_exit.Text = "Exit";
            this.ToolStripMenuItem_exit.Click += new System.EventHandler(this.ToolStripMenuItem_exit_Click);
            // 
            // ToolStripMenuItem_settings
            // 
            this.ToolStripMenuItem_settings.Name = "ToolStripMenuItem_settings";
            this.ToolStripMenuItem_settings.Size = new System.Drawing.Size(61, 20);
            this.ToolStripMenuItem_settings.Text = "Settings";
            this.ToolStripMenuItem_settings.Click += new System.EventHandler(this.toolStrip_Settings_Click);
            // 
            // ToolStripMenuItem_help
            // 
            this.ToolStripMenuItem_help.Name = "ToolStripMenuItem_help";
            this.ToolStripMenuItem_help.Size = new System.Drawing.Size(44, 20);
            this.ToolStripMenuItem_help.Text = "Help";
            this.ToolStripMenuItem_help.Click += new System.EventHandler(this.ToolStripMenuItem_help_Click);
            // 
            // ToolStripMenuItem_about
            // 
            this.ToolStripMenuItem_about.Name = "ToolStripMenuItem_about";
            this.ToolStripMenuItem_about.Size = new System.Drawing.Size(52, 20);
            this.ToolStripMenuItem_about.Text = "About";
            this.ToolStripMenuItem_about.Click += new System.EventHandler(this.ToolStripMenuItem_about_Click);
            // 
            // trackBar_smoothingStrength
            // 
            this.trackBar_smoothingStrength.Location = new System.Drawing.Point(64, 5);
            this.trackBar_smoothingStrength.Maximum = 100;
            this.trackBar_smoothingStrength.Minimum = 1;
            this.trackBar_smoothingStrength.Name = "trackBar_smoothingStrength";
            this.trackBar_smoothingStrength.Size = new System.Drawing.Size(141, 45);
            this.trackBar_smoothingStrength.TabIndex = 1;
            this.trackBar_smoothingStrength.TickFrequency = 10;
            this.trackBar_smoothingStrength.Value = 30;
            this.trackBar_smoothingStrength.Scroll += new System.EventHandler(this.trackBar_smoothStrength_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Strength";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.checkBox_smoothOnDraw);
            this.panel1.Controls.Add(this.checkBox_tabletMode);
            this.panel1.Controls.Add(this.checkBox_stayOnTop);
            this.panel1.Controls.Add(this.checkBox_manualInterpolation);
            this.panel1.Controls.Add(this.textBox_smoothingInterpolation);
            this.panel1.Controls.Add(this.textBox_smoothingStrength);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.trackBar_smoothingInterpolation);
            this.panel1.Controls.Add(this.trackBar_smoothingStrength);
            this.panel1.Location = new System.Drawing.Point(84, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(238, 114);
            this.panel1.TabIndex = 4;
            // 
            // checkBox_smoothOnDraw
            // 
            this.checkBox_smoothOnDraw.AutoSize = true;
            this.checkBox_smoothOnDraw.Location = new System.Drawing.Point(133, 73);
            this.checkBox_smoothOnDraw.Name = "checkBox_smoothOnDraw";
            this.checkBox_smoothOnDraw.Size = new System.Drawing.Size(107, 17);
            this.checkBox_smoothOnDraw.TabIndex = 9;
            this.checkBox_smoothOnDraw.Text = "Smooth On Draw";
            this.checkBox_smoothOnDraw.UseVisualStyleBackColor = true;
            this.checkBox_smoothOnDraw.CheckedChanged += new System.EventHandler(this.checkBox_smoothOnDraw_CheckedChanged);
            // 
            // checkBox_tabletMode
            // 
            this.checkBox_tabletMode.AutoSize = true;
            this.checkBox_tabletMode.Enabled = false;
            this.checkBox_tabletMode.Location = new System.Drawing.Point(133, 95);
            this.checkBox_tabletMode.Name = "checkBox_tabletMode";
            this.checkBox_tabletMode.Size = new System.Drawing.Size(86, 17);
            this.checkBox_tabletMode.TabIndex = 8;
            this.checkBox_tabletMode.Text = "Tablet Mode";
            this.checkBox_tabletMode.UseVisualStyleBackColor = true;
            this.checkBox_tabletMode.CheckedChanged += new System.EventHandler(this.checkBox_tabletMode_CheckedChanged);
            // 
            // checkBox_stayOnTop
            // 
            this.checkBox_stayOnTop.AutoSize = true;
            this.checkBox_stayOnTop.Location = new System.Drawing.Point(6, 95);
            this.checkBox_stayOnTop.Name = "checkBox_stayOnTop";
            this.checkBox_stayOnTop.Size = new System.Drawing.Size(86, 17);
            this.checkBox_stayOnTop.TabIndex = 7;
            this.checkBox_stayOnTop.Text = "Stay On Top";
            this.checkBox_stayOnTop.UseVisualStyleBackColor = true;
            this.checkBox_stayOnTop.CheckedChanged += new System.EventHandler(this.checkBox_stayOnTop_CheckedChanged);
            // 
            // checkBox_manualInterpolation
            // 
            this.checkBox_manualInterpolation.AutoSize = true;
            this.checkBox_manualInterpolation.Location = new System.Drawing.Point(6, 73);
            this.checkBox_manualInterpolation.Name = "checkBox_manualInterpolation";
            this.checkBox_manualInterpolation.Size = new System.Drawing.Size(122, 17);
            this.checkBox_manualInterpolation.TabIndex = 6;
            this.checkBox_manualInterpolation.Text = "Manual Interpolation";
            this.checkBox_manualInterpolation.UseVisualStyleBackColor = true;
            this.checkBox_manualInterpolation.CheckedChanged += new System.EventHandler(this.checkBox_manualInterpolation_CheckedChanged);
            // 
            // textBox_smoothingInterpolation
            // 
            this.textBox_smoothingInterpolation.Enabled = false;
            this.textBox_smoothingInterpolation.Location = new System.Drawing.Point(207, 40);
            this.textBox_smoothingInterpolation.MaxLength = 2;
            this.textBox_smoothingInterpolation.Name = "textBox_smoothingInterpolation";
            this.textBox_smoothingInterpolation.Size = new System.Drawing.Size(26, 20);
            this.textBox_smoothingInterpolation.TabIndex = 4;
            this.textBox_smoothingInterpolation.Text = "4";
            this.textBox_smoothingInterpolation.TextChanged += new System.EventHandler(this.textBox_smoothingInterpolation_TextChanged);
            // 
            // textBox_smoothingStrength
            // 
            this.textBox_smoothingStrength.Location = new System.Drawing.Point(207, 5);
            this.textBox_smoothingStrength.MaxLength = 3;
            this.textBox_smoothingStrength.Name = "textBox_smoothingStrength";
            this.textBox_smoothingStrength.Size = new System.Drawing.Size(26, 20);
            this.textBox_smoothingStrength.TabIndex = 4;
            this.textBox_smoothingStrength.Text = "30";
            this.textBox_smoothingStrength.TextChanged += new System.EventHandler(this.textBox_smoothingStrength_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Interpolation";
            // 
            // trackBar_smoothingInterpolation
            // 
            this.trackBar_smoothingInterpolation.Enabled = false;
            this.trackBar_smoothingInterpolation.Location = new System.Drawing.Point(64, 40);
            this.trackBar_smoothingInterpolation.Maximum = 20;
            this.trackBar_smoothingInterpolation.Name = "trackBar_smoothingInterpolation";
            this.trackBar_smoothingInterpolation.Size = new System.Drawing.Size(141, 45);
            this.trackBar_smoothingInterpolation.TabIndex = 1;
            this.trackBar_smoothingInterpolation.Value = 4;
            this.trackBar_smoothingInterpolation.Scroll += new System.EventHandler(this.trackBar_smoothingInterpolation_Scroll);
            // 
            // button_toggleDisplay
            // 
            this.button_toggleDisplay.BackColor = System.Drawing.Color.Gainsboro;
            this.button_toggleDisplay.BackgroundImage = global::Silky_Shark.Properties.Resources.toggledisplay;
            this.button_toggleDisplay.Location = new System.Drawing.Point(12, 119);
            this.button_toggleDisplay.Name = "button_toggleDisplay";
            this.button_toggleDisplay.Size = new System.Drawing.Size(26, 26);
            this.button_toggleDisplay.TabIndex = 5;
            this.toolTip_toggleOverScreen.SetToolTip(this.button_toggleDisplay, "Toggle which screen displays the overlay");
            this.button_toggleDisplay.UseVisualStyleBackColor = false;
            this.button_toggleDisplay.Click += new System.EventHandler(this.button_toggleScreen_Click);
            // 
            // button_smoothOnOff
            // 
            this.button_smoothOnOff.BackColor = System.Drawing.Color.Gainsboro;
            this.button_smoothOnOff.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_smoothOnOff.BackgroundImage")));
            this.button_smoothOnOff.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_smoothOnOff.Location = new System.Drawing.Point(12, 31);
            this.button_smoothOnOff.Name = "button_smoothOnOff";
            this.button_smoothOnOff.Size = new System.Drawing.Size(60, 60);
            this.button_smoothOnOff.TabIndex = 2;
            this.button_smoothOnOff.TabStop = false;
            this.toolTip_smoothingOnOff.SetToolTip(this.button_smoothOnOff, "Turn smoothing on/off");
            this.button_smoothOnOff.UseVisualStyleBackColor = false;
            this.button_smoothOnOff.Click += new System.EventHandler(this.button_SmoothOnOff_Click);
            // 
            // button_colorDialog
            // 
            this.button_colorDialog.BackColor = System.Drawing.Color.Gainsboro;
            this.button_colorDialog.BackgroundImage = global::Silky_Shark.Properties.Resources.eyedropper;
            this.button_colorDialog.Location = new System.Drawing.Point(46, 119);
            this.button_colorDialog.Name = "button_colorDialog";
            this.button_colorDialog.Size = new System.Drawing.Size(26, 26);
            this.button_colorDialog.TabIndex = 5;
            this.toolTip_cursorColor.SetToolTip(this.button_colorDialog, "Change the virtual cursor\'s color");
            this.button_colorDialog.UseVisualStyleBackColor = false;
            this.button_colorDialog.Click += new System.EventHandler(this.button_colorDialog_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(334, 151);
            this.Controls.Add(this.button_colorDialog);
            this.Controls.Add(this.button_toggleDisplay);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button_smoothOnOff);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "Silky Shark";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_smoothingStrength)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_smoothingInterpolation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_settings;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_help;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_exit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_about;
        private System.Windows.Forms.ToolTip toolTip_smoothingOnOff;
        private System.Windows.Forms.ToolTip toolTip_toggleOverScreen;
        private System.Windows.Forms.ToolTip toolTip_cursorColor;
        private System.Windows.Forms.ColorDialog colorDialog;
        public System.Windows.Forms.Button button_colorDialog;
        public System.Windows.Forms.CheckBox checkBox_tabletMode;
        public System.Windows.Forms.Button button_smoothOnOff;
        public System.Windows.Forms.Button button_toggleDisplay;
        public System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_saveConfig;
        public System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_restoreDefaults;
        public System.Windows.Forms.TrackBar trackBar_smoothingStrength;
        public System.Windows.Forms.TextBox textBox_smoothingStrength;
        public System.Windows.Forms.TextBox textBox_smoothingInterpolation;
        public System.Windows.Forms.TrackBar trackBar_smoothingInterpolation;
        public System.Windows.Forms.CheckBox checkBox_stayOnTop;
        public System.Windows.Forms.CheckBox checkBox_manualInterpolation;
        public System.Windows.Forms.CheckBox checkBox_smoothOnDraw;
    }
}

