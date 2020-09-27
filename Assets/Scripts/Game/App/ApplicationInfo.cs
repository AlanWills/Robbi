using Robbi.Objects;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.App
{
    [AddComponentMenu("Robbi/App/Application Info")]
    public class ApplicationInfo : Singleton<ApplicationInfo>
    {
        #region Properties and Fields

        [SerializeField]
        private BoolValue isEditor;
        public static bool IsEditor
        {
            get { return Instance.isEditor.value; }
        }

        [SerializeField]
        private BoolValue isMobile;
        public static bool IsMobile
        {
            get { return Instance.isMobile.value; }
        }

        [SerializeField]
        private BoolValue isDebugBuild;
        public static bool IsDebugBuild
        {
            get { return Instance.isDebugBuild.value; }
        }

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(gameObject);

            if (isEditor != null)
            {
                isEditor.value = Application.isEditor;
                Debug.LogFormat("isEditor set to {0}", isEditor.value);
            }

            if (isMobile != null)
            {
                isMobile.value = Application.isMobilePlatform;
                Debug.LogFormat("isMobile set to {0}", isMobile.value);
            }

            if (isDebugBuild != null)
            {
                isDebugBuild.value = Debug.isDebugBuild;
                Debug.LogFormat("isDebugBuild set to {0}", isDebugBuild.value);
            }
        }

        #endregion
    }
}
