using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.About
{
    public interface IAboutRepository
    {
        IAbout Read(string path);
    }
}
