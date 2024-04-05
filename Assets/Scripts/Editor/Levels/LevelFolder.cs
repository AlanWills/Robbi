using Robbi.Levels.Elements;
using RobbiEditor.Iterators;
using UnityEngine;

namespace RobbiEditor.Levels
{
    public class LevelFolder
    {
        private uint index;
        public uint Index
        {
            get => index;
            set
            {
                index = value;
                Path = string.Format("{0}Level{1}", LevelDirectories.LEVELS_PATH, index);
            }
        }
        public string Path { get; private set; }

        public string LevelDataName => $"Level{Index}Data";
        public string LevelDataPath => $"{Path}/{LevelDataName}.asset";

        public string PrefabName => $"Level{Index}";
        public string PrefabPath => $"{Path}/{PrefabName}.prefab";

        public string TutorialsFolderPath => $"{Path}/{LevelDirectories.TUTORIALS_NAME}";
        public string TutorialsPrefabName => $"Level{Index}Tutorials";
        public string TutorialsPrefabPath => $"{TutorialsFolderPath}{TutorialsPrefabName}.prefab";
        public string TutorialsFSMName => $"Level{Index}TutorialsFSM";
        public string TutorialsFSMPath => $"{TutorialsFolderPath}{TutorialsFSMName}.asset";
        public string TutorialsDGName => $"Level{Index}TutorialsDG";
        public string TutorialsDGPath => $"{TutorialsFolderPath}{TutorialsDGName}.asset";

        public string TestsFolderPath => $"{Path}/{LevelDirectories.TESTS_NAME}";
        public string TestFSMName => $"Level{Index}IntegrationTest";
        public string TestFSMPath => $"{TestsFolderPath}{TestFSMName}.asset";

        public string InteractablesFolderPath => $"{Path}/{LevelDirectories.INTERACTABLES_NAME}";
        public FindAssets<ScriptableObject> Interactables => new FindAssets<ScriptableObject>(InteractablesFolderPath);

        public string CollectablesFolderPath => $"{Path}/{LevelDirectories.COLLECTABLES_NAME}";
        public FindAssets<Collectable> Collectables => new FindAssets<Collectable>(CollectablesFolderPath);

        public string PortalsFolderPath => $"{Path}/{LevelDirectories.PORTALS_NAME}";
        public FindAssets<Portal> Portals => new FindAssets<Portal>(PortalsFolderPath);

        public string DoorsFolderPath => $"{Path}/{LevelDirectories.DOORS_NAME}";
        public FindAssets<Door> Doors => new FindAssets<Door>(DoorsFolderPath);

        public string LasersFolderPath => $"{Path}/{LevelDirectories.LASERS_NAME}";
        public FindAssets<Laser> Lasers => new FindAssets<Laser>(LasersFolderPath);

        public string EnemiesFolderPath => $"{Path}/{LevelDirectories.ENEMIES_NAME}";
        public FindAssets<Enemy> Enemies => new FindAssets<Enemy>(EnemiesFolderPath);

        public LevelFolder(uint index)
        {
            Index = index;
        }
    }
}
