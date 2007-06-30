/*
	PerfMonG (c) Lukasz Grzegorz Maciak
	contributions from mike@teamsandbox.com  
*/

using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace PerfMonG
{
	//all we got to do to make it draggable is inherit from FormBase class in DraggableForm.cs file
	public class PerfMonG : DraggableBase
	{
		private System.Windows.Forms.Label CPUText;
		private System.Windows.Forms.Label CPU;
		private System.Windows.Forms.Label RAM;
		private System.Windows.Forms.Label uptime;
		private System.Windows.Forms.Label UptimeLabel;
		private System.Windows.Forms.Label pageName;
		private System.Windows.Forms.Label page;
		private System.Windows.Forms.Label ProcsLabel;
		private System.Windows.Forms.Label procs;
		private System.Windows.Forms.Label RAMText;
		private System.Windows.Forms.Label HDLabel;
		private System.Windows.Forms.Label hd;
			
		private System.ComponentModel.Container components = null;

		private ContextMenu menu;
		private MenuItem exit;
		private MenuItem about;
		private MenuItem properties;

		private PerformanceMonitor pm;
		private Timer tm;

		private PropertiesDialog props;

		private Config configuration;

		public const string VERSION = "0.2.6-rc";
		public const string VERSION_MSG = "PerfMon " + VERSION + " (c) 2004 Lukasz Grzegorz Maciak";

		// EDITABLE SYSTEM VARIABLES:
		private int xPosition;
		private int yPosition;
		private double opacity;
		private Color backgroundColor;
		private Color textColor;

		private int cpuUpperTreshold;
		private int cpuLowerTreshold;
		private string date;
		
		private const string information = VERSION_MSG;


		/// <summary>
		/// Checks the Current size of the screen and positions the form if it was left off screen
		/// (for dual monitor to single monitor issue # 5) 
		/// mike mckinnon june 22 07
		/// </summary>
		private void CheckScreen()
		{
			int upperBound; 
			int tmpX = this.Location.X;
			int tmpY = this.Location.Y;

			Screen [] screens = Screen.AllScreens; 
			upperBound = screens.GetUpperBound(0);
			
			//they have only one screen
			if(upperBound == 0)
			{ 
				//check that the X and Y pos of the form are not less than the reg points 
				//of the screen or greater than the width and height of the screen 
				if(tmpX < screens[0].WorkingArea.X || tmpX > screens[0].WorkingArea.Width)
				{
					tmpX = screens[0].WorkingArea.X; 
				}
				if(tmpY < screens[0].WorkingArea.Y || tmpY > screens[0].WorkingArea.Height)
				{
					tmpY = screens[0].WorkingArea.Y;
				}
				//set new location (will usually go to 0,0 if the form was misplaced before 
				//otherwise put in its normal saved position 
				this.Location = new Point(tmpX, tmpY);
		 
			}
		}
        
		public PerfMonG()
		{
			InitializeComponent();
			
			configuration = new Config();
			setVars();
			
			this.Text = "PerfMon (c) lgm";

			Configure();
			//mcm added 
			CheckScreen(); 

			menu = new ContextMenu();
			about = new MenuItem("About");
			properties = new MenuItem("Properties");
			exit = new MenuItem("Exit");

			about.Click +=new EventHandler(DisplayAbout);
			properties.Click +=new EventHandler(PropertiesWindow);
			exit.Click +=new EventHandler(Close);

			menu.MenuItems.Add(about);
			menu.MenuItems.Add(properties);
			menu.MenuItems.Add(exit);

			this.ContextMenu = menu;

			props = new PropertiesDialog(configuration);

			pm = new PerformanceMonitor();
			tm = new Timer();

			tm.Tick +=new EventHandler(TimeTrigger);

			tm.Interval = 1000;
			tm.Start();
		}

		private void setVars()
		{
			xPosition = configuration.X;
			yPosition = configuration.Y;
			opacity = configuration.OPC;
			backgroundColor = configuration.BG;
			textColor = configuration.TXT;

			cpuUpperTreshold = configuration.CPU_MAX;
			cpuLowerTreshold = configuration.CPU_MIN;
			date = configuration.DATE;
		}
		
		private void Configure()
		{
			this.FormBorderStyle = FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.StartPosition = FormStartPosition.Manual;
			this.Location = new Point(xPosition, yPosition);
			this.ControlBox = false;
			this.TopMost = false;
			this.ShowInTaskbar = false;

			//mike@teamsandbox.com : added the fore colors for the bottom row of items
			this.BackColor = Color.Gray;
			this.CPUText.BackColor = backgroundColor;
			this.CPUText.ForeColor = textColor;
			this.CPU.BackColor = backgroundColor;
            this.RAMText.BackColor = backgroundColor;
			this.RAMText.ForeColor = textColor;
			this.RAM.BackColor = backgroundColor;
			this.RAM.ForeColor = textColor;
			this.uptime.BackColor = backgroundColor;
			this.uptime.ForeColor = textColor;
			this.UptimeLabel.BackColor = backgroundColor;
			this.UptimeLabel.ForeColor = textColor;
			this.pageName.BackColor = backgroundColor;
			this.pageName.ForeColor = textColor; 
			this.page.BackColor = backgroundColor;
			this.page.ForeColor = textColor; 
			this.ProcsLabel.BackColor = backgroundColor;
			this.ProcsLabel.ForeColor = textColor; 
			this.procs.BackColor = backgroundColor;
			this.procs.ForeColor = textColor; 
			this.HDLabel.BackColor = backgroundColor;
			this.HDLabel.ForeColor = textColor; 
			this.hd.BackColor = backgroundColor;
			this.hd.ForeColor = textColor; 
			
			this.Opacity = opacity;
			this.TransparencyKey = Color.Gray;
		}

		private void TimeTrigger(Object sender, EventArgs args)
		{
			
			float cp = pm.getCurrentCpuUsage();
			
			if( cp >= cpuUpperTreshold)
				CPU.ForeColor = Color.Red;
			else
                if(cp <= cpuLowerTreshold)
					CPU.ForeColor = Color.Blue;
				else
					CPU.ForeColor = SystemColors.ControlText;
            
			CPU.Text = cp.ToString("F2") +"%";
			RAM.Text = pm.getAvailableRAM();
			uptime.Text = pm.getUptime();
			page.Text = pm.getPagefile().ToString("F2") + "%";
			procs.Text = pm.getProcesses();
			hd.Text = pm.getDisk();
		}

		
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		private void InitializeComponent()
		{
			this.CPUText = new System.Windows.Forms.Label();
			this.CPU = new System.Windows.Forms.Label();
			this.RAM = new System.Windows.Forms.Label();
			this.uptime = new System.Windows.Forms.Label();
			this.UptimeLabel = new System.Windows.Forms.Label();
			this.pageName = new System.Windows.Forms.Label();
			this.page = new System.Windows.Forms.Label();
			this.RAMText = new System.Windows.Forms.Label();
			this.ProcsLabel = new System.Windows.Forms.Label();
			this.procs = new System.Windows.Forms.Label();
			this.HDLabel = new System.Windows.Forms.Label();
			this.hd = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// CPUText
			// 
			this.CPUText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.CPUText.Location = new System.Drawing.Point(8, 0);
			this.CPUText.Name = "CPUText";
			this.CPUText.Size = new System.Drawing.Size(72, 16);
			this.CPUText.TabIndex = 0;
			this.CPUText.Text = "CPU Usage:";
			// 
			// CPU
			// 
			this.CPU.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.CPU.Location = new System.Drawing.Point(72, 0);
			this.CPU.Name = "CPU";
			this.CPU.Size = new System.Drawing.Size(64, 16);
			this.CPU.TabIndex = 1;
			this.CPU.Text = "0%";
			// 
			// RAM
			// 
			this.RAM.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.RAM.Location = new System.Drawing.Point(176, 0);
			this.RAM.Name = "RAM";
			this.RAM.Size = new System.Drawing.Size(48, 16);
			this.RAM.TabIndex = 3;
			this.RAM.Text = "0MB";
			// 
			// uptime
			// 
			this.uptime.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.uptime.Location = new System.Drawing.Point(272, 0);
			this.uptime.Name = "uptime";
			this.uptime.Size = new System.Drawing.Size(72, 16);
			this.uptime.TabIndex = 4;
			this.uptime.Text = "1h 20m 10s";
			// 
			// UptimeLabel
			// 
			this.UptimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.UptimeLabel.Location = new System.Drawing.Point(224, 0);
			this.UptimeLabel.Name = "UptimeLabel";
			this.UptimeLabel.Size = new System.Drawing.Size(48, 16);
			this.UptimeLabel.TabIndex = 5;
			this.UptimeLabel.Text = "Uptime:";
			// 
			// pageName
			// 
			this.pageName.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pageName.Location = new System.Drawing.Point(8, 16);
			this.pageName.Name = "pageName";
			this.pageName.Size = new System.Drawing.Size(72, 16);
			this.pageName.TabIndex = 6;
			this.pageName.Text = "SWAP:";
			// 
			// page
			// 
			this.page.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.page.Location = new System.Drawing.Point(72, 16);
			this.page.Name = "page";
			this.page.Size = new System.Drawing.Size(64, 16);
			this.page.TabIndex = 7;
			this.page.Text = "0%";
			// 
			// RAMText
			// 
			this.RAMText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.RAMText.Location = new System.Drawing.Point(112, 0);
			this.RAMText.Name = "RAMText";
			this.RAMText.Size = new System.Drawing.Size(64, 16);
			this.RAMText.TabIndex = 2;
			this.RAMText.Text = "Free RAM:";
			// 
			// ProcsLabel
			// 
			this.ProcsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ProcsLabel.Location = new System.Drawing.Point(112, 16);
			this.ProcsLabel.Name = "ProcsLabel";
			this.ProcsLabel.Size = new System.Drawing.Size(64, 16);
			this.ProcsLabel.TabIndex = 8;
			this.ProcsLabel.Text = "Processes:";
			// 
			// procs
			// 
			this.procs.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.procs.Location = new System.Drawing.Point(176, 16);
			this.procs.Name = "procs";
			this.procs.Size = new System.Drawing.Size(48, 16);
			this.procs.TabIndex = 9;
			this.procs.Text = "0";
			// 
			// HDLabel
			// 
			this.HDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.HDLabel.Location = new System.Drawing.Point(224, 16);
			this.HDLabel.Name = "HDLabel";
			this.HDLabel.Size = new System.Drawing.Size(48, 16);
			this.HDLabel.TabIndex = 10;
			this.HDLabel.Text = "Free HD:";
			// 
			// hd
			// 
			this.hd.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.hd.Location = new System.Drawing.Point(272, 16);
			this.hd.Name = "hd";
			this.hd.Size = new System.Drawing.Size(72, 16);
			this.hd.TabIndex = 11;
			this.hd.Text = "0 MB";
			// 
			// PerfMonG
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(344, 30);
			this.Controls.Add(this.hd);
			this.Controls.Add(this.HDLabel);
			this.Controls.Add(this.procs);
			this.Controls.Add(this.ProcsLabel);
			this.Controls.Add(this.page);
			this.Controls.Add(this.pageName);
			this.Controls.Add(this.UptimeLabel);
			this.Controls.Add(this.uptime);
			this.Controls.Add(this.RAM);
			this.Controls.Add(this.RAMText);
			this.Controls.Add(this.CPU);
			this.Controls.Add(this.CPUText);
			this.Name = "PerfMonG";
			this.Text = "Form1";
			this.ResumeLayout(false);
		}


		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			try
			{
				Application.Run(new PerfMonG());
			}
			catch(Exception e)
			{
				MessageBox.Show(e.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
		}

		private void Close(object sender, EventArgs e)
		{
			if(MessageBox.Show(this, "Save settings?", "Exiting PerfMonG", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
			{
				resetConfig();
				configuration.writeConfig();
			}
 

			this.Close();
			this.Dispose();
		}

		private void DisplayAbout(object sender, EventArgs e)
		{
			MessageBox.Show(this, information, "About PerfMon", MessageBoxButtons.OK);

		}

		private void resetConfig()
		{
			//mike@teamsandbox.com :
			//re-write all config to memory so the props panel shows it correctly. 
			props.Dispose(); 
			configuration.X = this.Location.X; 
			configuration.Y = this.Location.Y;
			configuration.OPC = this.opacity; 
			configuration.BG = this.backgroundColor;
			configuration.TXT = this.textColor;
			configuration.CPU_MAX = this.cpuUpperTreshold;
			configuration.CPU_MIN = this.cpuLowerTreshold;
			configuration.DATE = this.date;
 
			//re-create props
			props = new PropertiesDialog(configuration);
		}

		private void PropertiesWindow(object sender, EventArgs e)
		{
			resetConfig();
	 
			if(props.ShowDialog(this)== DialogResult.OK)
			{
				xPosition = props.Xp;
				yPosition = props.Yp;
				opacity = props.OPC;
				backgroundColor = props.Bg;
				textColor = props.Txt;
				cpuUpperTreshold = props.CPMX;
				cpuLowerTreshold = props.CPMN;
				date = props.DATE;


				Configure();
			}
		}

	}
}
