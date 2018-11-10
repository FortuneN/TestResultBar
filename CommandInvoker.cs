using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TestWindow.Extensibility;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TestResultBar
{
    [Export(typeof(CommandInvoker))]
    public class CommandInvoker
    {
        private DTE _dte;

        [ImportingConstructor]
        public CommandInvoker(
            [Import(typeof(SVsServiceProvider))]SVsServiceProvider serviceProvider
        )
        {
            _dte = (DTE)serviceProvider.GetService(typeof(DTE));
        }

        public void RunAllTests()
        {
            // Throwing error on second run, why?
            _dte.ExecuteCommand("TestExplorer.RunAllTests");
        }
    }
}