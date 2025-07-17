﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.Commands
{
    public class TKSetShow
    {
        public TKSetShow(int layout)
        {
            Layout = Enum.IsDefined(typeof(Layouts), layout)
            ? (Layouts)layout
            : Layouts.NotUsed;
        }
        public Layouts Layout { get; }
    }
}
