﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualKeyboard.Services
{
    public interface ILayoutService
    {
        public Dictionary<Layouts, (int x, int y, int width, int height)> Dictionary { get; }
    }
}
