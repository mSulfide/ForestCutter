public class AutoAttackEnableUpgrader : AutoAttackerUpgrader<bool, AutoAttackEnableUpgradeList>
{
    public override void Invoke(bool upgrade)
    {
        AutoAttacker.enabled = upgrade;
    }
}