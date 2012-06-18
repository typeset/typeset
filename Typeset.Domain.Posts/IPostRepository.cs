using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.Post
{
    public interface IPostRepository
    {
        IEnumerable<IPost> Get(SearchCriteria searchCriteria);
    }
}
