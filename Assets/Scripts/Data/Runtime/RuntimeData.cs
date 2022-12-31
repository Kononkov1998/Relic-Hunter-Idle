using System;
using Services.SaveLoad;

namespace Data.Runtime
{
    [Serializable]
    public class RuntimeData : ISavable
    {
        public int TreasureHuntersCount;
        public int MaxTreasureHuntersCount;
        public int TreasureDiggersCount;
        public int MaxTreasureDiggersCount;
        public int TreasureCollectorsCount;
        public int MaxTreasureCollectorsCount;
        public int CollectedTreasures;

        public string SaveId => nameof(RuntimeData);
    }
}