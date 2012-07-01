using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.Configuration
{
    internal class Configuration : IConfiguration
    {
        public string DateFormat { get; set; }
        public string SyndicationAuthor { get; set; }
        public string SyndicationTitle { get; set; }

        public Configuration()
        {
            DateFormat = "MM-dd-yyyy";
        }
    }
}
