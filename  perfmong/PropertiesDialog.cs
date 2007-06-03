using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace PerfMonG
{
	public class PropertiesDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label Xpos;
		private System.Windows.Forms.Label Ypos;
		private System.Windows.Forms.TextBox X;
		private System.Windows.Forms.TextBox Y;
		private System.Windows.Forms.Button Accept;
		private System.Windows.Forms.Button Cacel;
		private System.Windows.Forms.Label TopBar;

		private System.ComponentModel.Container components = null;

		
		private System.Windows.Forms.Button BgColor;
		private System.Windows.Forms.Label BgColorLabel;
		private System.Windows.Forms.Label TextColorLabel;
		private System.Windows.Forms.Button TextColor;
		private System.Windows.Forms.Button Defaults;
		private System.Windows.Forms.Label OpacityText;
		private System.Windows.Forms.Button SaveSettings;
		private System.Windows.Forms.TrackBar OpacityBar;

        private ColorDialog colors;

		private string x, y;
		private double opc;
		private int xp, yp;
		private Color bg, txt;

		private Config config;
		
			
		public PropertiesDialog(Config conf)
		{
			config = conf;

			InitializeComponent();

			OpacityBar.Minimum = 3;
			OpacityBar.Maximum = 10;
			OpacityBar.TickFrequency = 1;

            colors = new ColorDialog();
			
			this.x = config.X.ToString();
			this.y = config.Y.ToString();
			this.opc = config.OPC;
			this.bg = config.BG;
			this.txt = config.TXT;

			setProperties();

		}

		private void setProperties()
		{
			X.Text = this.x;
			Y.Text = this.y;
			OpacityBar.Value = (int) (this.opc * 10);
			BgColor.BackColor = this.bg;
			TextColor.BackColor = this.txt;
			xp = Int32.Parse(x);
			yp = Int32.Parse(y);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Xpos = new System.Windows.Forms.Label();
			this.Ypos = new System.Windows.Forms.Label();
			this.X = new System.Windows.Forms.TextBox();
			this.Y = new System.Windows.Forms.TextBox();
			this.Accept = new System.Windows.Forms.Button();
			this.Cacel = new System.Windows.Forms.Button();
			this.TopBar = new System.Windows.Forms.Label();
			this.BgColor = new System.Windows.Forms.Button();
			this.BgColorLabel = new System.Windows.Forms.Label();
			this.TextColorLabel = new System.Windows.Forms.Label();
			this.TextColor = new System.Windows.Forms.Button();
			this.Defaults = new System.Windows.Forms.Button();
			this.SaveSettings = new System.Windows.Forms.Button();
			this.OpacityBar = new System.Windows.Forms.TrackBar();
			this.OpacityText = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.OpacityBar)).BeginInit();
			this.SuspendLayout();
			// 
			// Xpos
			// 
			this.Xpos.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Xpos.Location = new System.Drawing.Point(152, 24);
			this.Xpos.Name = "Xpos";
			this.Xpos.Size = new System.Drawing.Size(64, 24);
			this.Xpos.TabIndex = 0;
			this.Xpos.Text = "X Position:";
			this.Xpos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Ypos
			// 
			this.Ypos.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Ypos.Location = new System.Drawing.Point(152, 56);
			this.Ypos.Name = "Ypos";
			this.Ypos.Size = new System.Drawing.Size(64, 16);
			this.Ypos.TabIndex = 1;
			this.Ypos.Text = "Y Position:";
			this.Ypos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// X
			// 
			this.X.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.X.Location = new System.Drawing.Point(216, 24);
			this.X.Name = "X";
			this.X.Size = new System.Drawing.Size(32, 18);
			this.X.TabIndex = 2;
			this.X.Text = "";
			// 
			// Y
			// 
			this.Y.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Y.Location = new System.Drawing.Point(216, 56);
			this.Y.Name = "Y";
			this.Y.Size = new System.Drawing.Size(32, 18);
			this.Y.TabIndex = 3;
			this.Y.Text = "";
			// 
			// Accept
			// 
			this.Accept.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Accept.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Accept.Location = new System.Drawing.Point(16, 88);
			this.Accept.Name = "Accept";
			this.Accept.Size = new System.Drawing.Size(64, 24);
			this.Accept.TabIndex = 6;
			this.Accept.Text = "Accept";
			this.Accept.Click += new System.EventHandler(this.Accept_Click);
			// 
			// Cacel
			// 
			this.Cacel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Cacel.Location = new System.Drawing.Point(88, 88);
			this.Cacel.Name = "Cacel";
			this.Cacel.Size = new System.Drawing.Size(64, 24);
			this.Cacel.TabIndex = 7;
			this.Cacel.Text = "Cancel";
			this.Cacel.Click += new System.EventHandler(this.Camcel_Click);
			// 
			// TopBar
			// 
			this.TopBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TopBar.Location = new System.Drawing.Point(24, 0);
			this.TopBar.Name = "TopBar";
			this.TopBar.Size = new System.Drawing.Size(232, 24);
			this.TopBar.TabIndex = 8;
			this.TopBar.Text = "PerfMon Properties Editor";
			this.TopBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// BgColor
			// 
			this.BgColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BgColor.Location = new System.Drawing.Point(128, 24);
			this.BgColor.Name = "BgColor";
			this.BgColor.Size = new System.Drawing.Size(16, 16);
			this.BgColor.TabIndex = 9;
			this.BgColor.Click += new System.EventHandler(this.BgColor_Click);
			// 
			// BgColorLabel
			// 
			this.BgColorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.BgColorLabel.Location = new System.Drawing.Point(16, 24);
			this.BgColorLabel.Name = "BgColorLabel";
			this.BgColorLabel.Size = new System.Drawing.Size(104, 24);
			this.BgColorLabel.TabIndex = 10;
			this.BgColorLabel.Text = "Background Color:";
			this.BgColorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// TextColorLabel
			// 
			this.TextColorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.TextColorLabel.Location = new System.Drawing.Point(16, 56);
			this.TextColorLabel.Name = "TextColorLabel";
			this.TextColorLabel.Size = new System.Drawing.Size(104, 24);
			this.TextColorLabel.TabIndex = 11;
			this.TextColorLabel.Text = "Text Color:";
			this.TextColorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// TextColor
			// 
			this.TextColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.TextColor.Location = new System.Drawing.Point(128, 56);
			this.TextColor.Name = "TextColor";
			this.TextColor.Size = new System.Drawing.Size(16, 16);
			this.TextColor.TabIndex = 12;
			this.TextColor.Click += new System.EventHandler(this.TextColor_Click);
			// 
			// Defaults
			// 
			this.Defaults.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Defaults.Location = new System.Drawing.Point(272, 88);
			this.Defaults.Name = "Defaults";
			this.Defaults.Size = new System.Drawing.Size(112, 24);
			this.Defaults.TabIndex = 13;
			this.Defaults.Text = "Reset Settings";
			this.Defaults.Click += new System.EventHandler(this.Defaults_Click);
			// 
			// SaveSettings
			// 
			this.SaveSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.SaveSettings.Location = new System.Drawing.Point(160, 88);
			this.SaveSettings.Name = "SaveSettings";
			this.SaveSettings.Size = new System.Drawing.Size(104, 24);
			this.SaveSettings.TabIndex = 14;
			this.SaveSettings.Text = "Save Settings";
			this.SaveSettings.Click += new System.EventHandler(this.SaveSettings_Click);
			// 
			// OpacityBar
			// 
			this.OpacityBar.Location = new System.Drawing.Point(272, 32);
			this.OpacityBar.Name = "OpacityBar";
			this.OpacityBar.Size = new System.Drawing.Size(96, 45);
			this.OpacityBar.TabIndex = 15;
			// 
			// OpacityText
			// 
			this.OpacityText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.OpacityText.Location = new System.Drawing.Point(272, 16);
			this.OpacityText.Name = "OpacityText";
			this.OpacityText.Size = new System.Drawing.Size(88, 16);
			this.OpacityText.TabIndex = 16;
			this.OpacityText.Text = "Opacity";
			this.OpacityText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// PropertiesDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(392, 118);
			this.ControlBox = false;
			this.Controls.Add(this.OpacityText);
			this.Controls.Add(this.OpacityBar);
			this.Controls.Add(this.SaveSettings);
			this.Controls.Add(this.Defaults);
			this.Controls.Add(this.TextColor);
			this.Controls.Add(this.TextColorLabel);
			this.Controls.Add(this.BgColorLabel);
			this.Controls.Add(this.BgColor);
			this.Controls.Add(this.TopBar);
			this.Controls.Add(this.Cacel);
			this.Controls.Add(this.Accept);
			this.Controls.Add(this.Y);
			this.Controls.Add(this.X);
			this.Controls.Add(this.Ypos);
			this.Controls.Add(this.Xpos);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PropertiesDialog";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PropertiesDialog";
			((System.ComponentModel.ISupportInitialize)(this.OpacityBar)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void Camcel_Click(object sender, System.EventArgs e)
		{
			setProperties();
			this.Close();
		}

		private void BgColor_Click(object sender, System.EventArgs e)
		{
			colors.Color = this.bg;
			if (colors.ShowDialog() == DialogResult.OK)
			{
				this.bg = colors.Color;
				setProperties();
			}

		}

		private void TextColor_Click(object sender, System.EventArgs e)
		{
			colors.Color = this.txt;
			if (colors.ShowDialog() == DialogResult.OK)
			{
				this.txt = colors.Color;
				setProperties();
			}
		}

		private void Accept_Click(object sender, System.EventArgs e)
		{
			x = X.Text;
			y = Y.Text;
			opc = (double) ((double)(OpacityBar.Value) / (double)10);
			setProperties();
		}

		private void Defaults_Click(object sender, System.EventArgs e)
		{
			bg = config.BG;
			txt = config.TXT;
			opc = config.OPC;
			x = config.X.ToString();
			y = config.Y.ToString();
			
			setProperties();
		}

		private void SaveSettings_Click(object sender, System.EventArgs e)
		{
			if(MessageBox.Show(this, "Are you sure you want to save these values?\nYour configuration file will be overwritten!" ,"Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
				config.writeConfig(Int32.Parse(X.Text), Int32.Parse(Y.Text), (double) ((double)(OpacityBar.Value) / (double)10), BgColor.BackColor, TextColor.BackColor); 
		}

		// RETURN VALUES

		public Color Bg { get{return bg; } }
		public Color Txt { get{return txt; } }
		public int Xp {	get { return xp; }	}
		public int Yp {	get {return yp; } }
		public double OPC { get { return opc; } }
	

			
	}
}
