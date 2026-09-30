using UnityEngine;
using TMPro;

public class SaveShower : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _version;

    public void ShowSave(SaveData data)
    {
        _name.text = data.Name;
        if (_version != null)
            _version.text = $"v{data.Version}";
    }
}
