﻿using Robbi.FSM;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RobbiEditor.Validation
{
    public interface IFixableCondition<T> where T : Object
    {
        bool CanFix(T asset);
        void Fix(T asset, StringBuilder output);
    }
}
