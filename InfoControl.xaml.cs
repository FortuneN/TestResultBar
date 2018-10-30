using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace TestResultBar
{
	public partial class InfoControl : UserControl
	{
		public InfoControl()
		{
            InitializeComponent();
            PassedTestsCount.Text = "TestResultBar";
            FailedTestsCount.Text = "9";
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

	}
}