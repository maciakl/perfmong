// based on Anton Zamov's example code

using System;
using System.Diagnostics;

	public class PerformanceMonitor
	{
		protected PerformanceCounter cpuCounter;
		protected PerformanceCounter ramCounter;
		protected PerformanceCounter uptime;
		protected PerformanceCounter pagefile;
		protected PerformanceCounter processes;
		protected PerformanceCounter disk;
		
		public PerformanceMonitor()
		{
			cpuCounter = new PerformanceCounter();

			cpuCounter.CategoryName = "Processor";
			cpuCounter.CounterName = "% Processor Time";
			cpuCounter.InstanceName = "_Total";

			ramCounter = new PerformanceCounter("Memory", "Available MBytes");

			uptime = new PerformanceCounter("System", "System Up Time");

			pagefile = new PerformanceCounter();

			pagefile.CategoryName = "Paging File";
			pagefile.CounterName = "% Usage";
			pagefile.InstanceName = "_Total";

			processes = new PerformanceCounter("System", "Processes");
			
			disk = new PerformanceCounter();

			disk.CategoryName = "LogicalDisk";
			disk.CounterName = "Free Megabytes";
			disk.InstanceName = "_Total";
			
		}

		
		public float getCurrentCpuUsage()
		{
			return cpuCounter.NextValue();
		}

		public string getAvailableRAM()
		{
			return ramCounter.NextValue()+"MB";
		}

		public string getUptime()
		{
			double up = uptime.NextValue();

			int hours = (int)(up / 3600);
			int minutes = (int)((up%3600)/60);
			int seconds = (int)((up%3600)%60);

			string uptime_formated = (hours + " h " + minutes + " m " + seconds + " s");	

			return uptime_formated;
		}

		public float getPagefile()
		{
			return pagefile.NextValue();
		}

		public string getProcesses()
		{
			return processes.NextValue().ToString();
		}

		public string getDisk()
		{
			try
			{
				return (disk.NextValue() / 1024).ToString("F2") + " GB";
			}
			catch (InvalidOperationException)
			{
				return "N/A";
			}
		}
	}