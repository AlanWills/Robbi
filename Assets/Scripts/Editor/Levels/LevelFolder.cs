using Robbi.Levels.Elements;
using RobbiEditor.Iterators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RobbiEditor.Levels
{
    public class LevelFolder
    {
        private uint index;
        public uint Index
        {
            get { return index; }
            set
            {
                index = value;
                Path = string.Format("{0}Level{1}", LevelDirectories.LEVELS_PATH, index);
            }
        }
        public string Path { get; private set; }

        public string LevelDataName { get { return string.Format("Level{0}Data", Index); } }
        public string LevelDataPath { get { return string.Format("{0}/{1}.asset", Path, LevelDataName); } }

        public string PrefabName { get { return string.Format("Level{0}", Index); } }
        public string PrefabPath { get { return string.Format("{0}/{1}.prefab", Path, PrefabName); } }

        public string TutorialsFolderPath { get { return string.Format("{0}/{1}", Path, LevelDirectories.TUTORIALS_NAME); } }
        public string TutorialsPrefabName { get { return string.Format("Level{0}Tutorials", Index); } }
        public string TutorialsPrefabPath { get { return string.Format("{0}{1}.prefab", TutorialsFolderPath, TutorialsPrefabName); } }
        public string TutorialsFSMName { get { return string.Format("Level{0}TutorialsFSM", Index); } }
        public string TutorialsFSMPath { get { return string.Format("{0}{1}.asset", TutorialsFolderPath, TutorialsFSMName); } }
        public string TutorialsDGName { get { return string.Format("Level{0}TutorialsDG", Index); } }
        public string TutorialsDGPath { get { return string.Format("{0}{1}.asset", TutorialsFolderPath, TutorialsDGName); } }

        public string TestsFolderPath { get { return string.Format("{0}/{1}", Path, LevelDirectories.TESTS_NAME); } }
        public string TestFSMName { get { return string.Format("Level{0}IntegrationTest", Index); } }
        public string TestFSMPath { get { return string.Format("{0}{1}.asset", TestsFolderPath, TestFSMName); } }

        public string InteractablesFolderPath { get { return string.Format("{0}/{1}", Path, LevelDirectories.INTERACTABLES_NAME); } }
        public FindAssets<ScriptableObject> Interactables { get { return new FindAssets<ScriptableObject>(InteractablesFolderPath); } }

        public string CollectablesFolderPath { get { return string.Format("{0}/{1}", Path, LevelDirectories.COLLECTABLES_NAME); } }
        public FindAssets<Collectable> Collectables { get { return new FindAssets<Collectable>(CollectablesFolderPath); } }

        public string PortalsFolderPath { get { return string.Format("{0}/{1}", Path, LevelDirectories.PORTALS_NAME); } }
        public FindAssets<Portal> Portals { get { return new FindAssets<Portal>(PortalsFolderPath); } }

        public string DoorsFolderPath { get { return string.Format("{0}/{1}", Path, LevelDirectories.DOORS_NAME); } }
        public FindAssets<Door> Doors { get { return new FindAssets<Door>(DoorsFolderPath); } }

        public string LasersFolderPath { get { return string.Format("{0}/{1}", Path, LevelDirectories.LASERS_NAME); } }
        public FindAssets<Laser> Lasers { get { return new FindAssets<Laser>(LasersFolderPath); } }

        public LevelFolder(uint index)
        {
            Index = index;
        }
    }
}
