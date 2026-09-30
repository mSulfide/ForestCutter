public class AddDamageUpgrader : AttackerUpgrader<int>
{
    private readonly IncrementUpgrade _addDamage = new(0);

    public override void Invoke(int upgrade)
    {
        _addDamage.SetModifier(upgrade);
    }

    private void OnEnable()
    {
        Attacker.BaseDamage.Add(_addDamage);
    }

    private void OnDisable()
    {
        Attacker.BaseDamage.Remove(_addDamage);
    }
}