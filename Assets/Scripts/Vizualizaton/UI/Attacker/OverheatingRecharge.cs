using UnityEngine;
using UnityEngine.UI;

public class OverheatingRecharge : MonoBehaviour
{
    [SerializeField] private AutoAttacker _attacker;
    [SerializeField] private Image _image;

    private void Update()
    {
        _image.fillAmount = _attacker.Overheat;
    }
}