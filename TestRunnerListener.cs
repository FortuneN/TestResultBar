using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TestWindow.Extensibility;
using TestResultBar;

[Export(typeof(ITestContainerDiscoverer))]
[Export(typeof(TestRunnerListener))]
internal class TestRunnerListener : ITestContainerDiscoverer
{
    [Import]
    private ITestsService TestsService;

    [Import(typeof(InfoControl))]
    public InfoControl InfoControl { get; set; }

    [ImportingConstructor]
    internal TestRunnerListener(
        [Import(typeof(IOperationState))]IOperationState operationState
    )
    {
        operationState.StateChanged += OperationState_StateChanged;
    }

    public Uri ExecutorUri => new Uri("executor://TestResultBar/v1");


    public IEnumerable<ITestContainer> TestContainers
    {
        get
        {
            return new ITestContainer[0].AsEnumerable();
        }
    }


    public event EventHandler TestContainersUpdated;

    private async void OperationState_StateChanged(object sender, OperationStateChangedEventArgs e)
    {
        if (e.State == TestOperationStates.TestExecutionStarted)
        {
            InfoControl.ResetBackgroundColor();

        }
        else if (e.State == TestOperationStates.TestExecutionFinished)
        {
            IDisposableQuery<ITest> tests = await TestsService.GetTestsAsync();
            InfoControl.UpdateWithTestResult(tests);
        }

    }
}