using Robbi.Debugging.Logging;
using Robbi.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Robbi.Managers
{
    public abstract class PersistentManager<T, TDTO> : ScriptableObject 
        where T : PersistentManager<T, TDTO>
        where TDTO : new()
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

        #region Editor Only

#if UNITY_EDITOR

        protected static T EditorOnly_Load(string assetDatabasePath)
        {
            return AssetDatabase.LoadAssetAtPath<T>(assetDatabasePath);
        }

#endif

        #endregion

        protected static AsyncOperationHandle Load(string addressablePath, string persistentFilePath)
        {
            AsyncOperationHandle asyncOperationHandle = Addressables.LoadAssetAsync<T>(addressablePath);
            asyncOperationHandle.Completed += (AsyncOperationHandle obj) => { Load_Completed(obj, persistentFilePath); };
            
            return asyncOperationHandle;
        }

        private static void Load_Completed(AsyncOperationHandle obj, string persistentFilePath)
        {
            Debug.LogFormat("{0} load complete", typeof(T).Name);

            if (obj.IsValid() && obj.Result != null)
            {
                Instance = obj.Result as T;
                HudLogger.LogInfoFormat("{0} loaded", Instance.name);

                if (File.Exists(persistentFilePath))
                {
                    TDTO tDTO = JsonUtility.FromJson<TDTO>(File.ReadAllText(persistentFilePath));
                    if (tDTO != null)
                    {
                        Instance.Deserialize(tDTO);
                    }
                    else
                    {
                        Debug.LogFormat("Error deserialization data in {0}.  Using default DTO.", persistentFilePath);
                        Instance.Deserialize(new TDTO());
                    }
                }
                else
                {
                    Debug.LogFormat("{0} not found for manager {1}", persistentFilePath, Instance.name);
                    Instance.Deserialize(new TDTO());
                }
            }
            else
            {
                Debug.LogErrorFormat("{0} load failed.  IsValid: {1}", typeof(T).Name, obj.IsValid());
            }
        }

        public void Save(string filePath)
        {
            string serializedData = Instance.Serialize();
            File.WriteAllText(filePath, serializedData);
            
            // Needed to deal with browser async saving
            WebGLUtils.SyncFiles();

            HudLogger.LogInfoFormat("{0} saved", Instance.name);
        }

        protected abstract string Serialize();
        protected abstract void Deserialize(TDTO dto);

#endregion
    }
}
