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

        [MenuItem(RobbiEditorConstants.LEVEL_SCENE_MENU_ITEM)]
        public static void LoadLevelMenuItem()
        {
            LoadSceneSetMenuItem(RobbiEditorConstants.LEVEL_SCENE_SET_PATH);
        }

        [MenuItem(RobbiEditorConstants.PICK_LEVEL_SCENE_MENU_ITEM)]
        public static void LoadPickLevelMenuItem()
        {
            LoadSceneSetMenuItem(RobbiEditorConstants.PICK_LEVEL_SCENE_SET_PATH);
        }

        [MenuItem(RobbiEditorConstants.OPTIONS_SCENE_MENU_ITEM)]
        public static void LoadOptionsMenuItem()
        {
            LoadSceneSetMenuItem(RobbiEditorConstants.OPTIONS_SCENE_SET_PATH);
        }

        [MenuItem(RobbiEditorConstants.SHOP_SCENE_MENU_ITEM)]
        public static void LoadShopMenuItem()
        {
            LoadSceneSetMenuItem(RobbiEditorConstants.SHOP_SCENE_SET_PATH);
        }

        [MenuItem(RobbiEditorConstants.CREDITS_SCENE_MENU_ITEM)]
        public static void LoadCreditsMenuItem()
        {
            LoadSceneSetMenuItem(RobbiEditorConstants.CREDITS_SCENE_SET_PATH);
        }
    }
}