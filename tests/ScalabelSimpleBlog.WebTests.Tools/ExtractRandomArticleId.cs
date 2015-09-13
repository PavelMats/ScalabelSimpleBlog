﻿using Microsoft.VisualStudio.TestTools.WebTesting;
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

        public override string RuleDescription
        {
            get { return "Extracts Random Article Id from blog page"; }
        }

        public override void Extract(object sender, ExtractionEventArgs e)
        {
            if (e.Response.HtmlDocument != null)
            {
                var document = e.Response.HtmlDocument;
                // https://msdn.microsoft.com/en-us/library/ms243179.aspx  "show-article-link"
                var articles = document.GetFilteredHtmlTags("a").Where(x => x.GetAttributeValueAsString("class").Contains("show-article-link")).ToList();

                if(articles.Count > 0)
                {
                    var randomArticle = articles.Random();
                    // data-article-id
                    var articleId = randomArticle.GetAttributeValueAsString("data-article-id");
                    e.WebTest.Context.Add(this.ContextParameterName, articleId);
                    e.Success = true;
                    return;
                }
            }
            e.Success = false;
            e.Message = "Not Found";
        }
    }

    public static class EnumerableHelper<E>
    {
        private static Random r;

        static EnumerableHelper()
        {
            r = new Random();
        }

        public static T Random<T>(IEnumerable<T> input)
        {
            return input.ElementAt(r.Next(input.Count()));
        }
    }

    public static class EnumerableExtensions
    {
        public static T Random<T>(this IEnumerable<T> input)
        {
            return EnumerableHelper<T>.Random(input);
        }
    }
}
