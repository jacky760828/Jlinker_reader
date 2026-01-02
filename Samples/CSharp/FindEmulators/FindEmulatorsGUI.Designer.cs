namespace JLink_Find_Emulators
{
  partial class FindEmulatorsGUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindEmulatorsGUI));
            this.button2 = new System.Windows.Forms.Button();
            this.status_lab = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tb_app_bin_path = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.MCU_lab = new System.Windows.Forms.Label();
            this.Log_text = new System.Windows.Forms.RichTextBox();
            this.burn_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.product = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.NEW_FONT = new System.Windows.Forms.CheckBox();
            this.OLD_FONT = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.fandataGridView = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.TxtAddr = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.file_read = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.HEX_OLD = new System.Windows.Forms.CheckBox();
            this.HEX_NEW = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.year = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.moth = new System.Windows.Forms.Label();
            this.TIME = new System.Windows.Forms.Label();
            this.FAN_control = new System.Windows.Forms.Label();
            this.uart_use = new System.Windows.Forms.Label();
            this.protect = new System.Windows.Forms.Label();
            this.fan_c = new System.Windows.Forms.Label();
            this.Hex_read = new System.Windows.Forms.DataGridView();
            this.HEX_File_read = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fandataGridView)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Hex_read)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.SearchDevices);
            // 
            // status_lab
            // 
            resources.ApplyResources(this.status_lab, "status_lab");
            this.status_lab.Name = "status_lab";
            this.status_lab.Click += new System.EventHandler(this.label1_Click);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tb_app_bin_path
            // 
            resources.ApplyResources(this.tb_app_bin_path, "tb_app_bin_path");
            this.tb_app_bin_path.Name = "tb_app_bin_path";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // linkLabel1
            // 
            resources.ApplyResources(this.linkLabel1, "linkLabel1");
            this.linkLabel1.Name = "linkLabel1";
            // 
            // MCU_lab
            // 
            resources.ApplyResources(this.MCU_lab, "MCU_lab");
            this.MCU_lab.Name = "MCU_lab";
            // 
            // Log_text
            // 
            resources.ApplyResources(this.Log_text, "Log_text");
            this.Log_text.Name = "Log_text";
            // 
            // burn_button
            // 
            this.burn_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.burn_button, "burn_button");
            this.burn_button.Name = "burn_button";
            this.burn_button.UseVisualStyleBackColor = true;
            this.burn_button.Click += new System.EventHandler(this.burn_button_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // product
            // 
            resources.ApplyResources(this.product, "product");
            this.product.Name = "product";
            this.product.UseMnemonic = false;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Name = "label7";
            // 
            // NEW_FONT
            // 
            resources.ApplyResources(this.NEW_FONT, "NEW_FONT");
            this.NEW_FONT.Name = "NEW_FONT";
            this.NEW_FONT.UseVisualStyleBackColor = true;
            // 
            // OLD_FONT
            // 
            resources.ApplyResources(this.OLD_FONT, "OLD_FONT");
            this.OLD_FONT.Name = "OLD_FONT";
            this.OLD_FONT.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.fandataGridView);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // fandataGridView
            // 
            this.fandataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.fandataGridView, "fandataGridView");
            this.fandataGridView.Name = "fandataGridView";
            this.fandataGridView.RowTemplate.Height = 31;
            this.fandataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.fandataGridView_CellContentClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.Log_text);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // TxtAddr
            // 
            resources.ApplyResources(this.TxtAddr, "TxtAddr");
            this.TxtAddr.Name = "TxtAddr";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txtFilePath
            // 
            this.txtFilePath.BackColor = System.Drawing.SystemColors.ButtonFace;
            resources.ApplyResources(this.txtFilePath, "txtFilePath");
            this.txtFilePath.Name = "txtFilePath";
            // 
            // file_read
            // 
            resources.ApplyResources(this.file_read, "file_read");
            this.file_read.Name = "file_read";
            this.file_read.UseVisualStyleBackColor = true;
            this.file_read.Click += new System.EventHandler(this.file_read_Click);
            // 
            // button5
            // 
            resources.ApplyResources(this.button5, "button5");
            this.button5.Name = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // HEX_OLD
            // 
            resources.ApplyResources(this.HEX_OLD, "HEX_OLD");
            this.HEX_OLD.Name = "HEX_OLD";
            this.HEX_OLD.UseVisualStyleBackColor = true;
            // 
            // HEX_NEW
            // 
            resources.ApplyResources(this.HEX_NEW, "HEX_NEW");
            this.HEX_NEW.Name = "HEX_NEW";
            this.HEX_NEW.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // year
            // 
            resources.ApplyResources(this.year, "year");
            this.year.Name = "year";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // moth
            // 
            resources.ApplyResources(this.moth, "moth");
            this.moth.Name = "moth";
            // 
            // TIME
            // 
            resources.ApplyResources(this.TIME, "TIME");
            this.TIME.Name = "TIME";
            // 
            // FAN_control
            // 
            resources.ApplyResources(this.FAN_control, "FAN_control");
            this.FAN_control.Name = "FAN_control";
            // 
            // uart_use
            // 
            resources.ApplyResources(this.uart_use, "uart_use");
            this.uart_use.Name = "uart_use";
            // 
            // protect
            // 
            resources.ApplyResources(this.protect, "protect");
            this.protect.Name = "protect";
            // 
            // fan_c
            // 
            resources.ApplyResources(this.fan_c, "fan_c");
            this.fan_c.Name = "fan_c";
            // 
            // Hex_read
            // 
            this.Hex_read.AllowUserToOrderColumns = true;
            this.Hex_read.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.Hex_read, "Hex_read");
            this.Hex_read.Name = "Hex_read";
            this.Hex_read.RowTemplate.Height = 31;
            this.Hex_read.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Hex_read_CellContentClick_1);
            // 
            // HEX_File_read
            // 
            resources.ApplyResources(this.HEX_File_read, "HEX_File_read");
            this.HEX_File_read.Name = "HEX_File_read";
            this.HEX_File_read.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.groupBox2.UseCompatibleTextRendering = true;
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // FindEmulatorsGUI
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.CancelButton = this.button1;
            this.Controls.Add(this.Hex_read);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.HEX_File_read);
            this.Controls.Add(this.fan_c);
            this.Controls.Add(this.protect);
            this.Controls.Add(this.uart_use);
            this.Controls.Add(this.FAN_control);
            this.Controls.Add(this.TIME);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.moth);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.year);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.HEX_NEW);
            this.Controls.Add(this.HEX_OLD);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.file_read);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.OLD_FONT);
            this.Controls.Add(this.NEW_FONT);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.product);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.TxtAddr);
            this.Controls.Add(this.burn_button);
            this.Controls.Add(this.MCU_lab);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_app_bin_path);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.status_lab);
            this.Controls.Add(this.button2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FindEmulatorsGUI";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Gainsboro;
            this.Load += new System.EventHandler(this.FindEmulatorsGUI_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fandataGridView)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Hex_read)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label status_lab;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tb_app_bin_path;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label MCU_lab;
        private System.Windows.Forms.RichTextBox Log_text;
        private System.Windows.Forms.Button burn_button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label product;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox NEW_FONT;
        private System.Windows.Forms.CheckBox OLD_FONT;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView fandataGridView;
        private System.Windows.Forms.TextBox TxtAddr;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button file_read;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.CheckBox HEX_OLD;
        private System.Windows.Forms.CheckBox HEX_NEW;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label year;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label moth;
        private System.Windows.Forms.Label TIME;
        private System.Windows.Forms.Label FAN_control;
        private System.Windows.Forms.Label uart_use;
        private System.Windows.Forms.Label protect;
        private System.Windows.Forms.Label fan_c;
        private System.Windows.Forms.DataGridView Hex_read;
        private System.Windows.Forms.GroupBox HEX_File_read;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label16;
    }
}

