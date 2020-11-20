﻿using Robbi.Levels.Elements;
using RobbiEditor.Iterators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string TutorialsName { get { return string.Format("Level{0}Tutorials", Index); } }
        public string TutorialsPath { get { return string.Format("{0}{1}.prefab", TutorialsFolderPath, TutorialsName); } }
        public string TutorialsFSMName { get { return string.Format("Level{0}TutorialsFSM", Index); } }
        public string TutorialsFSMPath { get { return string.Format("{0}{1}.asset", TutorialsFolderPath, TutorialsFSMName); } }
        public string TutorialsDGName { get { return string.Format("Level{0}TutorialsDG", Index); } }
        public string TutorialsDGPath { get { return string.Format("{0}{1}.asset", TutorialsFolderPath, TutorialsDGName); } }

        public string TestsFolderPath { get { return string.Format("{0}/{1}", Path, LevelDirectories.TESTS_NAME); } }
        public string TestFSMName { get { return string.Format("Level{0}IntegrationTestFSM", Index); } }
        public string TestFSMPath { get { return string.Format("{0}{1}.asset", TestsFolderPath, TestFSMName); } }

        public string InteractablesFolderPath { get { return string.Format("{0}/{1}", Path, LevelDirectories.INTERACTABLES_NAME); ; } }
        public FindAssets<Interactable> Interactables { get { return new FindAssets<Interactable>(InteractablesFolderPath); } }

        public string DoorsFolderPath { get { return string.Format("{0}/{1}", Path, LevelDirectories.DOORS_NAME); ; } }
        public FindAssets<Door> Doors { get { return new FindAssets<Door>(DoorsFolderPath); } }

        public LevelFolder(uint index)
        {
            Index = index;
        }
    }
}