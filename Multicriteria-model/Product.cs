﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multicriteria_model
{
    abstract class Product
    {
        public abstract string Name { get; }
        public abstract uint Price { get; }
    }
}