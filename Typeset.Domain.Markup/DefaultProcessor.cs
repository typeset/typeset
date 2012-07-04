using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.Markup
{
    public class DefaultProcessor : IMarkupProcessor
    {
        public string Process(string input)
        {
            return input;
        }
    }
}
