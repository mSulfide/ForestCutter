public class TestAttacker : Attacker
{
    public struct Info
    {
        public uint ClickCount;
        public float Time;
    }

    public Info Kill(Health health)
    {
        const uint kLimit = 10000;

        float time = 0f;
        uint k = 0;
        while (health.IsAlive && k++ < kLimit)
        {
            health.TakeDamage(BaseDamage.GetValue());

            time += Cooldown.GetValue();
        }

        return new()
        {
            ClickCount = k,
            Time = time
        };
    }
}