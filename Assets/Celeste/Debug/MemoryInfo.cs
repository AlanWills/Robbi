﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;

namespace Celeste.Debugging
{
    [AddComponentMenu("Celeste/Debugging/Memory Info")]
    public class MemoryInfo : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField]
        private TextMeshProUGUI totalReservedSizeText;

        [SerializeField]
        private TextMeshProUGUI heapSizeText;

        #endregion

        #region Unity Methods

        private void Update()
        {
            totalReservedSizeText.text = string.Format("Reserved: {0}MB", Profiler.GetTotalReservedMemoryLong() / (1024 * 1024));
            heapSizeText.text = string.Format("Heap: {0}MB", Profiler.GetMonoHeapSizeLong() / (1024 * 1024));
        }

        #endregion
    }
}