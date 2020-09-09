﻿using Robbi.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Parameters
{
    public interface IParameterContainer
    {
        T CreateParameter<T>(string name) where T : ScriptableObject;

        T CreateParameter<T>(T original) where T : ScriptableObject, ICopyable<T>;

        void RemoveParameter(ScriptableObject parameter);
    }
}
