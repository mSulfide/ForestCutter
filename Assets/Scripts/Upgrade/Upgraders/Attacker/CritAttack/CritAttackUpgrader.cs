public class CritAttackerUpgrader : AttackerUpgrader<CritAttackUpgradeInfo>
{
    private readonly CritAttackUpgrade _critAttack = new(1f, 0f);

    public override void Invoke(CritAttackUpgradeInfo upgrade)
    {
        _critAttack.SetMultiplier(upgrade.CritMultiplayer);
        _critAttack.SetChance(upgrade.CritChance);
    }

    private void OnEnable()
    {
        Attacker.Attack.Add(_critAttack);
    }

    private void OnDisable()
    {
        Attacker.Attack.Remove(_critAttack);
    }
}