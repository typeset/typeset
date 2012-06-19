using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.About
{
    public interface IAbout
    {
        string FirstName { get; }
        string LastName { get; }
        string Bio { get; }
        string Email { get; }
        string TwitterUsername { get; }
        string GithubUsername { get; }
        IDictionary<string, string> Links { get; }
    }
}
