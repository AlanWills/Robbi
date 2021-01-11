using Celeste.Log;
using Celeste.Tools;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Celeste.Managers
{
    public abstract class PersistentManager<T, TDTO> : ScriptableObject 
        where T : PersistentManager<T, TDTO>
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

        protected static AsyncOperationHandle LoadAsync(string addressablePath, string persistentFilePath)
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
                HudLog.LogInfoFormat("{0} loaded", Instance.name);

                if (File.Exists(persistentFilePath))
                {
                    TDTO tDTO = JsonUtility.FromJson<TDTO>(File.ReadAllText(persistentFilePath));
                    if (tDTO != null)
                    {
                        Instance.Deserialize(tDTO);
                    }
                    else
                    {
                        Debug.LogFormat("Error deserialization data in {0}.  Using default manager values.", persistentFilePath);
                        Instance.SetDefaultValues();
                    }
                }
                else
                {
                    Debug.LogFormat("{0} not found for manager {1}.  Using default manager values.", persistentFilePath, Instance.name);
                    Instance.SetDefaultValues();
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

            HudLog.LogInfoFormat("{0} saved", Instance.name);
        }

        public async Task SaveAsync(string filePath)
        {
            string serializedData = Instance.Serialize();
            using (StreamWriter outputFileWriter = new StreamWriter(new FileStream(filePath, FileMode.Create)))
            {
                await outputFileWriter.WriteAsync(serializedData);
            }
        }

        protected abstract string Serialize();
        protected abstract void Deserialize(TDTO dto);
        protected abstract void SetDefaultValues();

#endregion
    }
}
