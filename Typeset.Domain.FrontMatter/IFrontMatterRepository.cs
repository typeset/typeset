using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Typeset.Domain.Common;

namespace Typeset.Domain.FrontMatter
{
    public interface IFrontMatterRepository
    {
        PageOf<IFrontMatter, FrontMatterSearchCriteria> Get(FrontMatterSearchCriteria searchCriteria);
    }
}
