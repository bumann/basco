using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Basco")]
[assembly: AssemblyDescription("Simple and easy testable (TDD) state machine.\r\nOnly 3 simple steps needed.\r\n1) Define triggers (enum)\r\n2) Create states (IState)\r\n3) Implement configurator (IBascoConfigurator)\r\n")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("bumann")]
[assembly: AssemblyProduct("Basco")]
[assembly: AssemblyCopyright("B. Bumann")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("4e49669d-85be-4d44-a681-62d6da3860d7")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.1.0")]
[assembly: AssemblyFileVersion("1.1.0")]

[assembly: InternalsVisibleTo("Basco.Test")]
