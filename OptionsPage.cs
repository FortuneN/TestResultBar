using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace TestResultBar
{
	[ClassInterface(ClassInterfaceType.AutoDual)]
	public class OptionsPage : DialogPage
	{
		private string format = "CPU: <#CPU>   RAM: <#RAM>";

		private int interval = 1000;

		private bool useFixedWidth;

		private int fixedWidth = 150;

		[Category("Design")]
		[Description("Sets the fixed width.")]
		[DisplayName("Fixed width")]
		public int FixedWith
		{
			get => this.fixedWidth;
			set
			{
				this.fixedWidth = value;
				this.OptionUpdated("FixedWidth", value);
			}
		}

		[Category("General")]
		[Description("The format of the information.")]
		[DisplayName("Format")]
		public string Format
		{
			get => this.format;
			set
			{
				this.format = value;
				this.OptionUpdated("Format", value);
			}
		}

		[Category("General")]
		[Description("The refresh interval (in ms).")]
		[DisplayName("Interval")]
		public int Interval
		{
			get => this.interval;
			set
			{
				this.interval = value;
				this.OptionUpdated("Interval", value);
			}
		}

		[Category("Design")]
		[Description("Determines whether fixed width should be used.")]
		[DisplayName("Use fixed width")]
		public bool UseFixedWidth
		{
			get => this.useFixedWidth;
			set
			{
				this.useFixedWidth = value;
				this.OptionUpdated("UseFixedWidth", value);
			}
		}

		private void OptionUpdated(string pName, object pValue)
		{
			var testResultBarPackage = (TestResultBarPackage)this.GetService(typeof(TestResultBarPackage));
			//testResultBarPackage?.OptionUpdated(pName, pValue);
		}
	}
}
