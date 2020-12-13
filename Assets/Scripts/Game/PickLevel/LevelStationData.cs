using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.PickLevel
{
    public class LevelStationData
    {
        public uint levelIndex;
        public bool isComplete;

        public LevelStationData(uint levelIndex, bool isComplete)
        {
            this.levelIndex = levelIndex;
            this.isComplete = isComplete;
        }
    }
}
