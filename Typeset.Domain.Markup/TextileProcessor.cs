using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Textile;

namespace Typeset.Domain.Markup
{
    public class TextileProcessor : IMarkupProcessor
    {
        public string Process(string input)
        {
            var output = TextileFormatter.FormatString(input);
            return output;
        }
    }
}
