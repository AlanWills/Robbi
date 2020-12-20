using UnityEditor;

namespace RobbiEditor
{
    public class ProjectFilePostprocessor : AssetPostprocessor
    {
        public static string OnGeneratedSlnSolution(string path, string content)
        {
            return content;
        }

        public static string OnGeneratedCSProject(string path, string content)
        {
            return content;
        }
    }
}