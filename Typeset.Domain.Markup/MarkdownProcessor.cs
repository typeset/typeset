using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarkdownSharp;

namespace Typeset.Domain.Markup
{
    public class MarkdownProcessor : IMarkupProcessor
    {
        public string Process(string input)
        {
            var options = new MarkdownOptions();
            var markdown = new Markdown();
            var output = markdown.Transform(input);
            return output;
        }
    }
}
