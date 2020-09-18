﻿using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.PropertyDrawers.Parameters
{
    [CustomPropertyDrawer(typeof(Vector3IntReference))]
    public class Vector3IntReferencePropertyDrawer : ParameterReferencePropertyDrawer
    {
    }
}