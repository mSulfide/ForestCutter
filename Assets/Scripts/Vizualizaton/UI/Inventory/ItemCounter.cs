using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCounter : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _element;
    
    public void SetValue(uint value)
    {
        _element.text = $"{value}";
    }

    public void SetItem(Item item)
    {
        _image.sprite = item.Icon;
    }
}
