using UnityEditor;
using static CelesteEditor.Scene.MenuItemUtility;


namespace RobbiEditor.Bootstrap
{
    public static class MenuItems
    {
        [MenuItem(RobbiEditorConstants.STARTUP_SCENE_MENU_ITEM)]
        public static void LoadStartupMenuItem()
        {
            LoadSceneSetMenuItem(RobbiEditorConstants.STARTUP_SCENE_SET_PATH);
        }

        [MenuItem(RobbiEditorConstants.BOOTSTRAP_SCENE_MENU_ITEM)]
        public static void LoadBootstrapMenuItem()
        {
            LoadSceneSetMenuItem(RobbiEditorConstants.BOOTSTRAP_SCENE_SET_PATH);
        }

        [MenuItem(RobbiEditorConstants.MAIN_MENU_SCENE_MENU_ITEM)]
        public static void LoadMainMenuMenuItem()
        {
            LoadSceneSetMenuItem(RobbiEditorConstants.MAIN_MENU_SCENE_SET_PATH);
        }
    }
}