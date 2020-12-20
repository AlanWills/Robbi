using CelesteEditor.Platform.Steps;
using Robbi.Levels;
using RobbiEditor.Tools;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.BuildSystem.AssetPreparationSteps
{
    [CreateAssetMenu(fileName = "PrepareAssets", menuName = "Robbi/Build System/Prepare Assets")]
    public class PrepareAssets : AssetPreparationStep
    {
        public override void Execute()
        {
            CompressTilemaps.MenuItem();
            FindAllLevelObjects.MenuItem();
            SetAddressablePaths.MenuItem();
            SetCurrentLevelToZero();
        }

        private void SetCurrentLevelToZero()
        {
            LevelManager.EditorOnly_Load().CurrentLevel_DefaultValue = 0;
            AssetDatabase.SaveAssets();
        }
    }
}