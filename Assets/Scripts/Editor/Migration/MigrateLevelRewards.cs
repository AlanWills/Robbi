using Robbi.Levels;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
using UnityEditor;

namespace RobbiEditor.Migration
{
    public static class MigrateLevelRewards
    {
        [MenuItem("Robbi/Migration/Migrate Level Rewards")]
        public static void MenuItem()
        {
            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                Level level = AssetDatabase.LoadAssetAtPath<Level>(levelFolder.LevelDataPath);
                level.SoftCurrencyPrize = 50;
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
