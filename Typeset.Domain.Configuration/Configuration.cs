using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.Configuration
{
    internal class Configuration : IConfiguration
    {
        public string DateFormat { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }

        public Configuration()
        {
            DateFormat = "dddd, MMMM d, yyyy";
        }
    }
}
