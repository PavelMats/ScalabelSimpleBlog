using Microsoft.VisualStudio.TestTools.WebTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalabelSimpleBlog.WebTests.Tools
{
    public class RandomIntByMask : WebTestPlugin
    {
        [System.ComponentModel.DisplayName("Target Context Parameter Name")]
        [System.ComponentModel.Description("Name of the context parameter that will receive the generated value.")]
        public string ContextParamTarget { get; set; }

        [System.ComponentModel.DisplayName("Input Format")]
        [System.ComponentModel.Description("Input format")]
        public string InputFormat { get; set; }

        [System.ComponentModel.DisplayName("Min Value")]
        public int  MinValue { get; set; }

        [System.ComponentModel.DisplayName("Max Value")]
        public int MaxValue { get; set; }

        public override void PreWebTest(object sender, PreWebTestEventArgs e)
        {
            var random = new Random();

            var randomValue = random.Next(this.MinValue, this.MaxValue);
            var outputString = string.Format(this.InputFormat, randomValue);

            // Set the context paramaeter with generated guid
            e.WebTest.Context[ContextParamTarget] = outputString;

            base.PreWebTest(sender, e);
        }
    }
}
