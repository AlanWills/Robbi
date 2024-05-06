using Celeste.Debug.Menus;
using Celeste.Parameters;
using Celeste.Tools;
using UnityEngine;

namespace Robbi.Levels
{
    [CreateAssetMenu(fileName = nameof(LevelDebugMenu), menuName = "Robbi/Levels/Level Debug Menu")]
    public class LevelDebugMenu : DebugMenu
    {
        [SerializeField] private LevelRecord levelRecord;
        [SerializeField] private FloatValue dragSpeed;

        protected override void OnDrawMenu()
        {
            levelRecord.LatestUnlockedLevel = GUIExtensions.UIntField("Latest Unlocked Level", levelRecord.LatestUnlockedLevel);
            levelRecord.LatestAvailableLevel = GUIExtensions.UIntField("Latest Available Level", levelRecord.LatestAvailableLevel);

            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label("Drag Speed");
                dragSpeed.Value = GUILayout.HorizontalSlider(dragSpeed.Value, 0.01f, 1);
                dragSpeed.Value = GUIExtensions.FloatField(dragSpeed.Value);
            }
        }
    }
}