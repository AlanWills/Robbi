﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Robbi.Debugging.Info
{
    [AddComponentMenu("Robbi/Debugging/Version")]
    public class Version : MonoBehaviour
    {
        public TextMeshProUGUI text;

        private void Start()
        {
            text.text = string.Format("Build: {0}", Application.version);
        }

        private void OnValidate()
        {
            if (text == null)
            {
                text = GetComponent<TextMeshProUGUI>();
            }
        }
    }
}