﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.Configuration
{
    internal class Configuration : IConfiguration
    {
        public bool DisqusDeveloperMode { get; set; }
        public string DisqusShortname { get; set; }
    }
}
