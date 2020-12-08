using Robbi.Objects;
using Robbi.Parameters;
using Robbi.Utils;
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
            get { return Instance.isEditor.Value; }
        }

        [SerializeField]
        private BoolValue isMobile;
        public static bool IsMobile
        {
            get { return Instance.isMobile.Value; }
        }

        [SerializeField]
        private BoolValue isDebugBuild;
        public static bool IsDebugBuild
        {
            get { return Instance.isDebugBuild.Value; }
        }

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            DontDestroyOnLoad(gameObject);

            isEditor.GetIsEditor();
            isMobile.GetIsMobile();
            isDebugBuild.GetIsDebugBuild();
        }

        #endregion
    }
}
