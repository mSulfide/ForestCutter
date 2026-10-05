using RandMath;
using UnityEngine;

public class SpawnerUpgrader : Upgrader<SpawnerUpgradeInfo>
{
    [SerializeField] private TreeSpawner _spawner;

    private readonly IncrementUpgrade _addMaxCount = new(0);
    private readonly CooldownUpgrade _delayUpgrade = new(0f);
    private readonly SetUpgrade<RandomValue<TreeSettings>> _poolUgrade = new(null);

    public override void Invoke(SpawnerUpgradeInfo upgrade)
    {
        _addMaxCount.SetModifier(upgrade.AddMaxCount);
        _delayUpgrade.SetModifier(upgrade.Delay);
        _poolUgrade.SetValue(upgrade.TreePool);
    }

    private void OnEnable()
    {
        _spawner.MaxSpawnCount.Add(_addMaxCount);
        _spawner.Delay.Add(_delayUpgrade);
        _spawner.TreePool.Add(_poolUgrade);
    }

    private void OnDisable()
    {
        _spawner.MaxSpawnCount.Remove(_addMaxCount);
        _spawner.Delay.Remove(_delayUpgrade);
        _spawner.TreePool.Remove(_poolUgrade);
    }
}