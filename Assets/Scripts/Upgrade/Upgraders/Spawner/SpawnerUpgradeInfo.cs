using RandMath;
using System;

[Serializable]
public class SpawnerUpgradeInfo
{
    public float Delay = 1f;
    public int AddMaxCount = 0;
    public RandomValue<TreeSettings> TreePool;
}