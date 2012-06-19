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
        IDictionary<string, string> Usernames { get; }
        IDictionary<string, string> Links { get; }
    }
}
