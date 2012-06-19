using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.Markup
{
    public interface IMarkupProcessor
    {
        string Process(string input);
    }
}
