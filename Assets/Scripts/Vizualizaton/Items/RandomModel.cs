using RandMath;
using UnityEngine;

public class RandomModel : MonoBehaviour
{
    [SerializeField] private RandomValue<GameObject> _model;

    private void OnEnable()
    {
        foreach (var model in _model)
            model.SetActive(false);
        ((GameObject)_model).SetActive(true);
    }
}
