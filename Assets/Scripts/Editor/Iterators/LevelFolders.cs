using RobbiEditor.Levels;
using System.Collections;
using System.Collections.Generic;
using System.IO;

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
