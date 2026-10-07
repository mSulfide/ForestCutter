public class TreeCascadeUpgrader : AttackerUpgrader<TreeCascadeUpgradeInfo>
{
    private readonly TreeCascadeUpgrade _upgrade = new(0f, 0);

    public override void Invoke(TreeCascadeUpgradeInfo upgrade)
    {
        _upgrade.SetForceModifire(upgrade.AddForce);
        _upgrade.SetCountModifire(upgrade.AddCount);
    }

    private void OnEnable()
    {
        Attacker.Attack.Add(_upgrade);
    }

    private void OnDisable()
    {
        Attacker.Attack.Remove(_upgrade);
    }
}