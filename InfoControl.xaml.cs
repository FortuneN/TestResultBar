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
                UpdatePopup(failedTests, true);
            });

        }

        private void SetTestCounts(int passedCount, int failedCount)
        {
            PassedTestsCount.Text = passedCount.ToString();
            FailedTestsCount.Text = failedCount.ToString();
        }

        private void SetBackgroundColor(int failedCount)
        {
            Color bgColor = failedCount == 0 ? Colors.Green : Colors.Red;
            Background = new SolidColorBrush(bgColor);
        }

        public void UpdatePopup(ITest[] tests, bool showPopup)
        {
            FailedTestsPopupContent.Children.Clear();
            foreach (ITest test in tests)
            {
                TextBlock textblock = new TextBlock();
                textblock.Text = test.DisplayName;
                FailedTestsPopupContent.Children.Add(textblock);
            }
            if (showPopup && tests.Count() > 0)
            {
                FailedTestsPopup.IsOpen = true;
                FailedTestsPopup.StaysOpen = false;
            }
        }

	}
}