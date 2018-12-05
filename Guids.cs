// Guids.cs
// MUST match guids.h
using System;
// ReSharper disable InconsistentNaming

namespace TestResultBar
{
	static class GuidList
	{
		public const string guidTestResultBarPkgString = "c48edd48-35b4-4f4c-8a73-c445898f41a9";
		public const string guidTestResultBarCmdSetString = "fc15eb63-6a2c-48ce-b952-85daabeb316c";

		public static readonly Guid guidTestResultBarCmdSet = new Guid(guidTestResultBarCmdSetString);

	};
}