using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows;
using EnvDTE;
using Microsoft.VisualBasic.Devices;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Process = System.Diagnostics.Process;
using Microsoft.VisualStudio.ComponentModelHost;
using System.ComponentModel.Composition;

namespace TestResultBar
{
	/// <summary>
	/// This is the class that implements the package exposed by this assembly.
	///
	/// The minimum requirement for a class to be considered a valid package for Visual Studio
	/// is to implement the IVsPackage interface and register itself with the shell.
	/// This package uses the helper classes defined inside the Managed Package Framework (MPF)
	/// to do it: it derives from the Package class that provides the implementation of the
	/// IVsPackage interface and uses the registration attributes defined in the framework to
	/// register itself and its components with the shell.
	/// </summary>
	// This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
	// a package.
	[PackageRegistration(UseManagedResourcesOnly = true)]
	// This attribute is used to register the information needed to show this package
	// in the Help/About dialog of Visual Studio.
	[InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
	[Guid(GuidList.guidTestResultBarPkgString)]

	[ProvideAutoLoad(UIContextGuids80.NoSolution)]
	[ProvideAutoLoad(UIContextGuids80.SolutionExists)]
	[ProvideAutoLoad(UIContextGuids80.EmptySolution)]
	[ProvideOptionPage(typeof(OptionsPage), "TestResultBar Info", "General", 0, 0, true)]

    [Export(typeof(TestResultBarPackage))]
	public sealed class TestResultBarPackage : Package
	{
		public InfoControl InfoControl;
		private StatusBarInjector injector;
        private DTE _dte;
        private BuildEvents _buildEvents;

        private OptionsPage optionsPage;

		/// <summary>
		/// Initialization of the package; this method is called right after the package is sited, so this is the place
		/// where you can put all the initialization code that rely on services provided by VisualStudio.
		/// </summary>
		protected override void Initialize()
		{
			Debug.WriteLine($"Entering Initialize() of: {this}");

			base.Initialize();

			_dte = (DTE)GetService(typeof(DTE));
			DTEEvents eventsObj = _dte.Events.DTEEvents;
			eventsObj.OnStartupComplete += InitExt;
			eventsObj.OnBeginShutdown += ShutDown;

            _buildEvents = _dte.Events.BuildEvents;
            _buildEvents.OnBuildDone += RunAllTests;
        }

		private void InitExt()
		{
			Debug.WriteLine("Init function loaded");

            var componentModel = (IComponentModel)GetService(typeof(SComponentModel));
            InfoControl = (InfoControl)componentModel.GetService<InfoControl>();
            injector = new StatusBarInjector(Application.Current.MainWindow);
            injector.InjectControl(InfoControl);

            //optionsPage = GetDialogPage(typeof(OptionsPage)) as OptionsPage;
            //if (optionsPage != null) infoControl.Format = optionsPage.Format;
        }

		private void ShutDown()
		{
		}

        private void RunAllTests(vsBuildScope scope, vsBuildAction action)
        {
            if (action == vsBuildAction.vsBuildActionBuild)
            {
                InfoControl.RunAllTests();
            }
        }
	}
}
