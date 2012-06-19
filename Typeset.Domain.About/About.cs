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
        public string TwitterUsername { get; set; }
        public string GithubUsername { get; set; }
        public IDictionary<string, string> Links { get; set; }

        public About()
        {
            Links = new Dictionary<string, string>();
        }
    }
}
