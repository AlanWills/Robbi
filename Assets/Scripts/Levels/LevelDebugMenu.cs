using Celeste.Debug.Menus;
using Celeste.Tools;
using UnityEngine;

namespace Robbi.Levels
{
    [CreateAssetMenu(fileName = nameof(LevelDebugMenu), menuName = "Robbi/Levels/Level Debug Menu")]
    public class LevelDebugMenu : DebugMenu
    {
        [SerializeField] private LevelRecord levelRecord;

        protected override void OnDrawMenu()
        {
            levelRecord.LatestUnlockedLevel = GUIExtensions.UIntField("Latest Unlocked Level", levelRecord.LatestUnlockedLevel);
            levelRecord.LatestAvailableLevel = GUIExtensions.UIntField("Latest Available Level", levelRecord.LatestAvailableLevel);
        }
    }
}