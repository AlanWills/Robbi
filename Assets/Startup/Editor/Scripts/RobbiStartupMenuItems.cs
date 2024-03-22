using UnityEditor;
using static CelesteEditor.Scene.MenuItemUtility;


namespace RobbiEditor.Startup
{
   public static class MenuItems
   {
       [MenuItem(RobbiStartupEditorConstants.SCENE_MENU_ITEM)]
       public static void LoadRobbiStartupMenuItem()
       {
           LoadSceneSetMenuItem(RobbiStartupEditorConstants.SCENE_SET_PATH);
       }
   }
}