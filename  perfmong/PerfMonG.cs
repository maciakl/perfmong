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
		#region members declaration
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

		public const string VERSION = "0.2.7";
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
		private System.Windows.Forms.Label CPUTotal;
		private System.Windows.Forms.Label CPUTotalText;
		
		private const string information = VERSION_MSG;
		#endregion 

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
			//determine if there is more than one processor 
			//if there is build labels and mouse events
			if(pm.getCpuProcessors() > 1)
			{ 
				for(int i=0; i<pm.getCpuProcessors();i++)
				{
					createCpuTextLabel(i); 
					createCpuDataLabel(i);
				}
				this.CPUTotalText.MouseHover += new System.EventHandler(this.CPUTotal_MouseHover);
				this.CPUTotalText.MouseLeave += new System.EventHandler(this.CPUTotal_MouseLeave);
			}
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

			//mike@teamsandbox.com : more elegant way to handle changing the colors				
			foreach(Control c in this.Controls)
			{
				if(c.GetType().ToString() == "System.Windows.Forms.Label")
				{
					Label lblTmp = (Label)c; 
					lblTmp.BackColor = backgroundColor; 
					lblTmp.ForeColor = textColor; 
				}
			}
			this.BackColor = Color.Gray;		
			this.Opacity = opacity;
			this.TransparencyKey = Color.Gray;
		}

		private void TimeTrigger(Object sender, EventArgs args)
		{
			int processorCount = pm.getCpuProcessors(); 
			//loop through processors and write output to each label that needs it.
			for(int i =0;i<processorCount;++i)
			{
				float cp = pm.getCurrentCpuUsage(i);
				string tmp = "CPU" + i; 
				foreach(Control c in this.Controls)
				{
					if(c.Name.ToString() == tmp)
					{
						if( cp >= cpuUpperTreshold)
							c.ForeColor = Color.Red;
						else
							if(cp <= cpuLowerTreshold)
							c.ForeColor = Color.Blue; 
						else
							c.ForeColor = SystemColors.ControlText; 
						c.Text = cp.ToString("F2") + "%"; 
					}
				}
			}

			//get total cpu usage - this is redundant
			///TODO: make a text formatting class or method that will handle the color changing by itself 
			///to remove this duplicated logic
			
			float cptotal = pm.getTotalCpuUsage(); 
			if( cptotal >= cpuUpperTreshold)
				CPUTotal.ForeColor = Color.Red;
			else
				if(cptotal <= cpuLowerTreshold)
				CPUTotal.ForeColor = Color.Blue; 
			else
				CPUTotal.ForeColor = SystemColors.ControlText; 
			CPUTotal.Text = cptotal.ToString("F2") + "%"; 
			RAM.Text = pm.getAvailableRAM();
			uptime.Text = pm.getUptime();
			page.Text = pm.getPagefile().ToString("F2") + "%";
			procs.Text = pm.getProcesses();
			hd.Text = pm.getDisk();
		}

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


		//mcm - methods to create labels for data and text 
		private void createCpuTextLabel(int i)
		{
			Label lbl = new Label(); 
			lbl.BackColor = backgroundColor; 
			lbl.ForeColor = textColor;
			lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			lbl.Left = 8; 
			lbl.Height = 18; 
			lbl.Width = 64; 
			lbl.Name = "CPU" + (i+1) + "Text"; 
			lbl.Text = "CPU" + (i+1) + " Usage:"; 
			lbl.Visible = false; 
			if(i==0)
			{
				lbl.Top = (this.CPUTotalText.Top - lbl.Height);
			}
			else
			{ 
				string tmp = "CPU" + i + "Text";
				foreach(Control c in this.Controls)
				{
					if(c.Name.ToString() == tmp)
					{
						Label prevLbl = (Label)c; 
						lbl.Top = (prevLbl.Top - lbl.Height); 
					}

				}
			}
			this.Controls.Add(lbl);

		}

		private void createCpuDataLabel(int i)
		{
			Label lbl = new Label(); 
			lbl.BackColor = backgroundColor; 
			lbl.ForeColor = textColor;
			lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			lbl.Left = 72; 
			lbl.Height = 18; 
			lbl.Width = 39; 
			lbl.Name = "CPU" + i;
			lbl.Text = "%";  
			lbl.Visible = false; 
			if(i==0)
			{
				lbl.Top = (this.CPUTotalText.Top - lbl.Height);
			}
			else
			{ 
				string tmp = "CPU" + i + "Text";
				foreach(Control c in this.Controls)
				{
					if(c.Name.ToString() == tmp)
					{
						Label prevLbl = (Label)c; 
						lbl.Top = (prevLbl.Top - lbl.Height); 
					}

				}
			}
			this.Controls.Add(lbl);
		}

		//mcm - mouse hover and leave events 
		protected void CPUTotal_MouseHover(object sender, System.EventArgs e)
		{
			foreach(Control c in this.Controls)
			{
				if(c.GetType().ToString() == "System.Windows.Forms.Label")
				{
					if(c.Name.Length > 3) 
						if(c.Name.Substring(0,3) == "CPU")
						{
							c.Visible = true; 
						}
				}
			}
		}

		protected void CPUTotal_MouseLeave(object sender, System.EventArgs e)
		{
			foreach(Control c in this.Controls)
			{
				if(c.GetType().ToString() == "System.Windows.Forms.Label")
				{
					if(c.Name.Length > 3) 
						//want to hide any labels that are for the CPU but not the CPUTotal Label.
						if(c.Name.Substring(0,3) == "CPU" && c.Name.Substring(0,4) != "CPUT")
						{
							c.Visible = false; 
						}
				}
			}
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
			this.CPUTotalText = new System.Windows.Forms.Label();
			this.CPUTotal = new System.Windows.Forms.Label();
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
			// CPUTotalText
			// 
			this.CPUTotalText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.CPUTotalText.Location = new System.Drawing.Point(8, 56);
			this.CPUTotalText.Name = "CPUTotalText";
			this.CPUTotalText.Size = new System.Drawing.Size(72, 16);
			this.CPUTotalText.TabIndex = 0;
			this.CPUTotalText.Text = "CPU Usage:";
			// 
			// CPUTotal
			// 
			this.CPUTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.CPUTotal.Location = new System.Drawing.Point(72, 56);
			this.CPUTotal.Name = "CPUTotal";
			this.CPUTotal.Size = new System.Drawing.Size(64, 16);
			this.CPUTotal.TabIndex = 1;
			this.CPUTotal.Text = "0%";
			// 
			// RAM
			// 
			this.RAM.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.RAM.Location = new System.Drawing.Point(176, 56);
			this.RAM.Name = "RAM";
			this.RAM.Size = new System.Drawing.Size(48, 16);
			this.RAM.TabIndex = 3;
			this.RAM.Text = "0MB";
			// 
			// uptime
			// 
			this.uptime.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.uptime.Location = new System.Drawing.Point(272, 56);
			this.uptime.Name = "uptime";
			this.uptime.Size = new System.Drawing.Size(72, 16);
			this.uptime.TabIndex = 4;
			this.uptime.Text = "1h 20m 10s";
			// 
			// UptimeLabel
			// 
			this.UptimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.UptimeLabel.Location = new System.Drawing.Point(224, 56);
			this.UptimeLabel.Name = "UptimeLabel";
			this.UptimeLabel.Size = new System.Drawing.Size(48, 16);
			this.UptimeLabel.TabIndex = 5;
			this.UptimeLabel.Text = "Uptime:";
			// 
			// pageName
			// 
			this.pageName.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.pageName.Location = new System.Drawing.Point(8, 72);
			this.pageName.Name = "pageName";
			this.pageName.Size = new System.Drawing.Size(72, 16);
			this.pageName.TabIndex = 6;
			this.pageName.Text = "SWAP:";
			// 
			// page
			// 
			this.page.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.page.Location = new System.Drawing.Point(72, 72);
			this.page.Name = "page";
			this.page.Size = new System.Drawing.Size(64, 16);
			this.page.TabIndex = 7;
			this.page.Text = "0%";
			// 
			// RAMText
			// 
			this.RAMText.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.RAMText.Location = new System.Drawing.Point(112, 56);
			this.RAMText.Name = "RAMText";
			this.RAMText.Size = new System.Drawing.Size(64, 16);
			this.RAMText.TabIndex = 2;
			this.RAMText.Text = "Free RAM:";
			// 
			// ProcsLabel
			// 
			this.ProcsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ProcsLabel.Location = new System.Drawing.Point(112, 72);
			this.ProcsLabel.Name = "ProcsLabel";
			this.ProcsLabel.Size = new System.Drawing.Size(64, 16);
			this.ProcsLabel.TabIndex = 8;
			this.ProcsLabel.Text = "Processes:";
			// 
			// procs
			// 
			this.procs.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.procs.Location = new System.Drawing.Point(176, 72);
			this.procs.Name = "procs";
			this.procs.Size = new System.Drawing.Size(48, 16);
			this.procs.TabIndex = 9;
			this.procs.Text = "0";
			// 
			// HDLabel
			// 
			this.HDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.HDLabel.Location = new System.Drawing.Point(224, 72);
			this.HDLabel.Name = "HDLabel";
			this.HDLabel.Size = new System.Drawing.Size(48, 16);
			this.HDLabel.TabIndex = 10;
			this.HDLabel.Text = "Free HD:";
			// 
			// hd
			// 
			this.hd.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.hd.Location = new System.Drawing.Point(272, 72);
			this.hd.Name = "hd";
			this.hd.Size = new System.Drawing.Size(72, 16);
			this.hd.TabIndex = 11;
			this.hd.Text = "0 MB";
			// 
			// PerfMonG
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(349, 96);
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
			this.Controls.Add(this.CPUTotal);
			this.Controls.Add(this.CPUTotalText);
			this.Name = "PerfMonG";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

	}
}
