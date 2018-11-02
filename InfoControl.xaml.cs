using Microsoft.VisualStudio.TestWindow.Extensibility;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace TestResultBar
{
    [Export(typeof(InfoControl))]
	public partial class InfoControl : UserControl
	{
		public InfoControl()
		{
            InitializeComponent();
            PassedTestsCount.Text = "TestResultBar";

		}

        public void SetPassedTests(string text)
        {
            this.Dispatcher.BeginInvoke((Action)delegate {
                PassedTestsCount.Text = text;
            });
        }
        public void UpdateWithTestResult(IDisposableQuery<ITest> tests)
        {
            this.Dispatcher.BeginInvoke((Action)delegate {
                ITest[] passedTests = tests.Where(t => t.State == TestState.Passed).ToArray();
                ITest[] failedTests = tests.Where(t => t.State == TestState.Failed).ToArray();
                SetTestCounts(passedTests.Count(), failedTests.Count());
                SetBackgroundColor(failedTests.Count());
            });

        }

        private void SetTestCounts(int passedCount, int failedCount)
        {
            PassedTestsCount.Text = passedCount.ToString();
            FailedTestsCount.Text = failedCount.ToString();
        }

        private void SetBackgroundColor(int failedCount)
        {
            if(failedCount == 0)
            {
                Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                Background = new SolidColorBrush(Colors.Red);
            }

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