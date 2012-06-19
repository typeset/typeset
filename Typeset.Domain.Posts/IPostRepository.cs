using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Typeset.Domain.Common;

namespace Typeset.Domain.Post
{
    public interface IPostRepository
    {
        PageOf<IPost, PostSearchCriteria> Get(PostSearchCriteria searchCriteria);
    }
}
