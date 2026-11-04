public class ColdingUpgrader : AutoAttackerUpgrader<ColdingUpgradeInfo>
{
    private readonly AddRangeUpgrade _waitColding = new(0f);
    private readonly AddRangeUpgrade _workColding = new(0f);

    public override void Invoke(ColdingUpgradeInfo upgrade)
    {
        _waitColding.SetModifier(upgrade.Wait);
        _workColding.SetModifier(upgrade.Work);
    }

    private void OnEnable()
    {
        AutoAttacker.WaitColding.Add(_waitColding);
        AutoAttacker.WorkColding.Add(_workColding);
    }

    private void OnDisable()
    {
        AutoAttacker.WaitColding.Remove(_waitColding);
        AutoAttacker.WorkColding.Remove(_workColding);
    }
}