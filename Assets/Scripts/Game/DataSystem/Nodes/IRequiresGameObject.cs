﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.DataSystem.Nodes
{
    public interface IRequiresGameObject
    {
        GameObject GameObject { get;set; }
    }
}