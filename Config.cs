using System;
using System.IO;
using System.Drawing;

namespace PerfMonG
{
		/*
		 * This class is going to look for a config file in the
		 *  current folder... If it doesn't find it, it will look
		 *  for it in the root directory (C: drive). If it is still
		 *  not there, it will just give up and use defaults...
		 * 
		 *  The config file should be constructed as follows:
		 * 
		 *  # is the comment sign - everything after # is ignored
		 * 
		 *  use only one option per line in the format atribute=value
		 *  
		 *	avaliable atributes:
		 * 
		 *	x		- the x coordinate of the window
		 *	y		- the y coordinate of the window
		 *	bg		- background color
		 *	txt		- text color
		 * 
		 *  **** => want to add: maxx, maxy, transparency
		 * 
		 *	color names must exist in the KnownColor enumeration
		 * 
		 * the parsing algorithm is actually very linient and will
		 * ignore alot of garbage in the config file 
		 * 
		 */
	
	public class Config
	{
		private readonly int DEFAULT_X = 330;
		private readonly int DEFAULT_Y = 680;
		private readonly double DEFAULT_OPACITY = .8;
		private readonly Color DEFAULT_BG = Color.White;
		private readonly Color DEFAULT_TXT = SystemColors.ControlText;
		
		private readonly string FILE = "\\PerfMonG\\pm.cfg";
		private string path;
		
		private int x;
		private int y;
		private double opc;
		private Color bg;
		private Color txt;

		public Config()
		{
			// get the appdata directory
			string appdata = Environment.GetEnvironmentVariable("Appdata");
			path = "";

			for(int i=0; i<= appdata.Length-1; i++)
			{
				path+=appdata.Substring(i,1);

				if(appdata.Substring(i,1) == "\\")
					path += "\\";
			}

			
			StreamReader sr = openConfig();

			if(sr == null)
			{
				setDefaults();
				Directory.CreateDirectory(path + "\\PerfMonG");
				writeConfig(X, Y, OPC, BG, TXT);
				
			}
			else
				try
				{
					readConfig(sr);
				}
				catch(Exception e)
				{
					System.Windows.Forms.MessageBox.Show(null, e.Message, "PerfMon Configuration Error", System.Windows.Forms.MessageBoxButtons.OK);
					setDefaults();
				}
			
		}

		public StreamReader openConfig()
		{
			StreamReader sr = null;
		
			// first try the current dir
			try
			{
				sr = new StreamReader(path + FILE);
			}
			catch(FileNotFoundException)
			{
				sr = null;
			}
			catch(DirectoryNotFoundException)
			{
				sr = null;
			}
			
			return sr;
		}
				
		public void readConfig(StreamReader sr)// throws FileNotFoundException
		{
			string line;
			int count = 0;
			char[] delimiters = ("= \t\n").ToCharArray();

			int ln = 0; // line counter

			while((line = sr.ReadLine()) != null)
			{
				string[] arguments = line.Split(delimiters);

				if(!arguments[0].StartsWith("#"))
				{					
					switch(arguments[0])
					{
						case "x":
							X = Int32.Parse(arguments[1]);
							count++;
							break;
						case "y":
							Y = Int32.Parse(arguments[1]);
							count++;
							break;
						case "bg":
							BG = Color.FromName(arguments[1]);
							count++;
							break;
						case "txt":				
							TXT = Color.FromName(arguments[1]);
							count++;
							break;
						case "opc":
							OPC = Double.Parse(arguments[1]);
							count++;
							break;
						default:
							throw new Exception("Config file misconfigured at line " + (ln+1));
					}
				}

				ln++;
			}

			if(count == 0)
				setDefaults();
			else 
				if(count<5)
					throw new Exception("Config file is misconfigured! Some values are missing.");

			sr.Close();
		}

		public void setDefaults()
		{
			X = DEFAULT_X;
			Y = DEFAULT_Y;
			OPC = DEFAULT_OPACITY;
			BG = DEFAULT_BG;
			TXT = DEFAULT_TXT;
		}

		public void writeConfig(int x, int y, double op, Color bg, Color txt)
		{
            StreamWriter wr;

			wr = new StreamWriter(path + FILE);

            wr.WriteLine("# Auto Generated PerfMon Config File");
			wr.WriteLine("# Last updated on: " + DateTime.Now);
			wr.WriteLine("x=" + x);
			wr.WriteLine("y=" + y);
			wr.WriteLine("opc=" + op);
			wr.WriteLine("bg=" + bg.Name);
			wr.WriteLine("txt=" + txt.Name);

			wr.Close();

		}

		public void writeConfig()
		{
			writeConfig(x, y, opc, bg, txt); 
		}

		// properties
		public int X { get	{ return x;	} set { x = value; } }
		public int Y { get { return y; } set { y = value; } }
		public double OPC { get { return opc; } set { opc = value;} }
		public Color BG { get { return bg; } set { bg = value; } }
		public Color TXT { get { return txt; } set {txt = value; } }
	}
}
