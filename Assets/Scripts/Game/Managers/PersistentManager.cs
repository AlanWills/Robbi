using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Robbi.Managers
{
    public abstract class PersistentManager<T> : ScriptableObject where T : PersistentManager<T>
    {
        #region Properties and Fields
        
        private static T instance;
        public static T Instance
        {
            get
            {
                Debug.AssertFormat(instance != null, "{0} is null.  Did you forget to wait for Load()", typeof(T).Name);
                return instance;
            }
            private set { instance = value; }
        }

        #endregion

        protected PersistentManager() { }

        #region Save/Load Methods

        public static AsyncOperationHandle Load(string filePath)
        {
            AsyncOperationHandle asyncOperationHandle = Addressables.LoadAssetAsync<T>(filePath);
            asyncOperationHandle.Completed += Load_Completed;
            
            return asyncOperationHandle;
        }

        private static void Load_Completed(AsyncOperationHandle obj)
        {
            Debug.LogFormat("{0} load complete", typeof(T).Name);

            if (obj.IsValid() && obj.Result != null)
            {
                Instance = obj.Result as T;
            }
            else
            {
                Debug.LogErrorFormat("{0} load failed.  IsValid: {1}", typeof(T).Name, obj.IsValid());
            }
        }

        #endregion
    }
}
