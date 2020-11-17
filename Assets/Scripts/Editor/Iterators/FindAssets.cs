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
    public class FindAssets<T> : IEnumerable<string> where T : Object
    {
        private string[] assetGuids;

        public FindAssets(string folder)
        {
            if (folder.EndsWith("/"))
            {
                folder = folder.Substring(0, folder.Length - 1);
            }

            assetGuids = Directory.Exists(folder) ? AssetDatabase.FindAssets(string.Format("t:{0}", typeof(T).Name), new string[] { folder }) : new string[0];
        }

        public IEnumerator<string> GetEnumerator()
        {
            foreach (string guid in assetGuids)
            {
                yield return AssetDatabase.GUIDToAssetPath(guid);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
