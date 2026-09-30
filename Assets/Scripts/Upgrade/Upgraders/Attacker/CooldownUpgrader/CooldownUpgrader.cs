public class CooldownUpgrader : AttackerUpgrader<float>
{
    private readonly CooldownUpgrade _cooldown = new(0f);

    public override void Invoke(float upgrade)
    {
        _cooldown.SetModifier(upgrade);
    }

    private void OnEnable()
    {
        Attacker.Cooldown.Add(_cooldown);
    }

    private void OnDisable()
    {
        Attacker.Cooldown.Remove(_cooldown);
    }
}