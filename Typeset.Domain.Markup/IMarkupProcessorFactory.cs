using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.Markup
{
    public enum ProcessorType
    {
        markdown,
        textile
    }

    public interface IMarkupProcessorFactory
    {
        IMarkupProcessor CreateInstance(ProcessorType processorType);
    }
}
