using CelesteEditor.BuildSystem.Steps;
using RobbiEditor.Tools;
using UnityEngine;

namespace RobbiEditor.BuildSystem.AssetPreparationSteps
{
    [CreateAssetMenu(fileName = nameof(PrepareAssets), menuName = "Robbi/Build System/Prepare Assets")]
    public class PrepareAssets : AssetPreparationStep
    {
        public override void Execute()
        {
            CompressTilemaps.MenuItem();
            FindAllLevelObjects.MenuItem();
            FixupInteractableModifiers.MenuItem();
            EnableTutorialSteps.MenuItem();
            ConfigureLevelRootTilemaps.MenuItem();
            SetAddressablePaths.MenuItem();
        }
    }
}