using Robbi.Save;
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
                if (instance == null)
                {
                    Debug.LogAssertionFormat("{0} is null so creating default instance.  Did you forget to wait for Load()", typeof(T).Name);
                    instance = CreateInstance<T>();

                }
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

        public abstract void Serialize(SaveData saveData);
        public abstract void Deserialize(SaveData saveData);

        #endregion
    }
}
