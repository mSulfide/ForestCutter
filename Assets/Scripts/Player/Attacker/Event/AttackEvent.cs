using UnityEngine;

[RequireComponent(typeof(Attacker))]
public class AttackEvent : MonoBehaviour
{
    private void AttackHandler(AttackInfo info)
    {
        foreach (var listener in info.Target.GetComponents<IAttackEventListener>())
            listener.AttackHandler(info);
    }

    private void OnEnable()
    {
        GetComponent<Attacker>().OnAttack += AttackHandler;
    }

    private void OnDisable()
    {
        GetComponent<Attacker>().OnAttack -= AttackHandler;
    }
}