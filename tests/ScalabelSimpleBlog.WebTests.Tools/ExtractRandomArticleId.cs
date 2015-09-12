using Microsoft.VisualStudio.TestTools.WebTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScalabelSimpleBlog.WebTests.Tools
{
    public class ExtractRandomArticleId : ExtractionRule
    {

        public override string RuleName
        {
            get { return "Extract Random Article Id"; }
        }

        public override void Extract(object sender, ExtractionEventArgs e)
        {
            if (e.Response.HtmlDocument != null)
            {
                var document = e.Response.HtmlDocument;
               // var articles = document.GetFilteredHtmlTags("a").Where(x => x);

            }
            e.Success = false;
            e.Message = "Not Found";
        }
    }
}
