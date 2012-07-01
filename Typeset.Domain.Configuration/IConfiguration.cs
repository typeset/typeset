﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Typeset.Domain.Configuration
{
    public interface IConfiguration
    {
        string DateFormat { get; }
        string SyndicationAuthor { get; }
        string SyndicationTitle { get; }
    }
}
