using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Typeset.Domain.Common;

namespace Typeset.Domain.Markup
{
    public interface IMarkupProcessorFactory
    {
        IMarkupProcessor CreateInstance(ContentType processorType);
    }
}
