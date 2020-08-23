using Robbi.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Robbi.State
{
    [AddComponentMenu("Robbi/State/Level Initializer")]
    public class LevelInitializer : MonoBehaviour
    {
        #region Unity Methods

        public void Start()
        {
            LoadLevel();
        }

        #endregion

        #region Level Loading

        private void LoadLevel()
        {
            LevelManager levelManager = LevelManager.Load();
            Addressables.LoadAssetAsync<Level>("Level" + levelManager.CurrentLevelIndex.ToString() + "Data").Completed += LoadLevelComplete; ;
        }

        private void LoadLevelComplete(AsyncOperationHandle<Level> level)
        {
            level.Result.Begin(gameObject);
        }

        #endregion
    }
}
