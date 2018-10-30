using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestWindow.Extensibility;

[Export(typeof(ITestContainerDiscoverer))]
[Export(typeof(TestRunnerListener))]
internal class TestRunnerListener : ITestContainerDiscoverer
{

    [ImportingConstructor]
    internal TestRunnerListener([Import(typeof(IOperationState))]IOperationState operationState)
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

    private void OperationState_StateChanged(object sender, OperationStateChangedEventArgs e)
    {
        //System.Windows.Forms.MessageBox.Show(e.State.ToString());
        if (e.State == TestOperationStates.TestExecutionFinished)
        {
            var s = e.Operation;
			Debug.WriteLine("Testerna är färdiga!");
            System.Windows.Forms.MessageBox.Show("results please!");
        }
    }
}