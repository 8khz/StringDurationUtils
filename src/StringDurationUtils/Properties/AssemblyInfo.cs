using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("StringDuration Utils")]
[assembly: AssemblyProduct("StringDuration Utils")]

[assembly: ComVisible(false)]

[assembly: AssemblyVersion(
	ThisAssembly.Git.SemVer.Major 
	+ "." 
	+ ThisAssembly.Git.SemVer.Minor 
	+ "." 
	+ ThisAssembly.Git.SemVer.Patch 
	+ "." 
	+ ThisAssembly.Git.Commits)]

[assembly: AssemblyFileVersion(
	ThisAssembly.Git.SemVer.Major 
	+ "." 
	+ ThisAssembly.Git.SemVer.Minor 
	+ "." 
	+ ThisAssembly.Git.SemVer.Patch
	+ "." 
	+ ThisAssembly.Git.Commits)]

[assembly: AssemblyInformationalVersion(
	ThisAssembly.Git.SemVer.Major
	+ "."
	+ ThisAssembly.Git.SemVer.Minor
	+ "."
	+ ThisAssembly.Git.SemVer.Patch
	+ "."
	+ ThisAssembly.Git.Commits
	+ "-" 
	+ ThisAssembly.Git.Branch 
	+ "+" 
	+ ThisAssembly.Git.Commit)]
