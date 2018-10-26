using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace TestResultBar
{
	public partial class InfoControl : UserControl
	{
		public readonly string[] Formats;

		public readonly string[] FormatDescriptions;

		private readonly Dictionary<string, TextBlockList> textBlockLists;

		private readonly long totalRam;

		private int fixedWidth = 150;

		private bool useFixedWidth;

		public int CpuUsage
		{
			set
			{
				string CpuValue = $"{value,2}%";
				textBlockLists["<CPU>"].Text = CpuValue;
				textBlockLists["<#CPU>"].Text = CpuValue;
				textBlockLists["<#CPU>"].Foreground = GetCpuColor(value);
			}
		}

		public int FixedWidth
		{
			get => fixedWidth;
			set
			{
				fixedWidth = value;
				UseFixedWidth = UseFixedWidth;
			}
		}

		public string Format
		{
			set
			{
				stackPanel.Children.Clear();
				textBlockLists.Clear();
				InitTextBlockLists();
				string str = value;
				while (str != "")
				{
					TextBlockList textBlockList;
					TextBlock nextTextBlock = GetNextTextBlock(ref str, out textBlockList);
					textBlockList?.Add(nextTextBlock);
					stackPanel.Children.Add(nextTextBlock);
				}
				foreach (TextBlockList textBlockList1 in textBlockLists.Values)
				{
					textBlockList1.Text = "N/A";
				}
			}
		}

		public long FreeRam
		{
			set
			{
				long num = totalRam - value;
				string readableByteSize = num.ToReadableByteSize("####.00");

				textBlockLists["<TOTAL_USE_RAM>"].Text = readableByteSize;
				textBlockLists["<#TOTAL_USE_RAM>"].Text = readableByteSize;
				textBlockLists["<#TOTAL_USE_RAM>"].Foreground = GetRamColor(num);

				int num1 = (int) (num * 100 / totalRam);
				readableByteSize = $"{num1:####.00}%";
				textBlockLists["<TOTAL_USE_RAM%>"].Text = readableByteSize;
				textBlockLists["<#TOTAL_USE_RAM%>"].Text = readableByteSize;
				textBlockLists["<#TOTAL_USE_RAM%>"].Foreground = GetRamColor(num);

				readableByteSize = value.ToReadableByteSize("####.00");
				textBlockLists["<FREE_RAM>"].Text = readableByteSize;
				textBlockLists["<#FREE_RAM>"].Text = readableByteSize;
				textBlockLists["<#FREE_RAM>"].Foreground = GetRamColor(num);

				num1 = (int) (value * 100 / totalRam);
				readableByteSize = $"{num1:####.00}%";
				textBlockLists["<FREE_RAM%>"].Text = readableByteSize;
				textBlockLists["<#FREE_RAM%>"].Text = readableByteSize;
				textBlockLists["<#FREE_RAM%>"].Foreground = GetRamColor(num);
			}
		}

		public long RamUsage
		{
			set
			{
				string readableByteSize = value.ToReadableByteSize("####.00");
				textBlockLists["<RAM>"].Text = readableByteSize;
				textBlockLists["<#RAM>"].Text = readableByteSize;
				textBlockLists["<#RAM>"].Foreground = GetRamColor(value);

				int num = (int) (value * 100 / totalRam);
				readableByteSize = $"{num:####.00}%";
				textBlockLists["<RAM%>"].Text = readableByteSize;
				textBlockLists["<#RAM%>"].Text = readableByteSize;
				textBlockLists["<#RAM%>"].Foreground = GetCpuColor(num);
			}
		}

		public int TotalCpuUsage
		{
			set
			{
				string TotalCpuValue = $"{value,2}%";
				textBlockLists["<TOTAL_CPU>"].Text = TotalCpuValue;
				textBlockLists["<#TOTAL_CPU>"].Text = TotalCpuValue;
				textBlockLists["<#TOTAL_CPU>"].Foreground = GetCpuColor(value);
			}
		}

		public bool UseFixedWidth
		{
			get => useFixedWidth;
			set
			{
				useFixedWidth = value;
				if (!useFixedWidth)
				{
					Width = double.NaN;
					return;
				}
				Width = FixedWidth;
			}
		}

		public InfoControl(long pTotalRam)
		{
			Formats = new []{ "CPU", "TOTAL_CPU", "RAM", "FREE_RAM", "TOTAL_USE_RAM", "RAM%", "FREE_RAM%", "TOTAL_USE_RAM%" };
			FormatDescriptions = new [] { "Cpu usage of Visual Studio", "Cpu usage of computer", "Ram usage of Visual Studio", "Free ram of computer", "Ram usage of computer", "Ram usage of Visual Studio in percent", "Free ram of computer in percent", "Ram usage of computer in percent" };

			totalRam = pTotalRam;
			InitializeComponent();
			textBlockLists = new Dictionary<string, TextBlockList>();
			Format = "CPU: <#CPU>   RAM: <#RAM>";
		}

		private Brush GetCpuColor(int cpu)
		{
			Color color;
			if (cpu > 50)
			{
				Color yellow = Colors.Yellow;
				color = yellow.FadeTo(Colors.Red, (cpu - 50) / 50f);
			}
			else
			{
				Color white = Colors.White;
				color = white.FadeTo(Colors.Yellow, cpu / 50f);
			}
			return new SolidColorBrush(color);
		}

		private TextBlock GetNextTextBlock(ref string format, out TextBlockList textBlockList)
		{
			TextBlock textBlock = new TextBlock
			{
				Foreground = new SolidColorBrush(Colors.White)
			};

			string str;
			int num = format.IndexOfAny(textBlockLists.Keys.ToArray(), out str);

			if (num == -1)
			{
				textBlock.Text = format;
				format = "";
				textBlockList = null;
			}
			else if (num != 0)
			{
				textBlock.Text = format.Substring(0, num);
				format = format.Substring(num);
				textBlockList = null;
			}
			else
			{
				textBlock.Text = "";
				format = format.Substring(str.Length);
				textBlockList = textBlockLists[str];
			}

			return textBlock;
		}

		private Brush GetRamColor(long ram)
		{
			int num = (int) (ram * 100 / totalRam);
			return GetCpuColor(num);
		}

		private void InitTextBlockLists()
		{
			foreach (string t in Formats)
			{
				textBlockLists[$"<{t}>"] = new TextBlockList();
				textBlockLists[$"<#{t}>"] = new TextBlockList();
			}
		}
	}
}