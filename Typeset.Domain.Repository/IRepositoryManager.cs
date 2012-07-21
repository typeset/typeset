using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.Repository
{
    public interface IRepositoryManager
    {
        void CheckoutOrUpdate(string repositoryUri, string path);
    }
}
