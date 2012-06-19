using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.About
{
    internal class About : IAbout
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string Email { get; set; }
        public IDictionary<string, string> Usernames { get; set; }
        public IDictionary<string, string> Links { get; set; }

        public About()
        {
            Usernames = new Dictionary<string, string>();
            Links = new Dictionary<string, string>();
        }
    }
}
