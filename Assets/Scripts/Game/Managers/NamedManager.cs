using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Managers
{
    public class NamedManager : MonoBehaviour
    {
        #region Unity Methods

        protected virtual void Awake()
        {
            name = GetType().Name;
        }

        #endregion
    }
}
