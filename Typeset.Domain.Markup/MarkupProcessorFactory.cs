using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Typeset.Domain.Common;

namespace Typeset.Domain.Markup
{
    public class MarkupProcessorFactory : IMarkupProcessorFactory
    {
        public IMarkupProcessor CreateInstance(ContentType processorType)
        {
            switch (processorType)
            {
                default:
                case ContentType.markdown:
                    return new MarkdownProcessor();
                case ContentType.textile:
                    return new TextileProcessor();
            }
        }
    }
}
