﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.ViewModels.Accounting
{
    public class ReportViewModel
    {
        public int Receive { get; set; }
        public int Pay { get; set; }
        public int Balance { get; set; }
    }
}
