# Reproduce issues with Diagnostic Source v4.7.0

1. Run below commands from VS Command Prompt
    ```cmd
    cd app
    dotnet build
    dotnet publish -f netstandard2.0 -r win-x64 --no-self-contained --output bin\Debug\ps6
    cd ..

    "..<Path>..\PowerShell-6.2.4-112\pwsh.exe" 
    Import-Module <Path>\app\bin\Debug\ps6\myModule.dll
    try { Test-SampleCmdlet } catch [Exception] { $PSItem.Exception.ToString() }
    exit
    ```

2. You'll see an error message as under:
    ```log
    System.IO.FileLoadException: Could not load file or assembly 'System.Diagnostics.DiagnosticSource, Version=4.0.5.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'. Could not find or load a specific file. (Exception from HRESULT: 0x80131621)
    File name: 'System.Diagnostics.DiagnosticSource, Version=4.0.5.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51' ---> System.IO.FileLoadException: Could not load file or assembly 'System.Diagnostics.DiagnosticSource, Version=4.0.5.0, Culture=neutral, PublicKeyToken=cc7b13ffcd2ddd51'.
      at System.Runtime.Loader.AssemblyLoadContext.LoadFromPath(IntPtr ptrNativeAssemblyLoadContext, String ilPath, String niPath, ObjectHandleOnStack retAssembly)
      at System.Runtime.Loader.AssemblyLoadContext.LoadFromAssemblyPath(String assemblyPath)
      at System.Reflection.Assembly.LoadFrom(String assemblyFile)
      at System.Reflection.Assembly.LoadFromResolveHandler(Object sender, ResolveEventArgs args)
      at System.AppDomain.InvokeResolveEvent(ResolveEventHandler eventHandler, RuntimeAssembly assembly, String name)
      at myModule.TestSampleCmdletCommand.ProcessRecord()
      at System.Management.Automation.Cmdlet.DoProcessRecord()
      at System.Management.Automation.CommandProcessor.ProcessRecord()
    ```

3. Run the same steps in a different Powershell Session, with either of the below variations:
    - Use System.Diagnostics.DiagnosticSource v4.6.0
    - Run in PowerShell v7.0.0

4. You'll see an output like under:
    ```
    Message
    -------
    Writing this message after loading DiagnosticSource
    ```
