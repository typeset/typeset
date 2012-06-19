using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.Markup
{
    public class ProcessorFactory : IMarkupProcessorFactory
    {
        public IMarkupProcessor CreateInstance(ProcessorType processorType)
        {
            switch (processorType)
            {
                default:
                case ProcessorType.markdown:
                    return new MarkdownProcessor();
                case ProcessorType.textile:
                    return new MarkdownProcessor();
            }
        }
    }
}
