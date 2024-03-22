using UnityEditor;
using static CelesteEditor.Scene.MenuItemUtility;


namespace RobbiEditor.Bootstrap
{
   public static class MenuItems
   {
       [MenuItem(RobbiBootstrapEditorConstants.SCENE_MENU_ITEM)]
       public static void LoadRobbiBootstrapMenuItem()
       {
           LoadSceneSetMenuItem(RobbiBootstrapEditorConstants.SCENE_SET_PATH);
       }
   }
}