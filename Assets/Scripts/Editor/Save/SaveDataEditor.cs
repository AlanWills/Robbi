using Robbi.Save;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Save
{
    public static class SaveDataEditor
    {
        #region Menu Items

        [MenuItem("Robbi/Save/Delete")]
        public static void DeleteSaveData()
        {
            if (File.Exists(SaveData.DEFAULT_FILE_PATH))
            {
                File.Delete(SaveData.DEFAULT_FILE_PATH);
            }
        }

        #endregion
    }
}
