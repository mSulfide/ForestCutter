public class ArmorIgnoreUpgrader : AttackerUpgrader<float, ArmorIgnoreUpgradeList>
{
    private readonly ArmorIgnoreUpgrade _upgrade = new(0f);

    public override void Invoke(float upgrade)
    {
        _upgrade.SetModifire(upgrade);
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