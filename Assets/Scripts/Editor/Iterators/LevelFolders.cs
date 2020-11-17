using RobbiEditor.Levels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Iterators
{
    public class LevelFolders : IEnumerable<LevelFolder>
    {
        #region Properties and Fields

        private LevelFolder levelFolder = new LevelFolder(0);

        #endregion

        public IEnumerator<LevelFolder> GetEnumerator()
        {
            while (Directory.Exists(levelFolder.Path))
            {
                yield return levelFolder;

                ++levelFolder.Index;
            }
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
