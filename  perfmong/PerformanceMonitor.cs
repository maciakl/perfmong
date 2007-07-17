// based on Anton Zamov's example code

using System;
using System.Diagnostics;
using System.Management; 


	public class PerformanceMonitor
	{

		protected PerformanceCounter[] cpuCounters; 
		protected PerformanceCounter cpuTotalCounter; 
		protected PerformanceCounter ramCounter;
		protected PerformanceCounter uptime;
		protected PerformanceCounter pagefile;
		protected PerformanceCounter processes;
		protected PerformanceCounter disk;

		public PerformanceMonitor()
		{
			int processorCount = this.getCpuProcessors(); 

			if(processorCount > 1)
			{
				cpuCounters = new PerformanceCounter[processorCount];
				for(int i = 0; i<processorCount;++i)
				{
					PerformanceCounter p = new PerformanceCounter(); 
					p.CategoryName = "Processor";
					p.CounterName = "% Processor Time"; 
					p.InstanceName = i.ToString();
					cpuCounters[i] = p; 
				}
			}
			
			cpuTotalCounter = new PerformanceCounter();
			cpuTotalCounter.CategoryName = "Processor";
			cpuTotalCounter.CounterName = "% Processor Time"; 
			cpuTotalCounter.InstanceName = "_Total"; 

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

		public int getCpuProcessors()
		{
			return int.Parse(Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS").ToString()); 
		}
		public float getCurrentCpuUsage(int index)
		{
			return cpuCounters[index].NextValue();	 
		}

		public float getTotalCpuUsage()
		{
			return cpuTotalCounter.NextValue();
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


