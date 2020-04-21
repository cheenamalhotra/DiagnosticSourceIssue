using System;
using System.Management.Automation;

namespace myModule
{
    [Cmdlet(VerbsDiagnostic.Test, "SampleCmdlet")]
    [OutputType(typeof(MessageWriter))]
    public class TestSampleCmdletCommand : PSCmdlet
    {
        // This method gets called once for each cmdlet in the pipeline when the pipeline starts executing
        protected override void BeginProcessing()
        {
            WriteVerbose("Begin!");
        }

        // This method will be called for each input received from the pipeline to this cmdlet; if no input is received, this method is not called
        protected override void ProcessRecord()
        {
            System.Diagnostics.DiagnosticSource _diagnostics = new MyDiag();

            if (_diagnostics.IsEnabled("DiagnosticListenerExample.Starting"))
            {
                // Testing only
            }

            WriteObject(new MessageWriter
            {
                Message = "Writing this message after loading DiagnosticSource"
            });
        }

        class MyDiag : System.Diagnostics.DiagnosticSource
        {
            public override bool IsEnabled(string name)
            {
                return false;
            }

            public override void Write(string name, object value)
            {
                throw new NotImplementedException();
            }
        }
        
        // This method will be called once at the end of pipeline execution; if no input is received, this method is not called
        protected override void EndProcessing()
        {
            WriteVerbose("End!");
        }
    }

    public class MessageWriter
    {
        public string Message { get; set; }
    }
}
