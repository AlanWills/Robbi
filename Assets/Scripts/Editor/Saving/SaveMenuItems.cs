using Robbi.Levels;
using Robbi.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Saving
{
    public static class SaveMenuItems
    {
        [MenuItem("Robbi/Save/Delete")]
        public static void DeleteSave()
        {
            if (File.Exists(OptionsManager.DefaultSavePath))
            {
                File.Delete(OptionsManager.DefaultSavePath);
                Debug.Log("Deleted Options Manager save data");
            }

            if (File.Exists(LevelManager.DefaultSavePath))
            {
                File.Delete(LevelManager.DefaultSavePath);
                Debug.Log("Deleted Level Manager save data");
            }
        }
    }
}
